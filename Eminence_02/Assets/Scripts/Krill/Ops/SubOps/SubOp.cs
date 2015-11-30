using UnityEngine;
using System.Collections;
using System;

public class SubOp 
{

	protected Op_Use opUse;
	protected Actor actor;

	protected string pointId;
	protected Transform targetPoint;
 

	public SubOp(Actor actor, Op_Use opUse)
	{
		this.actor = actor;
		this.opUse = opUse;

		GregBugger.Log ("\t<subOp> ["+GetType().Name+"] activated");
	 
	}

	public virtual void Begin()
	{

	}

	public virtual void Execute()
	{

	}

	public virtual void End()
	{
		GregBugger.Log ("\t<subOp> ["+GetType().Name+"] ended");
		opUse.SubOpFinished();
	}

	public virtual void OnMessageFromState (string msg)
	{

	}

		 
	public virtual void OnStateReportedFinish()
	{

	}

	//========================================
	protected Vector3 FindClosestPoint(Actor actor, GameItem gi)
	{
		
		Transform  closestTr = null;
		float closest = float.MaxValue;
		foreach(Transform t in gi.points)
		{
			float dist = Vector3.Distance(actor.transform.position, t.position);
			if(dist < closest)
			{
				closest = dist;
				closestTr = t;
			}
		}
		
		pointId = closestTr.gameObject.GetComponent<PointInfo>().id;
		targetPoint = closestTr;
		
		return closestTr.position;
	}

}
