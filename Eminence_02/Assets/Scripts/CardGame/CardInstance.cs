using UnityEngine;
using System.Collections;




public class CardInstance : MonoBehaviour {


	public CardItem cardItem;

	public CardPip top, left, right, bottom;

	public void Init(CardItem cardItem, PlayerColor currentColor)
	{
		this.cardItem = cardItem;

		SetupPip(top, 		cardItem.topValue, 		currentColor);
		SetupPip(bottom, 	cardItem.bottomValue, 	currentColor);
		SetupPip(left, 		cardItem.leftValue, 	currentColor);
		SetupPip(right, 	cardItem.rightValue, 	currentColor);
	}

	void SetupPip(CardPip pip, int val, PlayerColor color)
	{
		if(val >= 1 && val < 10)
			pip.text.text = val.ToString();
		else if(val == 10)
			pip.text.text = "A";
		else
			Debug.LogError ("Invalid card value ["+val+"] in cardItem ["+cardItem.guid+"]");

		 
		switch(color)
		{
		case PlayerColor.RED:
			pip.image.color = Color.red;
			break;
		case PlayerColor.BLUE:
			pip.image.color = Color.blue;
			break;
		}
	}

	public void Clicked()
	{
		Debug.Log ("Clicked");
	}


	 
 
}
