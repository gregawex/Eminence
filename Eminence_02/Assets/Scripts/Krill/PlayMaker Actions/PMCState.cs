
using UnityEngine;
using System.Collections;
using System;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.Character)]
public class PMCState : FsmStateAction {

	public enum StateOutMode { END_WITH_ANIMATION, CONTINUOUS }
	public enum ChannelMode { DONT_OPEN, OPEN_AND_LISTEN }

	protected Animator animator;
	bool pendingAnim;


	public FsmEvent sendEvent;
	public FsmString animationName;
	//public string AnimationName { get { return animationName; } }
 	public FsmFloat transitionDuration;
	protected StateOutMode stateOutMode;
	protected ChannelMode channelMode;


	protected void InitAnim(ChannelMode channelMode,  StateOutMode stateOutMode, float transitionDuration)
	{
		 
		this.stateOutMode = stateOutMode;
		this.transitionDuration = transitionDuration;
		this.channelMode = channelMode;
	}


	public override void Awake ()
	{
		base.Awake ();

		this.animator = base.Owner.GetComponent<Animator> ();
	}

	public override void OnEnter ()
	{
		base.OnEnter ();

		//if(channelMode == ChannelMode.OPEN_AND_LISTEN)
			//SceneManager.Instance.OpenChannel (this, OnChannelMessage);

		AnimatorTransitionInfo transitionInfo = animator.GetAnimatorTransitionInfo(0);
		
	 
		if (transitionInfo.anyState) 
		{
			pendingAnim = true;			 
		} 
		else 
		{
			animator.CrossFade (animationName.Value, transitionDuration.Value);
			AnimBegin();
		}

	}

	public override void OnUpdate ()
	{
		base.OnUpdate ();

		if (pendingAnim) 
		{
			
			AnimatorTransitionInfo transitionInfo = animator.GetAnimatorTransitionInfo(0);
			
			if(!transitionInfo.anyState)
			{
				pendingAnim = false;
				animator.CrossFade (animationName.Value, transitionDuration.Value);
				AnimBegin();
			}
		}
	}

	public override void OnExit ()
	{
		base.OnExit ();
		//if (channelMode == ChannelMode.OPEN_AND_LISTEN)
			//SceneManager.Instance.CloseChannel (this);
	}

	public virtual void OnChannelMessage(object obj)
	{

	}

	//=======================
	public virtual void AnimBegin()
	{
		
	}
}
