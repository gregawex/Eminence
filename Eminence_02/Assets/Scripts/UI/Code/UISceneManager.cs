using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class UISceneManager : MonoBehaviour {


	Dictionary<string, BasePanel> panelPool;
	Dictionary<string, Guard> guardPool = new Dictionary<string, Guard>();

	public static UISceneManager Instance { get; private set; }

	public Image bgImage;
	public Panel_Popup popup;
	public Text menuTitle;

	PlayMakerFSM fsm;

	void Awake()
	{
		Instance = this;

		panelPool = new Dictionary<string, BasePanel>();


	}
	// Use this for initialization
	void Start ()
	{

		fsm = GetComponent<PlayMakerFSM>();

	}

	public void TriggerTransitionChannel(string fsmEvent)
	{
		fsm.Fsm.Event(fsmEvent);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMenuTitle(string title)
	{
		menuTitle.text = title.ToUpper();
	}



	public void SubscribePanel(BasePanel panel)
	{
		Type t = panel.GetType();
		string name = t.Name.Substring(6);

		if(t.IsSubclassOf(typeof(BasePanel)))
			if(!panelPool.ContainsKey(name))
			{
				panelPool.Add(name, panel);
			}
	}

	public BasePanel GetPanel(string name)
	{
		if(panelPool.ContainsKey(name))
		{
			return panelPool[name];
		}

		return null;
	}

	public void SetBG(Sprite sprite)
	{
		bgImage.sprite = sprite;


	}

 
}
