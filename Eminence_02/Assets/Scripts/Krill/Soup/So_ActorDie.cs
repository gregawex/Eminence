using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_ActorDie : FsmStateAction 
{
	public FsmEvent finished;
	public FsmGameObject actorToKill;
	
	public override void OnEnter ()
	{
		base.OnEnter ();

		Actor actor = actorToKill.Value.GetComponent<Actor>();

		if(actor != null)
			actor.ActiveOp.OnOpInstruction(ActorOp.OpInstruction.ON_DIE, null);

		Fsm.Event(finished);

	}

	void OnDied()
	{

	}
}
