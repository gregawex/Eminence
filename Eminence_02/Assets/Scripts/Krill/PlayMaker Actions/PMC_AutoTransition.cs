using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.GUI)]
public class PMC_AutoTransition : FsmStateAction 
{

	public FsmEvent autoTransition;

	public override void OnEnter ()
	{
		base.OnEnter ();

		Fsm.Event(autoTransition);


	}
}
