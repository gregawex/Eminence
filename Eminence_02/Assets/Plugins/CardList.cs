using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Division { AETERNA, IXION, WILKURSE, HOLLOWS, HARLEQUIN, SCION }
public enum Rarity {VERY_COMMON, COMMON, FAIRLY_RARE, VERY_RARE, EXTREMELY_RARE, YOU_WILL_NEVER_SEE_THIS_CARD }
public enum PlayerControl { LOCAL, REMOTE, AI }

[System.Serializable]
public class CardList : ScriptableObject {
	
	//----------------------------------------------------------------------------
	// Public Variables:
	//----------------------------------------------------------------------------
	
	public List<CardItem> cardList = new List<CardItem>();
}

