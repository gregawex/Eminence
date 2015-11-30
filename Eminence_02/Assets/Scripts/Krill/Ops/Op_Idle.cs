using UnityEngine;
using System.Collections;
using System;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class Op_Idle : ActorOp
{

	public FsmEvent commOnFloor;
	public FsmEvent commDirectFloor;
	public FsmEvent commOnClickable;

	public FsmEvent commDie;

	 
	//public FsmEvent 

	public Op_Idle():base()
	{

	}
	public Op_Idle(Actor actor) : base (actor)
	{

	}

	public override void InitStates ()
	{
		base.InitStates ();

		//There's only IDLE here
		actor.RequestState<AS_Idle>();


	}
	public override void Begin ()
	{
		base.Begin ();

		ChangeState<AS_Idle>();
	}

	/*public override void Execute ()
	{
		base.Execute ();

		if(GameManager.Input.IsButtonPressed(ButtonType.X))
		{
			Fsm.Event(attackPressed);
		}
		else if(GameManager.Input.IsButtonPressed(ButtonType.Y))
		{
			Fsm.Event(defensePressed);
		}
	}*/

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
		case OpInstruction.DIRECT_FLOOR:
			Fsm.Event(commDirectFloor);
			break;
		case OpInstruction.ON_DIE:
			Fsm.Event(commDie);
			break;
		}
	}


}
