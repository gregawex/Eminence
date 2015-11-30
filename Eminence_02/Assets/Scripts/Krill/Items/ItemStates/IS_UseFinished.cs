using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class IS_UseFinished
: FsmStateAction {

	public FsmEvent finished;
	GameItem item;

	public override void Init (FsmState state)
	{
		base.Init (state);

		item = Fsm.GameObject.GetComponent<GameItem>();
	}

	public override void OnEnter ()
	{
		base.OnEnter ();

		item.OnUseFinished();
		SceneManager.Instance.ActivePC.ActiveItem = null;
		Fsm.Event(finished);
	}

}
