using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Inventory : PackObject 
{

	[Pack]
	public List<CardPack> CardPacks { get; set; }

}
