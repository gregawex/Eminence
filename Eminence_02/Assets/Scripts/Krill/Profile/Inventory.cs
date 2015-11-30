using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Inventory : PackObject 
{

	[Pack]
	public int HardCurrency { get; set; }

	[Pack]
	public int SoftCurrency { get; set; }

	[Pack]
	List<CardPack> CardPacks { get; set; }

}
