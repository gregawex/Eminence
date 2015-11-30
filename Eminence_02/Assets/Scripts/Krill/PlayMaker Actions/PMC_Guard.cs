using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.GUI)]
public class PMC_Guard : FsmStateAction 
{
 
	public FsmString menuTitle;
	public FsmGameObject panelObj;

	BasePanel panel;
	public FsmBool stayActivatedOnExit;
	
	public virtual void Awake ()
	{
		//UISceneManager.Instance.SubscribePanel(


		
	}

	public override void OnEnter ()
	{
		base.OnEnter ();

		if(menuTitle.Value != null && menuTitle.Value != "")
		{
			UISceneManager.Instance.SetMenuTitle(menuTitle.Value.ToString());
		}

		panel = panelObj.Value.GetComponent<BasePanel>();

		panelObj.Value.SetActive(true);

		if(panel != null)
		{

		}
		else{
			GregBugger.LogError("Panel cannot be found in PMC_Guard ["+GetType().Name+"]");
		}

		Fsm.Event("AUTO_TRANSITION");
	}
	
	public override void OnUpdate ()
	{
		base.OnUpdate ();


		
 
	}

	public override void OnExit ()
	{
		base.OnExit ();

		if(panel != null)
			panel.FinalizeProcess();

		if(!stayActivatedOnExit.Value)
			panelObj.Value.SetActive(false);
	}
	
	
	
}
