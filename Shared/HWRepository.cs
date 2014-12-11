using System;
using System.Collections.Generic;
using System.IO;
using HWPlanner.BL;

namespace HWPlanner.DAL {
	public class HWRepository {
		DL.HWDatabase db = null;
		protected static string dbLocation;		
		protected static HWRepository me;		
		
		static HWRepository ()
		{
			me = new HWRepository();
		}
		
		protected HWRepository()
		{
			// set the db location
			dbLocation = DatabaseFilePath;
			
			// instantiate the database	
			db = new HWPlanner.DL.HWDatabase(dbLocation);
		}
		
		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "HWDB.db3";
#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
	            var path = sqliteFilename;
#else



#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
	            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
#endif










				var path = Path.Combine (libraryPath, sqliteFilename);
#endif		






				return path;	
			}
		}

		public static HW GetHW(int id)
		{
            return me.db.GetItem<HW>(id);
		}
		
		public static IEnumerable<HW> GetHWs ()
		{
			return me.db.GetItems<HW>();
		}
		
		public static int SaveHW (HW item)
		{
			return me.db.SaveItem<HW>(item);
		}

		public static int DeleteHW(int id)
		{
			return me.db.DeleteItem<HW>(id);
		}
	}
}

