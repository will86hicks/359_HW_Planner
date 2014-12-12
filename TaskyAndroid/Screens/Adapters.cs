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
using Android.Graphics.Drawables;

namespace HWPlannerAndroid.Screens {
	class Adapters
{
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