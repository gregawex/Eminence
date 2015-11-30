using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//=================================================================================================
/*
██████╗  ██████╗  ██████╗ ████████╗    ██╗ ██╗  
██╔══██╗██╔═══██╗██╔═══██╗╚══██╔══╝    ╚██╗╚██╗ 
██████╔╝██║   ██║██║   ██║   ██║        ╚██╗╚██╗
██╔══██╗██║   ██║██║   ██║   ██║        ██╔╝██╔╝
██║  ██║╚██████╔╝╚██████╔╝   ██║       ██╔╝██╔╝ 
╚═╝  ╚═╝ ╚═════╝  ╚═════╝    ╚═╝       ╚═╝ ╚═╝
*/
//=================================================================================================

public class RootControl : MonoBehaviour, EventListener {

		protected Transform uiCamera;

	Dictionary<string, BasePanel> controllers = new Dictionary<string, BasePanel>();

	List<Guard> guards = new List<Guard>();

	static RootControl instance;

	public static RootControl Instance { get { return instance; } }

	protected virtual void Awake()
	{
		instance = this;
			 
		uiCamera = transform.FindChild ("Camera");

		//Default callback list hat every RootControl utomatically listens to.
		//If you want to listen to some specific stuff assign some more events in the child Awake.
		Eve.Listen<E_ErrorOccured> 	(new Action<BaseEvent>(Callback_Error), this);
		Eve.Listen<E_TaskFinished> 	(new Action<BaseEvent>(Callback_TaskFinished), this);
		Eve.Listen<E_PanelClose> 	(new Action<BaseEvent>  (Callback_PanelClose), this);
		Eve.Listen<E_ButtonPress> 	(new Action<BaseEvent> (Callback_ButtonPress), this);
		Eve.Listen<E_ServerResponse> (new Action<BaseEvent> (Callback_ServerResponse), this);
	}
	// Use this for initialization
	protected virtual void Start () {


	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

	protected virtual void OnDestroy()
	{
		foreach (Guard guard in guards) {
			guard.Destroy();
		}
	}

	//================================================================
	//DEFAULT callbacks
	protected virtual void Callback_Error<T>(T ev) where T : BaseEvent
	{
		E_ErrorOccured e = (ev as E_ErrorOccured);
		GregBugger.LogHighlight ("Event Error");
		Panel_Error ep = controllers ["ErrorPanel"] as Panel_Error;
		//ep.title.text = e.msg.title;
		ep.body.text = e.msg.body;
		ep.posCallback = e.positiveCallback;
		ep.negCallback = e.negativeFeedback;

		if (e.negativeFeedback != null) {
						ep.oneButtonPanel.SetActive (false);
						ep.twoButtonPanel.SetActive (true);
				}
		else {
			ep.oneButtonPanel.SetActive(true);
			ep.twoButtonPanel.SetActive(false);
		}

		ep.gameObject.SetActive (true);
	}
	
	protected virtual void Callback_TaskFinished<T>(T ev) where T : BaseEvent
	{
		GregBugger.LogHighlight ("Event TaskFinished");
	}

	protected virtual void Callback_PanelClose<T>(T ev) where T :BaseEvent
	{
		GregBugger.LogHighlight ("Event PanelClose");
	}

	protected virtual void Callback_ButtonPress<T>(T ev) where T :BaseEvent
	{
		GregBugger.LogHighlight ("Event ButtonPress");
	}

	protected virtual void Callback_ServerResponse<T>(T ev) where T :BaseEvent
	{
		GregBugger.LogHighlight ("Event ServerResponse");
	}
	//================================================================
	//CONTROLLER management
	protected T AddController<T>() where T : Guard//<T>
	{

		//Guard<T> guard = new Guard<T> ();
		int a = 0;

		Guard guard = Activator.CreateInstance (typeof(T), this) as T;

		guard.Init (this);
		controllers.Add (guard.Ctrl.name, guard.Ctrl);
		
		guard.Ctrl.transform.parent = uiCamera;
		guard.Ctrl.transform.localPosition = Vector3.zero;
		//root.transform.localScale = Vector3.one;

				GregBugger.Log ("Setting parent of " + guard.Ctrl.ToString () + " to " + uiCamera);
		
		guard.Ctrl.gameObject.SetActive(false);

		guards.Add (guard);

		return guard as T; 
	}

	public void Show(string controllerName, Action renderCallback = null)
	{
		string s = controllerName + "(Clone)";

		if (controllers.ContainsKey (s))
						Show (controllers [s], renderCallback);

				else {
						Debug.LogError ("can't Show controller [" + s + "]");

			foreach(KeyValuePair<string, BasePanel> kvp in controllers)
			{
				Debug.Log (" valid name: "+kvp.Key);
				           }
				}

	}

	public void ShowOnly(string controllerName)
	{
		if (controllers.ContainsKey (controllerName))
			ShowOnly (controllers [controllerName]);
		else
			Debug.LogError ("can't ShowOnly controller [" + controllerName + "]");
	}

	public void Show(BasePanel ctrl, Action renderCallback = null)
	{
		ctrl.gameObject.SetActive (true);
		//ctrl.SetRenderCallback (renderCallback);
	}

	public void ShowOnly(BasePanel ctrl)
	{
		CloseAll ();
		Show (ctrl);
	}

	public void CloseAll()
	{
		foreach (KeyValuePair<string, BasePanel> kvp in controllers) 
		{
			//kvp.Value.Close ();
		}
	}


	float delayTime = 0;
	Action delayCallback;
	protected void Delay(int i, Action callback)
	{
		delayTime = i;
		delayCallback = callback;
		StartCoroutine ("DoDelay");
	}

	IEnumerator DoDelay()
	{
		yield return new WaitForSeconds(delayTime);
		delayCallback ();
	}
}
