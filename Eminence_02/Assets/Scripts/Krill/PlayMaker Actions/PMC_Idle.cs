using UnityEngine;
using System.Collections;

public class PMC_Idle : PMCState 
{

	public override void Awake ()
	{
		base.Awake ();
		InitAnim (ChannelMode.OPEN_AND_LISTEN, StateOutMode.END_WITH_ANIMATION, 0.5f);
	}

	public override void OnExit ()
	{
		base.OnExit ();

		Event (base.sendEvent);
	}

	public override void OnChannelMessage (object obj)
	{
		base.OnChannelMessage (obj);
	}
 
}
