using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class AS_Defense : ActorState
{
	
	public AS_Defense(Actor actor, bool something)
		:base(actor, "Defense", StateOutMode.END_WITH_ANIMATION, 0.05f)
	{
		//actor.Animator.Play ("Idle");
		//actor.Animator.SetTrigger("TriggerIdle");
	}
	
	public override void OnAnimEnd ()
	{
		base.OnAnimEnd ();
		
		actor.ActiveOp.OnMessageFromState("DefenseEnded");
	}

	public override void Execute ()
	{
		base.Execute ();

		iTween.LookUpdate(actor.gameObject, SceneManager.Instance.testobj.position, 10f);
		
		PlayableCharacter pc = SceneManager.Instance.ActivePC;
	}
	
}