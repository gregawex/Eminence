﻿using UnityEngine;
using System.Collections;

public class JustTestRotate : MonoBehaviour {

	public float speed = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		this.transform.Rotate(Vector3.up, speed);
	}
}
