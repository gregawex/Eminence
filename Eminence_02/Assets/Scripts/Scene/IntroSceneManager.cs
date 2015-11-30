using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;


public class IntroSceneManager : BaseSceneManager {


	public GameObject bg;
	public Material fadeMat;
	public GameObject fadeObj;

	public Texture2D[] textures;

	Dictionary<string, Texture2D> textDict = new Dictionary<string, Texture2D> ();

	static IntroSceneManager instance;
	public static IntroSceneManager Instance { get { return instance; } }

	public AnimationCurve fadeCurve;
	public float speedMul = 1;

	float startTime;
	bool isFading;

	// Use this for initialization
	protected override void Awake () {
	

		instance = this;


				GregBugger.on = false;

		fadeMat.color = new Color (0, 0, 0, 0);
		fadeObj.SetActive (false);




		foreach (Texture2D t in textures) {

			GregBugger.Log ("background added: "+t.name);
			textDict.Add(t.name, t);
		}

		PersistentInfo.ProcessExecAssembly ();

		base.Awake ();


	}



	public void SetBG(string name)
	{
		if (textDict.ContainsKey (name)) {
						bg.GetComponent<Renderer>().material.mainTexture = textDict [name];
		} else {
			Debug.LogError("The background you are trying to set doesn't exist. ["+name+"]");
		}
	}

		void Update()
		{
				//HANDLE HOTKEYS

				if (Input.GetKeyDown (KeyCode.F10)) {
						if (GregBugger.on)
								GregBugger.on = false;
						else
								GregBugger.on = true;
				}


		if(isFading)
		   {

				float evalValue = (Time.time - startTime) * speedMul;
		
				float eval = fadeCurve.Evaluate (evalValue);

				fadeMat.color = new Color(0, 0, 0, eval);

				if(eval >= 1)
				{
					callback();
				isFading = false;
				}
			}

		}

	Action callback;
	public void FadeOut(Action a)
	{
		callback = a;
		isFading = true;
		startTime = Time.time;

		fadeObj.SetActive (true);
	}


	


	//------------------------------------------------fck this
	/*void Reset()
	{

		StartCoroutine ("Delay");
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ();
	}
	
	IEnumerator Delay()
	{
		yield return new WaitForSeconds(1.5f);
		uiPrefs.OpenAndRun ("DownloadBundlePanel", null, DownloadSuccess, null);
	}
	
	IEnumerator DelayLogin()
	{
		yield return new WaitForSeconds (1);
		
		//loginPanel.gameObject.SetActive (true);
		uiPrefs.OpenAndRun ("LoginPanel", null, LoginPressed, null);
	}
	
	void LoginPressed()
	{
		//loginPanel.gameObject.SetActive (false);
		FAKE_SERVER.Instance.FAKERequest (new RequestPack (Request.SEND_LOGIN, "this_is_a_json_with_login_details", LoginResult));
	}
	
	void LoginResult(string json)
	{
		Dictionary<string, object> dict = MiniJSON.Json.Deserialize (json) as Dictionary<string, object>;
		
		Dictionary<string,object> dict2 = dict ["response"] as Dictionary<string, object>;
		
		if (dict2 ["login_success"].ToString().ToLower() == "true") {
			
			Application.LoadLevel("card");
			
		} 
		else {
			ErrorMsg msg = ErrorMsg.LogError(ErrorCode.LOGIN_FAILED);

			ErrorPanel errorPanel = uiPrefs.GetUIController("ErrorPanel") as ErrorPanel;

			if(errorPanel != null)
			{
				errorPanel.title.text = msg.title;
				errorPanel.body.text = msg.body;

	

			}
			
			
		}
	}
	
	
	void DownloadSuccess()
	{
		StartCoroutine ("DelayLogin");
		uiPrefs.CloseUIController ("DownloadBundlePanel");
	}*/
}
