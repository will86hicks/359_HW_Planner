using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using HWPlanner.AL;
using HWPlanner.BL;

namespace HWPlanner.Screens {
	public class controller_iPhone : DialogViewController {
		List<HW> hws;
		
		public controller_iPhone () : base (UITableViewStyle.Plain, null)
		{
			Initialize ();
		}
		
		protected void Initialize()
		{
			NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowHWDetails(new HW()); };

			/*UILocalNotification notification = new UILocalNotification();
			var x = DateTime.Now;
			notification.FireDate = DateTime.Now.AddSeconds(5);
			notification.AlertAction = "View Alert";
			notification.AlertBody = "Your one minute alert has fired!";
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);*/

		}
		

		// MonoTouch.Dialog individual HWDetails view (uses /AL/HWDialog.cs wrapper class)
		LocalizableBindingContext context;
		HWDialog hwDialog;
		HW currentHW;
		DialogViewController detailsScreen;
		protected void ShowHWDetails (HW hw)
		{
			currentHW = hw;
			hwDialog = new HWDialog (hw);
			
			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Homework Assignment Details", "Homework Assignment Details");
			context = new LocalizableBindingContext (this, hwDialog, title);
			detailsScreen = new DialogViewController (context.Root, true);
			ActivateController(detailsScreen);

		}
		public void SaveHW()
		{
			context.Fetch (); // re-populates with updated values
			currentHW.Name = hwDialog.Name;
			currentHW.Notes = hwDialog.Notes;

			//Added these properties
			currentHW.DueDate = hwDialog.DueDate.AddHours(-6);		//For Some Reason it's adding 6 Hours to the DueDate when saving??
			currentHW.CourseName = hwDialog.CourseName;

			currentHW.Done = hwDialog.Done;
			BL.Managers.HWManager.SaveHW(currentHW);
			NavigationController.PopViewControllerAnimated (true);


			// create the notification
			var notification = new UILocalNotification();

			// set the fire date (the date time in which it will fire)
			notification.FireDate = currentHW.DueDate.AddMinutes (-1);

			// configure the alert stuff
			notification.AlertAction = "Homework Alert";
			notification.AlertBody = currentHW.Name + " is due in one minute!";

			// modify the badge
			notification.ApplicationIconBadgeNumber = 1;

			// set the sound to be the default sound
			notification.SoundName = UILocalNotification.DefaultSoundName;

			// schedule it
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);

			//context.Dispose (); // per documentation
		}
		public void DeleteHW ()
		{
			if (currentHW.ID >= 0)
				BL.Managers.HWManager.DeleteHW (currentHW.ID);
			NavigationController.PopViewControllerAnimated (true);
		}



		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			// reload/refresh
			PopulateTable();			
		}
		
		protected void PopulateTable ()
		{
			hws = BL.Managers.HWManager.GetHWs ().OrderBy(e=>e.DueDate).ToList ();
			var newHW = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("<new hw>", "<new hw>");
			Root = new RootElement ("HomeWork Planner") {
				new Section() {
					from h in hws
					select (Element) new CheckboxElement((h.Name==""?newHW:h.Name + " " + h.DueDate.ToShortDateString()), h.Done)
				}
			}; 
		}
		public override void Selected (MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var hw = hws[indexPath.Row];
			ShowHWDetails(hw);


		}
		public override Source CreateSizingSource (bool unevenRows)
		{
			return new EditingSource (this);
		}
		public void DeleteHWRow(int rowId)
		{
			BL.Managers.HWManager.DeleteHW(hws[rowId].ID);
		}
	}
}