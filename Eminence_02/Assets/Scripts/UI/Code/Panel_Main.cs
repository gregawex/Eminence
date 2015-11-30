using UnityEngine;
using System.Collections;

public class Panel_Main : BasePanel {

	protected override void Awake ()
	{
		base.Awake ();
	}


	protected override void Update ()
	{
		base.Update ();
	}

		void pressed_social()
		{
				Send ("social");
		}

}
