using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class AS_Idle : ActorState
{

	public AS_Idle(Actor actor)
		:base(actor, "Idle", StateOutMode.END_WITH_ANIMATION, 0.3f)
	{
		//actor.Animator.Play ("Idle");
		actor.Animator.SetTrigger("TriggerIdle");
	}
 
}
