using UnityEngine;
using System.Collections;

public class PS_Automatic : PlayableState
{

	public PS_Automatic(){}
	public PS_Automatic(PlayableCharacter pc) : base(pc)
	{

	}

	public override void Begin ()
	{

		pc.Bot.enabled = false;
		//pc.AdventureCtrl.enabled = false;
	}

	public override void Execute ()
	{
		if (op != null)
			op.Execute ();
	}

	public override void End ()
	{

	}
 
}
