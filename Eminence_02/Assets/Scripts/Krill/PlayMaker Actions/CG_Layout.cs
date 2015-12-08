using UnityEngine;
using System.Collections;


using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
public class CG_Layout : FsmStateAction 
{

	public FsmEvent finished;

	public override void OnEnter ()
	{
		base.OnEnter ();

		foreach( CardInstance ci in CardSceneManager.Instance.Player_RED.CardInstances)
		{
			ci.transform.position = CardSceneManager.Instance.red_holder.position;
			ci.gameObject.SetActive(false);
		}

		foreach( CardInstance ci in CardSceneManager.Instance.Player_BLU.CardInstances)
		{
			ci.transform.position = CardSceneManager.Instance.blu_holder.position;
			ci.gameObject.SetActive(false);
		}

	}

	public override void OnUpdate ()
	{
		base.OnUpdate ();


	}

}
