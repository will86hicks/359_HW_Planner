using System;
using System.Collections.Generic;
using Tasky.BL;

namespace Tasky.BL.Managers
{
	public static class HWManager
	{
		static HWManager ()
		{
		}
		
		public static HW GetTask(int id)
		{
			return DAL.HWRepository.GetTask(id);
		}
		
		public static IList<HW> GetTasks ()
		{
			return new List<HW>(DAL.HWRepository.GetTasks());
		}
		
		public static int SaveTask (HW item)
		{
			return DAL.HWRepository.SaveTask(item);
		}
		
		public static int DeleteTask(int id)
		{
			return DAL.HWRepository.DeleteTask(id);
		}
		
	}
}