using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AmbienceController : MonoBehaviour {


	public Color ambientLight;
	// Use this for initialization
	void Start () {
	
	}

#if UNITY_EDITOR
	// Update is called once per frame
	[ExecuteInEditMode]
	void Update () {
	
		RenderSettings.ambientLight = this.ambientLight;
	}
#endif
}
