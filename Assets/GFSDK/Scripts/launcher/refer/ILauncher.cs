using System;

namespace GFSDK
{
	enum CallbackType 
	{
		LAUNCHER,
		EVENTTOKEN
	}

	interface OnCallback
	{
		void onResponse( CallbackType type, string response );
	}

	interface ILauncher
	{
		void setZone(GFZoneType zone);
		void launchGF365(string param, OnCallback listener);
		void getGameRegId(string param, OnCallback listener);
	}
}

