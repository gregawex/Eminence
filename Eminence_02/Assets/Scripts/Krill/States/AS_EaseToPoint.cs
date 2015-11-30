using UnityEngine;
using System.Collections;
using System;

public class AS_EaseToPoint : ActorState 
{

	GameItem item;
	Transform targetPoint;
	bool ignoreRotation;

	public AS_EaseToPoint(Actor actor, bool somethin)
		:base(actor, "Idle", StateOutMode.END_WITH_ANIMATION, 0.8f /*0.05f*/)
	{
		
	}

	public override void Init ( params object[] objs)
	{
		base.Init ( objs);

		if(objs != null && objs.Length > 0)
		{
			this.item = objs[0] as GameItem;
			this.targetPoint = objs[1] as Transform;

			if(objs.Length>2)
			{
				this.ignoreRotation = (bool)objs[2] ;
			}
			else if(objs.Length <=2)
			{
				this.ignoreRotation = false;
			}
		}
	}

	float angleTo;

	public override void Begin ()
	{
		base.Begin ();

		if(targetPoint == null) return;

		angleTo = actor.transform.rotation.y - targetPoint.rotation.y;
		actor.gameObject.GetComponent<CharacterController>().enabled = false;
		actor.Bot.enabled = false;

		//actor.transform.rotation = item.point.rotation;
		iTween.MoveTo(actor.gameObject, iTween.Hash("position", targetPoint.position,  "time", 2f));

		if(!ignoreRotation)
			iTween.RotateTo(actor.gameObject, iTween.Hash("y", targetPoint.eulerAngles.y, "time", 2f ));

	}

	public override void Execute ()
	{
		base.Execute ();

		//actor.transform.position = Vector3.Lerp(actor.transform.position, targetPoint.position, Time.deltaTime * 2f);

		 //Quaternion.Lerp(	actor.transform.rotation, Quaternion.AngleAxis(270, Vector3.up), Time.deltaTime * 10f);

		float dot = Vector3.Dot(actor.transform.forward, targetPoint.forward);

		if(Vector3.Distance(actor.transform.position, targetPoint.position) < 0.03f)
		if((!ignoreRotation && dot >= 0.99f) || ignoreRotation)
		{

			//SNAP
			if(!ignoreRotation)
			{
				actor.transform.rotation = targetPoint.rotation;
				//actor.transform.position = new Vector3(targetPoint.position.x, actor.transform.position.y, targetPoint.position.z);
			}
			actor.ActiveOp.OnMessageFromState("EaseFinished");
			iTween.Stop(actor.gameObject);
		}

	}

	void OnComplete()
	{
		actor.ActiveOp.OnMessageFromState("EaseFinished");
	}

	public override void End ()
	{
		base.End ();
		//actor.transform.rotation = targetPoint.rotation;
		//actor.Bot.enabled = true;
		actor.gameObject.GetComponent<CharacterController>().enabled = true;
	}
 
}
