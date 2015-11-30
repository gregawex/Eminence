using UnityEngine;
using System.Collections;
using System;

public class SubOp_Test : SubOp 
{


	enum State { WalkTo, Ease }
	State state;
	public SubOp_Test(Actor actor, Op_Use opUse) : base(actor, opUse)
	{


	}
 

	public override void Begin ()
	{
		base.Begin ();

		state = State.WalkTo;
		
		actor.RequestState<AS_Walk>();
		actor.RequestState<AS_EaseToPoint>();
		opUse.ChangeState<AS_Walk>();
		SceneManager.Instance.testobj.transform.position = actor.ActiveItem.point.position;
	}

	public override void Execute ()
	{
		base.Execute ();

		switch(state)
		{
		case State.WalkTo:

			break;
		case State.Ease:
			break;
		}


	}

	public override void OnMessageFromState (string msg)
	{
		base.OnMessageFromState (msg);
		
		if(msg == "TargetReached")
		{
			state = State.Ease;
			opUse.ChangeState<AS_Idle>();

			 
		}
	}
}
