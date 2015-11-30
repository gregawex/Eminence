using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseSceneManager : MonoBehaviour {


	//public  UIPrefs uiPrefs;
	static BaseSceneManager instance;
	public static BaseSceneManager Instance { get { return instance; } }



	protected virtual void Awake()
	{
		instance = this;

		GameObject root = GameObject.Instantiate(Resources.Load("UIRoot", typeof(GameObject))) as GameObject;
		root.transform.localPosition = Vector3.zero;
		root.transform.localScale = Vector3.one;
		root.AddComponent (PersistentInfo.RootControlType_autoScene);
		root.SetActive(true);

	}
	// Use this for initialization
	protected virtual void Start () {
	

 

	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}


}
