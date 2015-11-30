using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StickyButton : MonoBehaviour {


		public Image sprite;
		public string normalSpriteName, pressedSpriteName;

		StickyGroup group;

		bool isPressed;

		bool dirty;

	// Use this for initialization
	void Start () {
				SetSprite (false);

				group = transform.parent.gameObject.GetComponent<StickyGroup> ();

				if (group != null) {

						group.RegisterButton (this);
				}
	}

 
		void OnEnable()
		{
				//dirty = true;
		 
		}
	
	
 

		public void SetSprite(bool isPressed)
		{
				this.isPressed = isPressed;

				if (!isPressed) {
						sprite.name = normalSpriteName;
				} else {
						sprite.name = pressedSpriteName;
				}
		}

		void Pressed()
		{
				if (group != null) {

						//SetSprite (true);
						group.ButtonPressed (this);

				} else {
						Debug.LogError ("Sticky button doesn't have a sticky group [" + gameObject.name + "]");
				}
		}
}
