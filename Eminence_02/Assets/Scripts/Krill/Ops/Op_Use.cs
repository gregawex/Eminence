using UnityEngine;
using System.Collections;
using System;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class Op_Use : ActorOp
{

  

	public FsmEvent finished;

	GameItem item;

	SubOp subOp;

	public FsmEvent commOnFloor;
	
	
	public override void Init (FsmState state)
	{
		base.Init (state);
 
		
	}

	public override void OnEnter ()
	{
		base.OnEnter ();



		//Actor actor = Fsm.GameObject.GetComponent<Actor>();
		Actor actor = SceneManager.Instance.ActivePC;
		item = actor.ActiveItem;
		
		if(!string.IsNullOrEmpty(item.Pack.ActiveOp))
		{
 
			GregBugger.Log ("Op_Use received ["+item.Pack.ActiveOp+"] for execution");

			Type t = Type.GetType(item.Pack.ActiveOp);
 
			if(t != null)
			{
				subOp = Activator.CreateInstance(t, actor, this) as SubOp;
				subOp.Begin();

			}
			else 
			{
				GregBugger.LogError("SubOp ["+item.Pack.ActiveOp+"] doesn't exist");
			}




			
		}
		else{
			
			GregBugger.LogError("Can't use GameItem's op because it's null in ["+Fsm.GameObjectName+"]");
		}

	}

	public override void OnUpdate ()
	{
		base.OnUpdate ();

		if(subOp != null)
		{
			subOp.Execute();
		}
	}

	public void SubOpFinished()
	{
		subOp = null;
		Fsm.Event(finished);


		if(actor.ActiveItem != null)
		{

			foreach(FsmStateAction act in actor.ActiveItem.FSM.Fsm.ActiveState.Actions)
			{
				IS_TriggerOp i = act as IS_TriggerOp;

				i.OnOpFinished();
			}

			//actor.ActiveItem = null;

		}

 
	}

	public override void OnOpInstruction (OpInstruction comm, object obj, Action finishedCallback= null)
	{
		base.OnOpInstruction (comm, obj, finishedCallback);

		switch(comm)
		{
		case OpInstruction.ON_FLOOR:
			Fsm.Event(commOnFloor);
			break;
		}
	}

	public override void OnMessageFromState (string msg)
	{
		base.OnMessageFromState (msg);
		subOp.OnMessageFromState(msg);

	}


}
