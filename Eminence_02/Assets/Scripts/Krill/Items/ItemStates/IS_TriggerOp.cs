using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.Character)]
public class IS_TriggerOp : FsmStateAction 
{

	public FsmEvent finished;

	public FsmString opToTrigger;

	//public FsmGameObject item;

	GameItem item;
	string stateName;
	
	
	public override void Init (FsmState state)
	{
		base.Init (state);
		
		item = Fsm.GameObject.GetComponent<GameItem>();
		this.stateName = state.Name;
		
	}

	public override void OnEnter ()
	{
		base.OnEnter ();
		//SceneManager.Instance.

		if(item.TargetActor != null)
		{

			//if(opToTrigger.Value == "Op_
			//item.TargetActor.ActiveOp.OnOpInstruction(ActorOp.OpInstruction.ON_FLOOR);
			item.TargetActor.ActiveItem = item;
			item.SetItemOp(opToTrigger.Value);
			item.TargetActor.ActiveOp.OnOpInstruction(ActorOp.OpInstruction.ON_GAMEITEM, item, OnOpFinished);

		}
		else
		{
			GregBugger.LogError("Target actor was NULL in IS_TriggerOp");
		}
	}

	public void OnOpFinished()
	{
		Fsm.Event(finished);
	}


}
