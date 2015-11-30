using UnityEngine;
using System.Collections;

public class Op_AscendStairs : OpModul
{

	 


	public override void Begin ()
	{
		base.Begin ();
		pc.Animator.Play ("AscendingStairs");
	}

	public override void Execute ()
	{
		base.Execute ();

	}

	public override void End ()
	{
		base.End ();
	}
 
}
