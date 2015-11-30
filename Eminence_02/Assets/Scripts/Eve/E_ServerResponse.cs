using UnityEngine;
using System.Collections;

public class E_ServerResponse : BaseEvent {

	public readonly string json;

	public E_ServerResponse(string json)
	{
		this.json = json;
	}
}
