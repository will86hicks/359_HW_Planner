using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using HWPlanner.BL;
using System;
using System.Linq;
using Java.Lang;

namespace HWPlannerAndroid.Screens {
	[Activity (Label = "Homework Planner", MainLauncher = true, Icon="@drawable/ic_launcher")]			
	public class HomeScreen : Activity {
		protected Adapters.HWListAdapter taskList;
		protected IList<HW> tasks;
		protected Button addHWButton = null;
		protected ListView taskListView = null;
		
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


			// set our layout to be the home screen
			SetContentView(HWAndroid.Resource.Layout.HomeScreen);

			//Find our controls
			taskListView = FindViewById<ListView> (HWAndroid.Resource.Id.lstHWs);
			addHWButton = FindViewById<Button> (HWAndroid.Resource.Id.btnAddHW);

			// wire up add task button handler
			if(addHWButton != null) {
				addHWButton.Click += (sender, e) => {
					StartActivity(typeof(HWDetailsScreen));
				};
			}
			
			// wire up task click handler
			if(taskListView != null) {
				taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var taskDetails = new Intent (this, typeof (HWDetailsScreen));
					taskDetails.PutExtra ("HWID", tasks[e.Position].ID);
					StartActivity (taskDetails);
				};
			}
		}
		
		protected override void OnResume ()
		{
			base.OnResume ();

			tasks = HWPlanner.BL.Managers.HWManager.GetHWs().OrderBy(e=>e.DueDate).ToList();
			
			// create our adapter
			taskList = new Adapters.HWListAdapter(this, tasks);

			//Hook up our adapter to our ListView
			taskListView.Adapter = taskList;
		}
	}
}