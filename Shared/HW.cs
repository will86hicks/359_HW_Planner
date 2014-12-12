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
		}

		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }


		public DateTime DueDate{ get; set; }
		public string CourseName{ get; set; }
		 
		// new property
		public bool Done { get; set; }
	}
}

