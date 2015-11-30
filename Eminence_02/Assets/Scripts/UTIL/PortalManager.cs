using UnityEngine;
using System.Collections;

public class PortalManager : MonoBehaviour {


	protected Portal [] portals;
	// Use this for initialization
	void Start () {

		portals = GameObject.FindObjectsOfType<Portal>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Open(Portal portal)
	{

	}
}
