using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum ContextType { GAMEPLAY, PC_BUSY, UI }

abstract public class InputMode 
{
	public SceneContext ActiveContext { get; protected set; }
	public ContextType ActiveContextType { get; protected set; }

	protected Dictionary<IInputListener, IInputListener> listeners = new Dictionary<IInputListener, IInputListener>();
	protected Dictionary<ButtonType, ButtonPack> buttonPool;
 

	public InputMode()
	{

		buttonPool = new Dictionary<ButtonType, ButtonPack>();

		foreach(ButtonType bt in Enum.GetValues(typeof(ButtonType)))
		{
			buttonPool.Add(bt, new ButtonPack());
		}
	}

	public void SetButton(ButtonType bt, bool b)
	{
		buttonPool[bt].pressed = b;

		if(b)
		{
			buttonPool[bt].pressStart = Time.time;
		}
		else{
			//buttonPool[bt].pressStart = -1;
		}

	}


	public virtual bool IsDirectionalInputPressed() 
	{ 

		return false;
	}

	public virtual bool IsButtonPressed(ButtonType buttonType)
	{
		return buttonPool[buttonType].pressed;
	}

	public virtual float GetButtonCharge(ButtonType buttonType)
	{
		return Time.time - buttonPool[buttonType].pressStart;
	}

	public virtual void DeleteStartTime(ButtonType buttonType)
	{
		buttonPool[buttonType].pressStart = -1;
	}

	protected Dictionary<ContextType, SceneContext> contexts = new Dictionary<ContextType, SceneContext>();


	public void ChangeContext(ContextType contextType)
	{
		if(ActiveContextType == contextType)
			return;



		if(contexts.ContainsKey(contextType))
		{
			ActiveContextType = contextType;

			ActiveContext.End();
			ActiveContext = contexts[contextType];
			ActiveContext.Begin();
		}
		else{
			GregBugger.LogError("Current input mode ["+this.GetType()+" doesn't define context type ["+contextType.ToString()+"]");
		}
	}

	public void SubscribeListener(IInputListener listener)
	{
		listeners.Add(listener, listener);
	}

	public void UnsubscribeListener(IInputListener listener)
	{
		listeners.Remove(listener);
	}


}

public class ButtonPack
{
	public bool pressed;
	public float pressStart;

	public ButtonPack()
	{

	}

}
