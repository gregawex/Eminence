using UnityEngine;
using System.Collections;

public class AS_TurnAround : ActorState {

	public AS_TurnAround(Actor actor, bool something)
		:base(actor, null, StateOutMode.END_WITH_ANIMATION, /*0.05f*/ 10f)
	{
		
	}

	public override void Execute ()
	{
		base.Execute ();

		iTween.LookUpdate(actor.gameObject, SceneManager.Instance.testobj.position, 10f);
		
		PlayableCharacter pc = SceneManager.Instance.ActivePC;


	}

}
