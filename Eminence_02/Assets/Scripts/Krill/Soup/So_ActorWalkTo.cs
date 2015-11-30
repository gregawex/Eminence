using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_ActorWalkTo : FsmStateAction 
{
	public FsmEvent finished;

	public FsmGameObject target;
	public FsmGameObject actor;

	public override void OnEnter ()
	{
		base.OnEnter ();
	}

}
