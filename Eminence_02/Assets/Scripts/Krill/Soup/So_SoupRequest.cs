using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_SoupRequest : FsmStateAction 
{
	public FsmEvent finished;
	public FsmGameObject soupObject;

	public override void OnEnter ()
	{
		base.OnEnter ();
		Eve.TriggerEvent(new E_SoupRequest(soupObject.Name));
	}
}
