using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLog : MonoBehaviour {

	public UITextList textList;

	void OnEnable () {
		Application.logMessageReceived += HandleLog;
	}

	void OnDisable () {
		Application.logMessageReceived -= HandleLog;
	}

	void HandleLog(string logString, string stackTrace, LogType type){

		System.DateTime dt = System.DateTime.Now;

		string newString = "[" + dt.ToString("H:mm:ss.fff") + "][" + type + "] " + logString;

		if (textList != null)
		{
			textList.Add(newString);
		}
	}


}
