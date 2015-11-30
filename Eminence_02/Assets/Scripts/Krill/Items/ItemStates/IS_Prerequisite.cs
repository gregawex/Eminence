using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.Character)]
public class IS_Prerequisite : FsmStateAction 
{

	public FsmEvent pass;
	public FsmEvent noPass;
	
	public FsmString [] requirements;

	public override void OnEnter ()
	{
		base.OnEnter ();

		int rnd = Random.Range(0, 10);

		if(rnd < 5)
			Fsm.Event(pass);
		else
			Fsm.Event(noPass);
	}
}
