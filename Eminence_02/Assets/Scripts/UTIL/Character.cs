using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using System;
using System.Reflection;

[RequireComponent(typeof(AdventureController))]
[RequireComponent(typeof(AIBot))]
public class Character : MonoBehaviour {

	protected Dictionary<Type, State> states = new Dictionary<Type, State> ();

	protected AdventureController adventureCtrl;
	public AdventureController AdventureCtrl { get { return adventureCtrl; } }
	protected AIBot bot;
	public AIBot Bot { get { return bot; } }

	public Area CurrentArea { get; set; }
	public Animator Animator { get ; private set; }

	protected virtual void Awake()
	{
		adventureCtrl = GetComponent<AdventureController> ();
		bot = GetComponent<AIBot> ();
		Animator = GetComponent<Animator> ();
	}
	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
}
