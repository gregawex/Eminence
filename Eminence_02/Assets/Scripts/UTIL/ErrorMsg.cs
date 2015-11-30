using UnityEngine;
using System.Collections;


//ERROR codes that begin with 'INTERNAL' will not trigger in-game UI display.
//INTERNAL codes are only for developers. See GregBugger or Debug.Log for the output.
public enum ErrorCode 
{ 
	SERVER_FAILURE = 0,
	UNKNOWN_ERROR = 1,
	SERVER_RETURNED_NULLJSON = 2,
	ASSET_BUNDLE_DOWNLOAD = 3,
	LOCAL_BUNDLE_ERROR = 4,
	LOGIN_FAILED = 5,
	INTERNAL_UIPREFS_MISSING = 6,
	INTERNAL_ERROR = 7
}

 public class ErrorMsg 
{
	static bool LOGGING = true;
	static bool LOG_TO_DEBUG = true;
	static bool LOG_TO_GREGBUGGER = false;

	static string exception;


	private string[,] Msg = new string[,]
	{
		/*0*/			{"Server failure", "Server Failure"},
		/*1*/			{"Unknown error", "Unknown Error"},
		/*2*/			{"Server returned nulljson", "Server returned nulljson"},
		/*3*/			{"Download Error", "An error occured while downloading asset bundles"},
		/*4*/			{"Bundle Load Error", "The bundle couldn't be loaded from the local source"},
		/*5*/			{"Login Failed", "The login details you provided are invalid"},
		/*6*/			{"UIPrefs Missing", "A UIPRefs component is missing from the UIRoot GameObject."},
		/*7*/			{"An Internal Error Occured", ""} 
	};

	public readonly string title;
	public readonly string body;
	public readonly string errorCode;

	ErrorMsg()
	{
		//noooo go away
	}

	 ErrorMsg(ErrorCode ec)
	{
		this.title = Msg [(int)ec, 0];
		this.body = Msg [(int)ec, 1];
		this.errorCode = ec.ToString () + " (" + (int)ec + ")";

		if (exception != null)
						errorCode += " ["+exception+"]";

		if(LOG_TO_GREGBUGGER ) 	GregBugger.LogError (errorCode);
		if(LOG_TO_DEBUG ) 		Debug.LogError (errorCode);
	}

	public static ErrorMsg LogError(ErrorCode ec)
	{

		if (LOGGING) {

			//Create an error instance
			ErrorMsg msg = new ErrorMsg(ec);

			//Do not show internal errors to users. FCK IT
			/*if(!ec.ToString().StartsWith("INTERNAL"))
			{
				GameObject instance = GameObject.Instantiate(Resources.Load("ErrorPanel", typeof(GameObject))) as GameObject;
				//instance.transform.parent = BaseSceneManager.Instance.uiPrefs.centerAnchor.transform;
				instance.transform.localPosition = Vector3.zero;
				instance.transform.localScale = Vector3.one;
				instance.SetActive(true);
			}*/

			return msg;
		}

		return null;
	}

	public static ErrorMsg LogError(ErrorCode ec, string exc)
	{
		exception = exc;

		return LogError (ec);
	}
}
