using System.Collections;

namespace GFSDK
{
	public enum GFStoreType 
	{
#if UNITY_IPHONE
        AppStore
#endif

#if UNITY_ANDROID
		GooglePlay,
		
        OneStore
#endif
	}
}

