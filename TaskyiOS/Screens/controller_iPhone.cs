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
			
			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("HW Details", "HW Details");
			context = new LocalizableBindingContext (this, hwDialog, title);
			detailsScreen = new DialogViewController (context.Root, true);
			ActivateController(detailsScreen);
		}
		public void SaveHW()
		{
			context.Fetch (); // re-populates with updated values
			currentHW.Name = hwDialog.Name;
			currentHW.Notes = hwDialog.Notes;
			currentHW.DueDate = hwDialog.DueDate;
			currentHW.CourseName = hwDialog.CourseName;
			currentHW.Done = hwDialog.Done;
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
			hws = BL.Managers.HWManager.GetHWs ().ToList ();
			var newHW = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("<new hw>", "<new hw>");
			Root = new RootElement ("HWPlanner") {
				new Section() {
					from h in hws
					select (Element) new CheckboxElement((h.Name==""?newHW:h.Name), h.Done)
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