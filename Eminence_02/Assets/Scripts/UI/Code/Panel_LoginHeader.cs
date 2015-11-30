using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Panel_LoginHeader : BasePanel 
{
	public Text time;

	string format;

	protected override void Awake ()
	{
		base.Awake ();

		format = "HH:mm";

	}
 

	protected override void Update ()
	{
		base.Update ();

		DateTime dateTime = DateTime.Now;


		time.text = dateTime.ToString(format);
	}
}
