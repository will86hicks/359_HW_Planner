
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using System;
using HWPlanner.BL;
using Java.Lang;
using System.Linq;
namespace HWPlannerAndroid.Screens {

	[Activity (Label = "HW Details")]			
	public class HWDetailsScreen : Activity {
		protected HW task = new HW();
		protected Button cancelDeleteButton = null;
		protected EditText notesTextEdit = null;
		protected EditText nameTextEdit = null;
		protected Button saveButton = null;
		protected Button dateButton = null;
		CheckBox doneCheckbox;
		//Setting up date
		DateTime _date;
		DateTime time = DateTime.Today;
		//setting up time
		string AM_PM;
		protected Button timeButton;
		private int hour;
		private int minute;
		const int TIME_DIALOG_ID = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			View titleView = Window.FindViewById(Android.Resource.Id.Title);
			if (titleView != null) {
			  IViewParent parent = titleView.Parent;
			  if (parent != null && (parent is View)) {
			    View parentView = (View)parent;
			    parentView.SetBackgroundColor(Color.Rgb(0xFF, 0xFF ,0xFF)); //38, 117 ,255
			  }
			}

			int taskID = Intent.GetIntExtra("HWID", 0);
			if(taskID > 0) {
				task = HWPlanner.BL.Managers.HWManager.GetHW(taskID);
			}
			
			// set our layout to be the home screen
			SetContentView(HWAndroid.Resource.Layout.HWDetails);
			nameTextEdit = FindViewById<EditText>(HWAndroid.Resource.Id.txtName);
			notesTextEdit = FindViewById<EditText>(HWAndroid.Resource.Id.txtNotes);
			saveButton = FindViewById<Button>(HWAndroid.Resource.Id.btnSave);
			doneCheckbox = FindViewById<CheckBox>(HWAndroid.Resource.Id.chkDone);
			dateButton = FindViewById<Button> (HWAndroid.Resource.Id.btnDueDate);

			// find all our controls
			cancelDeleteButton = FindViewById<Button>(HWAndroid.Resource.Id.btnCancelDelete);
			
			
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
			//due date button
			dateButton.Click += delegate {
				ShowDialog (0);
			} ;

			//make current date the date
			_date = task.DueDate;
			if (_date >= time) {
				dateButton.Text = _date.ToString ("d");
			} else {
				dateButton.Text = time.AddDays (1.0).ToString();
			}

			//TIME!!!
			// Capture our View elements
			timeButton = FindViewById<Button> (HWAndroid.Resource.Id.DueTimeButton);

			// Add a click listener to the button
			timeButton.Click += (o, e) => ShowDialog (TIME_DIALOG_ID);

			// Get the current time
			hour = task.TimeHour;
			minute = task.TimeMinute;
			string time2;
			if(hour < 12) {
				AM_PM = "AM";
			} else {
				AM_PM = "PM";
			}

			if(hour == 0){
				time2 = string.Format ("{0}:{1}:{2}", hour+12, minute.ToString ().PadLeft (2, '0'),AM_PM);
			}else if(hour <= 12){
				time2 = string.Format ("{0}:{1}:{2}", hour, minute.ToString ().PadLeft (2, '0'),AM_PM);
			}else{
				time2 = string.Format ("{0}:{1}:{2}", hour-12, minute.ToString ().PadLeft (2, '0'),AM_PM);
			}
			timeButton.Text = time2;

		}
		protected override Dialog OnCreateDialog (int id)
		{
			if (id == 0) {
				return new DatePickerDialog (this, HandleDateSet, _date.Year, _date.Month - 1, _date.Day); 
			} 
			if (id == TIME_DIALOG_ID)
				return new TimePickerDialog (this, TimePickerCallback, hour, minute, false);
			else {
				return null;
			}
		}
		void HandleDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			_date = e.Date;
			var dateButton = FindViewById<Button> (HWAndroid.Resource.Id.btnDueDate);
			dateButton.Text = _date.ToString ("d");
		}
		private void TimePickerCallback (object sender, TimePickerDialog.TimeSetEventArgs e)
		{
			hour = e.HourOfDay;
			minute = e.Minute;
			string time2;
			if(hour < 12) {
				AM_PM = "AM";
			} else {
				AM_PM = "PM";
			}

			if(hour == 0){
				time2 = string.Format ("{0}:{1}:{2}", hour+12, minute.ToString ().PadLeft (2, '0'),AM_PM);
			}else if(hour <= 12){
				time2 = string.Format ("{0}:{1}:{2}", hour, minute.ToString ().PadLeft (2, '0'),AM_PM);
			}else{
				time2 = string.Format ("{0}:{1}:{2}", hour-12, minute.ToString ().PadLeft (2, '0'),AM_PM);
			}
			timeButton.Text = time2;
		}
		protected void Save()
		{
			task.Name = nameTextEdit.Text;
			task.Notes = notesTextEdit.Text;
			task.Done = doneCheckbox.Checked;
			if (_date >= time) {
				task.DueDate = _date;
			} else {
				task.DueDate = time.AddDays(1.0);
			}
			task.TimeHour = hour;
			task.TimeMinute = minute;
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