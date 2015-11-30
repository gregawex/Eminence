using UnityEngine;
using System.Collections;




using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
public class PMC_ButtonCharge : FsmStateAction 
{
	public FsmFloat minDuration, maxDuration;
	public ButtonType buttonType;
 	public FsmEvent onEvent;

	float start;
	
	public virtual void Awake ()
	{
		
		
	}

	public override void OnEnter ()
	{
		base.OnEnter ();

		Fsm.GameObject.GetComponent<PlayableCharacter>().ActiveOp.ChangeState<AS_Idle>();

		start = Time.time;
	}
	
	public override void OnUpdate ()
	{
		base.OnUpdate ();

		if(!GameManager.Input.IsButtonPressed(buttonType))
		{
			float charge = GameManager.Input.GetButtonCharge(buttonType);

			if(charge > 0)
			{
				if(charge >= minDuration.Value && charge <= maxDuration.Value)
				{
					Fsm.Event(onEvent);
				}
			}
			else{
				GameManager.Input.DeleteStartTime(buttonType);
			}
		}

	}
	
	
	
}
