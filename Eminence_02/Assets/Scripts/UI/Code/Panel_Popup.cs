using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel_Popup : BasePanel {


	public Text text;

	int a=0;
	PopupButtonSetup popupButtonSetup;
	string buttonText_positive, buttonText_negative;

	public ButtonSetup one, two;


	public void Open(PopupButtonSetup popupButtonSetup, string text, string buttonText_positive, string buttonText_negative)
	{
		this.popupButtonSetup = popupButtonSetup;
		this.buttonText_positive = buttonText_positive;
		this.buttonText_negative = buttonText_negative;
		this.text.text = text.ToUpper();

		switch(popupButtonSetup)
		{
		case PopupButtonSetup.SINGLE:
			one.gameObject.SetActive(true);
			two.gameObject.SetActive(false);
			one.positive.name = buttonText_positive;
			break;
		case PopupButtonSetup.TWO_CHOICE:
			one.gameObject.SetActive(false);
			two.gameObject.SetActive(true);

			two.positive.name = buttonText_positive;
			two.negative.name = buttonText_negative;
			break;
		}



		 

	}
 
}
