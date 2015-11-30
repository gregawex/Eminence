using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RenderSetter : MonoBehaviour {


	public float ambientIntensity;
	public Color ambientLight;
	public UnityEngine.Rendering.AmbientMode ambientMode;

	public bool fog;
	public FogMode fogMode;
	public Color fogColor;
	public float fogDensity;
	public float fogStartDistance, fogEndDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	[ExecuteInEditMode]
	void Update () 
	{
	
		RenderSettings.ambientIntensity = this.ambientIntensity;
		RenderSettings.ambientLight = this.ambientLight;
		RenderSettings.ambientMode =  ambientMode;

		RenderSettings.fog = this.fog;
		RenderSettings.fogMode = fogMode;
		RenderSettings.fogColor = fogColor;
		RenderSettings.fogDensity = fogDensity;
		RenderSettings.fogStartDistance = fogStartDistance;
		RenderSettings.fogEndDistance = fogEndDistance;
	}
}
