using UnityEngine;
using System.Collections;
using System;

public class RequestPack 
{

	public readonly Request request;
	public readonly string json;
	public readonly Action<string> callback;

	public RequestPack(Request req, string json, Action<string> callback)
	{
		this.request = request;
		this.json = json;
		this.callback = callback;

		int a = 0;
	}

}
