using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.ComponentModel;


public class IgaworksNanooPluginIOS : MonoBehaviour
{
		
	#region Events
#if UNITY_IPHONE

	// Nanoo
	public static event Action <string> getNanooFanPageDidComplete;
	public static event Action <string> getNanooFanPageFailedWithError;
	public static event Action willOpenNanooFanPage;
	public static event Action didOpenNanooFanPage;
	public static event Action willCloseNanooFanPage;
	public static event Action didCloseNanooFanPage;

#endif
	#endregion

	#region	Interface to native implementation

	[DllImport("__Internal")]
	extern public static void _IgaworksNanooSetCallbackHandler(string handlerName);

	[DllImport("__Internal")]
	extern public static void _GetNanooFanPage();

	#endregion

	#region Declarations for non-native for IgaworksNanoo

	/// <summary>
	/// Sets the callback handler.
	/// </summary>
	/// <param name='handlerName'>
	/// Handler name. Must match a Unity GameObject name, for the native code
	/// to utilize UnitySendMessage() properly.
	/// </param>
	public static void IgaworksNanooSetCallbackHandler(string handlerName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_IgaworksNanooSetCallbackHandler(handlerName);
		#endif
	}
	
	/// <summary>
	///  Nanoo fan page를 요청한다
	/// </summary>
	public static void GetNanooFanPage()
	{
		#if UNITY_IPHONE
		_GetNanooFanPage();
		#endif
	}

	#endregion

	#region Nanoo Callback Methods
	/// <summary>
	/// getNanooFanPage 요청이 성공하면, nanoo fan page url을 return합니다.
	/// </summary>	
	public void GetNanooFanPageDidComplete(string nanooFanPageurl)
	{
		#if UNITY_IPHONE
		getNanooFanPageDidComplete(nanooFanPageurl);
		#endif
	}
	
	/// <summary>
	/// getNanooFanPage 요청이 실패하면, error을 return합니다.
	/// </summary>	
	public void GetNanooFanPageFailedWithError(string error)
	{
		#if UNITY_IPHONE
		getNanooFanPageFailedWithError(error);
		#endif
	}
	
	/// <summary>
	/// Nanoo page가 open되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void WillOpenNanooFanPage()
	{
		#if UNITY_IPHONE
		willOpenNanooFanPage();
		#endif
	}
	/// <summary>
	/// Nanoo page가 open된 직후에 delegate에 노티된다.
	/// </summary>	
	public void DidOpenNanooFanPage()
	{
		#if UNITY_IPHONE
		didOpenNanooFanPage();
		#endif
	}
	
	/// <summary>
	/// Nanoo page가 close되기 직전에 delegate에 노티된다.
	/// </summary>	
	public void WillCloseNanooFanPage()
	{
		#if UNITY_IPHONE
		willCloseNanooFanPage();
		#endif
	}
	
	/// <summary>
	/// Nanoo page가 close된 직후에 delegate에 노티된다.
	/// </summary>	
	public void DidCloseNanooFanPage()
	{
		#if UNITY_IPHONE
		didCloseNanooFanPage();
		#endif
	}
	
	#endregion
}
