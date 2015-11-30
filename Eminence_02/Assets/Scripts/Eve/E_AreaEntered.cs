using UnityEngine;
using System.Collections;

public class E_AreaEntered : BaseEvent 
{

	public Area Area { get; private set; }

	public Actor Actor { get; private set; }

	public E_AreaEntered(Area area, Actor enteredCharacter)
	{
		this.Area = area;
		this.Actor = enteredCharacter;
	
	}

	public string GetAreaName()
	{
		return Area.name;
	}

	/*public override void Destroy ()
	{
		area = null;
	}*/

	//[OutFunc]
	public void OnEvent()
	{
		
	}

	public override string ToString ()
	{
		return string.Format ("[E_AreaEntered: Area={0}, Controller={1}]", Area, Actor);
	}
}
