using UnityEngine;
using System.Collections;

public class Guard_Main : Guard//<Guard_Main> 
{

	public Guard_Main()
	{}

	public Guard_Main(RootControl root)
		:base(root)
	{
		
	}

	public override void OnPanelMessage (object ev, params object[] obj)
	{
				string s = ev.ToString ();
				switch (s) {

				case "social":
						//base.Ctrl.CloseAndShow ("Panel_Social");
						break;
				}
	}
}
