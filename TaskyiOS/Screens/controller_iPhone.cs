using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using HWPlanner.AL;
using HWPlanner.BL;

namespace HWPlanner.Screens {
	public class controller_iPhone : DialogViewController {
		List<HW> tasks;
		
		public controller_iPhone () : base (UITableViewStyle.Plain, null)
		{
			Initialize ();
		}
		
		protected void Initialize()
		{
			NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			NavigationItem.RightBarButtonItem.Clicked += (sender, e) => { ShowHWDetails(new HW()); };
		}
		

		// MonoTouch.Dialog individual HWDetails view (uses /AL/HWDialog.cs wrapper class)
		LocalizableBindingContext context;
		HWDialog taskDialog;
		HW currentHW;
		DialogViewController detailsScreen;
		protected void ShowHWDetails (HW task)
		{
			currentHW = task;
			taskDialog = new HWDialog (task);
			
			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("HW Details", "HW Details");
			context = new LocalizableBindingContext (this, taskDialog, title);
			detailsScreen = new DialogViewController (context.Root, true);
			ActivateController(detailsScreen);
		}
		public void SaveHW()
		{
			context.Fetch (); // re-populates with updated values
			currentHW.Name = taskDialog.Name;
			currentHW.Notes = taskDialog.Notes;
			currentHW.Done = taskDialog.Done;
			BL.Managers.HWManager.SaveHW(currentHW);
			NavigationController.PopViewControllerAnimated (true);
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
			tasks = BL.Managers.HWManager.GetHWs ().ToList ();
			var newHW = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("<new task>", "<new task>");
			Root = new RootElement ("HWPlanner") {
				new Section() {
					from t in tasks
					select (Element) new CheckboxElement((t.Name==""?newHW:t.Name), t.Done)
				}
			}; 
		}
		public override void Selected (MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var task = tasks[indexPath.Row];
			ShowHWDetails(task);
		}
		public override Source CreateSizingSource (bool unevenRows)
		{
			return new EditingSource (this);
		}
		public void DeleteHWRow(int rowId)
		{
			BL.Managers.HWManager.DeleteHW(tasks[rowId].ID);
		}
	}
}