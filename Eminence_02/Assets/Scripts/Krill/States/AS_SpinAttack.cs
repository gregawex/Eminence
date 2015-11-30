using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class AS_SpinAttack : ActorState
{
	
	public AS_SpinAttack(Actor actor, bool something)
		:base(actor, "SpinAttack", StateOutMode.END_WITH_ANIMATION, 0.05f)
	{
		//actor.Animator.Play ("Idle");
		//actor.Animator.SetTrigger("TriggerIdle");
	}
	
	public override void OnAnimEnd ()
	{
		base.OnAnimEnd ();
		
		actor.ActiveOp.OnMessageFromState("SpinAttackEnded");
	}
	
}