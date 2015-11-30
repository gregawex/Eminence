using UnityEngine;
using System.Collections;

using System.Text.RegularExpressions;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class So_ActorSay : FsmStateAction {


	public FsmEvent finished;

	public FsmGameObject actorObj;
	public FsmString [] lines;

	SpeechBox speechBox;
	Actor actor;
	int wordCount;
	float textHoldTime;
	int textIndex;

	enum State { SETUP, PRE_TEXT, SHOW_TEXT, HOLD_TEXT, FINALIZE_AND_LEAVE }
	State state;
 
	public override void Init (FsmState state)
	{
		base.Init (state);


	}


	float startTime;
	public override void OnEnter ()
	{
		base.OnEnter ();
		speechBox = SceneManager.Instance.uiCanvas.speechBox;
		actor = actorObj.Value.GetComponent<Actor>();

		state = State.SETUP;

		startTime = Time.time;
		textIndex = 0;
	}

	public override void OnUpdate ()
	{
		base.OnUpdate ();

		switch (state)
		{
		case State.SETUP:
			startTime = Time.time;
			wordCount = WordCounting.CountWords1(lines[textIndex].Value);
			speechBox.text.gameObject.SetActive(false);

			if(wordCount < 5)
			{
				textHoldTime = 2;
			}
			else
				textHoldTime = Mathf.Clamp((wordCount / 5) +1, 2, 6);

			state = State.PRE_TEXT;
			break;

		case State.PRE_TEXT:

			if(Time.time - startTime > 0.5f)
				state = State.SHOW_TEXT;
			break;
		case State.SHOW_TEXT:

			speechBox.gameObject.SetActive(true);
			speechBox.text.gameObject.SetActive(true);
			speechBox.text.color = actor.speechTextColor;
			speechBox.text.text = "["+actor.name+"] "+lines[textIndex].Value;
			state = State.HOLD_TEXT;
			startTime = Time.time;
			break;
		case State.HOLD_TEXT:

			if(Time.time - startTime > textHoldTime)
				state = State.FINALIZE_AND_LEAVE;

			break;
		case State.FINALIZE_AND_LEAVE:

			//We have reached the end
			if(lines.Length -1 == textIndex)
			{
				Fsm.Event (finished);
			}
			//carry on
			else
			{
				textIndex ++;
				state = State.SETUP;
			}
			break;
		}

		if(Time.time - startTime > 3)
		{
			Fsm.Event(finished);
		}
	}


}

