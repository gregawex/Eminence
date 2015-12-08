using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardPack : PackObject 
{
	public readonly string SLOTNAME = "CARD_PACK";

	[Pack]
	public List<string> CardTokens;

}
