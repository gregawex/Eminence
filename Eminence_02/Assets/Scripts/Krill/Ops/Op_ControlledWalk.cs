﻿using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class Op_ControlledWalk : ActorOp
{
	
	public FsmEvent targetReached;
	//public FsmEvent runButtonDown;
	public FsmEvent use;
	
	public Op_ControlledWalk() : base()
	{
		
	}
	
	public Op_ControlledWalk(Actor actor) : base (actor)
	{
		
	}
	public override void InitStates ()
	{
		base.InitStates ();
		
		actor.RequestState<AS_ControlledWalk>();
		actor.RequestState<AS_TurnAround>();
	 
		
	}
	
	public override void Begin ()
	{
		base.Begin ();
		//actor.Bot.enabled = true;
		actor.Bot.enabled = false;
 
			ChangeState<AS_TurnAround>();
	}

	public override void Execute ()
	{
		base.Execute ();

		/*if(GameManager.Input.IsButtonPressed(ButtonType.B))
		{
			Fsm.Event(runButtonDown);
			return;
		}*/

		if(!GameManager.Input.IsDirectionalInputPressed())
		{
			Fsm.Event(targetReached);
			//ChangeState<AS_Idle>();
			return;
		}



		if(actor.ActiveState is AS_TurnAround)
		{
			Vector3 targetVec = (SceneManager.Instance.testobj.position - SceneManager.Instance.ActivePC.transform.position).normalized;
			
			float dot = Vector3.Dot(targetVec, SceneManager.Instance.ActivePC.transform.forward);
			
			if(dot > 0.95)
			{
				ChangeState<AS_ControlledWalk>();
			}
		}
	}

	public override void End ()
	{
		base.End ();


	}
	
	public override void OnMessageFromState (string msg)
	{
		base.OnMessageFromState (msg);
		
	 
	}

	public override void OnOpInstruction (OpInstruction comm, object obj, System.Action finishedCallback)
	{
		base.OnOpInstruction (comm, obj, finishedCallback);

		switch(comm)
		{
		case OpInstruction.ON_GAMEITEM:
			Fsm.Event(use);
			break;
		}
	}
	
}