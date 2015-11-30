using UnityEngine;
using System.Collections;
using System;

public class E_ErrorOccured : BaseEvent 
{

	public readonly ErrorMsg msg;

	public readonly Action positiveCallback;
	public readonly Action negativeFeedback;

	public E_ErrorOccured(ErrorMsg msg, Action pos, Action neg)
	{
		this.msg = msg;
		this.positiveCallback = pos;
		this.negativeFeedback = neg;
	}



}
