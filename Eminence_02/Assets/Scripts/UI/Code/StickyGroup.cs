using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StickyGroup : MonoBehaviour {

		List<StickyButton> buttons = new List<StickyButton>();

		StickyButton activeButton;


		public void RegisterButton(StickyButton button)
		{

				buttons.Add (button);
			
		}

		public void ButtonPressed(StickyButton button)
		{
				activeButton = button;

				foreach (StickyButton b in buttons) {

						if (b == button) {
								b.SetSprite (true);
								continue;
						}

						b.SetSprite (false);
				}
		}
}
