using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardRoster 
{

	static CardRoster instance;
	public static CardRoster Instance { get { if(instance == null) instance = new CardRoster(); return instance; } }

	//<GUID, CardItem>
	Dictionary<string, CardItem> cards = new Dictionary<string, CardItem>();

 

	public void FeedCardList(CardList cardList)
	{


		foreach(CardItem c in cardList.cardList)
		{
			if(!cards.ContainsKey(c.guid))
			{
				cards.Add(c.guid, c);

				Debug.Log ("Card "+c.name+" added");
			}
		}
	}
}
