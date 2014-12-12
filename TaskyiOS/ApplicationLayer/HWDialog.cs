using System;
using MonoTouch.UIKit;
using HWPlanner.BL;
using MonoTouch.Dialog;

namespace HWPlanner {
	/// <summary>
	/// Wrapper class for HW, to use with MonoTouch.Dialog. If it was just iOS platform
	/// we could apply these attributes directly to the HW class, but because we share that
	/// with other platforms this wrapper provides a bridge to MonoTouch.Dialog.
	/// </summary>
	public class HWDialog {

		//private DateTime Bummfuck;
		public HWDialog (HW hw)
		{
			Name = hw.Name;
			Notes = hw.Notes;
			DueDate = hw.DueDate;
			CourseName = hw.CourseName;
			Done = hw.Done;
		}
		
		[Localize]
		[Entry("hw name")]
		public string Name { get; set; }

		[Localize]
		[Entry("other hw info")]
		public string Notes { get; set; }

		[Localize]
		[Entry("due date")]

		public DateTime DueDate {

			get;


			set ;//{
				//date = value;
				//DateTime date = new DateTime ();
				//date = DateTime.Today;
				//DueDate = date;
			//}
		}
	


		[Localize]
		[Entry("course name")]
		public string CourseName { get; set; }
		
		// new property
		[Localize]
		[Entry("Done")]
		public bool Done { get; set; }

		[Localize]
		[Section ("")]
		[OnTap ("SaveHW")]
		[Alignment (UITextAlignment.Center)]
    	public string Save;
		
		[Localize]
		[Section ("")]
		[OnTap ("DeleteHW")]
		[Alignment (UITextAlignment.Center)]
    	public string Delete;
	}
}