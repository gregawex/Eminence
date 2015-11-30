using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class CardVar : Attribute
{

}

[System.Serializable]
public class CardItem : ScriptableObject {
	
	//----------------------------------------------------------------------------
	// Public Variables:
	//----------------------------------------------------------------------------


	[CardVar]
	public string name;
	[CardVar]
	public string guid;
	[CardVar]
	public Division division;
	[CardVar]
	public Rarity rarity;
	[CardVar]
	public int topValue, bottomValue, leftValue, rightValue;
	[CardVar]
	public int stars;

	[CardVar]
	public Texture2D cardSprite;
	[CardVar]
	public Texture2D promoSprite;
	[CardVar]
	public Texture2D teaserSprite;
	
	void Awake()
	{
		//guid = Guid.NewGuid().ToString();
	}
	

	

	
 
}

