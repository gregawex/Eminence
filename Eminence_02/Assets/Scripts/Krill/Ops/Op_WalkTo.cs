using UnityEngine;
using System.Collections;


using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class Op_WalkTo : ActorOp
{

	public FsmEvent targetReached;

	public Op_WalkTo() : base()
	{

	}

	public Op_WalkTo(Actor actor) : base (actor)
	{
		
	}
	public override void InitStates ()
	{
		base.InitStates ();

		actor.RequestState<AS_Walk>();
		actor.RequestState<AS_EaseToPoint>();

	}

	public override void Begin ()
	{
		base.Begin ();
		//actor.Bot.enabled = true;
		actor.Bot.enabled = true;
 
		if(Vector3.Distance(SceneManager.Instance.ActivePC.transform.position, SceneManager.Instance.testobj.position) < Constants.PC_NO_CLICK_RADIUS)
			ChangeState<AS_EaseToPoint>();
		else
		ChangeState<AS_Walk>();
	}

	public override void OnMessageFromState (string msg)
	{
		base.OnMessageFromState (msg);

		if(msg == "TargetReached")
			//actor.TriggerOp(DefaultOp.IDLE);
			Fsm.Event(targetReached);
	}
 
}
