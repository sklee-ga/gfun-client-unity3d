using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GFSDK
{
	sealed class GFAndroid : MonoBehaviour, ILauncher
	{
		private const string AndroidGFJavaClass = "kr.co.gconhub.gf365.addon.extension.NativeProxy";

		private static string objectName = "GFAndroid";
		
		private static GFAndroid _instance;
					
		private OnCallback callback = null;
		
#if UNITY_ANDROID
		private AndroidJavaObject unityActivity;
		
		private AndroidJavaClass gfJava;
		private AndroidJavaClass _plugins
		{
			get
			{					
				if( gfJava == null )
				{
					gfJava = new AndroidJavaClass( AndroidGFJavaClass );
					
					if( gfJava == null )
					{
						throw new MissingReferenceException(
							string.Format( "GFAndroid failed to load {0} class", AndroidGFJavaClass ) );
					}
				}
				
				return gfJava;
			}
		}
#endif
		
		void Awake()
		{
#if UNITY_ANDROID
			AndroidJavaClass unityPlayer = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" );
			unityActivity = unityPlayer.GetStatic<AndroidJavaObject>( "currentActivity" );
#endif
		}
		
		public static GFAndroid Instance
		{
			get
			{
				if( null == _instance )
				{
					_instance = FindObjectOfType( typeof( GFAndroid ) ) as GFAndroid;
					
					if( null == _instance )
					{
						_instance = new GameObject( objectName ).AddComponent<GFAndroid>();

                        DontDestroyOnLoad( _instance );
					}
				}
				
				return _instance;
			}
		}
			
        public void setZone(GFZoneType zone)
        {
#if UNITY_ANDROID
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
				_plugins.CallStatic("setZone", zone.ToString());
            }));
#endif
        }

		public void launchGF365(string param, OnCallback listener)
        {
            callback = listener;

#if UNITY_ANDROID
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
				_plugins.CallStatic("launchGF365", unityActivity, param, objectName, "onLauncher");                    
            }));
#endif
        }

		public void getGameRegId(string param, OnCallback listener)
		{
			callback = listener;

#if UNITY_ANDROID
			unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			{
				//_plugins.CallStatic("getEventToken", unityActivity, param, objectName, "onEventToken");                    
				_plugins.CallStatic("getGameRegId", unityActivity, param, objectName, "onEventToken");                    
			}));
#endif
		}

		public void onLauncher(string response)
        {
			callback.onResponse(CallbackType.LAUNCHER, response.ToString());
        }

		public void onEventToken(string response)
		{
			callback.onResponse(CallbackType.EVENTTOKEN, response.ToString());
		}
	}
}

