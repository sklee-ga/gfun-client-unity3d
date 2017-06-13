using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.ComponentModel;


public class AdPopcornOfferwallPluginIOS : MonoBehaviour
{

	// for AdPopcorn Style
	public const int AdPopcornThemeBlueColor = 1;
	public const int AdPopcornThemeRedColor = 2;
	public const int AdPopcornThemeYellowColor = 3;
	
	#region Events
#if UNITY_IPHONE
	public static event Action offerWallWillOpen;
	public static event Action offerWallOpened;
	public static event Action offerWallWillClose;
	public static event Action offerWallClosed;

	public static event Action promotionEventWillOpen;
	public static event Action promotionEventOpened;
	public static event Action promotionEventWillClose;
	public static event Action promotionEventClosed;

	public static event Action loadVideoAdSuccess;
	public static event Action <string> loadVideoAdFailedWithError;
	public static event Action willOpenVideoAd;
	public static event Action didOpenVideoAd;
	public static event Action willCloseVideoAd;
	public static event Action didCloseVideoAd;
	public static event Action <string> showVideoAdFailedWithError;

#endif
	#endregion

	#region	Interface to native implementation
	[DllImport("__Internal")]
	extern public static void _AdPopcornOfferwallSetCallbackHandler(string handlerName);

	[DllImport("__Internal")]
	extern public static void _OpenOfferWallWithViewController(bool isSetDelegate, bool isUseUserDataDictionaryForFilter);

	[DllImport ("__Internal")]
	extern private static void _SetUserDataDictionaryForFilterKeyValue(string filterKey, string filterValue);

	[DllImport("__Internal")]
	extern public static void _GetClientPendingRewardItems();

	[DllImport("__Internal")]
	extern public static void _DidGiveRewardItemWithRewardKey(string rewardKey);

	[DllImport("__Internal")]
	extern public static void _OpenPromotionEvent(bool isSetDelegate);

	[DllImport("__Internal")]
	extern public static void _LoadVideoAd(bool isSetDelegate);

	[DllImport("__Internal")]
	extern public static void _ShowVideoAdWithViewController(bool isSetDelegate);

	[DllImport("__Internal")]
	extern public static void _SetAdPopcornThemeColor(int adPopcornThemeColor);

	[DllImport("__Internal")]
	extern public static void _SetAdPopcornTextThemeColor(int adPopcornTextThemeColor);

	[DllImport("__Internal")]
	extern public static void _SetAdPopcornRewardThemeColor(int adPopcornRewardThemeColor);

	[DllImport("__Internal")]
	extern public static void _SetAdPopcornRewardCheckThemeColor(int adPopcornRewardCheckThemeColor);

	[DllImport("__Internal")]
	extern public static void _SetAdPopcornOfferWallTitle(string adPopcornOfferWallTitle);


	#endregion

	#region Declarations for non-native for AdPopcornOfferwall
	/// <summary>
	/// Sets the callback handler.
	/// </summary>
	/// <param name='handlerName'>
	/// Handler name. Must match a Unity GameObject name, for the native code
	/// to utilize UnitySendMessage() properly.
	/// </param>

	public static void AdPopcornOfferwallSetCallbackHandler(string handlerName)
	{
        #if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_AdPopcornOfferwallSetCallbackHandler(handlerName);
        #endif
	}

	/// <summary>
	/// Open offerwall.
	/// </summary>
	/// <param name='isSetDelegate'>
	/// A <see cref="System.bool"/> 
	/// is set AdPopcornDelegate
	/// <param name='userDataDictionaryForFilter'>
	/// A <see cref="System.Dictionary<string, string>"/> 
	/// filter dictionary(targeting info)
	/// </param>
	public static void OpenOfferWallWithViewController(bool isSetDelegate, Dictionary<string, string> userDataDictionaryForFilter)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		bool isUseUserDataDictionaryForFilter = false;
		
		if(userDataDictionaryForFilter != null)
		{
			foreach (KeyValuePair<string, string> kvp in userDataDictionaryForFilter)
			{
				_SetUserDataDictionaryForFilterKeyValue(kvp.Key, kvp.Value);
			}
			
			isUseUserDataDictionaryForFilter = true;
		}
		
		_OpenOfferWallWithViewController(isSetDelegate, isUseUserDataDictionaryForFilter);
		#endif
	}
	
	/// <summary>
	/// 친구초대 이벤트를 요청한다.
	/// </summary>
	/// <param name='isSetDelegate'>
	/// A <see cref="System.bool"/> 
	/// is set AdPopcornDelegate
	/// </param>
	public static void OpenPromotionEvent(bool isSetDelegate) 
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_OpenPromotionEvent(isSetDelegate);
		#endif
	}

	public static void LoadVideAd(bool isSetDelegate)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_LoadVideoAd(isSetDelegate);
		#endif
	}

	public static void ShowVideoAdWithViewController(bool isSetDelegate)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_ShowVideoAdWithViewController(isSetDelegate);
		#endif
	}

	/// <summary>
	/// IGAWorks에 리워드 지급이 필요한 정보가 있는지 확인 요청을 한다.
	/// </summary>
	public static void GetClientPendingRewardItems() 
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_GetClientPendingRewardItems();
		#endif
	}

	/// <summary>
	/// IGAWorks에 리워드 지급 확정 처리를 요청한다. 이곳에서 사용자에게 리워드 지급 처리를 한다. 지급 처리가 완료 되었다면, 해당 메소드를 호출하여 IGAWorks에 리워드 지급 확정 처리를 요청한다.
	/// </summary>
	/// <param name='rewardKey'>
	/// A <see cref="System.string"/> 
	/// reward key
	/// </param>
	public static void DidGiveRewardItemWithRewardKey(string rewardKey)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_DidGiveRewardItemWithRewardKey(rewardKey);
		#endif

	}
	
	public static void SetAdPopcornThemeColor(int adPopcornThemeColor)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetAdPopcornThemeColor(adPopcornThemeColor);
		#endif
	}
	
	public static void SetAdPopcornTextThemeColor(int adPopcornTextThemeColor)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetAdPopcornTextThemeColor(adPopcornTextThemeColor);
		#endif
	}
	
	public static void SetAdPopcornRewardThemeColor(int adPopcornRewardThemeColor)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetAdPopcornRewardThemeColor(adPopcornRewardThemeColor);
		#endif
	}
	
	public static void SetAdPopcornRewardCheckThemeColor(int adPopcornRewardCheckThemeColor)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetAdPopcornRewardCheckThemeColor(adPopcornRewardCheckThemeColor);
		#endif
	}
	
	public static void SetAdPopcornOfferWallTitle(String adPopcornOfferWallTitle)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetAdPopcornOfferWallTitle(adPopcornOfferWallTitle);
		#endif
		
	}

	#endregion

	#region AdPopcorn Callback Methods
	/// <summary>
	/// offerwall이 open되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void OfferWallWillOpen()
	{
		#if UNITY_IPHONE
		if ( offerWallWillOpen != null) {
			offerWallWillOpen();
		}
		#endif
	}
	
	/// <summary>
	/// offerwall이 open된 직후에 delegate에 노티된다.
	/// </summary>	
	public void OfferWallOpened()
	{
		#if UNITY_IPHONE
		if ( offerWallOpened != null) {
			offerWallOpened();
		}
		
		#endif
	}
	
	/// <summary>
	/// offerwall이 clsoe되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void OfferWallWillClose()
	{
		#if UNITY_IPHONE
		if ( offerWallWillClose != null) {
			offerWallWillClose();
		}
		
		#endif
	}
	
	/// <summary>
	/// offerwall이 clsoe된 직후에 delegate에 노티된다.
	/// </summary>	
	public void OfferWallClosed()
	{
		#if UNITY_IPHONE
		if ( offerWallClosed != null) {
			offerWallClosed();
		}
		#endif
	}
	
	/// <summary>
	/// promotion event가 open되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void PromotionEventWillOpen()
	{
		#if UNITY_IPHONE
		if ( promotionEventWillOpen != null) {
			promotionEventWillOpen();
		}
		#endif
	}
	
	/// <summary>
	/// promotion event가 open된 직후에 delegate에 노티된다.
	/// </summary>	
	public void PromotionEventOpened()
	{
		#if UNITY_IPHONE
		if ( promotionEventOpened != null) {
			promotionEventOpened();
		}
		#endif
	}
	
	/// <summary>
	/// promotion event가 clsoe되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void PromotionEventWillClose()
	{
		#if UNITY_IPHONE
		if ( promotionEventWillClose != null) {
			promotionEventWillClose();
		}
		#endif
	}
	
	/// <summary>
	/// promotion event가 clsoe된 직후에 delegate에 노티된다.
	/// </summary>	
	public void PromotionEventClosed()
	{
		#if UNITY_IPHONE
		if ( promotionEventClosed != null) {
			promotionEventClosed();
		}
		#endif
	}

	public void LoadVideoAdSuccess()
	{
		#if UNITY_IPHONE
		if ( loadVideoAdSuccess != null) {
			loadVideoAdSuccess();
		}
		#endif
	}

	public void LoadVideoAdFailedWithError(string error)
	{
		#if UNITY_IPHONE
		if (error != null && loadVideoAdFailedWithError != null) {
			loadVideoAdFailedWithError(error);
		}
		#endif
	}

	public void WillOpenVideoAd()
	{
		#if UNITY_IPHONE
		if ( willOpenVideoAd != null) {
			willOpenVideoAd();
		}
		#endif
	}

	public void DidOpenVideoAd()
	{
		#if UNITY_IPHONE
		if ( didOpenVideoAd != null) {
			didOpenVideoAd();
		}
		#endif
	}

	public void WillCloseVideoAd()
	{
		#if UNITY_IPHONE
		if ( willCloseVideoAd != null) {
			willCloseVideoAd();
		}
		#endif
	}

	public void DidCloseVideoAd()
	{
		#if UNITY_IPHONE
		if ( didCloseVideoAd != null) {
			didCloseVideoAd();
		}
		#endif
	}

	public void ShowVideoAdFailedWithError(string error)
	{
		#if UNITY_IPHONE
		if ( error != null && showVideoAdFailedWithError != null) {
			showVideoAdFailedWithError(error);
		}
		#endif
	}

	#endregion
	
}
