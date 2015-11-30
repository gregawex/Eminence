using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
 
public class ActorOp : FsmStateAction
{

	public enum DefaultOp { IDLE, WALK }
	public enum OpInstruction {ON_FLOOR, ON_GAMEITEM, ON_DIE, DIRECT_FLOOR}


	protected Actor actor;
 
	protected Action finishedCallback;


	public string Name { get; private set; }


	bool forcedCycle, forceFinish = false;
	int animLoopCount = 0;

	public ActorOp()
	{

	}
	public ActorOp(Actor actor)
	{
		this.actor = actor;
		Name = GetType().Name;

		//Now fill the States roster in the Actor
		InitStates();
	}

	public override void Awake ()
	{
		base.Awake ();

		actor = Fsm.GameObject.GetComponent<Actor>();
		
		Name = GetType().Name;
		
		//Now fill the States roster in the Actor
		InitStates();

	}

	public override void Init (FsmState state)
	{
		base.Init (state);


	}

	public virtual void InitStates()
	{

	}

	//===========================================================
	//PLAYMAKER CYCLE
	//===========================================================
	public override void OnEnter ()
	{
		base.OnEnter ();
		Begin();
	}
	public override void OnUpdate ()
	{
		base.OnUpdate ();
		Execute();
	}
	public override void OnExit ()
	{
		base.OnExit ();
		End();
	}

	//===========================================================
	//OP CYCLE
	//===========================================================
	public virtual void Begin()
	{

		//ChangeState <AS_Idle> ();
		GregBugger.Log("<Op> Begin ["+Name+"]");
	}
	
	public virtual void Execute()
	{
		
	}
	
	public virtual void End()
	{
		GregBugger.Log("<Op> End ["+Name+"]");


	}

	//===========================================================
	//MESSAGE EXCHANGE
	//===========================================================

	//Message from the STATE (message from below)
	public virtual void OnMessageFromState(string msg)
	{

	}

	//Message from the CONTEXT/ SCENEMANAGER (message from above)
	public virtual void OnOpInstruction(OpInstruction comm, object obj, Action finishedCallback = null)
	{
		GregBugger.Log ("comm ["+comm.ToString()+"], current actorOp ["+this.GetType()+"]");

		this.finishedCallback = finishedCallback;

	}

	//===========================================================
	//STATE FUNCTIONS
	//===========================================================
	
	//Sets the pending state. Doesn't take immediate effect, the new state will trigger when the currently executing
	//state finishes with its task.
	public void ChangeState<T> (params object[] objs) where T: ActorState
	{
		Type st = typeof(T);

 
		ChangeState (st, objs);
		
	}
	
	public void ChangeState(Type st, params object[] objs) 
	{




		if (!actor.States.ContainsKey (st)) {
			st = typeof(AS_Idle);
			GregBugger.LogError("Can't change to state, forcing IDLE");
		}
		else 
		{
			
		}
		
		if(actor.ActiveState == null)
		{
			actor.ActiveState = actor.States[st];
			actor.ActiveState.Begin();

			if(objs != null)
				actor.ActiveState.Init( objs);
		}
		else
		{
			actor.PendingState = actor.States[st];
			actor.ActiveState.End ();
			actor.ActiveState = actor.PendingState;


			if(objs != null)
				actor.ActiveState.Init( objs);

			actor.ActiveState.Begin();
			//activeState.End();
		}

	}
	
	//Interrupts the current state and starts a new state immediately, regardless of what the active state is doing.
	//Use it when the new state needs immediate effect eg. the punch should affect the enemy regardless of what
	//state its animation is currently in.
	public void InterruptState<T> () where T: ActorState
	{
		
		if(actor.States.ContainsKey(typeof(T)))
		{
			forcedCycle = true;
			animLoopCount = 0;
			if(actor.ActiveState != null)
				actor.ActiveState.End ();
			actor.ActiveState = actor.States[typeof(T)];

			actor.ActiveState.BeginImmediately ();
		}
	}

	public void InterruptState (Type t)  
	{
		
		if(actor.States.ContainsKey(t))
		{
			forcedCycle = true;
			animLoopCount = 0;

			if(actor.ActiveState != null)
				actor.ActiveState.End ();
			actor.ActiveState = actor.States[t];
			actor.ActiveState.BeginImmediately ();
		}
	}
	
	public void VoidState ()
	{
		actor.PendingState = null;
	}
	

	
	void CheckForActiveStateFinish()
	{
		
		
		if(forcedCycle)
		{
			forcedCycle = false;
			return;
		}
		
		if(actor.ActiveState.HasFinished() || forceFinish )
		{
			
			
			forceFinish = false;
			
			//If there is a pending state, switch to that
			if(actor.PendingState != null)
			{
				if(actor.PendingState != actor.ActiveState)
					animLoopCount = 0;
				else 
					animLoopCount ++;
				
				actor.ActiveState.End ();
				actor.ActiveState = actor.PendingState;
				
				
			}
			//If there isn't a pending state, there's noone to switch to, so start over the current state
			else
			{
				actor.ActiveState.End ();
				//in the end we might have set a new state so check for pending again
				if(actor.PendingState != null)
					actor.ActiveState = actor.PendingState;
				
				animLoopCount ++;
				
			}
			
			actor.ActiveState.Begin();
			actor.PendingState = null;
		}
	}



}


