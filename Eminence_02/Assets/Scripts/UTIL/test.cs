using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class test : MonoBehaviour{

	void Awake()
	{
		PlayMakerFSM fsm = gameObject.GetComponent<PlayMakerFSM>();

		fsm.SendEvent("Collect Broken Pieces");
	}

}


[ActionCategory(ActionCategory.Character)]
public class testState : FsmStateAction 
{


	public FsmEvent bypass;

	public override void Awake ()
	{
		base.Awake ();

	
	}

	public override void OnEnter ()
	{
		base.OnEnter ();
		Fsm.Event(bypass);
	}


 
}

[ActionCategory(ActionCategory.Character)]
public class say : FsmStateAction{

	public FsmString text;

	public FsmString [] texts;

}

[ActionCategory(ActionCategory.Character)]
public class prerequisite : FsmStateAction{

	public FsmEvent pass;
	public FsmEvent noPass;

	public FsmString requirement;
	
}




