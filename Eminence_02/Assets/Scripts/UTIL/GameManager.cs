using UnityEngine;
using System.Collections;



public static class GameManager 
{



	public static InputMode Input { get; private set; }

	public static void LandingInit(InputMode input)
	{
		Input = input;
	}

}
