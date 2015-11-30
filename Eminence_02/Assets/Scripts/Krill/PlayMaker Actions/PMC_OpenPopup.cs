using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using HutongGames.PlayMaker;

[ActionCategory(ActionCategory.GUI)]
public class PMC_OpenPopup : FsmStateAction 
{
	public FsmEvent onPositive, onNegative;
	public FsmString text;
	public PopupButtonSetup buttonSetup;
	public FsmString buttonText_positive, buttonText_negative;

	public override void OnEnter ()
	{
		base.OnEnter ();

		UISceneManager.Instance.popup.Open(buttonSetup, text.Value, buttonText_positive.Value, buttonText_negative.Value);
		UISceneManager.Instance.popup.gameObject.SetActive(true);
	}

	 

	public override void OnExit ()
	{
		base.OnExit ();

		UISceneManager.Instance.popup.gameObject.SetActive(false);

	}
}
