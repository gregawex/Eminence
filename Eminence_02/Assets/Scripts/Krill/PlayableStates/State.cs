using UnityEngine;
using System.Collections;

public abstract class State 
{
	public abstract void Begin();
	public abstract void Execute();
	public abstract void End();

	public State()
	{

	}
}
