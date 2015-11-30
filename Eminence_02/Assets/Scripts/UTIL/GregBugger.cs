#if UNITY_EDITOR
//#define ENABLE_GREGBUGGER
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Greg: Gone and forgotten cause hes a noob -- no more infecting the project with his fat fingers :D

public  class GregBugger : MonoBehaviour 
{

	public static bool on = false;
	public static bool toConsole = false;

	static Color c;
	
	static GDE [,] entries;

	static GameObject instance;

	
	public enum Channel { DEFAULT}
	public  static Channel activeChannel = Channel.DEFAULT;
 

	static string stringToEdit;
	
	public static float vSliderValue = 280.0F;
	static int bufferSize = 300;
	static int pointer;

	static GUIStyle labelStyle;

	static Font myFont;
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

	}

	public static void Toggle()
	{
		 
		if (instance == null) {
						Init ();
			on = true;

				} else {

						if (on)
								on = false;
						else
								on = true;
				}
		 
	}


	static void Init()
	{
		Debug.Log ("GregBugger Init..");

				instance = new GameObject("Greg's Debugger");
				instance.AddComponent<GregBugger>();

				stringToEdit = "";
				
				entries = new GDE[5, bufferSize];
				
				for(int a=0; a< 5; a++)
				{
					for(int b=0; b< bufferSize; b++)
					{
						entries[a, b] = new GDE();
						entries[a, b].text = ""+a+", "+b;
					}
				}

			
		 myFont = (Font)Resources.Load("Kraken/Fonts/PressStart2P", typeof(Font));

			pointer = bufferSize - 20;
			
			// Create style for a button


			 

	
	}
	
	//Kryptonite
	
	public enum TraceMode { NORMAL, ERROR, WARNING, OK, BLUE, SYSTEM }
	
	public  static void Log(string text)
	{
		if(entries == null) Init ();

		if(activeChannel != Channel.DEFAULT) return;
		//c = Color.white;
		SetMode (TraceMode.NORMAL);
		
		push(text, 0);

	}

	public static void LogError(string text)
	{
		if(entries == null) Init ();

		if(activeChannel != Channel.DEFAULT) return;
		SetMode (TraceMode.ERROR);
		
		push(text, 0);
	}

	public static void LogWarning(string text)
	{
		if(entries == null) Init ();

		if(activeChannel != Channel.DEFAULT) return;
		SetMode (TraceMode.WARNING);
		
		push(text, 0);
	}

	public static void LogHighlight(string text)
	{
		if(entries == null) Init ();

		if(activeChannel != Channel.DEFAULT) return;
		SetMode (TraceMode.BLUE);
		
		push(text, 0);
	}

	public static void LogSystem(string text)
	{
		if(entries == null) Init ();
		
		if(activeChannel != Channel.DEFAULT) return;
		SetMode (TraceMode.SYSTEM);
		
		push(text, 0);
	}



	[Obsolete]
	public static void Push(string text, int column, TraceMode mode)
	{

		if(activeChannel != Channel.DEFAULT) return;

		SetMode(mode);
		
		push(text, column);

	}

	[Obsolete]
	public  static void Push(Channel channel, string text, int column)
	{

		if(activeChannel != channel) return;
		c = Color.white;
		
		push(text, column);

	}

	[Obsolete]
	public  static void Push(Channel channel, string text, int column, TraceMode mode)
	{

		if(activeChannel != channel) return;
		SetMode(mode);
		
		push(text, column);

	}

	[Obsolete]
	public  static void PushOnce(string text, int column)
	{

		if(activeChannel != Channel.DEFAULT) return;

		if(entries[column, 19].text == text) return;
			
		c = Color.white;
		
		push(text, column);

	}

	[Obsolete]
	public  static void PushOnce(string text, int column, TraceMode mode)
	{

		if(activeChannel != Channel.DEFAULT) return;

		if(entries[column, 19].text == text) return;

		
		SetMode(mode);
		
		push(text, column);

	}	
	[Obsolete]
	public  static void PushOnce(Channel channel, string text, int column)
	{

		if(activeChannel != channel) return;
		
		if(entries[column, 19].text == text) return;
			
		c = Color.white;
		
		push(text, column);

	}

	[Obsolete]
	public  static void PushOnce(Channel channel, string text, int column, TraceMode mode)
	{

		if(activeChannel != channel) return;

		if(entries[column, 19].text == text) return;

		
		SetMode(mode);
		
		push(text, column);

	}	

	[Obsolete]
	public  static void Pin(string text, int column, int row)
	{

		if(activeChannel != Channel.DEFAULT) return;
	
		c = Color.white;
		
		
		pin(text, column, row);

	}	

	[Obsolete]
	public  static void Pin(string text, int column, int row,  TraceMode mode)
	{

		if(activeChannel != Channel.DEFAULT) return;

		SetMode(mode);
		
		pin(text, column, row);

	}	

	[Obsolete]
	public  static void Pin(Channel channel, string text, int column, int row)
	{

		if(activeChannel != channel) return;

		c = Color.white;
		
		pin(text, column, row);

	}	

	[Obsolete]
	public  static void Pin(Channel channel, string text, int column, int row,  TraceMode mode)
	{

		if(activeChannel != channel) return;

		SetMode(mode);
		
		pin(text, column, row);

	}
	

	void OnGUI()
	{
	
		if(!on) return;
		if(entries == null) return;

		labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		labelStyle.fontSize = 10;
		
		// Load and set Font

		labelStyle.font = myFont;
	
		
		 
 
		
		
		vSliderValue = GUI.VerticalSlider(new Rect(Screen.width - 10, 0, 200, 200), vSliderValue, 0, bufferSize - 20);
		pointer = (int)vSliderValue;


		
		//a is the column
		int a = 0;
		
		//for(int a=0; a< 5; a++)
		//{
			GUI.Box(new Rect(-10 + (a * 200), -10, Screen.width + 20, 220), "");
			
			for(int b=pointer; b < pointer + 20; b++)
			{
				GUI.contentColor = entries[a, b].c;
				if(!entries[a, b].pin) GUI.Label(new Rect ( 20 + (a * 200), 0 + ((b - pointer) * 10) , Screen.width - 50, 20), entries[a, b].text, labelStyle );
				
				//if(entries[a, b].pin) GUI.Box (new Rect ( 20 + (a * 200), 0 + ((b - pointer) * 20) , 200, 20), entries[a, b].text );
			}
		//}
		

	}


	/*private static void calc()
	{ 

		Mathos.Parser.MathParser p = new Mathos.Parser.MathParser(true, true, true);
				
				p.LocalVariables.Add ("x", 100);
				p.LocalVariables.Add ("shipspecific", 0);
		

			 
			 	bool valid = true;
				float f = p.Parse(stringToEdit, out valid);
					
				if(valid)
				GregBugger.Instance.Push(GregBugger.Channel.MATHOS, f.ToString() , 2);
		   		else
				GregBugger.Instance.Push(GregBugger.Channel.MATHOS, "Invalid formula" , 2, TraceMode.ERROR);
				
				stringToEdit = "";

	}*/
 
	private  static void SetMode(TraceMode mode)
	{

		switch(mode)
		{
		case TraceMode.NORMAL: c = Color.white; break;
		case TraceMode.WARNING: c = Color.yellow; break;
		case TraceMode.ERROR: c = Color.red; break;
		case TraceMode.OK: c = Color.cyan; break;
		case TraceMode.BLUE: c = Color.magenta; break;
		case TraceMode.SYSTEM: c = Color.grey; break;
		}

	}
	
	private  static void pin(string text, int column, int row)
	{

		entries[column, row].c = c;
		entries[column, row].text = text;
		entries[column, row].pin = true;

	}
	
	private  static void push(string text, int column)
	{

		for(int i=0; i< bufferSize - 1; i++)
		{
			entries[column, i].c = entries[column, i + 1].c;
			entries[column, i].text = entries[column, i + 1].text;
		}
		
		entries[column, bufferSize - 1].c = c;
		entries[column, bufferSize - 1].text = text;
		
		if(toConsole && c == Color.white) 			Debug.Log (text);
		else if(toConsole && c == Color.red) 			Debug.LogError (text);

	}
	
	public static void ClearColumn(Channel targetChannel, int col)
	{

				if (activeChannel != targetChannel)
						return;
		for(int i=0; i< bufferSize; i++)
		{
			entries[col, i] = new GDE();
		}

	}
	
 
}

public class GDE
{
	public string text;
	public Color c;
	public bool pin;
}

//To The Devil's House Take Me
