using UnityEngine;
using System.Collections;

public class AS_ControlledWalk  : ActorState {


	public AS_ControlledWalk(Actor actor, bool somethin)
		:base(actor, "Walk", StateOutMode.END_WITH_ANIMATION, 0.1f)
	{
		
	}
	
	public override void Begin ()
	{
		actor.Bot.enabled = false;
		//actor.Bot.targetReachedCallback = TargetReached ;
		//actor.Bot.target = SceneManager.Instance.testobj;
		//base.animator.SetTrigger ("StartWalk");
		
		base.Begin ();
	}

	public override void Execute ()
	{
		base.Execute ();

		iTween.LookUpdate(actor.gameObject, SceneManager.Instance.testobj.position, 10f);

		PlayableCharacter pc = SceneManager.Instance.ActivePC;

		actor.CharacterController.Move(actor.transform.forward * 0.03f);

		/*if(pc.CurrentGroundType == GroundType.FLOOR)
		{
			pc.Animator.applyRootMotion = true;
		}
		else if(pc.CurrentGroundType == GroundType.STAIRS)
		{
			pc.Animator.applyRootMotion = false;
			pc.CharacterController.Move(pc.transform.forward * 0.07f);
		}*/

		//pc.Animator.applyRootMotion = false;
		//
		//pc.CharacterController.Move(pc.transform.forward * 0.07f);

	}
	
	
	public override void End ()
	{
		
		
		base.End ();
		
		actor.Bot.target = null;
	}
	

}
