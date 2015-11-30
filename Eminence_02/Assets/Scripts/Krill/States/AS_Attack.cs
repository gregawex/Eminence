using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class AS_Attack : ActorState
{
	
	public AS_Attack(Actor actor, bool something)
		:base(actor, "Attack", StateOutMode.END_WITH_ANIMATION, 0.05f)
	{
		//actor.Animator.Play ("Idle");
		//actor.Animator.SetTrigger("TriggerIdle");
	}

	public override void OnAnimEnd ()
	{
		base.OnAnimEnd ();

		actor.ActiveOp.OnMessageFromState("AttackEnded");
	}
	
}
