using UnityEngine;
using System.Collections;

public abstract class PlayableState : State
{

	protected PlayableCharacter pc;
	public PlayableState(){}
	protected OpModul op;

	public PlayableState(PlayableCharacter pc) : base ()
	{
		this.pc = pc;
	}

	public void Init(PlayableCharacter pc)
	{
		this.pc = pc;
		 
	}

	public void FeedOp(OpModul op)
	{
		this.op = op;
		op.SetCharacter (pc);
		op.Begin ();
	}

 

 
}
