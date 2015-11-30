using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
/*
 * 
 * 
 * 
 * 
Temporary FTP Hostname: 93.188.160.52
Full FTP Hostname: ftp.awxhost.bugs3.com
FTP Username: u478576287
FTP Password: ••••••••••mishmash80
*/


//cpanel.serversfree.com

public enum Request { SEND_LOGIN }

public class FAKE_SERVER : MonoSingleton<FAKE_SERVER> {
 
	bool hasFinished;

	public void FAKERequest(RequestPack req)
	{
		hasFinished = false;

		StartCoroutine ("Delay", req);
	}

	IEnumerator Delay(object req)
	{int a = 0;
		RequestPack pack = req as RequestPack;
		yield return new WaitForSeconds (UnityEngine.Random.Range (1, 4));

		switch (pack.request) {
		case Request.SEND_LOGIN:

			bool v = UnityEngine.Random.Range(0, 10) > 5 ? true : false;

			Dictionary<string, object> dict = new Dictionary<string, object>();
			Dictionary<string, object> dict2 = new Dictionary<string, object>();
			dict.Add("response", dict2);
			dict2.Add("login_success", v.ToString());
			Debug.Log (v.ToString());

			string responseJSON  = MiniJSON.Json.Serialize(dict);
			pack.callback(responseJSON);
			break;
		}
	}

}
