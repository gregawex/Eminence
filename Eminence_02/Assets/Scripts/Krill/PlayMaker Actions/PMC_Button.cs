using UnityEngine;
using System.Collections;




using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
public class PMC_Button : FsmStateAction 
{
	public ButtonType buttonType;
	public ButtonState buttonState;
	public FsmEvent onEvent;

	public virtual void Awake ()
	{
		 
 
	}

	public override void OnUpdate ()
	{
		base.OnUpdate ();

		if(buttonState == ButtonState.PRESSED)
			if(GameManager.Input.IsButtonPressed(buttonType))
			{
				Fsm.Event(onEvent);
			}

		if(buttonState == ButtonState.RELEASED)
		{
			if(!GameManager.Input.IsButtonPressed(buttonType))
			{
				Fsm.Event(onEvent);
			}
		}
	}
	
 
	
}
