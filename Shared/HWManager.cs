using System;
using System.Collections.Generic;
using HWPlanner.BL;

namespace HWPlanner.BL.Managers
{
	public static class HWManager
	{
		static HWManager ()
		{

		}
		
		public static HW GetHW(int id)
		{
			return DAL.HWRepository.GetHW(id);
		}
		
		public static IList<HW> GetHWs ()
		{
			return new List<HW>(DAL.HWRepository.GetHWs());
			//var x = new List<HW>(DAL.HWRepository.GetHWs());
			//return x;
			//var x = new List<HW>(DAL.HWRepository.GetHWs());

		}
		
		public static int SaveHW (HW item)
		{
			return DAL.HWRepository.SaveHW(item);
		}
		
		public static int DeleteHW(int id)
		{
			return DAL.HWRepository.DeleteHW(id);
		}
		
	}
}