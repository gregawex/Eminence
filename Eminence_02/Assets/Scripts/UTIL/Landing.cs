using UnityEngine;
using System.Collections;

public class Landing : MonoBehaviour {


	public Material fader;
	public Material bg;
	public Texture awex, kraken;


	// Use this for initialization
	void Start () {
	
		GameManager.LandingInit(new IM_PC_Classic_PAC());

		fader.color = new Color (1, 1, 1, 1);
		//bg.SetTexture(
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
