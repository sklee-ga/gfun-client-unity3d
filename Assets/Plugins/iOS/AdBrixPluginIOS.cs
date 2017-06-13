using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.ComponentModel;


public class AdBrixPluginIOS : MonoBehaviour
{
	// for cohort 분석
	public const int AdBrixCustomCohort_1 = 1;
	public const int AdBrixCustomCohort_2 = 2;
	public const int AdBrixCustomCohort_3 = 3;


	// for commerceCurrency defines
	public const int AdBrixCurrencyKRW = 1;
	public const int AdBrixCurrencyUSD = 2;
	public const int AdBrixCurrencyJPY = 3;
	public const int AdBrixCurrencyEUR = 4;
	public const int AdBrixCurrencyGBP = 5;
	public const int AdBrixCurrencyCNY = 6;
	public const int AdBrixCurrencyTWD = 7;
	public const int AdBrixCurrencyHKD = 8;

	// for commerceV2 

	//payment method
	public const int AdBrixPaymentCreditCardMethod      = 1;
	public const int AdBrixPaymentBankTransferMethod    = 2;
	public const int AdBrixPaymentMobilePaymentMethod   = 3;
	public const int AdBrixPaymentETCMethod             = 4;

	//sharing channel
	public const int AdBrixSharingFacebookChannel       = 1;
	public const int AdBrixSharingKakaoTalkChannel      = 2;
	public const int AdBrixSharingKakaoStoryChannel     = 3;
	public const int AdBrixSharingLineChannel           = 4;
	public const int AdBrixSharingWhatsAppChannel       = 5;
	public const int AdBrixSharingQQChannel             = 6;
	public const int AdBrixSharingWeChatChannel         = 7;
	public const int AdBrixSharingSMSChannel            = 8;
	public const int AdBrixSharingEmailChannel          = 9;
	public const int AdBrixSharingCopyUrlChannel        = 10;
	public const int AdBrixSharingETCChannel            = 11;

	#region Events
#if UNITY_IPHONE


#endif
	#endregion

	#region	Interface to native implementation



	[DllImport("__Internal")]
	extern public static void _FirstTimeExperience(string name);
	
	[DllImport("__Internal")]
	extern public static void _FirstTimeExperienceWithParam(string name, string param);
	
	[DllImport("__Internal")]
	extern public static void _Retention(string name);
	
	[DllImport("__Internal")]
	extern public static void _RetentionWithParam(string name, string param);
	
	[DllImport("__Internal")]
	extern public static void _Buy(string name);
	
	[DllImport("__Internal")]
	extern public static void _BuyWithParam(string name, string param);

	[DllImport("__Internal")]
	extern public static void _ShowViralCPINotice();
	
	[DllImport("__Internal")]
	extern public static void _SetCustomCohort(int customCohortType, string filterName);
	
	[DllImport("__Internal")]
	extern public static void _CrossPromotionShowAD(string activityName);


	/// <summary>
	/// Commerce
	/// </summary>
	[DllImport("__Internal")]
	extern public static void _AdBrixPurchase (string orderId, string productId, string productName, double price, int quantity, string currencyString, string category);

	[DllImport("__Internal")]
	extern public static void _AdBrixPurchaseWithJson (string jsonString);

	[DllImport("__Internal")]
	extern public static void _AdBrixPurchaseList (string[] pArr, int arrCnt);



	/// <summary>
	/// CommerceV2
	/// </summary>

	[DllImport("__Internal")]
	extern public static string _AdBrixCurrencyName (int currency);

	[DllImport("__Internal")]
	extern public static string _AdBrixPaymentMethodName (int method);

	[DllImport("__Internal")]
	extern public static string _AdBrixSharingChannelName (int channel);

	[DllImport("__Internal")]
	extern public static void _CommerceV2Purchase (string productID, double price, string currency, string paymentMethod);

	[DllImport("__Internal")]
	extern public static void _CommerceV2PurchaseII (string orderID, string purchaseDataJsonString, double discount, double deliveryCharge, string paymentMethod);

	[DllImport("__Internal")]
	extern public static void _DeeplinkOpen (string deeplinkUrl);

	[DllImport("__Internal")]
	extern public static void _ProductView (string purchaseDataJsonString);

	[DllImport("__Internal")]
	extern public static void _Refund (string orderID, string purchaseDataJsonString, double penaltyCharge);

	[DllImport("__Internal")]
	extern public static void _RefundBulk (string orderID, string purchaseDataJsonString, double penaltyCharge);

	[DllImport("__Internal")]
	extern public static void _AddToCart (string purchaseDataJsonString);

	[DllImport("__Internal")]
	extern public static void _AddToCartBulk (string purchaseDataJsonString);

	[DllImport("__Internal")]
	extern public static void _Login (string usn);

	[DllImport("__Internal")]
	extern public static void _AddToWishList (string purchaseDataJsonString);

	[DllImport("__Internal")]
	extern public static void _CategoryView (string categoryString);

	[DllImport("__Internal")]
	extern public static void _ReviewOrder (string orderID, string purchaseDataJsonString, double discount, double deliveryCharge);

	[DllImport("__Internal")]
	extern public static void _ReviewOrderBulk (string orderID, string purchaseDataJsonString, double discount, double deliveryCharge);

	[DllImport("__Internal")]
	extern public static void _PaymentView (string orderID, string purchaseDataJsonString, double discount, double deliveryCharge);

	[DllImport("__Internal")]
	extern public static void _Search (string purchaseDataJsonString, string keyword);

	[DllImport("__Internal")]
	extern public static void _Share (string channel, string purchaseDataJsonString);

	#endregion

	#region Declarations for non-native for AdBrix

	public static void Purchase(string productID, double price, string currency, string paymentMethod)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_CommerceV2Purchase(productID, price, currency, paymentMethod);
		#endif
	}

	public static void Purchase(string orderID, AdBrixCommerceProductModel product, double discount, double deliveryCharge, string paymentMethod)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		if( product == null)
		{
			Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
		}

		string jsonArray = "[";
		jsonArray = jsonArray + stringifyCommerceV2Item(product) + "]";


		_CommerceV2PurchaseII (orderID, jsonArray, discount, deliveryCharge, paymentMethod);
		#endif
	}

	public static void Purchase(string orderID, List<AdBrixCommerceProductModel> products, double discount, double deliveryCharge, string paymentMethod)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		if( products == null)
		{
			Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
		}

		List<AdBrixCommerceProductModel>filterList = new List<AdBrixCommerceProductModel>();
		for(int i  = 0; i < products.Count; i++)
		{
			if(products[i] != null)filterList.Add(products[i]);
		}

		if(filterList == null || filterList.Count == 0)
		{
			Debug.Log ("AdBrixPluginIOS >> Filtered item list is empty");
		}

		string jsonArray = "[";
		for(int i = 0; i < filterList.Count; i++)
		{
			AdBrixCommerceProductModel item = filterList[i];
			if(i == (filterList.Count-1))
			{
				jsonArray = jsonArray + stringifyCommerceV2Item(item) + "]";
			}
			else
			{
				jsonArray = jsonArray + stringifyCommerceV2Item(item) + ",";		
			}
		}
		_CommerceV2PurchaseII (orderID, jsonArray, discount, deliveryCharge, paymentMethod);
		#endif
	}

	public static class Commerce
	{
		public static void Purchase(string productID, double price, string currency, string paymentMethod)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;
		
			_CommerceV2Purchase(productID, price, currency, paymentMethod);
			#endif
		}

		public static void Purchase(string orderID, AdBrixCommerceProductModel product, double discount, double deliveryCharge, string paymentMethod)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;
		
			if( product == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			string jsonArray = "[";
			jsonArray = jsonArray + stringifyCommerceV2Item(product) + "]";

		
			_CommerceV2PurchaseII (orderID, jsonArray, discount, deliveryCharge, paymentMethod);
			#endif
		}
			
		public static void Purchase(string orderID, List<AdBrixCommerceProductModel> products, double discount, double deliveryCharge, string paymentMethod)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( products == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			List<AdBrixCommerceProductModel>filterList = new List<AdBrixCommerceProductModel>();
			for(int i  = 0; i < products.Count; i++)
			{
				if(products[i] != null)filterList.Add(products[i]);
			}

			if(filterList == null || filterList.Count == 0)
			{
				Debug.Log ("AdBrixPluginIOS >> Filtered item list is empty");
			}

			string jsonArray = "[";
			for(int i = 0; i < filterList.Count; i++)
			{
				AdBrixCommerceProductModel item = filterList[i];
				if(i == (filterList.Count-1))
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + "]";
				}
				else
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + ",";		
				}
			}
			_CommerceV2PurchaseII (orderID, jsonArray, discount, deliveryCharge, paymentMethod);
			#endif
		}

		public static void DeeplinkOpen(string deeplinkUrl)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;
			_DeeplinkOpen(deeplinkUrl);
			#endif
		}

		public static void ProductView(AdBrixCommerceProductModel product)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( product == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			string jsonArray = "[";
			jsonArray = jsonArray + stringifyCommerceV2Item(product) + "]";


			_ProductView (jsonArray);
			#endif
		}

		public static void Refund(string orderID, AdBrixCommerceProductModel product, double penaltyCharge)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( product == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			string jsonArray = "[";
			jsonArray = jsonArray + stringifyCommerceV2Item(product) + "]";


			_Refund (orderID, jsonArray, penaltyCharge);
			#endif
		}

		public static void RefundBulk(string orderID, List<AdBrixCommerceProductModel> products, double penaltyCharge)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( products == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			List<AdBrixCommerceProductModel>filterList = new List<AdBrixCommerceProductModel>();
			for(int i  = 0; i < products.Count; i++)
			{
				if(products[i] != null)filterList.Add(products[i]);
			}

			if(filterList == null || filterList.Count == 0)
			{
				Debug.Log ("AdBrixPluginIOS >> Filtered item list is empty");
			}

			string jsonArray = "[";
			for(int i = 0; i < filterList.Count; i++)
			{
				AdBrixCommerceProductModel item = filterList[i];
				if(i == (filterList.Count-1))
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + "]";
				}
				else
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + ",";		
				}
			}

			_RefundBulk (orderID, jsonArray, penaltyCharge);
			#endif
		}

		public static void AddToCart(AdBrixCommerceProductModel product)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( product == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			string jsonArray = "[";
			jsonArray = jsonArray + stringifyCommerceV2Item(product) + "]";


			_AddToCart (jsonArray);
			#endif
		}

		public static void AddToCartBulk(List<AdBrixCommerceProductModel> products)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( products == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			List<AdBrixCommerceProductModel>filterList = new List<AdBrixCommerceProductModel>();
			for(int i  = 0; i < products.Count; i++)
			{
				if(products[i] != null)filterList.Add(products[i]);
			}

			if(filterList == null || filterList.Count == 0)
			{
				Debug.Log ("AdBrixPluginIOS >> Filtered item list is empty");
			}

			string jsonArray = "[";
			for(int i = 0; i < filterList.Count; i++)
			{
				AdBrixCommerceProductModel item = filterList[i];
				if(i == (filterList.Count-1))
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + "]";
				}
				else
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + ",";		
				}
			}

			_AddToCartBulk (jsonArray);
			#endif
		}

		public static void Login(string usn)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;
			_Login(usn);
			#endif
		}

		public static void AddToWishList(AdBrixCommerceProductModel product)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( product == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			string jsonArray = "[";
			jsonArray = jsonArray + stringifyCommerceV2Item(product) + "]";

			_AddToWishList (jsonArray);
			#endif
		}

		public static void CategoryView(AdBrixCommerceProductCategoryModel category)
		{
			string pStr = category.CategoryFullString;
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;
			_CategoryView(pStr);
			#endif
		}

		public static void ReViewOrder(string orderID, AdBrixCommerceProductModel product, double discount, double deliveryCharge)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( product == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			string jsonArray = "[";
			jsonArray = jsonArray + stringifyCommerceV2Item(product) + "]";


			_ReviewOrder (orderID, jsonArray, discount, deliveryCharge);
			#endif
		}

		public static void ReViewOrderBulk(string orderID, List<AdBrixCommerceProductModel> products, double discount, double deliveryCharge)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( products == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			List<AdBrixCommerceProductModel>filterList = new List<AdBrixCommerceProductModel>();
			for(int i  = 0; i < products.Count; i++)
			{
				if(products[i] != null)filterList.Add(products[i]);
			}

			if(filterList == null || filterList.Count == 0)
			{
				Debug.Log ("AdBrixPluginIOS >> Filtered item list is empty");
			}

			string jsonArray = "[";
			for(int i = 0; i < filterList.Count; i++)
			{
				AdBrixCommerceProductModel item = filterList[i];
				if(i == (filterList.Count-1))
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + "]";
				}
				else
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + ",";		
				}
			}
			_ReviewOrderBulk (orderID, jsonArray, discount, deliveryCharge);
			#endif
		}

		public static void PaymentView(string orderID, List<AdBrixCommerceProductModel> products, double discount, double deliveryCharge)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( products == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			List<AdBrixCommerceProductModel>filterList = new List<AdBrixCommerceProductModel>();
			for(int i  = 0; i < products.Count; i++)
			{
				if(products[i] != null)filterList.Add(products[i]);
			}

			if(filterList == null || filterList.Count == 0)
			{
				Debug.Log ("AdBrixPluginIOS >> Filtered item list is empty");
			}

			string jsonArray = "[";
			for(int i = 0; i < filterList.Count; i++)
			{
				AdBrixCommerceProductModel item = filterList[i];
				if(i == (filterList.Count-1))
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + "]";
				}
				else
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + ",";		
				}
			}
			_PaymentView (orderID, jsonArray, discount, deliveryCharge);
			#endif
		}

		public static void Search(List<AdBrixCommerceProductModel> products, string keyword)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( products == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			List<AdBrixCommerceProductModel>filterList = new List<AdBrixCommerceProductModel>();
			for(int i  = 0; i < products.Count; i++)
			{
				if(products[i] != null)filterList.Add(products[i]);
			}

			if(filterList == null || filterList.Count == 0)
			{
				Debug.Log ("AdBrixPluginIOS >> Filtered item list is empty");
			}

			string jsonArray = "[";
			for(int i = 0; i < filterList.Count; i++)
			{
				AdBrixCommerceProductModel item = filterList[i];
				if(i == (filterList.Count-1))
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + "]";
				}
				else
				{
					jsonArray = jsonArray + stringifyCommerceV2Item(item) + ",";		
				}
			}
			_Search (jsonArray, keyword);
			#endif
		}

		public static void Share(string channel, AdBrixCommerceProductModel product)
		{
			#if UNITY_IPHONE
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			if( product == null)
			{
				Debug.Log ("AdBrixPluginIOS >> Null or Empty Item");
			}

			string jsonArray = "[";
			jsonArray = jsonArray + stringifyCommerceV2Item(product) + "]";


			_Share (channel, jsonArray);
			#endif
		}
	}





	public static string stringifyCommerceV2Item(AdBrixCommerceProductModel item)
	{
		string jsonString = "";
		string jsonAttrString = "\"extra_attrs\":{";
		if(item != null)
		{
			string productId = item.ProductId;
			string productName = item.ProductName;
			double price = item.Price;
			string currency = item.CurrencyString;
			double discount = item.Discount;
			int quantity = item.Quantity;
			string category = item.Category;
			Dictionary<string, string> extraAttrsDic = item.ExtraAttrs;

			jsonString  = "{ \"productId\": " + "\"" + productId + "\"" + ", " +
				"\"productName\": " + "\"" + productName + "\"" + ", " +
				"\"price\": " + price + ", " +
				"\"currency\": " + "\"" + currency + "\"" + ", " +
				"\"discount\": " + discount + ", " +
				"\"quantity\": " + "\"" + quantity + "\"" + ", " +
				"\"category\": " + "\"" + category + "\"" + ", ";

			if (extraAttrsDic != null) {
				int pCnt = 0;
				foreach (KeyValuePair<string, string> pair in extraAttrsDic) {
					if (pCnt == (extraAttrsDic.Count - 1)) {
						jsonAttrString = jsonAttrString + stringifyCommerceV2ItemAttr (pair) + "}";
					} else {
						jsonAttrString = jsonAttrString + stringifyCommerceV2ItemAttr (pair) + ",";
					}
					pCnt++;
				}
			} 
			else
			{
				jsonAttrString = jsonAttrString + "}";
			}
		}
		return jsonString + jsonAttrString + "}";
	}

	public static string stringifyCommerceV2ItemAttr(KeyValuePair<string, string> extraAttr)
	{
		string jsonstring;

		jsonstring = "\"" + extraAttr.Key.ToString () + "\":" + "\"" + extraAttr.Value.ToString () + "\"";

		return jsonstring;
	}

	/// <summary>
	/// first time experience의 activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void FirstTimeExperience(string name)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_FirstTimeExperience(name);
		#endif
	}
	
	/// <summary>
	/// first time experience의 activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="param">
	/// A <see cref="System.String"/>
	/// </param>
	public static void FirstTimeExperienceWithParam(string name, string param)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_FirstTimeExperienceWithParam(name, param);
		#endif
	}
	
	/// <summary>
	/// retention activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void Retention(string name)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_Retention(name);
		#endif
	}
	
	/// <summary>
	/// retention activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="param">
	/// A <see cref="System.String"/>
	/// </param>
	public static void RetentionWithParam(string name, string param)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_RetentionWithParam(name, param);
		#endif
	}
	
	/// <summary>
	/// buy의 activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void Buy(string activityName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_Buy(activityName);
		#endif
	}
	
	/// <summary>
	/// buy의 activity에 해당할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="param">
	/// A <see cref="System.String"/>
	/// </param>
	public static void BuyWithParam(string name, string param)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_BuyWithParam(name, param);
		#endif
	}

	/// <summary>
	/// CPI + 친구초대 이벤트 요청시 호출한다.
	/// </summary>
	public static void ShowViralCPINotice()
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;

		_ShowViralCPINotice();
		#endif
	}
	
	/// <summary>
	/// Cohort 분석 요청시 호출한다.
	/// </summary>
	/// <param name="customCohortType">
	/// A <see cref="int"/>
	/// </param>
	/// <param name="filterName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void SetCustomCohort(int customCohortType, string filterName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_SetCustomCohort(customCohortType, filterName);
		#endif
	}


	public static void AdBrixPurchase(string orderId, string productId, string productName, double price, int quantity, string currencyString, string category)
	{
		#if UNITY_IPHONE
		_AdBrixPurchase(orderId, productId, productName, price, quantity, currencyString, category);
		#endif
	}
		
	public static void AdBrixPurchaseWithJson(string jsonString)
	{
		#if UNITY_IPHONE
		_AdBrixPurchaseWithJson(jsonString);
		#endif
	}
		
	public static void AdBrixPurchaseList(List<AdBrixItemModel> pArr)
	{

		#if UNITY_IPHONE
		string[] myArray =  new string[pArr.Count];
		for (int i = 0; pArr.Count > i; i++)
		{
		AdBrixItemModel pObject = (AdBrixItemModel)pArr [i];
		myArray[i] = pObject.OrderId +"&"+ pObject.ProductId +"&"+ pObject.ProductName +"&"+ pObject.Price +"&"+ pObject.Quantity +"&"+ pObject.CurrencyString +"&"+ pObject.Category;
		}
		if(pArr.Count > 0)
		_AdBrixPurchaseList(myArray, pArr.Count);
		#endif
	}

	public static string AdBrixCurrencyName(int currency) 
	{
		string res = null;
		#if UNITY_IPHONE
		res = _AdBrixCurrencyName (currency);
		#endif
		return res;
	}

	public static string AdBrixPaymentMethodName(int method) 
	{
		string res = null;
		#if UNITY_IPHONE
		res = _AdBrixPaymentMethodName (method);
		#endif
		return res;
	}


	public static string AdBrixSharingChannelName(int channel) 
	{
		string res = null;
		#if UNITY_IPHONE
		res = _AdBrixSharingChannelName (channel);
		#endif
		return res;
	}

	#endregion	
	
	
	#region Declarations for non-native for CrossPromotion
	/// <summary>
	/// Cross Promotion 광고를 노출하고자 할때 호출한다.
	/// </summary>
	/// <param name="activityName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void CrossPromotionShowAD(string activityName)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		_CrossPromotionShowAD(activityName);
		#endif
	}
	#endregion

}


public class AdBrixItemModel
{
	public string OrderId { get; set; }
	public string ProductId { get; set; }
	public double Price { get; set; } 
	public string CurrencyString{ get; set; } 
	public string Category{ get; set; }
	public int Quantity{ get; set; } 
	public string ProductName{ get; set; }

	public AdBrixItemModel(string orderId, string productId, string productName, double price, int quantity, string currencyString, string category)
	{
		this.OrderId = orderId;
		this.ProductId = productId;
		this.Price = price; 
		this.CurrencyString = currencyString; 
		this.Category = category;
		this.Quantity = quantity;
		this.ProductName = productName;
	}

	public static AdBrixItemModel create(string orderID, string productID, string productName, double price, int quantity, string currencyString, string category){

		return new AdBrixItemModel(orderID, productID, productName, price, quantity, currencyString, category);

	}
}

/// <summary>
/// Ad brix commerce product model.
/// </summary>
public class AdBrixCommerceProductModel
{
	public string ProductId { get; set; }
	public double Price { get; set; } 
	public double Discount;
	public string CurrencyString{ get; set; } 
	public string Category{ get; set; }
	public int Quantity{ get; set; } 
	public string ProductName{ get; set; }
	public Dictionary<string, string>ExtraAttrs{ get; set; }

	public AdBrixCommerceProductModel(string productId, string productName, double price, double discount, int quantity, string currencyString, AdBrixCommerceProductCategoryModel category, AdBrixCommerceProductAttrModel extraAttrs)
	{
		ExtraAttrs = null;
		ExtraAttrs = new Dictionary<string, string>();

		if(extraAttrs != null)
		{
			for(int i = 0 ; i < 5;  i ++)
			{
				if (!String.IsNullOrEmpty(extraAttrs.getKey(i)))
				{
					if (!extraAttrs.getKey(i).Equals(""))
					{
						ExtraAttrs.Add (extraAttrs.getKey (i), extraAttrs.getValue (i));
					}
				}
			}
		}
		else
		{
			ExtraAttrs = null;
		}

		this.ProductId = productId;
		this.ProductName = productName;
		this.Price = price; 
		this.Discount = discount; 
		this.Quantity = quantity;
		this.CurrencyString = currencyString; 
		this.Category = category.CategoryFullString;
	}

	public static AdBrixCommerceProductModel create(string productId, string productName, double price, double discount, int quantity, string currencyString, AdBrixCommerceProductCategoryModel category, AdBrixCommerceProductAttrModel extraAttrs)
	{
		return new AdBrixCommerceProductModel(productId, productName, price, discount, quantity, currencyString, category, extraAttrs);
	}
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

public class AdBrixCommerceProductAttrModel
{
	public string[] Key{ get; set; }
	public string[] Value{ get; set; }

	public static AdBrixCommerceProductAttrModel create(Dictionary<string, string> attrData)
	{
		AdBrixCommerceProductAttrModel pObject = new AdBrixCommerceProductAttrModel();
		
		pObject.Key = new string[5];
		pObject.Value = new string[5];
		int count = 0;
		foreach(KeyValuePair<string, string> entry in attrData)
		{
			pObject.setKeyAndVal (count, entry.Key, entry.Value);
			if (count > 4)
			{
//				AdPopcornLogWarning(@"[AdBrixCommerce Warning] Warning from AdBrixCommerceProductAttrModel create:!\n\
//                                AdBrixCommerceProductAttrModel parameter counts must set less then 5, data from the 6th to the end gonna be missed!!");
				break;
			}
			count++;
		} 

		return pObject;
	}

	private void setKeyAndVal(int pIndex, string key, string value)
	{
		Key[pIndex]		= key;
		Value[pIndex] 	= value;
	}

	public string getKey (int pIndex)
	{
		return Key[pIndex];
	}

	public string getValue (int pIndex)
	{
		return Value[pIndex];
	}
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

public class AdBrixCommerceProductCategoryModel
{
	public string[] Key{ get; set; }
	public string[] Value{ get; set; }
	public string[] Categories{ get; set; }
	public string CategoryFullString{ get; set; }
	List<string> CategoryArr;

	private string setCategoryFullString(List<string> categories)
	{
		string fullString = @"";
		int count = 0;
		categories.ForEach(delegate(String pCat)
		{
				if (!String.IsNullOrEmpty(pCat) && (count != 0))
				{
					if(!pCat.Equals(""))
					{
						fullString = fullString + "."+pCat;
					}
				}
				else if(!String.IsNullOrEmpty(pCat) && (count == 0))
				{
					if(!pCat.Equals(""))
					{
						fullString = pCat;
					}
				}

				count++;
		});

		return fullString;
	}

	public static AdBrixCommerceProductCategoryModel create(string category1)
	{
		AdBrixCommerceProductCategoryModel pObject = new AdBrixCommerceProductCategoryModel();
		List<string> pArr = new List<string>();
		pArr.Add (category1);
		pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
		return pObject;
	}

	public static AdBrixCommerceProductCategoryModel create(string category1, string category2)
	{
		AdBrixCommerceProductCategoryModel pObject = new AdBrixCommerceProductCategoryModel();
		List<string> pArr = new List<string>();
		pArr.Add (category1);
		pArr.Add (category2);
		pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
		return pObject;
	}

	public static AdBrixCommerceProductCategoryModel create(string category1, string category2, string category3)
	{
		AdBrixCommerceProductCategoryModel pObject = new AdBrixCommerceProductCategoryModel();
		List<string> pArr = new List<string>();
		pArr.Add (category1);
		pArr.Add (category2);
		pArr.Add (category3);
		pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
		return pObject;
	}

	public static AdBrixCommerceProductCategoryModel create(string category1, string category2, string category3, string category4)
	{
		AdBrixCommerceProductCategoryModel pObject = new AdBrixCommerceProductCategoryModel();
		List<string> pArr = new List<string>();
		pArr.Add (category1);
		pArr.Add (category2);
		pArr.Add (category3);
		pArr.Add (category4);

		pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
		return pObject;
	}

	public static AdBrixCommerceProductCategoryModel create(string category1, string category2, string category3, string category4, string category5)
	{
		AdBrixCommerceProductCategoryModel pObject = new AdBrixCommerceProductCategoryModel();
		List<string> pArr = new List<string>();
		pArr.Add (category1);
		pArr.Add (category2);
		pArr.Add (category3);
		pArr.Add (category4);
		pArr.Add (category5);

		pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
		return pObject;
	}

//	- (NSString *)getFullString;
//	{
//		return _categoryFullString;
//	}
}


