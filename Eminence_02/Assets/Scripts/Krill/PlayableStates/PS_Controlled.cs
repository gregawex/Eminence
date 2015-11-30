using UnityEngine;
using System.Collections;

public class PS_Controlled : PlayableState 
{
	public PS_Controlled(){}
	public PS_Controlled(PlayableCharacter pc) :base(pc)
	{

	}

	public override void Begin ()
	{
		pc.Bot.enabled = true;
		//pc.AdventureCtrl.enabled = true;

	}

	public override void Execute ()
	{

	}

	public override void End ()
	{

	}
 
}
