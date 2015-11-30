using UnityEngine;
using System.Collections;

public class PersonalInfo : PackObject
{

	[Pack]
	public string Name { get; set; }

	[Pack]
	public string UserName { get; set; }
	[Pack]
	public string Password { get; set; }


}
