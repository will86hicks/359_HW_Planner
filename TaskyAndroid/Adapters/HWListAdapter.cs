using System;
using System.Collections.Generic;
using Android.Widget;
using HWPlanner.BL;
using Android.App;
using Android;

namespace HWPlannerAndroid.Adapters {
	public class HWListAdapter : BaseAdapter<HW> {
		protected Activity context = null;
		protected IList<HW> tasks = new List<HW>();
		
		public HWListAdapter (Activity context, IList<HW> tasks) : base ()
		{
			this.context = context;
			this.tasks = tasks;
		}
		
		public override HW this[int position]
		{
			get { return tasks[position]; }
		}
		
		public override long GetItemId (int position)
		{
			return position;
		}
		
		public override int Count
		{
			get { return tasks.Count; }
		}
		
		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// Get our object for position
			var item = tasks[position];			

			//Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
			// gives us some performance gains by not always inflating a new view
			// will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
			var view = (convertView ?? 
					context.LayoutInflater.Inflate(
					Android.Resource.Layout.SimpleListItemChecked,
					parent, 
					false)) as CheckedTextView;


			string AM_PM = "";
			string time2;
			if(item.TimeHour < 12) {
				AM_PM = "AM";
			} else {
				AM_PM = "PM";
			}
			if(item.TimeHour == 0){
				time2 = string.Format ("{0}:{1}:{2}", item.TimeHour+12, item.TimeMinute.ToString ().PadLeft (2, '0'),AM_PM);
			}else if(item.TimeHour <= 12){
				time2 = string.Format ("{0}:{1}:{2}", item.TimeHour, item.TimeMinute.ToString ().PadLeft (2, '0'),AM_PM);
			}else{
				time2 = string.Format ("{0}:{1}:{2}", item.TimeHour-12, item.TimeMinute.ToString ().PadLeft (2, '0'),AM_PM);
			}
			view.SetText (item.Name==""?"<new task>":item.Name + " " + item.DueDate.ToShortDateString()+" " + time2, TextView.BufferType.Normal);
			view.Checked = item.Done;
			
			//Finally return the view
			return view;
		}
	}
}