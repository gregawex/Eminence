
using UnityEngine;
using System.Collections;
using System;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class RequiresState : Attribute
{
	public readonly Type stateType;
	
	public RequiresState(Type stateType)
	{
		this.stateType = stateType;
	}
}

public class ActorState 
{
	
	public enum StateOutMode { END_WITH_ANIMATION, CONTINUOUS }
	StateOutMode outMode;
	
	string name;
	public string Name { get { return name; } }
	
	Type type;
	public Type SelfType { get { return type; } }
	
	protected string animationName;
	public string AnimationName { get { return animationName; } }
	
	protected Actor actor;
	
	protected bool hasFinished;
	
	protected float transitionDuration;

	//private bool endWithAnim;
	//protected bool WillEndWithAnim { get { return endWithAnim; } private set { endWithAnim = value; } }
	
	protected Animator animator;
	bool pendingAnim;
	
	bool pendingBuzz;

	protected object [] objects;

	
	public ActorState(Actor actor, string animationName, StateOutMode outMode, float transitionDuration = 0.1f )
	{
		this.type = GetType ();
		this.name = type.Name;
		this.actor = actor;
		this.animator = actor.Animator;
		
		this.animationName = animationName;
		this.transitionDuration = transitionDuration;
		
		this.outMode = outMode;
		
	}
	
	public bool HasFinished()
	{
		return hasFinished;
	}


	
	public virtual void Reset()
	{
		
	}

	//=======================================================
	//ANIMATOR EVENTS
	//=======================================================

	public virtual void Init( params object [] objs)
	{
		this.objects = objs;

	}

	public virtual void Begin()
	{
		hasFinished = false;
		pendingAnim = false;
		
		Reset ();
		GregBugger.Log("<AS> Begin ["+Name+"]");

		
	 
		
		//if(animator.HasState(0,hash)){
		//animator.Play (animationName);
		//}
		//animator.SetTrigger ("Start" + animationName);
		
		AnimatorTransitionInfo transitionInfo = animator.GetAnimatorTransitionInfo(0);
		
		//Currently no transition happening
		if (transitionInfo.anyState) {
			pendingAnim = true;
			//Debug.Log("In transition");
			
		} else {

			if(animationName != null)
				animator.CrossFade (animationName, transitionDuration);
			else 
				pendingAnim = true;
			//AnimBegin();
		}
		
		
		
		
	}

	public virtual void BeginImmediately()
	{
		hasFinished = false;
		pendingAnim = false;
		
		Reset ();
		GregBugger.Log("<AS> BeginImmediately ["+Name+"]");

		//pendingAnim = true;
		pendingAnim = true;
		animator.CrossFade (animationName, 0);
	}
	
	
	public virtual void Execute()
	{
		/*if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Base."+animationName))
		{
			Debug.LogError("animation state missing: "+animationName+" in "+nme.name);
		}*/

		AnimatorTransitionInfo transitionInfo = animator.GetAnimatorTransitionInfo (0);


		if (pendingAnim) {

						if (!transitionInfo.anyState) {
								pendingAnim = false;

								if(animationName != null)
								animator.CrossFade (animationName, transitionDuration);
								//AnimBegin ();
								//Debug.Log("pending anim solved");
						}
				} else {
					
					/*if(!transitionInfo.anyState)
						if(!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
							HasFinished();*/
				}


	}
	
	public virtual void End()
	{
		//animator.SetTrigger ("Finish" + animationName);
		GregBugger.Log("<AS> End ["+Name+"]");
	}

	//=======================================================
	//ANIMATOR EVENTS
	//=======================================================
	//YOu have to make sure you have the AnimReporter script attached to the AnimState in Animator
	//in order to receive these events.
	public virtual void OnAnimBegin()
	{
		
	}
	
	public virtual void OnAnimEnd()
	{
		
	}

	public virtual void OnAnimEvent(string msg)
	{

	}
 

}