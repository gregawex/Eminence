using UnityEngine;
using System.Collections;
using System;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class Op_Die : ActorOp
{
	
	public FsmEvent commOnFloor;
	public FsmEvent commOnClickable;
	//public FsmEvent 
	
	public Op_Die():base()
	{
		
	}
	public Op_Die(Actor actor) : base (actor)
	{
		
	}
	
	public override void InitStates ()
	{
		base.InitStates ();
		
		//There's only IDLE here
		actor.RequestState<AS_Die>();
		
		
	}
	public override void Begin ()
	{
		base.Begin ();
		
		ChangeState<AS_Die>();
	}
	
	public override void OnOpInstruction (OpInstruction comm, object obj, Action finishedCallback = null)
	{
		base.OnOpInstruction (comm, obj, finishedCallback);
		
		switch(comm)
		{
		case OpInstruction.ON_FLOOR:
			Fsm.Event(commOnFloor);
			
			break;
		case OpInstruction.ON_GAMEITEM:
			Fsm.Event(commOnClickable);
			break;
		}
	}

 
	
	
}
