using UnityEngine;
using System.Collections;

public class Panel_PressX : BasePanel {

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
	}

	void Pressed()
	{
		Send ("pressed_x");
	}
}
