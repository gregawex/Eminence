using UnityEngine;
using System.Collections;
using System;

public class Guard_Login : Guard //<Guard_Login> 
{
	public Guard_Login()
	{}


	public Guard_Login (RootControl root) : base(root)
	{

	}

	public override void OnPanelMessage (object ev, params object[] obj)
	{
		string s = ev.ToString ();
		switch (s) {

		case "create_account":
			//Ctrl.CloseAndShow("Panel_AccountCreation");
			break;
		case "login":
			RequestPack req = new RequestPack (Request.SEND_LOGIN, "whatever", delegate(string ob) {
			Eve.TriggerEvent(new E_ServerResponse(ob)); });
			FAKE_SERVER.Instance.FAKERequest (req);

			//Ctrl.Close();
			int a=0;
			break;
		}

	}


	public void Open()
	{

	}

	public void Out()
	{

	}


}
