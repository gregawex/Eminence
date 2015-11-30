using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameItemPack : PackObject
{

	[Pack]
	public string ActiveStateName { get; set; }

	[Pack]
	public string ActiveOp { get; set; }

}
