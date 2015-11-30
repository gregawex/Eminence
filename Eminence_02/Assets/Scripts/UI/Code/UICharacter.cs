using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour {


		public Image characterSprite, shadowSprite;
	public string characterSpriteName, shadowSpriteName;
	

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		public void SetGender(int i)
		{
				if (i == 0) {

						characterSprite.name = "character-male-base";
						shadowSprite.name = "character-male-shadow";
						//characterSprite.transform.localScale = new Vector3 (characterSprite., characterSprite.mainTexture.height);
						//characterSprite.MakePixelPerfect ();
						//shadowSprite.MakePixelPerfect ();
				} else {
						characterSprite.name = "character-female-base";
						shadowSprite.name = "character-female-shadow";
						//characterSprite.transform.localScale = new Vector3 (characterSprite.mainTexture.width, characterSprite.mainTexture.height);
						//characterSprite.MakePixelPerfect ();
						//shadowSprite.MakePixelPerfect ();
				}

				int a = 0;


		}
}
