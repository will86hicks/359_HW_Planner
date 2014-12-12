using HWPlanner.BL.Contracts;
using HWPlanner.DL.SQLite;
using System;
namespace HWPlanner.BL
{
	/// <summary>
	/// Represents a HW.
	/// </summary>
	public class HW : IBusinessEntity
	{
		public HW ()
		{
			DueDate = DateTime.Today;
			TimeHour = DateTime.Now.Hour;
			TimeMinute = DateTime.Now.Minute;
		}

		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		// new property
		public bool Done { get; set; }
		public DateTime DueDate { get; set; }
		public int TimeHour { get; set; }
		public int TimeMinute { get; set;}
		public string CourseName { get; set; }
	}
}

