using System;
using System.Text;
using LitJson;

namespace GFSDK
{
	public class GFLauncherImpl
	{
		private ILauncher plugin = null;
		private static Response response = new Response();

		private static OnGFLauncherDelegate launcherDelegate = null;
		private static OnGFEventTokenDelegate evttokenDelegate = null;

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

		public static void launchGF365App(GFStoreType storeType, int gameId, OnGFLauncherDelegate listener)
		{
			checkSDK();
			launcherDelegate = listener;

			try
			{
				StringBuilder sb = new StringBuilder();
				LitJson.JsonWriter writer = new LitJson.JsonWriter(sb);

				writer.WriteObjectStart();
				writer.WritePropertyName("storeType"); writer.Write(storeType.ToString());
				writer.WritePropertyName("eventId"); writer.Write(gameId);
				writer.WriteObjectEnd();

				instance.plugin.launchGF365(sb.ToString(), response);
			}
			catch( Exception e )
			{
				Console.WriteLine(e.ToString());
			}
		}

		public static void getGameRegId(GFStoreType storeType, int gameId, OnGFEventTokenDelegate listener)
		{
			checkSDK();
			evttokenDelegate = listener;

			try
			{
				StringBuilder sb = new StringBuilder();
				LitJson.JsonWriter writer = new LitJson.JsonWriter(sb);

				writer.WriteObjectStart();
				writer.WritePropertyName("storeType"); writer.Write(storeType.ToString());
				writer.WritePropertyName("gameId"); writer.Write(gameId);
				writer.WriteObjectEnd();

				instance.plugin.getGameRegId(sb.ToString(), response);
			}
			catch( Exception e )
			{
				Console.WriteLine(e.ToString());
			}
		}

		private static void responseLauncher(string response)
		{
			GFResult result = new GFResult(response);

			if (launcherDelegate != null)
			{
				if (launcherDelegate is OnGFLauncherDelegate)
				{
					((OnGFLauncherDelegate)launcherDelegate)(result);
				} 
			}
		}

		private static void responseEvtToken(string response)
		{
			GFResult result = new GFResult(response);
			string gameRegId = null;

			try
			{
				Console.WriteLine("PPPPPPPPPPPPPPPPPPP");
				Console.WriteLine(response);

				JsonData jsonData = JsonMapper.ToObject(response);
				gameRegId = jsonData["gameRegId"].ToString ();
			}
			catch (Exception e)
			{
				result = new GFResult (ResultCode.EXCEPTION_OCCURE , "Exception occured.");
				Console.WriteLine(e.ToString());
			} 

			if (evttokenDelegate != null)
			{
				if (evttokenDelegate is OnGFEventTokenDelegate)
				{
					((OnGFEventTokenDelegate)evttokenDelegate)(result , gameRegId);
				}
			}
		}

		private class Response : OnCallback
		{
			public void onResponse( CallbackType type, string response )
			{
				switch( type )
				{
				case CallbackType.LAUNCHER:
					responseLauncher(response);
					break;

				case CallbackType.EVENTTOKEN:
					responseEvtToken (response);
					break;

				default :
					break;
				}
			}
		}

		static object lockInstance = new object();
		static GFLauncherImpl _instance;

		internal static GFLauncherImpl instance
		{
			get
			{
				if( _instance == null )
				{
					lock( lockInstance )
					{
						if( _instance == null )
						{
							_instance = new GFLauncherImpl();
						}
					}
				}

				return _instance;
			}           
		}
	}
}

