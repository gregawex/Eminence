using UnityEngine;
using System.Collections;

public class E_TaskFinished : BaseEvent 
{
	public readonly object sender;
	public readonly bool success;
	public readonly string taskName;

	public E_TaskFinished(object sender, bool success, string taskName)
	{
		this.sender = sender;
		this.success = success;
		this.taskName = taskName;
	}

}
