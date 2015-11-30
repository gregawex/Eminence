using UnityEngine;
using System.Collections;

public class AS_Die : ActorState {
	
	
	bool isMirrored;
	
	public AS_Die(Actor actor, bool b)
		:base(actor, "Die", StateOutMode.END_WITH_ANIMATION, 0.01f)
	{
		
	}
	
	public override void Init ( params object[] objs)
	{
		//string s = objs[0] as string;
		
 
	}
	
	float y;
	public override void Begin ()
	{
		base.Begin ();
		
		actor.Bot.enabled = false;
		
		
	}
	
	
	public override void Execute ()
	{
		base.Execute ();
		
 
	}
	
	public override void End ()
	{
		base.End ();
		 
	}
	
	public override void OnAnimBegin ()
	{
		base.OnAnimBegin ();
		
		
	}
	
	public override void OnAnimEvent (string msg)
	{
		base.OnAnimEvent (msg);
 
	}
	
	
}
