using UnityEngine;
using System.Collections;

public class E_PanelClose : BaseEvent {

	public readonly BasePanel panel;

	public E_PanelClose(BasePanel panel)
	{
		this.panel = panel;
	}


}
