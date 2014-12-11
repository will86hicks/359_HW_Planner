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
		public HWDialog (HW task)
		{
			Name = task.Name;
			Notes = task.Notes;
			Done = task.Done;
		}
		
		[Localize]
		[Entry("task name")]
		public string Name { get; set; }

		[Localize]
		[Entry("other task info")]
		public string Notes { get; set; }
		
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