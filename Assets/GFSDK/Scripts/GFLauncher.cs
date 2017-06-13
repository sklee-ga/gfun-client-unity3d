using System;
using System.Text;

namespace GFSDK
{
	public class GFLauncher
	{
		private static string VERSION = "1.3.2";

		public static string getVersion()
		{
			return VERSION;
		}

		[Obsolete("Deprecated")]
		public static void launchGF365(GFStoreType storeType, String eventId, OnGFLauncherDelegate listener)
		{
			GFLauncherImpl.launchGF365App(storeType, Int32.Parse(eventId), listener);
		}

		[Obsolete("Deprecated")]
		public static void getGameRegid(GFStoreType storeType, String eventId, OnGFEventTokenDelegate listener)
		{
			GFLauncherImpl.getGameRegId(storeType, Int32.Parse(eventId), listener);
		}

		public static void launchGF365(GFStoreType storeType, int eventId, OnGFLauncherDelegate listener)
		{
			GFLauncherImpl.launchGF365App(storeType, eventId, listener);
		}

		public static void getGameRegId(GFStoreType storeType, int eventId, OnGFEventTokenDelegate listener)
		{
			GFLauncherImpl.getGameRegId(storeType, eventId, listener);
		}

		static object lockInstance = new object();
		static GFLauncher _instance;

		internal static GFLauncher instance
		{
			get
			{
				if( _instance == null )
				{
					lock( lockInstance )
					{
						if( _instance == null )
						{
							_instance = new GFLauncher();
						}
					}
				}

				return _instance;
			}           
		}
	}
}

