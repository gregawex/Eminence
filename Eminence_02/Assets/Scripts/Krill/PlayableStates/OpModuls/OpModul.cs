using UnityEngine;
using System.Collections;

public abstract class OpModul 
{
	public virtual void Begin(){ GregBugger.Log (""+pc.name+" is executing opModul ["+GetType().Name+"]");}
	public virtual void Execute(){}
	public virtual void End()
	{
		if (nextOM == null) 
		{
			pc.IsUserControlled = true;
		}
	}

	public OpModul nextOM;

	protected PlayableCharacter pc;

	public void SetCharacter (PlayableCharacter character)
	{
		this.pc = character as PlayableCharacter;
	}
 
}
