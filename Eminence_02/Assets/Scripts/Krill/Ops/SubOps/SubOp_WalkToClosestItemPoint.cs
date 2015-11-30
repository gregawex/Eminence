using UnityEngine;
using System.Collections;

public class SubOp_WalkToClosestItemPoint : SubOp
{
	

	
	public SubOp_WalkToClosestItemPoint(Actor actor, Op_Use opUse) : base(actor, opUse)
	{
		
		
	}
	
	public override void Begin ()
	{
		base.Begin ();
		
		
		
		actor.RequestState<AS_Walk>();
		actor.RequestState<AS_EaseToPoint>();
	

		actor.Bot.enabled = true;
		opUse.ChangeState<AS_Walk>();
		
		
		SceneManager.Instance.testobj.transform.position = FindClosestPoint(actor, actor.ActiveItem);
	}
	

	
	public override void OnMessageFromState (string msg)
	{
		base.OnMessageFromState (msg);
		
		
		if(msg == "TargetReached")
		{
			
			opUse.ChangeState<AS_EaseToPoint>(actor.ActiveItem, targetPoint);
			actor.Bot.enabled = false;
		}
		else if(msg == "EaseFinished")
		{
			
			//opUse.ChangeState<AS_OpenDoorOut>(pointId);

			//opUse.ChangeState<AS_Idle>();

			End ();
			
		}
	}
	
}
