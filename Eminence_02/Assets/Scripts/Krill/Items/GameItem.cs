using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ExecuteInEditMode]
public class GameItem : MonoBehaviour, EventListener
{

	GameItemPack gameItemData;

	public static string SLOTNAME { get { return Application.loadedLevelName+"_items"; } }

	public Transform point { get { return points[0]; } }
	public Transform [] points;
	public Transform [] reachPoints;
	public Transform focusPoint;
	public string guid;
	public string itemName;

	public bool displaysFocusIcon;


	//private GameItemPack pack;

	protected PlayMakerFSM fsm;
	public PlayMakerFSM FSM { get { return fsm; } }

	GameItemPack pack;
	public GameItemPack Pack { get { return pack; } }

	string cachedRuntimeState;
	IS_Action cachedRuntimeAction;
	Actor targetActor;
	public Actor TargetActor { get { return targetActor; } }


	public bool initializing;

	//This guid is only used for the itemPool in screenManager 
	Guid itemPoolGuid;
	public string ItemPoolGuid { get { return itemPoolGuid.ToString(); } }


	

	[ExecuteInEditMode]
	protected virtual void Awake()
	{
		initializing = true;

		//Eve.Listen<E_ItemEvent>(OnItemEvent, this);
		//GregPacker.SlotName = Application.loadedLevelName+"_items";
		 
 
		if(this.GetComponent<BoxCollider>() == null)
		{
			this.gameObject.AddComponent<BoxCollider>();
			this.gameObject.layer = 11;
		}



		if(Application.isPlaying)
		{
			//GregBugger.LogHighlight("GameItem [name: "+name+", gameObject: "+gameObject.name+"] unpacking");
			//GregPacker.Unpack(SLOTNAME);

			itemPoolGuid = Guid.NewGuid();
			
		}
		//==================
		//On Component add
		if(string.IsNullOrEmpty(guid))
		{
			pack = GregPacker.Create<GameItemPack>(SLOTNAME, null);
			pack.ActiveStateName = "Default";
			guid = pack.Guid.ToString();


			if(gameObject.GetComponent<PlayMakerFSM>() == null)
				fsm = gameObject.AddComponent<PlayMakerFSM>();
 
 			if(focusPoint == null)
			{
				GameObject g = new GameObject();
				g.transform.parent = this.transform;
				g.transform.localPosition = Vector3.zero;
				g.transform.rotation = this.transform.rotation;

				g.name = "focus";

				this.focusPoint = g.transform;
			}

			GameObject go = new GameObject("point");

			points = new Transform[1];

			points[0] = go.transform;
			points[0].position = this.transform.position;
			points[0].parent = this.transform;

			//GregPacker.Pack(SLOTNAME);
			GregPacker.MakeDirty(SLOTNAME);
		}
		else
		{
			fsm = GetComponent<PlayMakerFSM>();

			//pack.ActiveStateName = "Default";

			if(GregPacker.GetPackObject<GameItemPack>(SLOTNAME, guid) == null)
			{
				pack = GregPacker.Create<GameItemPack>(SLOTNAME, guid);
				pack.ActiveStateName = "Default";
				guid = pack.Guid.ToString();

				//GregPacker.Pack(SLOTNAME);
				GregPacker.MakeDirty(SLOTNAME);
			}
			else
			{
				pack = GregPacker.GetPackObject<GameItemPack>(SLOTNAME, guid);
			}
	
		}




		if(pack != null && string.IsNullOrEmpty(pack.ActiveStateName))
			pack.ActiveStateName = "Default";



	}

	protected virtual void Start()
	{
		if(gameObject.activeSelf)
			OnEnable ();
	}

	protected virtual void OnEnable()
	{
		if(Application.isPlaying)
			SceneManager.Instance.ItemEnabled(this);
	}

	protected virtual void OnDisable()
	{
		if(Application.isPlaying)
			SceneManager.Instance.ItemDisabled(this);
	}

	


	protected virtual void Update()
	{
	
		if(initializing)
			initializing = false;

		if(Application.isPlaying)
		{
			if(cachedRuntimeState != fsm.Fsm.ActiveStateName)
			{
				cachedRuntimeState = fsm.Fsm.ActiveStateName;
				cachedRuntimeAction = null;

				if(fsm.Fsm.ActiveState != null)
				foreach(FsmStateAction stateAction in fsm.Fsm.ActiveState.Actions)
				{
					IS_Action act = stateAction as IS_Action;

					//NOTE: this can be NULL too!
					cachedRuntimeAction = act;
				}
			}

		}
	}

	/*void OnItemEvent(BaseEvent ev)
	{
		E_ItemEvent e = ev as E_ItemEvent;

		if(cachedRuntimeAction != null && e.target == this)
		{
			targetActor = e.actor;
			cachedRuntimeAction.OnItemEvent(e);

		}
	}*/

	public virtual void OnUse(Actor targetActor, ItemEvent itemEvent)
	{
		this.targetActor = targetActor;
		if(cachedRuntimeAction != null)
			cachedRuntimeAction.OnItemEvent(itemEvent);
	}


	public virtual void OnUseFinished()
	{

	}

 

	protected virtual void OnDrawGizmos()
	{

		foreach(Transform t in points)
		{
			if(t != null)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawSphere(t.position, 0.2f);
			}

		}

		if(focusPoint != null)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(focusPoint.position, 0.1f);
		}
	}

	public string GetSavedState()
	{
		return pack.ActiveStateName;
	}

	public void SaveState(string stateName)
	{
		pack.ActiveStateName = stateName;
		//GregPacker.Pack(SLOTNAME);
		GregPacker.MakeDirty(SLOTNAME);
	}

	public void SetItemOp(string op)
	{
		pack.ActiveOp = op;
	}

	protected virtual void Reset()
	{

	}

	public virtual Transform GetFocusPoint()
	{
		return focusPoint;
	}

	public virtual Transform GetReachPoint()
	{
		if(reachPoints != null && reachPoints.Length > 0)
		{
			return reachPoints[0];
		}

		return null;
	}
}
