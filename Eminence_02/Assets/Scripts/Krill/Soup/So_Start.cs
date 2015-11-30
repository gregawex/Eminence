using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_Start : FsmStateAction {


	public FsmEvent finished;

	public override void OnUpdate ()
	{
		base.OnUpdate ();

		Fsm.Event(finished);
	}



	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
