using System;
using System.Text;
using LitJson;

namespace GFSDK
{
	public class GFEnvironment
	{
		private ILauncher plugin = null;

		private static void checkSDK()
		{
			if(instance.plugin == null)
			{
				#if UNITY_ANDROID
				instance.plugin = GFAndroid.Instance;
				#elif UNITY_IPHONE 
				instance.plugin = GFIOS.Instance;
				#endif
			}	
		}

		public static void setZone(GFZoneType zoneType)
		{
			checkSDK();

			instance.plugin.setZone(zoneType);
		}

		static object lockInstance = new object();
		static GFEnvironment _instance;

		internal static GFEnvironment instance
		{
			get
			{
				if( _instance == null )
				{
					lock( lockInstance )
					{
						if( _instance == null )
						{
							_instance = new GFEnvironment();
						}
					}
				}

				return _instance;
			}           
		}
	}
}

