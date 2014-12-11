using HWPlanner.BL.Contracts;
using HWPlanner.DL.SQLite;

namespace HWPlanner.BL
{
	/// <summary>
	/// Represents a HW.
	/// </summary>
	public class HW : IBusinessEntity
	{
		public HW ()
		{
		}

		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		// new property
		public bool Done { get; set; }
	}
}

