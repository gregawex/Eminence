using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using HutongGames.PlayMaker;


public class SoupItem : MonoBehaviour {


	SoupItemPack pack;
	public SoupItemPack Pack { get { return pack; } }

	public enum SoupItemType { INTERACTIVE, AUTOMATIC }
	SoupItemType type;
	

	public string guid;
	public string name;

	public static string SLOTNAME { get { return Application.loadedLevelName+"_soup"; } }
	
	//private GameItemPack pack;

	So_ProcessInput [] inputStates;
	
	protected PlayMakerFSM fsm;

	public bool initializing;

	public bool shuttingDown;

	public string CurrentlyExecutingSequence { get; set; }

	protected virtual void Start()
	{

		initializing = true;
		fsm = GetComponent<PlayMakerFSM>();



		type = SoupItemType.AUTOMATIC;


		
		

		List<So_ProcessInput> pis = new List<So_ProcessInput>();

		foreach(FsmState state in fsm.FsmStates)
		{

			
			foreach(FsmStateAction act in state.Actions)
			{
				So_ProcessInput pr = act as So_ProcessInput;
				
				if(pr != null)
				{
					pis.Add(pr);
				}
			}
		}
		
		inputStates = pis.ToArray();

		if(inputStates.Length > 0)
			type = SoupItemType.INTERACTIVE;
		
		
		if(string.IsNullOrEmpty(guid))
		{
			//GregPacker.SlotName = Application.loadedLevelName+"_items";
			pack = GregPacker.Create<SoupItemPack>(SLOTNAME, null);

			foreach(So_ProcessInput input in inputStates)
				pack.AddEntry(input.sequenceID.Value, "Default");

			guid = pack.Guid.ToString();
			name = gameObject.name;
			
			
			if(gameObject.GetComponent<PlayMakerFSM>() == null)
				fsm = gameObject.AddComponent<PlayMakerFSM>();
			
 
			
			//GregPacker.Pack(SLOTNAME);
			GregPacker.MakeDirty(SLOTNAME);
		}
		else
		{
			fsm = GetComponent<PlayMakerFSM>();
			
			//pack.ActiveStateName = "Default";
			
			if(GregPacker.GetPackObject<SoupItemPack>(SLOTNAME, guid) == null)
			{
				pack = GregPacker.Create<SoupItemPack>(SLOTNAME, guid);

				foreach(So_ProcessInput input in inputStates)
					pack.AddEntry(input.sequenceID.Value, "Default");

				guid = pack.Guid.ToString();
				
				//GregPacker.Pack(SLOTNAME);
				GregPacker.MakeDirty(SLOTNAME);
			}
			else
			{
				pack = GregPacker.GetPackObject<SoupItemPack>(SLOTNAME, guid);
			}
			
		}

		switch(type)
		{
		case SoupItemType.INTERACTIVE:

			break;
		case SoupItemType.AUTOMATIC:


			break;

		}


	}

	void OnEnable()
	{
		this.shuttingDown = false;
	}


	
	// Update is called once per frame
	protected virtual void Update () {


	
	}

	public string GetSavedState()
	{
		if(CurrentlyExecutingSequence != null)
		{

			SoupItemPackEntry entry = Pack.GetActiveEntry(CurrentlyExecutingSequence);
 

			if(entry != null)
				return entry.ActiveStateName;

		}

		return null;
	}

	
	public void SaveState(string stateName)
	{

		//pack.SetStateName( stateName);
		//GregPacker.Pack(SLOTNAME);
		GregPacker.MakeDirty(SLOTNAME);
		
	}

	public void NotInitializing()
	{
		if(initializing)
			initializing = false;
	}

	public void FinishedWithSequence()
	{
		Soup.Instance.CloseAll();
	}
}
