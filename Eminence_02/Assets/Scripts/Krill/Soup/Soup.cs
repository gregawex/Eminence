using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soup : MonoBehaviour, EventListener 
{

	Dictionary<string, PlayMakerFSM> fsms;

	PlayMakerFSM activeFsm;

	static Soup instance;
	public static Soup Instance { get { return instance; } }



	Dictionary<string, SoupContext> contexts = new Dictionary<string, SoupContext>();

 
	public SoupContext ActiveContext { get; private set;}

	void Awake()
	{

		instance = this;

		Eve.Listen<E_SoupRequest>(OnSoupRequest, this);
	

		fsms = new Dictionary<string, PlayMakerFSM>();

		for(int i=0; i< this.transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);

			PlayMakerFSM fsm = child.gameObject.GetComponent<PlayMakerFSM>();

			if(fsm != null)
			{
				fsms.Add(child.name, fsm);
				child.gameObject.SetActive(false);

				contexts.Add(child.gameObject.name, new SoupContext(fsm));
			}
		}
	}
 

	public void OnSoupRequest(BaseEvent ev)
	{

		E_SoupRequest e = ev as E_SoupRequest;

		CloseAll();

	 

		if(fsms.ContainsKey(e.soupName) && SceneManager.Instance.ActivePC.name != e.soupName)
		{

			activeFsm = fsms[e.soupName];
			activeFsm.gameObject.SetActive(true);

		}
 		else
		{
				if(fsms.ContainsKey("ActivePC") && e.soupName == SceneManager.Instance.ActivePC.name)
				{
					activeFsm = fsms["ActivePC"];
					activeFsm.gameObject.SetActive(true);
				}
			else if(e.soupName != SceneManager.Instance.ActivePC.name)
			{
				activeFsm = fsms[e.soupName];
				activeFsm.gameObject.SetActive(true);
			}
				else
				{
					GregBugger.LogError("Invalid soup item ["+e.soupName+"]");
				}

			
		}

		if(activeFsm != null)
			ActiveContext = contexts[activeFsm.gameObject.name];
		else
			GregBugger.LogError("Can't set soup context: activeFSM is null");
	}



	public void CloseAll()
	{
		foreach(KeyValuePair<string, PlayMakerFSM> kvp in fsms)
		{
			(kvp.Value).gameObject.SetActive(false);
		}



	}

}

public class SoupContext
{

	public SoupContext(PlayMakerFSM fsm)
	{

	}
}
