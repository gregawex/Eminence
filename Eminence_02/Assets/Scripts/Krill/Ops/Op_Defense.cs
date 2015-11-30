using UnityEngine;
using System.Collections;
using System;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class Op_Defense : ActorOp
{
	
	public FsmEvent finished;
	 
	
	public FsmEvent commDie;
	//public FsmEvent 
	
	public Op_Defense():base()
	{
		
	}
	public Op_Defense(Actor actor) : base (actor)
	{
		
	}
	
	public override void InitStates ()
	{
		base.InitStates ();
		
		//There's only IDLE here
		actor.RequestState<AS_Defense>();
		
		
	}
	public override void Begin ()
	{
		base.Begin ();
		
		ChangeState<AS_Defense>();
	}

 

	public override void OnMessageFromState (string msg)
	{
		base.OnMessageFromState (msg);
		
		switch(msg)
		{
		case "DefenseEnded":
			Fsm.Event(finished);
			break;
		}
	}
	
	
	
	public override void OnOpInstruction (OpInstruction comm, object obj, Action finishedCallback = null)
	{
		base.OnOpInstruction (comm, obj, finishedCallback);
		
		switch(comm)
		{
			
		case OpInstruction.ON_DIE:
			Fsm.Event(commDie);
			break;
		}
	}
	
	
}
