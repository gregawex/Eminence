using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_ActorUse : FsmStateAction {

	public FsmEvent finished;

	public FsmGameObject actor;
	public FsmGameObject item;

	public override void OnEnter ()
	{
		base.OnEnter ();
	}
}
