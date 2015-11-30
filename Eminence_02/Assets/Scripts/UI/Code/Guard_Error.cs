using UnityEngine;
using System.Collections;

public class Guard_Error : Guard //<Guard_Error> 
{

	public Guard_Error (){}

	public Guard_Error(RootControl root)
		:base(root)
	{
		int a = 0;
	}

	public override void OnPanelMessage (object ev, params object [] obj)
	{
		string s = ev.ToString ();

		switch (s) {


		case "on_enable":
			break;
		case "positive":
			break;
		case "negative":
			break;
				}
	}

}
