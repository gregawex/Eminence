using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class E_ButtonPress : BaseEvent {

	public readonly BasePanel sender;
	public readonly Button button;

	public E_ButtonPress(BasePanel sender, Button button)
	{
		this.sender = sender;
		this.button = button;
	}

}
