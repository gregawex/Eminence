using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public static class PersistentInfo 
{

	static Dictionary<string, Type> rootControlTypes;
	public static Type RootControlType_autoScene 
	{ get 
		{ 
			string n = "RC_"+Application.loadedLevelName;
			if(rootControlTypes.ContainsKey(n))
				return rootControlTypes[n];
			else
			{
				ErrorMsg.LogError(ErrorCode.INTERNAL_ERROR, "Trying to access RootControlType ["+n+"] but it doesn't exist. Root controls must follow the naming convention RC_(activeSceneName)");
				return null;
			}
		} 
	}

	public static void ProcessExecAssembly()
	{
		rootControlTypes = new Dictionary<string, Type> ();

		Assembly a = Assembly.GetExecutingAssembly ();
		
			foreach(Type t in a.GetTypes())
			{
				if(t.IsSubclassOf(typeof(RootControl)))
				{
					rootControlTypes.Add(t.Name, t);
				}
			}
		
	}

}
