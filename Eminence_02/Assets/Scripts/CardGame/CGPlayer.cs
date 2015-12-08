using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGPlayer
{

	string userID;
	string [] cardTokens;

	public CardItem [] CardItems { get; private set; }

	public PlayerColor Color { get; private set; }

	public CardInstance [] CardInstances { get; private set; }

	public CGPlayer(string userID, CardPack cardPack, PlayerColor color)
	{
		this.userID = userID;
		this.cardTokens = cardTokens;

		this.Color = color;
		 
		CardItems = CardRoster.Instance.GetCardItems(cardPack.CardTokens.ToArray());
	}

	public void FeedCardInstances(CardInstance [] cardInstances)
	{
		this.CardInstances = cardInstances;
	}
}
