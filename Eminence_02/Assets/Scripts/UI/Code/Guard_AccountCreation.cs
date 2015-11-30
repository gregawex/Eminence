using UnityEngine;
using System.Collections;

public class Guard_AccountCreation : Guard {

		public Guard_AccountCreation():base()
		{
		}

		public Guard_AccountCreation(RootControl root):base(root)
		{

		}

		public override void OnPanelMessage (object ev, params object[] obj)
		{
		string s = ev.ToString ();

		switch (s) {
		case "gender":
			break;
				}
		}

	 
}
