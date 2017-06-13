using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LitJson;

namespace GFSDK
{
	sealed class GFIOS : MonoBehaviour, ILauncher 
	{
		private static string objectName = "GFIOS";
			
		private static GFIOS _instance;
			
        private static OnCallback callback = null;
            
		#if UNITY_IPHONE
        [DllImport("__Internal")]
		public static extern void _setZone(string _zone);

        [DllImport("__Internal")]
		public static extern void _launchGF365(string _object, string _callback, string _param);

		[DllImport("__Internal")]
		public static extern void _getGameRegId(string _object, string _callback, string _param);
		#endif

		void Awake()
		{                
		}
			
		public static GFIOS Instance
		{
			get{					
				if( null == _instance )
				{					
					_instance = FindObjectOfType( typeof( GFIOS ) ) as GFIOS;

					if( null == _instance )
					{					
						_instance = new GameObject( objectName ).AddComponent<GFIOS>();

                        DontDestroyOnLoad( _instance );
					}
				}
				
				return _instance;
			}
		}

		public void setZone(GFZoneType zone)
        {
			#if UNITY_IPHONE
				_setZone(zone.ToString());
			#endif
        }

		public void launchGF365(string param, OnCallback listener)
        {
            callback = listener;

			#if UNITY_IPHONE
			_launchGF365(objectName, "onLauncher", param);
			#endif
        }

		public void getGameRegId(string param, OnCallback listener)
		{ 
			callback = listener;

			#if UNITY_IPHONE
			_getGameRegId(objectName, "onEventToken", param);
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

