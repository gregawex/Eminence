using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_ProcessInput : FsmStateAction 
{

	public FsmBool requireInput;

	public FsmEvent match;
	public FsmEvent no_match;

	public FsmString sequenceID;

	public SoupItem soupItem;

	public override void Init (FsmState state)
	{
		base.Init (state);

		soupItem = state.Fsm.GameObject.GetComponent<SoupItem>();

	}

	public override void OnEnter ()
	{
		base.OnEnter ();

		soupItem.CurrentlyExecutingSequence = sequenceID.Value;

		if(soupItem.CurrentlyExecutingSequence == null)
			GregBugger.LogError("SequenceID in ProcessInput of FSM ["+Fsm.GameObjectName+"] is not set");
		
		if(!requireInput.Value)
			Fsm.Event(match);
	 
		

	}
}
