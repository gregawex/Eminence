using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathAnchor : MonoBehaviour {

	Dictionary<string, PathNode> pool;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos() {
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (transform.position, 0.1f);

		if (pool != null) 
		{
			foreach(KeyValuePair<string,PathNode> kvp in pool)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawSphere (kvp.Value.Position, 0.1f);

				foreach(KeyValuePair<PathNode.Directions, PathNode> k in kvp.Value.Connections)
					Gizmos.DrawLine (k.Value.Position, kvp.Value.Position);
			}
		}
	}

	public void FeedPool(Dictionary<string, PathNode> pool)
	{
		this.pool = pool;
	}
}
