using UnityEngine;
using System.Collections;


using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_Action : FsmStateAction 
{


	public FsmEvent bypass;
	public FsmEvent finished;

	public FsmBool saveState;

	SoupItem soupItem;
	string stateName;



	public override void Init (FsmState state)
	{
		base.Init (state);

		soupItem = Fsm.GameObject.GetComponent<SoupItem>();
		this.stateName = state.Name;

	}

	public override void OnEnter ()
	{
		base.OnEnter ();

		if(!saveState.Value)
		{
			Fsm.Event(finished);
			return;
		}

		string savedState = soupItem.GetSavedState();

		Debug.Log (""+savedState+" >> "+stateName);

		if(soupItem.shuttingDown)
		{
			soupItem.Pack.SetStateName(soupItem.CurrentlyExecutingSequence, stateName);
			soupItem.SaveState("");
			soupItem.FinishedWithSequence();
		}
		else
		if(savedState != stateName && (stateName.ToLower() != "default" || !string.IsNullOrEmpty(savedState)))
		{
			
			// we have been here and we haven't just finsihed a sequence, so fall through
			if(!soupItem.shuttingDown )
			{
				Fsm.Event(bypass);
			}
			//we haven't been here before and we have just finsihed a sequence so save and shut down
			else
			{

				soupItem.Pack.SetStateName(soupItem.CurrentlyExecutingSequence, stateName);

				soupItem.SaveState(stateName);

				soupItem.FinishedWithSequence();
				 
			}
			
		}
		// the default action will trigger if the savedstate is null or default
		else if(stateName.ToLower() == "default" && (string.IsNullOrEmpty(savedState) || savedState.ToLower() == "default"))
		{
			soupItem.SaveState(stateName);
			Fsm.Event(finished);
		}
		// this is the state we have as saved, so let's trigger it if it's not an end sequence. If it is, just shut down.
		else{

			if(!soupItem.shuttingDown)
			{
				Fsm.Event(finished);
			}
			else{
				soupItem.FinishedWithSequence();
			}


		}
	}
}
