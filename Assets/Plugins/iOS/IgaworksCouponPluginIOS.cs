using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.ComponentModel;


public class IgaworksCouponPluginIOS : MonoBehaviour
{
		
	#region Events
#if UNITY_IPHONE

	// Coupon
	public static event Action <string> igaworksCouponValidationDidComplete;

#endif
	#endregion

	#region	Interface to native implementation

	[DllImport("__Internal")]
	extern public static void _IgaworksCouponSetCallbackHandler(string handlerName);

	[DllImport("__Internal")]
	extern public static void _ShowCoupon();

	[DllImport("__Internal")]
	extern public static void _CheckCoupon(string code);

	#endregion

	#region Declarations for non-native for IgaworksCoupon

	/// <summary>
	/// Sets the callback handler.
	/// </summary>
	/// <param name='handlerName'>
	/// Handler name. Must match a Unity GameObject name, for the native code
	/// to utilize UnitySendMessage() properly.
	/// </param>
	public static void IgaworksCouponSetCallbackHandler(string handlerName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_IgaworksCouponSetCallbackHandler(handlerName);
		#endif
	}

	/// <summary>
	/// 쿠폰 입력 창을 통해, 입력받은 쿠폰 코드를 검증한다.
	/// </summary>
	public static void ShowCoupon()
	{
		#if UNITY_IPHONE
		_ShowCoupon();
		#endif
	}
	
	/// <summary>
	///  쿠폰 코드를 검증한다.
	/// </summary>
	/// <param name="code">
	/// A <see cref="System.string"/> 
	/// </param>
	public static void CheckCoupon(string code)
	{
		#if UNITY_IPHONE
		_CheckCoupon(code);
		#endif
	}


	#endregion

	#region Coupon Callback Methods

	/// <summary>
	/// Coupon code 검증 후에 delegate에 노티된다.
	/// </summary>	
	public void IgaworksCouponValidationDidComplete(string message)
	{
		#if UNITY_IPHONE
		if (igaworksCouponValidationDidComplete != null)
		{
			igaworksCouponValidationDidComplete(message);
		}
		#endif
	}
	
	#endregion
}
