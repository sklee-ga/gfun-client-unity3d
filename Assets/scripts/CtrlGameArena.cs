using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using UnityEngine.Networking;

using GFSDK;


public class CtrlGameArena : MonoBehaviour {

	public UILabel lblRegId;
	public UILabel lblAccessId;

	private int gf365_gameId = 4;
	private string gf365_regId = "";


	void Start()
	{
		setRegId ("G01400442963240");

		// set zone
		GFEnvironment.setZone(GFZoneType.WORKS);
		Debug.Log ("GF365 launcher version : " + GFLauncher.getVersion ());

		setRegId ("G01400442963240");
	}

	// this function will be called by GAplugin 
	public void LaunchFromUrl(string str)
	{
		Debug.Log ("GAPlug: " + str);
	}

	// this function will be called by GAplugin 
	public void GF365UserMatch(string gameRegId)
	{
		Debug.Log ("GAPlug gameRegId: " + gameRegId);
		setRegId(gameRegId);
	}


	// ------------------
	public void GF365getAccessId()
	{
		if (this.gf365_regId.Length < 1) {
			Debug.Log ("require gameRegId");
			return;
		}
		
		string url = "http://gfuntest.gamearena.co.kr:8690/works/login/v2/" + this.gf365_regId;
		StartCoroutine(GF365RequestbyHttp(url, ""));
	}


	public void GF365Join()
	{
		if (this.gf365_regId.Length < 1) {
			Debug.Log ("require gameRegId");
			return;
		}

		string url = "http://gfuntest.gamearena.co.kr:8690/works/join/v2/" + this.gf365_regId;

		StartCoroutine(GF365RequestbyHttp(url, ""));
	}


	public void GF365SendScore()
	{
		if (this.gf365_regId.Length < 1) {
			Debug.Log ("require gameRegId");
			return;
		}

		string url = "http://gfuntest.gamearena.co.kr:8690/works/score/v2/" + this.gf365_regId;
		string jsonString = "{\"ServerId\":100, \"ScoreId\": \"K100\"}";

		StartCoroutine(GF365RequestbyHttp(url, jsonString));

		Debug.Log ("sent Score");
	}

	public void getGF365regId()
	{
		if (this.gf365_regId.Length > 0) {
			Debug.Log ("already received gameRegId: "+ this.gf365_regId);
			return;
		}

		GFLauncher.getGameRegId(GFStoreType.GooglePlay, gf365_gameId, OnGameRegId);
		Debug.Log ("GF365 called getGameRegId");
	}

	public void OnGameRegId(GFResult result, string gameRegId)
	{
		setRegId(gameRegId);
		Debug.Log ("GF365 - result code :" + result.getResponse ());
		Debug.Log ("GF365 - result msg :" + result.getMessage ());
		Debug.Log ("GF365 - gameRegId :" + gameRegId);
	}

	private void setRegId(string gameRegId)
	{
		this.gf365_regId = gameRegId;
		lblRegId.text = this.gf365_regId;

		Debug.Log ("setRegId: " + gameRegId);
	}


	IEnumerator GF365RequestbyHttp(string url, string postData)
	{
		UnityWebRequest www = UnityWebRequest.Post(url, "");
		www.SetRequestHeader("Content-Type", "application/json");

		if (postData != "") {
			
			byte[] bodyRaw = Encoding.UTF8.GetBytes(postData);
			www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
			www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		}

		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {

			if (www.responseCode == 200) {
				Debug.Log ("responded GF365: " + www.downloadHandler.text);			
			} else {			
				Debug.Log ("www.response error: " + www.responseCode);
			}
		}


	}



}
