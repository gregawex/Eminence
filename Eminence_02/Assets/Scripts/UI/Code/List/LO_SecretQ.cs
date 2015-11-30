using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LO_SecretQ : ListOpener 
{
	
	protected override void Awake ()
	{
		base.Awake ();
		
		
	}
	
	public override void Clicked ()
	{
		base.Clicked ();
		
		//listBox.gameObject.SetActive(true);
		
		List<string> list = new List<string>();
		
		foreach(SecretQuestion c in Enum.GetValues(typeof(SecretQuestion)))
		{
			list.Add(c.ToString().Replace("_", " "));

			 
		}
		listBox.SetupList(list.ToArray());
	}
	
}