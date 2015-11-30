using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.GUI)]
public class PMC_SetBackground : FsmStateAction {

	public FsmObject spriteObj;

	public override void OnEnter ()
	{
		base.OnEnter ();

		Texture2D tex = spriteObj.Value as Texture2D;

		Sprite sprite = null;
		if(tex != null)
		 sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));

		 

		UISceneManager.Instance.SetBG(sprite);
	}

}
