using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

public class TypeManager 
{

	static TypeManager instance;
	public static TypeManager Instance { get { if(instance == null) instance = new TypeManager(); return instance;} }

	Dictionary<Type, OpModul> opModuls = new Dictionary<Type, OpModul>();
	Dictionary<Type, ActorOp> chModuls = new Dictionary<Type, ActorOp>();
 

	public TypeManager()
	{
		instance = this;

		foreach (Type t in Assembly.GetExecutingAssembly().GetTypes()) 
		{

			if(t.IsSubclassOf(typeof(OpModul)))
			{
				opModuls.Add(t, Activator.CreateInstance(t) as OpModul);
			}

			if(t.IsSubclassOf(typeof(ActorOp)))
			{
				//chModuls.Add(t, Activator.CreateInstance(t) as CharacterOp);
			}
 
		}
	}

	public OpModul GetOpModul(string name)
	{
		Type t = Type.GetType (name);

		if (t != null)
			return GetOpModul (t);

		return null;

	}

	public OpModul GetOpModul(Type t)
	{
		if(opModuls.ContainsKey(t))
		   return opModuls[t];

		return null;
	}

}
