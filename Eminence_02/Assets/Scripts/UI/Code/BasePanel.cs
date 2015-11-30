using UnityEngine;
using System.Collections;
using System;

/*
██╗██╗██╗		PLEASE DO NOT USE SetActive(false) ON THIS GameObject EITHER EXTERALLY OR INTERNALLY IF YOU WANT TO TURN IT OFF, 
██║██║██║					USE THE >>Close()<< METHOD INSTEAD. THIS ALLOWS THE CTRL TO PROPERLY SHUT DOWN.
██║██║██║		
╚═╝╚═╝╚═╝
██╗██╗██╗
╚═╝╚═╝╚═╝
*/


public class BasePanel : MonoBehaviour {

	_State State { get; set; }

	protected Guard guard;
	public Guard Guard { set { if(guard == null) guard = value; } get { return guard; } }






	protected virtual void Awake() 
	{

		UISceneManager.Instance.SubscribePanel(this);

	}
	// Use this for initialization
	protected virtual void Start () {



	}


	
	// Update is called once per frame
	protected virtual void Update () {
	



	}

	public virtual void FinalizeProcess()
	{

	}

	public virtual void Trans_Channel0()
	{
		TransRequest("Trans_Channel0");

	}

	public virtual void Trans_Channel1()
	{
		TransRequest("Trans_Channel1");

	}

	public virtual void Trans_Channel2()
	{
		TransRequest("Trans_Channel2");
	}
	public virtual void Trans_Channel3()
	{
		TransRequest("Trans_Channel3");
	}
	
	public virtual void Trans_Channel4()
	{
		TransRequest("Trans_Channel4");
	}
	public virtual void Trans_Channel5()
	{
		TransRequest("Trans_Channel5");
	}

	public virtual void Trans_Tab0()
	{
		TransRequest("Trans_Tab0");
	}
	public virtual void Trans_Tab1()
	{
		TransRequest("Trans_Tab1");
	}
	public virtual void Trans_Tab2()
	{
		TransRequest("Trans_Tab2");
	}
	public virtual void Trans_Tab3()
	{
		TransRequest("Trans_Tab3");
	}
	public virtual void Trans_Tab4()
	{
		TransRequest("Trans_Tab4");
	}
	public virtual void Trans_Tab5()
	{
		TransRequest("Trans_Tab5");
	}

	public virtual void Trans_Positive()
	{
		TransRequest("Trans_Positive");
	}
	public virtual void Trans_Negative()
	{
		TransRequest("Trans_Negative");
	}

	protected virtual void TransRequest(string eventName)
	{
		UISceneManager.Instance.TriggerTransitionChannel(eventName);
		//gameObject.SetActive(false);
	}

	public void Send(object obj, params object [] objs)
	{
		GregBugger.Log ("Sending internal msg: " + obj.ToString ());
		if(guard != null)
			guard.OnPanelMessage (obj, objs);
	}

	

	void OnDestroy()
	{
		if(guard != null)
		guard.Destroy ();
		guard = null;

		int a = 0;
	}



}
