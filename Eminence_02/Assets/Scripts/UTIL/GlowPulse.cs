using UnityEngine;
using System.Collections;

public class GlowPulse : MonoBehaviour {
	
	//public MeshRenderer fader;
	
	public Material glowMaterial;
	public AnimationCurve fadeAnim;
	public float minValue = 0;
	public float maxValue = 1;
	public float speedMul = 1;
	
	
	
	void Start()
	{
		
	}
	
	void Update()
	{
		if (glowMaterial == null)
						return;

		Material m = glowMaterial;
		float eval = fadeAnim.Evaluate (Time.time * speedMul);

		float range = maxValue - minValue;
		float val = Mathf.Clamp01(minValue + (range * eval));
		
		m.SetColor ("_TintColor", new Color (1, 1, 1, val));
	}
}
