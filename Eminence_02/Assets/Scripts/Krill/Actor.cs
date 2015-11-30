/*

 █████╗  ██████╗████████╗ ██████╗ ██████╗ tm
██╔══██╗██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗
███████║██║        ██║   ██║   ██║██████╔╝
██╔══██║██║        ██║   ██║   ██║██╔══██╗
██║  ██║╚██████╗   ██║   ╚██████╔╝██║  ██║
╚═╝  ╚═╝ ╚═════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


using Pathfinding;
//using RootMotion.FinalIK;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[RequireComponent(typeof(Pathfinding.AIBot))]
[RequireComponent(typeof(ActorPhysics))]
[RequireComponent(typeof(PlayMakerFSM))]
public class Actor : MonoBehaviour 
{
 
	public string name;
	public Color speechTextColor;


	Animator animator;
	public Animator Animator{ get { return animator; } }
	protected AIBot bot;
	public AIBot Bot { get { return bot; } }
 
	//protected LookAtIK lookAtIK;
	//public LookAtIK LookAtIK { get { return lookAtIK; } }
	protected bool isUserControlled;
	public bool IsUserControlled { get { return isUserControlled; } set { isUserControlled = value; } }


	ActorState activeState;
	public ActorState ActiveState { get { return activeState; } set { activeState = value; } }
	ActorState pendingState;
	public ActorState PendingState { get { return pendingState; } set { pendingState = value; } }
	protected Dictionary< Type, ActorState> states;
	public Dictionary<Type, ActorState> States { get { return states; } }

	public CharacterController CharacterController { get; private set; } 

	Dictionary<ActorOp.DefaultOp, ActorOp> defaultOps = new Dictionary<ActorOp.DefaultOp, ActorOp>();
	ActorOp activeOp;
	public ActorOp ActiveOp { get { return activeOp; } }

	public GameItem ActiveItem { get; set; }

	public Area ActiveArea { get; private set; }


	//The camera will point at this position
	public Transform head;

	PlayMakerFSM fsm;
	public PlayMakerFSM FSM { get { return fsm; } }
	FsmState cachedState;

	//FullBodyBipedIK ik;

	//GroundType currentGroundType;
	//public GroundType CurrentGroundType {get { return currentGroundType; } private set { currentGroundType = value; } }
	//FloorMaterial currentFloorMaterial;
	//public FloorMaterial CurrentFloorMaterial {get { return currentFloorMaterial; } private set { currentFloorMaterial = value; } }


	BuzzUnit leftHandBuzz, rightHandBuzz;



	protected virtual void Awake()
	{
		states = new Dictionary<Type, ActorState> ();

		bot = GetComponent<AIBot> ();
		animator = GetComponent<Animator> ();
		fsm = GetComponent<PlayMakerFSM>();
		//lookAtIK = GetComponent<LookAtIK>();

	

		CharacterController = GetComponent<CharacterController>();

		//States should be added by the ops themselves. Only they know what they need.


		AddState<AS_Idle> 					(new AS_Idle (this));
		AddState<AS_Walk> 					(new AS_Walk (this));
		/*
		AddState<AS_AscendStairs> 			(new AS_AscendStairs(this));
		AddState<AS_OpenDoorIn> 			(new AS_OpenDoorIn(this));
		AddState<AS_OpenDoorOut> 			(new AS_OpenDoorOut(this));
		*/




		defaultOps.Add(ActorOp.DefaultOp.IDLE, new Op_Idle(this));
		defaultOps.Add(ActorOp.DefaultOp.WALK, new Op_WalkTo(this));

 
	}

	protected virtual void Start()
	{
		//ChangeState<AS_Idle> ();
		//TriggerOp(ActorOp.DefaultOp.IDLE);
	}

	protected virtual void Update()
	{
		 


		 //-----------------------------
		//Handle IK reach

		//WE Probably won't use IK solver in Eminence but I keep this code anyway because YOU NEVER KNOW
		//-----------------------------
		/*if(leftHandBuzz != null)
			ik.solver.leftHandEffector.positionWeight = leftHandBuzz.Perc;

		else if(rightHandBuzz != null)
			ik.solver.rightHandEffector.positionWeight = rightHandBuzz.Perc;
	 
		*/

		if(cachedState != fsm.Fsm.ActiveState)
		{
			foreach(FsmStateAction action in fsm.Fsm.ActiveState.Actions)
			{
				 
				if(action is ActorOp)
				{
					activeOp = action as ActorOp;
					cachedState = fsm.Fsm.ActiveState;
					GregBugger.Log ("Changed activeOp to ["+activeOp.ToString()+"], running Fsm state is ["+fsm.Fsm.ActiveStateName+"]");
				}
			}

		}

		if (isUserControlled)
						ExecuteControlled ();
				else
						ExecuteAuto ();
 
	}

	void OnGUI()
	{
		
		if(leftHandBuzz != null)
			GUI.Label(new Rect(10, 400, 300, 20), "left "+ leftHandBuzz.Perc);
		
		if(rightHandBuzz != null)
			GUI.Label(new Rect(10, 420, 300, 20), "right "+ rightHandBuzz.Perc);
	}

	void OnControllerColliderHit(ControllerColliderHit info)
	{

	}

	public void AddState<T> (ActorState state) where T : ActorState
	{
		if(!states.ContainsKey(typeof(T)))
			states.Add (typeof(T), state);
	}

	public void RequestState<T>() where T : ActorState
	{
		if(!Application.isPlaying) return;

		if(!states.ContainsKey(typeof(T)))
		{
			states.Add(typeof(T), (T)Activator.CreateInstance (typeof(T), this, false));
		}
	}


 
	
	//============================================================
	//ANIM
	//============================================================
	public enum AnimReport { ENTER, UPDATE, EXIT }
	public void OnAnimReport(AnimReport report)
	{
		if(activeState != null)
		{
			switch(report)
			{
			case AnimReport.ENTER:
				activeState.OnAnimBegin();
				break;
			case AnimReport.UPDATE:
				break;
			case AnimReport.EXIT:
				activeState.OnAnimEnd();
				break;
			}

		}
		else
		{
			GregBugger.LogWarning ("-- AnimReport ["+report.ToString()+"] received without active state");
		}
	}

	public void OnAnimEvent(string msg)
	{
		activeState.OnAnimEvent(msg);
	}

	//===========================================================
	//OP CALLS
	//===========================================================
	public void TriggerOp(ActorOp op)
	{
		/*if(activeOp != null)
			activeOp.End();
		activeOp = op;
		activeOp.Begin ();*/
	}

	public void TriggerOp(ActorOp.DefaultOp defaultOp)
	{
		/*if(activeOp != null)
			activeOp.End();
		activeOp = defaultOps[defaultOp];
		activeOp.Begin();*/
	}

 
	//===========================================================
	//===========================================================
	/*
	 * CONTEXT
	  _______  _    _  _____  ______   _    __ 
  		| |   | |  | |  | |  | |  \ \ | |  / / 
  		| |   | |==| |  | |  | |  | | | |=< <  
  		|_|   |_|  |_| _|_|_ |_|  |_| |_|  \_\ 
	*/
	//===========================================================
	//===========================================================
	//Make all the decisions here. The states are merely for animations and 
	//calling back the enemy to do physical stuff. States don't get to think.
	protected virtual void Execute()
	{
		if(activeOp != null)
			activeOp.Execute();

		if (activeState != null)
			activeState.Execute ();
	}
	protected virtual void ExecuteControlled()
	{
		Execute ();
	}

	protected virtual void ExecuteAuto()
	{
		Execute ();
	}

	//===========================================================
	//EXTERNAL CALLS

	public void SetActiveArea(Area area)
	{
		ActiveArea = area;
	}

}
