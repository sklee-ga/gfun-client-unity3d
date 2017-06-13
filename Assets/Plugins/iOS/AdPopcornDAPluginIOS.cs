using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.ComponentModel;


public class AdPopcornDAPluginIOS : MonoBehaviour
{

	// DA
	public const int DABannerViewSize320x50 = 0;
		
	#region Events
#if UNITY_IPHONE
	
	//DA
	public static event Action <string> daBannerViewDidFailToReceiveAdWithError;
	public static event Action daBannerViewDidLoadAd;
	public static event Action daBannerViewWillLeaveApplication;
	
	public static event Action <string> daInterstitialAdDidFailToReceiveAdWithError;
	public static event Action daInterstitialAdDidLoad;
	public static event Action daInterstitialAdWillLeaveApplication;
	
	public static event Action <string> daPopupAdDidFailToReceiveAdWithError;
	public static event Action daPopupAdDidLoad;
	public static event Action daPopupAdWillLeaveApplication;
	
	public static event Action <string> daNativeAdDidFailWithError;
	public static event Action <string> daNativeAdDidFinishLoading;
	
	public static event Action daInterstitialAdWillOpen;
	public static event Action daInterstitialAdDidOpen;
	public static event Action daInterstitialAdWillClose;
	public static event Action daInterstitialAdDidClose;
	
	public static event Action daPopupAdWillOpen;
	public static event Action daPopupAdDidOpen;
	public static event Action daPopupAdWillClose;
	public static event Action daPopupAdDidClose;

	public static event Action <string> daOneSpotAdDidFailToReceiveAdWithError;
	public static event Action daOneSpotAdDidLoad;
	public static event Action daOneSpotAdWillLeaveApplication;

	public static event Action daOneSpotAdWillOpen;
	public static event Action daOneSpotAdDidOpen;
	public static event Action daOneSpotAdWillClose;
	public static event Action daOneSpotAdDidClose;
	public static event Action daOneSpotAdDidCompleteVideoAd;

#endif
	#endregion

	#region	Interface to native implementation

	[DllImport("__Internal")]
	extern public static void _AdPopcornDASetCallbackHandler(string handlerName);

	[DllImport("__Internal")]
	extern public static void _DAInitWithBannerViewSize(int size, float originX, float originY, string appKey, string spotKey);

	[DllImport("__Internal")]
	extern public static void _DABannerViewSetAdRefreshRate(int adRefreshRate);
	
	[DllImport("__Internal")]
	extern public static void _DABannerViewSetDelegate();
	
	[DllImport("__Internal")]
	extern public static void _DACloseBannerView();
	
	[DllImport("__Internal")]
	extern public static void _DAInterstitialAdInitWithKey(string appKey, string spotKey);
	
	[DllImport("__Internal")]
	extern public static void _DAInterstitialAdSetDelegate();
	
	[DllImport("__Internal")]
	extern public static void _DAInterstitialAdPresentFromViewController();
	
	[DllImport("__Internal")]
	extern public static void _DAPopupAdInitWithKey(string appKey, string spotKey);

	[DllImport("__Internal")]
	extern public static void _DAPopupAdSetDelegate();
	
	[DllImport("__Internal")]
	extern public static void _DAPopupAdPresentFromViewController();
	
	[DllImport("__Internal")]
	extern public static void _DANativeAdInitWithKey(string appKey, string spotKey);
	
	[DllImport("__Internal")]
	extern public static void _DANativeAdSetDelegate();
	
	[DllImport("__Internal")]
	extern public static void _DANativeAdLoadAd();
	
	[DllImport("__Internal")]
	extern public static void _DANativeAdCallImpression(string impressionUrl);
	
	[DllImport("__Internal")]
	extern public static void _DANativeAdClick(string clickUrl);

	[DllImport("__Internal")]
	extern public static void _DAOneSpotAdInitWithKey(string appKey, string spotKey);

	[DllImport("__Internal")]
	extern public static void _DAOneSpotAdSetDelegate();

	[DllImport("__Internal")]
	extern public static void _DAOneSpotAdPresentFromViewController();

	#endregion

	#region Declarations for non-native for AdPopcornDA

	/// <summary>
	/// Sets the callback handler.
	/// </summary>
	/// <param name='handlerName'>
	/// Handler name. Must match a Unity GameObject name, for the native code
	/// to utilize UnitySendMessage() properly.
	/// </param>
	public static void AdPopcornDASetCallbackHandler(string handlerName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_AdPopcornDASetCallbackHandler(handlerName);
		#endif
	}

	/// <summary>
	/// init banner view
	/// </summary>
	/// <param name="size">
	/// A <see cref="System.int"/>
	/// </param>
	/// <param name="originX">
	/// A <see cref="System.float"/>
	/// </param>
	/// <param name="originY">
	/// A <see cref="System.float"/>
	/// </param>
	/// <param name="appKey">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="spotKey">
	/// A <see cref="System.String"/>
	/// </param>
	public static void DAInitWithBannerViewSize(int size, float originX, float originY, string appKey, string spotKey)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DAInitWithBannerViewSize(size, originX, originY, appKey, spotKey);
		#endif
	}

	/// <summary>
	/// 배너 광고 refresh 주기를 설정한다.
	/// </summary>
	/// <param name="adRefreshRate">
	/// A <see cref="System.int"/>
	/// </param>
	public static void DABannerViewSetAdRefreshRate(int adRefreshRate)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DABannerViewSetAdRefreshRate(adRefreshRate);
		#endif
		
	}
	
	/// <summary>
	/// set delegate.
	/// </summary>
	public static void DABannerViewSetDelegate()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DABannerViewSetDelegate();
		#endif
		
	}
	
	/// <summary>
	/// set delegate.
	/// </summary>
	public static void DACloseBannerView()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DACloseBannerView();
		#endif
		
	}

	/// <summary>
	/// init interstitial ad
	/// </summary>
	/// <param name="appKey">
	/// A <see cref="System.String"/>
	/// </param>
	/// /// <param name="spotKey">
	/// A <see cref="System.String"/>
	/// </param>
	public static void DAInterstitialAdInitWithKey(string appKey, string spotKey)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DAInterstitialAdInitWithKey(appKey, spotKey);
		#endif
	}

	/// <summary>
	/// set delegate.
	/// </summary>	
	public static void DAInterstitialAdSetDelegate()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DAInterstitialAdSetDelegate();
		#endif
		
	}
	
	/// <summary>
	/// load interstitial ad.
	/// </summary>	
	public static void DAInterstitialAdPresentFromViewController()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DAInterstitialAdPresentFromViewController();
		#endif
	}
	
	/// <summary>
	/// init popup ad
	/// </summary>
	/// <param name="appKey">
	/// A <see cref="System.String"/>
	/// </param>
	/// /// <param name="spotKey">
	/// A <see cref="System.String"/>
	/// </param>
	public static void DAPopupAdInitWithKey(string appKey, string spotKey)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DAPopupAdInitWithKey(appKey, spotKey);
		#endif
		
	}

	/// <summary>
	/// set delegate.
	/// </summary>	
	public static void DAPopupAdSetDelegate()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DAPopupAdSetDelegate();
		#endif
		
	}
	
	/// <summary>
	/// load popup ad.
	/// </summary>	
	public static void DAPopupAdPresentFromViewController()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DAPopupAdPresentFromViewController();
		#endif
	}
	
	/// <summary>
	/// init native ad
	/// </summary>
	/// <param name="appKey">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="spotKey">
	/// A <see cref="System.String"/>
	/// </param>
	public static void DANativeAdInitWithKey(string appKey, string spotKey)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DANativeAdInitWithKey(appKey, spotKey);
		#endif
	}
	
	/// <summary>
	/// set delegate.
	/// </summary>	
	public static void DANativeAdSetDelegate()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DANativeAdSetDelegate();
		#endif
	}
	
	/// <summary>
	/// load native ad.
	/// </summary>	
	public static void DANativeAdLoadAd()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DANativeAdLoadAd();
		#endif
	}
	
	/// <summary>
	/// native ad 노출시, 노출에 대한 리포팅을 위해 호출한다. 전달받은 결과 json중, ImpressionURL로 요청한다.
	/// </summary>
	/// <param name="impressionUrl">
	/// A <see cref="System.String"/>
	/// </param>
	public static void DANativeAdCallImpression(string impressionUrl)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DANativeAdCallImpression(impressionUrl);
		#endif
	}
	
	/// <summary>
	/// native ad 클릭시, 랜딩페이지로 이동하기 위해 호출한다. 전달받은 결과 json중, RedirectURL로 요청한다.
	/// </summary>
	/// <param name="clickUrl">
	/// A <see cref="System.String"/>
	/// </param>
	public static void DANativeAdClick(string clickUrl)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DANativeAdClick(clickUrl);
		#endif
		
	}

	/// <summary>
	/// init oneSpotAd
	/// </summary>
	/// <param name="appKey">
	/// A <see cref="System.String"/>
	/// </param>
	/// /// <param name="spotKey">
	/// A <see cref="System.String"/>
	/// </param>
	public static void DAOneSpotAdInitWithKey(string appKey, string spotKey)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_DAOneSpotAdInitWithKey(appKey, spotKey);
		#endif

	}

	/// <summary>
	/// set oneSpotAd delegate.
	/// </summary>	
	public static void DAOneSpotAdSetDelegate()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_DAOneSpotAdSetDelegate();
		#endif

	}

	/// <summary>
	/// show oneSpotAd
	/// </summary>	
	public static void DAOneSpotAdPresentFromViewController()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_DAOneSpotAdPresentFromViewController();
		#endif
	}

	#endregion

	#region Coupon Callback Methods

	/// <summary>
	/// DABannerView의 광고 수신 실패시, delegate에 노티한다.
	/// </summary>	
	/// <param name="error">
	/// A <see cref="System.String"/>
	/// </param>
	public void DABannerViewDidFailToReceiveAdWithError(string error)
	{
		#if UNITY_IPHONE
		if (error != null && daBannerViewDidFailToReceiveAdWithError != null) 
		{
			daBannerViewDidFailToReceiveAdWithError(error);
		}
		#endif
	}
	
	/// <summary>
	/// DABannerView의 광고 load 완료 후, delegate에 노티한다.
	/// </summary>	
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	public void DABannerViewDidLoadAd()
	{
		#if UNITY_IPHONE
		if (daBannerViewDidLoadAd != null)
		{
			daBannerViewDidLoadAd();
		}
		#endif
	}
	
	/// <summary>
	/// DABannerView의 광고 클릭시, 랜딩 페이지로 이동 직전에 delegate에 노티한다.
	/// </summary>	
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	public void DABannerViewWillLeaveApplication()
	{
		#if UNITY_IPHONE
		if (daBannerViewWillLeaveApplication != null)
		{
			daBannerViewWillLeaveApplication();
		}
		#endif
	}
	
	/// <summary>
	/// DAInterstitialAd의 광고 수신 실패시, delegate에 노티한다.
	/// </summary>	
	/// <param name="error">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAInterstitialAdDidFailToReceiveAdWithError(string error)
	{
		#if UNITY_IPHONE
		if (error != null && daInterstitialAdDidFailToReceiveAdWithError != null) 
		{
			daInterstitialAdDidFailToReceiveAdWithError(error);
		}
		#endif
	}
	
	/// <summary>
	/// DAInterstitialAd의 광고 load 완료 후, delegate에 노티한다.
	/// </summary>	
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAInterstitialAdDidLoad()
	{
		#if UNITY_IPHONE
		if (daInterstitialAdDidLoad != null)
		{
			daInterstitialAdDidLoad();
		}
		#endif
	}
	
	/// <summary>
	/// DAInterstitialAd의 광고 클릭시, 랜딩 페이지로 이동 직전에 delegate에 노티한다.
	/// </summary>	
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAInterstitialAdWillLeaveApplication()
	{
		#if UNITY_IPHONE
		if (daInterstitialAdWillLeaveApplication != null)
		{
			daInterstitialAdWillLeaveApplication();
		}
		#endif
	}
	
	/// <summary>
	/// DAPopupAd의 광고 수신 실패시, delegate에 노티한다.
	/// </summary>	
	/// <param name="error">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAPopupAdDidFailToReceiveAdWithError(string error)
	{
		#if UNITY_IPHONE
		if (error != null && daPopupAdDidFailToReceiveAdWithError != null) 
		{
			daPopupAdDidFailToReceiveAdWithError(error);
		}
		#endif
	}
	
	/// <summary>
	/// DAPopupAd의 광고 load 완료 후, delegate에 노티한다.
	/// </summary>	
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAPopupAdDidLoad()
	{
		#if UNITY_IPHONE
		if (daPopupAdDidLoad != null) 
		{
			daPopupAdDidLoad();
		}
		#endif
	}
	
	/// <summary>
	/// DAPopupAd의 광고 클릭시, 랜딩 페이지로 이동 직전에 delegate에 노티한다.
	/// </summary>	
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAPopupAdWillLeaveApplication()
	{
		#if UNITY_IPHONE
		if (daPopupAdWillLeaveApplication != null)
		{
			daPopupAdWillLeaveApplication();
		}
		#endif
	}
	
	/// <summary>
	/// DANativeAd 요청 실패시, delegate에 노티한다.
	/// </summary>	
	/// <param name="error">
	/// A <see cref="System.String"/>
	/// </param>
	public void DANativeAdDidFailWithError(string error)
	{
		#if UNITY_IPHONE
		if (error != null && daNativeAdDidFailWithError != null) 
		{
			daNativeAdDidFailWithError(error);
		}
		#endif
	}
	
	/// <summary>
	/// DANativeAd 요청 성공시, delegate에 노티한다.
	/// </summary>	
	/// <param name="error">
	/// A <see cref="System.String"/>
	/// </param>
	public void DANativeAdDidFinishLoading(string nativeAdvertisingResultJson)
	{
		#if UNITY_IPHONE
		if (daNativeAdDidFinishLoading != null)
		{
			daNativeAdDidFinishLoading(nativeAdvertisingResultJson);
		}
		#endif
	}
	
	/// <summary>
	/// DAInterstitialAd가 open되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void DAInterstitialAdWillOpen()
	{
		#if UNITY_IPHONE
		if (daInterstitialAdWillOpen != null)
		{
			daInterstitialAdWillOpen();
		}
		#endif
	}
	
	/// <summary>
	/// DAInterstitialAd가 Open된 직후에 delegate에 노티된다.
	/// </summary>	
	public void DAInterstitialAdDidOpen()
	{
		#if UNITY_IPHONE
		if (daInterstitialAdDidOpen != null)
		{
			daInterstitialAdDidOpen();
		}
		#endif
	}
	
	/// <summary>
	/// DAInterstitialAd가 close되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void DAInterstitialAdWillClose()
	{
		#if UNITY_IPHONE
		if (daInterstitialAdWillClose != null)
		{
			daInterstitialAdWillClose();
		}
		#endif
	}
	
	/// <summary>
	/// DAInterstitialAd가 close된 직후에 delegate에 노티된다.
	/// </summary>	
	public void DAInterstitialAdDidClose()
	{
		#if UNITY_IPHONE
		if (daInterstitialAdDidClose != null)
		{
			daInterstitialAdDidClose();
		}
		#endif
	}
	
	/// <summary>
	/// DAPopupAd open되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void DAPopupAdWillOpen()
	{
		#if UNITY_IPHONE
		if (daPopupAdWillOpen != null)
		{
			daPopupAdWillOpen();
		}
		#endif
	}
	
	/// <summary>
	/// DAPopupAd Open된 직후에 delegate에 노티된다.
	/// </summary>	
	public void DAPopupAdDidOpen()
	{
		#if UNITY_IPHONE
		if (daPopupAdDidOpen != null)
		{
			daPopupAdDidOpen();
		}
		#endif
	}
	
	/// <summary>
	/// DAPopupAd close되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void DAPopupAdWillClose()
	{
		#if UNITY_IPHONE
		if (daPopupAdWillClose != null)
		{
			daPopupAdWillClose();
		}
		#endif
	}
	
	/// <summary>
	/// DAPopupAd close된 직후에 delegate에 노티된다.
	/// </summary>	
	public void DAPopupAdDidClose()
	{
		#if UNITY_IPHONE
		if (daPopupAdDidClose != null)
		{
			daPopupAdDidClose();
		}
		#endif
	}

	/// <summary>
	/// DAOneSpotAd의 광고 수신 실패시, delegate에 노티한다.
	/// </summary>	
	/// <param name="error">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAOneSpotAdDidFailToReceiveAdWithError(string error)
	{
		#if UNITY_IPHONE
		if (error != null && daOneSpotAdDidFailToReceiveAdWithError != null) 
		{
			daOneSpotAdDidFailToReceiveAdWithError(error);
		}
		#endif
	}

	/// <summary>
	/// DAOneSpotAd의 광고 load 완료 후, delegate에 노티한다.
	/// </summary>	
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAOneSpotAdDidLoad()
	{
		#if UNITY_IPHONE
		if (daOneSpotAdDidLoad != null) 
		{
			daOneSpotAdDidLoad();
		}
		#endif
	}

	/// <summary>
	/// DAOneSpotAd의 광고 클릭시, 랜딩 페이지로 이동 직전에 delegate에 노티한다.
	/// </summary>	
	/// <param name="message">
	/// A <see cref="System.String"/>
	/// </param>
	public void DAOneSpotAdWillLeaveApplication()
	{
		#if UNITY_IPHONE
		if (daOneSpotAdWillLeaveApplication != null)
		{
			daOneSpotAdWillLeaveApplication();
		}
		#endif
	}
	/// <summary>
	/// DAOneSpotAd가 open되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void DAOneSpotAdWillOpen()
	{
		#if UNITY_IPHONE
		if (daOneSpotAdWillOpen != null)
		{
			daOneSpotAdWillOpen();
		}
		#endif
	}

	/// <summary>
	/// DAOneSpotAd가 Open된 직후에 delegate에 노티된다.
	/// </summary>	
	public void DAOneSpotAdDidOpen()
	{
		#if UNITY_IPHONE
		if (daOneSpotAdDidOpen != null)
		{
			daOneSpotAdDidOpen();
		}
		#endif
	}

	/// <summary>
	/// DAOneSpotAd가 close되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void DAOneSpotAdWillClose()
	{
		#if UNITY_IPHONE
		if (daOneSpotAdWillClose != null)
		{
			daOneSpotAdWillClose();
		}
		#endif
	}

	/// <summary>
	/// DAOneSpotAd가 close된 직후에 delegate에 노티된다.
	/// </summary>	
	public void DAOneSpotAdDidClose()
	{
		#if UNITY_IPHONE
		if (daOneSpotAdDidClose != null)
		{
			daOneSpotAdDidClose();
		}
		#endif
	}

	/// <summary>
	/// DAOneSpotAd의 video 광고 complete된a 직후에 delegate에 노티된다.
	/// </summary>	
	public void DAOneSpotAdDidCompleteVideoAd()
	{
		#if UNITY_IPHONE
		if (daOneSpotAdDidCompleteVideoAd != null)
		{
			daOneSpotAdDidCompleteVideoAd();
		}
		#endif
	}

	#endregion
}
