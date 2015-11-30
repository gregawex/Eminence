using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_FinishSequence : FsmStateAction {


	public FsmEvent finished;

	SoupItem soupItem;

	public override void OnEnter ()
	{
		base.OnEnter ();

		soupItem =Fsm.GameObject.GetComponent<SoupItem>();

		soupItem.shuttingDown = true;

		SceneManager.Instance.uiCanvas.speechBox.gameObject.SetActive(false);


		if(finished != null)
		{
			Fsm.Event(finished);
		}
		else
		{
			soupItem.Pack.SetStateName(soupItem.CurrentlyExecutingSequence, "Finished");
			soupItem.CurrentlyExecutingSequence = null;
		}
	}
}
