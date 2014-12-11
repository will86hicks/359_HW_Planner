
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Graphics;
using Android.Views;

using HWPlanner.BL;

namespace HWPlannerAndroid.Screens {

	[Activity (Label = "HW Details")]			
	public class HWDetailsScreen : Activity {
		protected HW task = new HW();
		protected Button cancelDeleteButton = null;
		protected EditText notesTextEdit = null;
		protected EditText nameTextEdit = null;
		protected Button saveButton = null;
		CheckBox doneCheckbox;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			View titleView = Window.FindViewById(Android.Resource.Id.Title);
			if (titleView != null) {
			  IViewParent parent = titleView.Parent;
			  if (parent != null && (parent is View)) {
			    View parentView = (View)parent;
			    parentView.SetBackgroundColor(Color.Rgb(0x26, 0x75 ,0xFF)); //38, 117 ,255
			  }
			}

			int taskID = Intent.GetIntExtra("HWID", 0);
			if(taskID > 0) {
				task = HWPlanner.BL.Managers.HWManager.GetHW(taskID);
			}
			
			// set our layout to be the home screen
			SetContentView(Resource.Layout.HWDetails);
			nameTextEdit = FindViewById<EditText>(Resource.Id.txtName);
			notesTextEdit = FindViewById<EditText>(Resource.Id.txtNotes);
			saveButton = FindViewById<Button>(Resource.Id.btnSave);
			doneCheckbox = FindViewById<CheckBox>(Resource.Id.chkDone);
			
			// find all our controls
			cancelDeleteButton = FindViewById<Button>(Resource.Id.btnCancelDelete);
			
			
			// set the cancel delete based on whether or not it's an existing task
			cancelDeleteButton.Text = (task.ID == 0 ? "Cancel" : "Delete");
			
			// name
			nameTextEdit.Text = task.Name;
			
			// notes
			notesTextEdit.Text = task.Notes;

			// done
			doneCheckbox.Checked = task.Done;

			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		protected void Save()
		{
			task.Name = nameTextEdit.Text;
			task.Notes = notesTextEdit.Text;
			task.Done = doneCheckbox.Checked;
			HWPlanner.BL.Managers.HWManager.SaveHW(task);
			Finish();
		}
		
		protected void CancelDelete()
		{
			if(task.ID != 0) {
				HWPlanner.BL.Managers.HWManager.DeleteHW(task.ID);
			}
			Finish();
		}
	}
}