using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LO_Countries : ListOpener 
{

	protected override void Awake ()
	{
		base.Awake ();

		 
	}

	public override void Clicked ()
	{
		base.Clicked ();

		listBox.gameObject.SetActive(true);

		List<string> list = new List<string>();

		foreach(Countries c in Enum.GetValues(typeof(Countries)))
		{
			list.Add(c.ToString().Replace("_", " "));
		}
		listBox.SetupList(list.ToArray());
	}
 
}
