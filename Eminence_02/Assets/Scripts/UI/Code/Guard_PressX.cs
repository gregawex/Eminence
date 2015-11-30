using UnityEngine;
using System.Collections;

public class Guard_PressX : Guard 
{

	public Guard_PressX()
	{}

	public Guard_PressX(RootControl root):base(root) 
	{

	}

	public override void OnPanelMessage (object ev, params object[] obj)
	{
		if (ev.ToString () == "pressed_x") 
		{
			//base.Ctrl.CloseAndShow("Panel_Login");

		}
	}



}
