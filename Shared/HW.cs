using Tasky.BL.Contracts;
using Tasky.DL.SQLite;

namespace Tasky.BL
{
	/// <summary>
	/// Represents a Task.
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

