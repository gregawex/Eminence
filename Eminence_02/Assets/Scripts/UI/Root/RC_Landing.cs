using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RC_Landing : RootControl {

	Guard_Login guardLogin;
	Guard_Error guardError;
	Guard_DownloadBundle guardBundle;
	Guard_Main guardMain;
	Guard_PressX guardPressX;
	Guard_AccountCreation guardAccountCreation;
	
	

	enum IntState { DOWNLOAD, LOGIN, EXITING }
	IntState state;

	protected override void Awake()
	{
		base.Awake ();

		guardLogin = base.AddController<Guard_Login> ();
		guardError = base.AddController<Guard_Error> ();
		guardBundle = base.AddController<Guard_DownloadBundle> ();
		guardMain = base.AddController<Guard_Main> ();
		guardPressX = base.AddController<Guard_PressX> ();
		guardAccountCreation = base.AddController<Guard_AccountCreation> ();


		//KManager.Instance.Init (AppDomain.CurrentDomain);

	}
	// Use this for initialization
	protected override void Start () {
	
		Show (guardPressX.Ctrl);


		//KManager.Instance.Execute ();
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}

	protected override void Callback_TaskFinished<T> (T ev)
	{
		base.Callback_TaskFinished (ev);

		switch (state) {

		case IntState.DOWNLOAD:
			state = IntState.LOGIN;
			Delay (2, delegate() {Show (guardLogin.Ctrl);});

			break;
		case IntState.LOGIN:
			state = IntState.EXITING;
			Delay (2, delegate() {Application.LoadLevel(1);});
			break;
		}
	}

	protected override void Callback_ServerResponse<T> (T ev)
	{
		base.Callback_ServerResponse (ev);

		switch (state) {

		case IntState.LOGIN:


			E_ServerResponse e = ev as E_ServerResponse;
			Dictionary<string, object> dict = MiniJSON.Json.Deserialize (e.json) as Dictionary<string, object>;
			
			Dictionary<string,object> dict2 = dict ["response"] as Dictionary<string, object>;
			
			if (dict2 ["login_success"].ToString().ToLower() == "true") {
				state = IntState.EXITING;
				Application.LoadLevel("card");
				Delay (2, delegate() {Application.LoadLevel(1);});
				
			} 
			else {
				CloseAll();
				ErrorMsg msg = ErrorMsg.LogError(ErrorCode.LOGIN_FAILED);
				Panel_Error pe = (guardError.Ctrl as Panel_Error);
				//pe.title.text = msg.title;
				pe.body.text = msg.body;
				pe.posCallback = OpenDownload;
				Show (pe);
				
				
			}



			break;
		}
	}
	

	protected override void Callback_ButtonPress<T> (T ev)
	{
		base.Callback_ButtonPress (ev);

		switch (state) {
		case IntState.LOGIN:
			E_ButtonPress e = ev as E_ButtonPress;
			/*if(e.button.reference.ToLower() == "positive") 
			{
				CloseAll();
				//bundlePanel.
			}*/
			
			break;
		}
	}

	void OpenDownload()
	{
		CloseAll ();

		Show (guardLogin.Ctrl);


	}





}
