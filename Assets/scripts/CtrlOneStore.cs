using UnityEngine;
using System.Collections;

using IapResponse;
using IapVerifyReceipt;
using IapError;


public class CtrlOneStore : MonoBehaviour {

	public GameObject iapButtonGroup;


	//------------------------------------------------
	// Default Variables
	//------------------------------------------------

	private AndroidJavaClass unityPlayerClass = null;
	private AndroidJavaObject currentActivity = null;
	private AndroidJavaObject iapRequestAdapter = null;

	// debug_mode
	// false = Release mode
	// true = Debug mode
	bool debug_mode = false;
	string AppId = "OA00715089";
	string[] productIds = {"0910076644"};

	private string txId = "";
	private string signData = "";

	void Awake() {

		#if UNITY_ANDROID
			iapButtonGroup.SetActive(true);
			Debug.Log("onestore iapButtonGroup display activated");
		#else
			iapButtonGroup.SetActive(false);
			Debug.Log("onestore iapButtonGroup display deactivated");
		#endif
		
	}


	// Use this for initialization
	void Start () {

		// should be same GameOjbect with ClassName. if not cannot get callback response from android native.
		// 클래스 명과 게임 오브젝트 명이 다르면 Android에서 보내는 콜백을 받을 수 없습니다.
		string className = this.GetType().Name;
		string gameObjectName = gameObject.name;
		if(className != gameObjectName) 
			Debug.LogError("클래스 명과 게임 오브젝트 명이 다릅니다. 반드시 동일하게 입력하세요.");
		

		//-----------------
		// Initialize
		//-----------------
		unityPlayerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity");

		if (currentActivity != null) {
			// RequestAdapter를 초기화
			// ---------------------------------
			// 함수 parameter 정리
			// ---------------------------------
			// (1) 콜백을 받을 클래스 이름
			// (2) Activity Context
			// (3) debug 여부
			iapRequestAdapter = new AndroidJavaObject("com.onestore.iap.unity.RequestAdapter", "CtrlOneStore", currentActivity, debug_mode);
		}

		Debug.Log ("Called com.onestore.iap.unity.RequestAdapter.!");
		
	}


	void Destroy () 
	{
		if (unityPlayerClass != null)
			unityPlayerClass.Dispose ();
		if (currentActivity != null)
			currentActivity.Dispose ();
		if (iapRequestAdapter != null)
			iapRequestAdapter.Dispose ();
	}

	//------------------------------------------------
	//
	// Exit
	//
	//------------------------------------------------
	public void Exit () 
	{
		if (iapRequestAdapter != null) 
		{
			Debug.Log ("Exit called!!!");
			iapRequestAdapter.Call ("exit");

			Debug.Log ("Called IAP exit");
		}
	}


	//------------------------------------------------
	//
	// Payment - Request
	//
	//------------------------------------------------

	public void RequestPayment()
	{
		// ---------------------------------
		// 함수 parameter 정리
		// ---------------------------------
		// (0) 메소드이름 : 구매요청
		// ---------------------------------
		// (1) appId
		// (2) productId
		// (3) proudtName
		// (4) tId
		// (5) bpInfo
		// ----------------------------------
		iapRequestAdapter.Call ("requestPayment", AppId, "0910076644", "소주 한잔", "TID_XXXXXX", "BPINFO_XXXXXX");
		Debug.Log ("Called requestPayment - 0910076644-소주 한잔");
	}

	public void VerifyReceipt() 
	{
		// ---------------------------------
		// 함수 parameter 정리
		// ---------------------------------
		// (0) 메소드이름 : 구매요청
		// ---------------------------------
		// (1) appId
		// (2) txId
		// (3) signData
		// ----------------------------------

		// string txId = "TX_00000000473186";
		// string signData = "MIIIFgYJKo.......AoMCUNyb3NzQ2VydD.==";

		if (this.txId.Length < 1) {
			Debug.LogError ("require txId");
			return;
		}

		if (this.signData.Length < 1) {
			Debug.LogError ("require eReceipt data");
			return;
		}

		iapRequestAdapter.Call ("verifyReceipt", AppId, this.txId, this.signData);
		Debug.Log ("Called verifyReceipt - " + this.txId);
	}

	//------------------------------------------------
	//
	// Payment - Callback
	//
	//------------------------------------------------

	public void PaymentResponse(string response) 
	{
		Debug.Log ("--------------------------------------------------------");
		Debug.Log ("[UNITY] PaymentResponse >>> " + response);
		Debug.Log ("--------------------------------------------------------");

		// Parsing Json string to "Reponse" class
		Response data = JsonUtility.FromJson<Response> (response);
		Debug.Log (">>> " + data.ToString());
		Debug.Log ("--------------------------------------------------------");

		// Try ReceiptVerification
		this.txId = data.result.txid;
		this.signData = data.result.receipt;
		iapRequestAdapter.Call ("verifyReceipt", AppId, data.result.txid, data.result.receipt);
	}

	public void PaymentError(string message) 
	{
		Debug.Log ("--------------------------------------------------------");
		Debug.Log ("[UNITY] PaymentError >>> " + message);
		Debug.Log ("--------------------------------------------------------");

		// Parsing Json string to "Error" class
		Error data = JsonUtility.FromJson<Error> (message);
		Debug.Log (">>> " + data.ToString());
		Debug.Log ("--------------------------------------------------------");
	}

	public void ReceiptVerificationResponse(string result) 
	{
		Debug.Log ("--------------------------------------------------------");
		Debug.Log ("[UNITY] ReceiptVerificationResponse >>> " + result);
		Debug.Log ("--------------------------------------------------------");

		// Parsing Json string to "VerifyReceipt" class
		VerifyReceipt data = JsonUtility.FromJson<VerifyReceipt> (result);
		Debug.Log (">>> " + data.ToString());
		Debug.Log ("--------------------------------------------------------");
	}

	public void ReceiptVerificationError(string message) 
	{
		Debug.Log ("--------------------------------------------------------");
		Debug.Log ("[UNITY] ReceiptVerificationError >>> " + message);
		Debug.Log ("--------------------------------------------------------");

		// Parsing Json string to "Error" class
		Error data = JsonUtility.FromJson<Error> (message);
		Debug.Log (">>> " + data.ToString());
		Debug.Log ("--------------------------------------------------------");
	}

	// ------------------------------------------------------


	//------------------------------------------------
	//
	// Command - Request
	//
	//------------------------------------------------

	public void RequestPurchaseHistory() 
	{
		// ---------------------------------
		// 함수 parameter 정리
		// ---------------------------------
		// (0) 메소드이름 : 구매내역 조회
		// ---------------------------------
		// (1) 필요시에는 UI 노출
		// (2) appId
		// (3) productIds
		// ----------------------------------
		iapRequestAdapter.Call ("requestPurchaseHistory", false, AppId, productIds);
		//iapRequestAdapter.Call ("requestPurchaseHistory", true, AppId, productIds); // UI노출 없이 Background로만 진행

		Debug.Log ("Called requestPurchaseHistory - OA00715089 ");
	}

	public void RequestProductInfo() 
	{
		// ---------------------------------
		// 함수 parameter 정리
		// ---------------------------------
		// (0) 메소드이름 : 상품정보 조회
		// ---------------------------------
		// (1) 필요시에는 UI 노출
		// (2) appId
		// ----------------------------------
		iapRequestAdapter.Call ("requestProductInfo", false, AppId);
		//iapRequestAdapter.Call ("requestProductInfo", true, AppId); // UI노출 없이 Background로만 진행

		Debug.Log ("Called requestProductInfo - OA00715089 ");
	}

	public void RequestCheckPurchasability() 
	{
		// ---------------------------------
		// 함수 parameter 정리
		// ---------------------------------
		// (0) 메소드이름 : 구매가능여부 조회
		// ---------------------------------
		// (1) 필요시에는 UI 노출
		// (2) appId
		// (3) productIds
		// ----------------------------------
		iapRequestAdapter.Call ("requestCheckPurchasability", debug_mode, AppId, productIds);
		//iapRequestAdapter.Call ("requestCheckPurchasability", true, AppId, productIds); // UI노출 없이 Background로만 진행

		Debug.Log ("Called requestCheckPurchasability - 0910076644/ OA00715089 ");
	}

	public void RequestSubtractPoints() 
	{
		// ---------------------------------
		// 함수 parameter 정리
		// ---------------------------------
		// (0) 메소드이름 : 상품 속성변경 요청 
		// ---------------------------------
		// (1) 필요시에는 UI 노출
		// (2) action(아이템차감)
		// (3) appId
		// (4) productIds
		// ----------------------------------
		iapRequestAdapter.Call ("requestChangeProductProperties", debug_mode, "subtract_points", AppId, productIds);
		//iapRequestAdapter.Call ("requestChangeProductProperties", true, "subtract_points", AppId, productIds); // UI노출 없이 Background로만 진행

		Debug.Log ("Called request subtract_points - 0910076644/OA00715089 ");
	}

	public void RequestCancelSubscription() 
	{
		// ---------------------------------
		// 함수 parameter 정리
		// ---------------------------------
		// (0) 메소드이름 : 상품 속성변경 요청 
		// ---------------------------------
		// (1) 필요시에는 UI 노출
		// (2) action(자동결제 취소)
		// (3) appId
		// (4) productIds
		// ----------------------------------
		iapRequestAdapter.Call ("requestChangeProductProperties", debug_mode, "cancel_subscription", AppId, productIds);
		//iapRequestAdapter.Call ("requestChangeProductProperties", true, "cancel_subscription", AppId, productIds); // UI노출 없이 Background로만 진행

		Debug.Log ("Called request cancel_subscription- 0910076644/OA00715089 ");
	}


	//------------------------------------------------
	//
	// Command - Callback
	//
	//------------------------------------------------

	public void CommandResponse(string response) 
	{
		Debug.Log ("--------------------------------------------------------");
		Debug.Log ("[UNITY] CommandResponse >>> " + response);
		Debug.Log ("--------------------------------------------------------");

		// Parsing Json string to "Reponse" class
		Response data = JsonUtility.FromJson<Response> (response);
		Debug.Log (">>> " + data.ToString());
		Debug.Log ("--------------------------------------------------------");
	}

	public void CommandError(string message) 
	{
		Debug.Log ("--------------------------------------------------------");
		Debug.Log ("[UNITY] CommandError >>> " + message);
		Debug.Log ("--------------------------------------------------------");

		// Parsing Json string to "Error" class
		Error data = JsonUtility.FromJson<Error> (message);
		Debug.Log (">>> " + data.ToString());
		Debug.Log ("--------------------------------------------------------");
	}


	//#endif


}
