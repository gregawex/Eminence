using UnityEngine;
using System.Collections;

public class CamTarget : MonoBehaviour {

	void OnDrawGizmos() {
				// Draw a yellow sphere at the transform's position
				Gizmos.color = Color.black;
				Gizmos.DrawSphere (transform.position, 0.1f);

	}
}
