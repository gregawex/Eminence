using UnityEngine;
using System.Collections;
using System;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.Character)]
public class IS_Action : FsmStateAction
{

 
	bool hasRun;

	public FsmEvent show, examined, used;
	public FsmEvent bypass;

	public FsmString labelText;

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

		SceneManager.Instance.ChangeContext(ContextType.GAMEPLAY);

		item.itemName = labelText.Value;

		if(item.GetSavedState() != stateName)
		{

		
			if(item.initializing )
			{
				Fsm.Event(bypass);
			}
			else
			{
				item.SaveState(stateName);
 
			}

		}
		
		hasRun = true;
	}

	void OnExamine()
	{
		SceneManager.Instance.ActivePC.ActiveItem = item;
		SceneManager.Instance.CloseLens();
		//SceneManager.Instance.ChangeContext<Context_Gameplay>();

		SceneManager.Instance.ChangeContext(ContextType.PC_BUSY);

		Fsm.Event(examined);
	}

	void OnUse()
	{

		SceneManager.Instance.ActivePC.ActiveItem = item;
		SceneManager.Instance.CloseLens();
		//SceneManager.Instance.ChangeContext<Context_Gameplay>();

		SceneManager.Instance.ChangeContext(ContextType.PC_BUSY);

		Fsm.Event(used);
	}

	public void OnItemEvent(ItemEvent itemEvent)
	{
		switch(itemEvent)
		{
		case ItemEvent.SHOW:
			
			//SceneManager.Instance.uiCanvas.actionSelector.mainLens.AddChild("examine", OnExamine, Lens.LensPosition.LEFT, Lens.LensSize.MEDIUM);
			//SceneManager.Instance.uiCanvas.actionSelector.mainLens.AddChild("use", OnUse, Lens.LensPosition.RIGHT, Lens.LensSize.MEDIUM);
			
			Fsm.Event(show);
			
			 
			
			break;
		case ItemEvent.EXAMINE:
			OnExamine();
			Fsm.Event(examined);
			break;
		case ItemEvent.USE:
			OnUse();
			Fsm.Event(used);
			break;
			
		}
	}

 
 
}
