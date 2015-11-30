using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class PlayableCharacter : Actor {


	// Use this for initialization]]


	PlayableState activeState;

	public bool TEST_overrideUserControl;

	 
	//public Area CurrentArea { get; set; } 




	protected override void Awake ()
	{
		base.Awake ();


		base.isUserControlled = TEST_overrideUserControl;
		 
 	}

	protected override void Start () 
	{
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update ();

 
	}


 

	public void FeedOp(OpModul op)
	{
		if (activeState != null)
			activeState.FeedOp (op);
	}
}

 
