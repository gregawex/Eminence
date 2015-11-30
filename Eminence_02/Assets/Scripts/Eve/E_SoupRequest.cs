using UnityEngine;
using System.Collections;

public class E_SoupRequest : BaseEvent {

	public string soupName;

	public E_SoupRequest(string soupName)
	{
		this.soupName = soupName;
	}
}
