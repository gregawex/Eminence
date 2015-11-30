using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.GUI)]
public class PMC_PanelTransition : FsmStateAction 
{
	

	public FsmEvent onEvent;
	
	public virtual void Awake ()
	{

	}
	
	public override void OnUpdate ()
	{
		base.OnUpdate ();
	}
	
	
	
}
