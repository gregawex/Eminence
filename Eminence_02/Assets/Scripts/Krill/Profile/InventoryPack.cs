using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryPack : PackObject
{
	[Pack]
	public List<string> ItemIDs { get; set; }
	[Pack]
	public List<string> WaypointIDs { get; set; }

 
}
