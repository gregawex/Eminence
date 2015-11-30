using UnityEngine;
using System.Collections;

public class AS_Walk : ActorState
{

	public AS_Walk(Actor actor)
		:base(actor, "Walk", StateOutMode.END_WITH_ANIMATION, /*0.05f*/ 0.05f)
	{
		
	}

	public override void Begin ()
	{
		actor.Bot.enabled = true;
		actor.Bot.targetReachedCallback = TargetReached ;
		actor.Bot.target = SceneManager.Instance.testobj;
		//base.animator.SetTrigger ("StartWalk");

		base.Begin ();
	}

	public override void Execute ()
	{
		base.Execute ();

		actor.CharacterController.Move(actor.transform.forward);
	}

	public override void End ()
	{


		base.End ();

		actor.Bot.target = null;
	}

	void TargetReached()
	{
		//actor.TriggerOp(ActorOp.DefaultOp.IDLE);
		actor.ActiveOp.OnMessageFromState("TargetReached");
	}
}
