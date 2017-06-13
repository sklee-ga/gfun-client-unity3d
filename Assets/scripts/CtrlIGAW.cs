using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IgaworksUnityAOS;


public class CtrlIGAW : MonoBehaviour {

	private string myDeviceId ;

	void Awake() {
		//유니티 엔진이 초기화될 때, IGAW 플러그인을 초기화 합니다.

		#if UNITY_ANDROID
		IgaworksUnityPluginAOS.InitPlugin ();

		Debug.Log ("Call IgaworksUnityPluginAOS.InitPlugin");
		#endif


		#if UNITY_IOS
		// 유니티 엔진이 초기화 될 때, 플러그인도 초기화
		IgaworksCorePluginIOS.IgaworksCoreWithAppKey("745171783", "12a064b41a3648cf");
		Debug.Log ("Call IgaworksCorePluginIOS.IgaworksCoreWithAppKey(...)");

		// 플러그인에서 노출하는 로그의 수준을 설정 (all log trace)	
		IgaworksCorePluginIOS.SetLogLevel(IgaworksCorePluginIOS.IgaworksCoreLogTrace);
		#endif
	}


	// Use this for initialization
	void Start () {

		// 유저 고유식별값
		myDeviceId = SystemInfo.deviceUniqueIdentifier;


		#if UNITY_ANDROID
		// 유저식별값입력
		IgaworksUnityPluginAOS.Common.setUserId(myDeviceId);

		// 라이브옵스 초기화
		IgaworksUnityPluginAOS.LiveOps.initialize ();

		// 공지팝업 데이터로드 (Android)
		IgaworksUnityPluginAOS.LiveOps.requestPopupResource();
		#endif


		#if UNITY_IOS
		// 유저식별값입력
		IgaworksCorePluginIOS.SetUserId(myDeviceId);

		// 라이브옵스를 초기화합니다. (iOS Push)
		LiveOpsPluginIOS.LiveOpsInitPush();

		// 공지팝업 데이터로드 (iOS)
		LiveOpsPluginIOS.LiveOpsPopupGetPopups();


		//set Delegate Listener for Igaworks plugin
		IgaworksCorePluginIOS.SetCallbackHandler("BasicGameObject"); 

		// 델리게이트 등록
		LiveOpsPluginIOS.liveOpsPopupGetPopupsResponded += HandleLiveOpsPopupGetPopupsResponded;

		#endif

		Debug.Log ("myDeviceId: " + myDeviceId);
	}

	public void ShowPopoNotice() {

		//공지팝업을 노출합니다.
		#if UNITY_ANDROID
		IgaworksUnityPluginAOS.LiveOps.showPopUp("popup-first-default");
		Debug.Log ("Call IgaworksUnityPluginAOS.LiveOps.showPopUp ");
		#endif

		#if UNITY_IOS
		LiveOpsPluginIOS.LiveOpsPopupShowPopups("popup-first-default");
		Debug.Log ("Call LiveOpsPluginIOS.LiveOpsPopupShowPopups ");
		#endif

		Debug.Log ("done ShowPopupNotice");

	}

	// 델리게이트 구현
	public void HandleLiveOpsPopupGetPopupsResponded()
	{
		LiveOpsPluginIOS.LiveOpsPopupShowPopups("popup-first-default");
		Debug.Log ("delegate LiveOpsPluginIOS.LiveOpsPopupShowPopups");
	}

	void OnApplicationPause(bool pauseStatus){

		if (pauseStatus) {
			IgaworksUnityPluginAOS.Common.endSession();
		} else {
			IgaworksUnityPluginAOS.Common.startSession();
			// 라이브옵스 활성화
			IgaworksUnityPluginAOS.LiveOps.resume();
		}
	}

	// push message on/off
	public void pushonoff() {

		UIToggle current = UIToggle.current;

		//true:수신모드, false:수신거부모드
		#if UNITY_ANDROID
		IgaworksUnityPluginAOS.LiveOps.enableService (current.value);
		#endif

		#if UNITY_IOS
		LiveOpsPluginIOS.LiveOpsSetRemotePushEnable(current.value);
		#endif

		Debug.Log ("Push enableService status: " + current.value);
	}

	// client push message
	public void SendClientPush()
	{

		#if UNITY_ANDROID

		/*
		IgaworksUnityPluginAOS.LiveOps.setNormalClientPushEvent(
		3,                 	// Delay seconds. message will be reach after this second.
		"Let’s play now!",      // message
		100010,			// Event ID, for cancle transaction. (취소할 때 쓰기 위한 값.)
		true			// display or not when app running 
		);
		*/


		IgaworksUnityPluginAOS.LiveOps.setBigTextClientPushEvent(
			2, 				// Delay seconds, 몇 초 뒤에 보낼지 설정
			"contentText", 			// contents
			"bigContentTitle", 		// 일반 푸시 내용
			"Let's play now! WoooooooW!!",	// 푸시에 표시될 빅 텍스트
			"summaryText", 			// 푸시에 표시될 요약 텍스트
			1, 				// Event ID, 취소할 때 쓰기 위한 것
			false				// 앱이 실행 중일 때에도 보이게 할 것인지 설정
		);

		#endif


		Debug.Log ("Sent push message");
	}


	// adbrix 
	public void SendAdbrixData() {

		#if UNITY_ANDROID
		SendAdbrixDataAndroid();
		Debug.Log ("Done Send AdbrixData from Android");
		#endif


		#if UNITY_IOS
		SendAdbrixDataiOS();
		Debug.Log ("Done Send AdbrixData from iOS");
		#endif
	}



	private void SendAdbrixDataiOS(){

		IgaworksCorePluginIOS.SetAge(26);
		Debug.Log ("Sent setAge(26) data");

		IgaworksCorePluginIOS.SetGender(IgaworksCorePluginIOS.IgaworksCoreGenderFemale);
		Debug.Log ("Sent setGender data");

		//AdBrixPluginIOS.FirstTimeExperience(string userActivity);
		AdBrixPluginIOS.FirstTimeExperience("LoadMainLogo");
		AdBrixPluginIOS.FirstTimeExperience("ContentLoading");
		AdBrixPluginIOS.FirstTimeExperience("JoinGF365");
		AdBrixPluginIOS.FirstTimeExperience("TutorialComplete");

		Debug.Log ("Sent firstTimeExperience data");


		string currency = AdBrixPluginIOS.AdBrixCurrencyName(AdBrixPluginIOS.AdBrixCurrencyKRW);
		AdBrixPluginIOS.AdBrixPurchase("oid_1","pid_1","gold_package", 1100.00, 1,currency, "cat1.cat2.cat3.cat4.cat5");

		Debug.Log ("Sent AdBrixPurchase AdbrixData");

		//AdBrixPluginIOS.Retention(string inAppActivity);
		AdBrixPluginIOS.Retention("openStore");
		AdBrixPluginIOS.Retention("stageClear");
		AdBrixPluginIOS.Retention("JoinGF365");
		AdBrixPluginIOS.Retention("purchaseItemWithVirtualCurrency");
		AdBrixPluginIOS.Retention("inviteFriends");

		Debug.Log ("Sent Retention data");
	}


	private void SendAdbrixDataAndroid() {

		// age
		//IgaworksUnityPluginAOS.Adbrix.setAge(int age);
		IgaworksUnityPluginAOS.Adbrix.setAge(26);

		Debug.Log ("Sent setAge(26) data");

		// gender
		//IgaworksUnityPluginAOS.Adbrix.setGender(IgaworksUnityPluginAOS.Gender gender);
		IgaworksUnityPluginAOS.Adbrix.setGender(IgaworksUnityPluginAOS.Gender.MALE);

		Debug.Log ("Sent setGender data");

		// user pattern
		//IgaworksUnityPluginAOS.Adbrix.firstTimeExperience(String userActivity);
		IgaworksUnityPluginAOS.Adbrix.firstTimeExperience("LoadMainLogo");
		IgaworksUnityPluginAOS.Adbrix.firstTimeExperience("ContentLoading");
		IgaworksUnityPluginAOS.Adbrix.firstTimeExperience("JoinGF365");
		IgaworksUnityPluginAOS.Adbrix.firstTimeExperience("TutorialComplete");

		Debug.Log ("Sent firstTimeExperience data");

		// in app purchasing
		// 애드브릭스에 노출되는 매출은 price x quantity 로 계산됩니다.
		/*
		 *  + orderID : 주문 아이디 (주의! 동일한 orderID가 전송될 경우 중복제거되어 분석됩니다. orderID가 중복되지 않도록 주의 해주세요.)
			+ productID : 상품 아이디
			+ productName* : 상품명 (Buy api를 사용했던 앱을 업데이트 하는 경우, productName에 기존 사용하셨던 buy api의 purchaseItemName을 입력합니다.)
			+ price* : 상품단가
			+ quantity* : 구매 수량
			+ currency : 구매 통화 단위 (IgawCommerce.Currency 사용)
			+ category : 최대 5단계의 상품 카테고리, 각 단계는 마침표(.)으로 구분
			   "*"표시된 항목은 필수 입력값입니다. 나머지 항목은 필요에 따라 연동하시면 됩니다.
			    purchase API를 통해서 앱의 매출 지표가 집계되기 때문에 구글결제 등의 실제 구매에 대해서만 사용하는 것을 권장합니다.
	     */
		//IgaworksUnityPluginAOS.Adbrix.purchase(String orderID, String productID, String productName, double price, int quantity
		// , Currency currency, String category);
		IgaworksUnityPluginAOS.Adbrix.purchase("oid_1","pid_1","gold_package", 1100.00, 1
			,IgaworksUnityPluginAOS.Adbrix.Currency.KR_KRW, "cat1.cat2.cat3.cat4.cat5");

		Debug.Log ("Sent purchase data");

		// In App Activity
		// 유저활동 분석(In App Activity)는 위 2가지 유저행동 외의 모든 패턴을 추적하는데 사용합니다.
		//IgaworksUnityPluginAOS.Adbrix.retention(String inAppActivity);

		IgaworksUnityPluginAOS.Adbrix.retention("openStore");
		IgaworksUnityPluginAOS.Adbrix.retention("stageClear");
		IgaworksUnityPluginAOS.Adbrix.retention("JoinGF365");
		IgaworksUnityPluginAOS.Adbrix.retention("purchaseItemWithVirtualCurrency");
		IgaworksUnityPluginAOS.Adbrix.retention("inviteFriends");

		// Sub Activity 설정
		// 각각의 유저 분석 액티비티는 하위 액티비티 설정이 가능합니다.
		//IgaworksUnityPluginAOS.Adbrix.retention(String inAppActivity,String subActivity);
		IgaworksUnityPluginAOS.Adbrix.retention("stageClear","Tutorial");


		Debug.Log ("Sent retention data");
	}





}
