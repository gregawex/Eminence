
//--- USING
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;

//-----
/*
//-------------------------------------------------------------------------------~~~


 ██████╗ ██████╗ ███████╗ ██████╗ ██████╗  █████╗  ██████╗██╗  ██╗███████╗██████╗ 
██╔════╝ ██╔══██╗██╔════╝██╔════╝ ██╔══██╗██╔══██╗██╔════╝██║ ██╔╝██╔════╝██╔══██╗
██║  ███╗██████╔╝█████╗  ██║  ███╗██████╔╝███████║██║     █████╔╝ █████╗  ██████╔╝
██║   ██║██╔══██╗██╔══╝  ██║   ██║██╔═══╝ ██╔══██║██║     ██╔═██╗ ██╔══╝  ██╔══██╗
╚██████╔╝██║  ██║███████╗╚██████╔╝██║     ██║  ██║╚██████╗██║  ██╗███████╗██║  ██║
 ╚═════╝ ╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═╝     ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝


//------------------------------------------------------------------------~~~
//
// an example of proper object hierarchy serialization. (C) 2014, Greg Farkas
//
//------------------------------------------------------------------------~~~
*/


public static class GregPacker 
{
	public enum IOMode { JSON, CRYPT, JSON_AND_CRYPT }
	//IO MODE !!! In the released game ALWAYS SET THIS TO CRYPT unless you have a good reason not to!
	//JSON is only meant to be for testing purposes
	private static IOMode iomode = IOMode.JSON_AND_CRYPT;

	//
	public static bool _lock 		{ get; private set ; }
	public static bool _isUnpacking { get; private set ; }
	
	static Dictionary<string, Dictionary<Guid, PackObject>> packages = new Dictionary<string, Dictionary<Guid, PackObject>>();
	static Dictionary<Guid, ObjectRefs>	linkableRefBuffer = new Dictionary<Guid, ObjectRefs>();//pending refs, link them when all objects are loaded
	static EventArgs e =null;

	static Dictionary<string, string> dirtySlots = new Dictionary<string, string>();

	public static event GPackerLauncher gpLauncher;
	public delegate void GPackerLauncher();

	static List<PackObject> unpackedObjects;
	public static PackObject[] GetUnpackedObjects() 
	{
		return unpackedObjects.ToArray ();
	}

	public enum PackMode { SINGLE, FULL }

	static string slotName;
	/*public static string SlotName { get { return slotName; } 
		set 
		{ 
			slotName = value; 

#if UNITY_ANDROID
			path = Application.persistentDataPath + "/GPacks";
#endif

#if UNITY_EDITOR
			path = Application.dataPath + "/GPacks";
#endif
			file =  string.IsNullOrEmpty(slotName) ? path + "/saved.json" : path + "/"+slotName+".json";
			cryptfile =  string.IsNullOrEmpty(slotName) ? path + "/crypt.gpak" : path + "/"+slotName+".gpak";

		} 
	}*/

	private static string path, file, cryptfile;

	/*public static PackObject Create(Type type)
	{
		_lock = true;

		PackObject po = Activator.CreateInstance(type) as PackObject;

		_lock = false;
		
		PackIn(po);

		return po;
	}*/

	public static T Create<T>(string slotName, string overrideGuid = null, params object[] args) where T : PackObject
	{
		_lock = true;
		
		PackObject po = Activator.CreateInstance(typeof(T), args) as PackObject;

		if(!string.IsNullOrEmpty(overrideGuid))
		{
			po.OverrideGuid(overrideGuid);
		}

		_lock = false;
		
		PackIn(slotName, po);
		
		return po as T;
	}

	public static T CreateSingle<T>(params object[] args) where T : PackObject
	{
		_lock = true;
		
		PackObject po = Activator.CreateInstance(typeof(T), args) as PackObject;
		
		_lock = false;
		

		
		return po as T;
	}

	//EVERYTHING YOU CREATE WILL PERSIST BETWEEN RUNS 
	//Therefore make sure to:
	// 1. call create from the OnFirstRun method. This will create the base structure for you. Useful for singletons and managers.
	// 2. call Destroy on the PackableObject once you don't need it anymore. Useful for objects that have to persist but don't last forever (eg. a building on an island)
	public static PackObject Create(string typeName)
	{
		/*Type type = Type.GetType(typeName);
		if(type == null || type.IsAssignableFrom(typeof(PackObject))) return null;

		PackObject po = Activator.CreateInstance(type) as PackObject;
		
		PackIn (po);
		*/
		
		return null;
	}

	public static T GetPackObject<T>(string slotName, string guid) where T : PackObject
	{
		if(packages.ContainsKey(slotName))
		{
			Dictionary<Guid, PackObject> dict = packages[slotName];

			Guid g = new Guid(guid);
			if(dict.ContainsKey(g))
				return (T)dict[g];
		}

		return null;
	}

	public static void Destroy(string slotName, PackObject po)
	{

		Dictionary<Guid, PackObject> package = GetPackage(slotName);
		package.Remove(po.Guid);
		po = null;
	}

	public static void DeleteFragments(params string [] frags)
	{
		/*foreach (KeyValuePair<Guid, PackObject> kvp in package) 
		{

		}*/

	
	}

	private static void FirstRun()
	{
		_isUnpacking = false;
		//Debug.Log ("internal first run");
		//gpLauncher();
	
		//Pack();
	
		_isUnpacking = true;
	}
	
	private static void PackIn(string slotName, PackObject po)
	{

		Dictionary<Guid, PackObject> package = GetPackage(slotName);

		if(po.Guid != null && !package.ContainsKey(po.Guid))
			package.Add(po.Guid, po);
	}

	
	//Load

	 static void SetIO(string fileName, string path)
	{
		//Debug.Log ("# Setting slotname to: " + fileName);
		//Debug.Log ("# Setting IO path to: " + path);

		GregPacker.slotName = fileName;
		GregPacker.path = path;
		file = path+"\\"+fileName+".json";//string.IsNullOrEmpty(slotName) ? path + "/saved.json" : path + "/"+slotName+".json";
		cryptfile =  path+"\\"+fileName+".gpak";//string.IsNullOrEmpty(slotName) ? path + "/crypt.gpak" : path + "/"+slotName+".gpak";
	}

	static bool ioMode = true;

	public static PackObject [] UnpackFromString(string str)
	{
		ioMode = false;
		return DoUnpack (PackMode.SINGLE, "", "", str);
	}

	public static PackObject [] UnpackFromFile(string filename, string path)
	{
		ioMode = true;
		SetIO (filename, path);



		return DoUnpack (PackMode.SINGLE, filename, path);

	}

	public static void Unpack(string slotName = null)
	{
		ioMode = true;

		 

		if(string.IsNullOrEmpty(slotName))
		{
			foreach(KeyValuePair<string, Dictionary<Guid, PackObject>> kvp in packages)
			{
			
				path = Application.persistentDataPath+"/GPacks";
				file = path + "/" + kvp.Key+".json";
				cryptfile =  path + "/" + kvp.Key+".gpak";
				DoUnpack (PackMode.FULL, file, path, null);
			}
		}
		else
		{

			path = Application.persistentDataPath+"/GPacks";
			 
			file = path + "/" + slotName+".json";
			cryptfile =  path + "/" + slotName+".gpak";
			DoUnpack (PackMode.FULL, slotName, path, null);
		}
	}

	 static PackObject [] DoUnpack(PackMode mode, string filename, string path, string passedInJSON = null)
	{
		//ioMode = true;
		_isUnpacking = true;
		bool runningInFirstRunMode = false;

		string json = null;

		slotName = filename;

		Debug.Log ("UNPACKING---- "+slotName);
		GregBugger.Log("UNPACKING ["+slotName+"]");



		if (ioMode)
						json = ReadFromFile ();
				else
						json = passedInJSON;

		//if json isn't present that means the app hasn't run yet
		//So do the First run logic which creates the structure with all objects and saves
		//then read the json again and carry on
		if(string.IsNullOrEmpty(json))
		{


			if(ioMode)
				json = PackageToJson(filename);//ReadFromFile();
			else
				json = passedInJSON;

			//FirstRun(json);

			runningInFirstRunMode = true;
		}
 

		Dictionary<string, object> dict = MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;

		Dictionary<Guid, PackObject> packageBuffer = null;

		Dictionary<Guid, PackObject> package = GetPackage(slotName);

		if (mode == PackMode.FULL) 
		{
			packageBuffer = new Dictionary<Guid, PackObject> (package);
				
		} 
		else 
		{
			packageBuffer = new Dictionary<Guid, PackObject> ();
		}

		//Debug.Log("LOADED JSON: "+MiniJSON.Json.Serialize(dict));

		//Debug.Log ("Trace Dict------------------------------------------");
		foreach(KeyValuePair<string, object> kvp in dict)
		{
			//Debug.Log ("-- "+kvp.Key);
		}

		//Debug.Log ("Instantiate objects------------------------------------------");
		//Instantiate all objects
		foreach(KeyValuePair<string, object> kvp in dict)
		{
			Dictionary<string, object> objectDict = kvp.Value as Dictionary<string, object>;
			Dictionary<string, object> infoDict = objectDict["info"] as Dictionary<string, object>;

			Type type = Type.GetType(infoDict["name"].ToString());

			if(type == null) type = Type.GetType(infoDict["fullname"].ToString());


			if(type == null)
				Debug.LogError("Type from JSON cannot be found: "+infoDict["fullname"].ToString());
			//just instantiate the objects, dont worry about populating them now
			Guid guid = new Guid(kvp.Key.ToString());

			if(!packageBuffer.ContainsKey(guid))
			{
				PackObject po = Activator.CreateInstance(type) as PackObject;
				PropertyInfo propInfo = typeof(PackObject).GetProperty("guid",
			                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			 
				propInfo.SetValue(po, guid, null);
				//Debug.Log ("  --> guid: "+po.Guid);
				packageBuffer.Add(guid, po);
			}

		}
		//Debug.Log ("Trace package------------------------------------------");
		foreach(KeyValuePair<Guid, PackObject> kvp in packageBuffer)
		{
			//Debug.Log ("-- "+kvp.Key);
		}


		
		//Debug.Log ("Populate------------------------------------------");

		//go through the package and populate the objects with data yay!
		foreach(KeyValuePair<Guid, PackObject> kvp in packageBuffer)
		{
			Dictionary<string, object> objectDict = null;
			try{
				objectDict= dict[kvp.Value.Guid.ToString()] as Dictionary<string, object>;
			}
			catch(System.Exception e) 
			{ 
				Debug.LogError("Invalid object creation. Make sure you do not use the PackObject's constructor!"); 
				continue;
			}
			

			//Debug.Log (kvp.Value.Guid.ToString());
			Dictionary<string, object> dict_info = objectDict["info"] as Dictionary<string, object>;
			Dictionary<string, object> dict_coll = objectDict["coll"] as Dictionary<string, object>;
			Dictionary<string, object> dict_refs = objectDict["refs"] as Dictionary<string, object>;
			Dictionary<string, object> dict_params = objectDict["params"] as Dictionary<string, object>;

			PackObject po = kvp.Value;
			Type type = po.GetType();

		//-- PARAMS
			foreach(KeyValuePair<string, object> k in dict_params)
			{

				string entryName = k.Key;

				if(k.Value == null) continue;
				string entryValue = k.Value.ToString();
				//Debug.Log ("#--> "+entryName+", "+entryValue);


				PropertyInfo propInfo = type.GetProperty(entryName,
				             BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

				TypeConverter typeConverter = TypeDescriptor.GetConverter(propInfo.PropertyType);
				object propValue = typeConverter.ConvertFromString(entryValue);
				propInfo.SetValue (kvp.Value, propValue, null);

			}
		//-- collections
			foreach(KeyValuePair<string, object> k in dict_coll)
			{
				Dictionary<string, object> collDict = k.Value as Dictionary<string, object>;
				Dictionary<string, object> infoDict = collDict["info"] as Dictionary<string, object>;
				string colltype = infoDict["colltype"].ToString();
				string collname = k.Key.ToString();
				Type elemType = Type.GetType(infoDict["elemtype"].ToString());


				switch(colltype)
				{
				case "List":
					List<object> list = collDict["elements"] as List<object>;
					PropertyInfo propInfo = type.GetProperty(collname,
					                                         BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					var listType = typeof(List<>);
					var constructedListType = listType.MakeGenericType(elemType);
					IList ien  = Activator.CreateInstance(constructedListType) as IList;
					propInfo.SetValue(po, ien, null);

					if(IsPrimitive(elemType))
					{
						foreach(object obj in list)
						{
							//TODO: populate the right list
							ien.Add(Convert.ChangeType(obj , elemType));

						}
					}
					else{
						foreach(object obj in list)
						{
							ien.Add(packageBuffer[new Guid(obj.ToString())]);
						}
					}
					break;
				}

			}


			//This is weird here. I create a temp dictionary, add all the package guids. The values are bools that
			//mark whether the packobject has a parent or not.
			//I will need this info to be able to set a list of topmost objects
			Dictionary<Guid, bool> topmostDict = new Dictionary<Guid, bool>();

			foreach(KeyValuePair<Guid, PackObject> kv in packageBuffer)
			{
				topmostDict.Add(kv.Key, true);
			}

		//-- REFS
			foreach(KeyValuePair<string, object> k in dict_refs)
			{
				
				string entryName = k.Key;
				
				if(k.Value == null && !string.IsNullOrEmpty(k.Value.ToString())) continue;
				string entryValue = k.Value.ToString();
				//Debug.Log ("#--> "+entryName+", "+entryValue);
				
				
				PropertyInfo propInfo = type.GetProperty(entryName,
				                                         BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				
				//TypeConverter typeConverter = TypeDescriptor.GetConverter(propInfo.PropertyType);
				//object propValue = typeConverter.ConvertFromString(entryValue);
				//object propValue = typeConverter.ConvertFromString();
				Guid targetRef = new Guid(k.Value.ToString());
				propInfo.SetValue (kvp.Value, packageBuffer[targetRef], null);
				topmostDict[targetRef] = false;
				
			}


			//--UNPACKED
			unpackedObjects = new List<PackObject>();


			foreach(KeyValuePair<Guid, bool> k in topmostDict)
			{
				if(k.Value)
				{
					unpackedObjects.Add(packageBuffer[k.Key]);
				}
			}


		}

		_isUnpacking = false;


		//Debug.Log ("Call Packer's own constructors------------------------------------------");


		//CREATE a copy of package because it would get out of sync if you called CREATE from FirstRun or Start
		Dictionary<Guid, PackObject> origPackage = new Dictionary<Guid, PackObject>(packageBuffer);

		//OnFirstRun executes only for the very first time the application is executed.
		//You have to put your Create() calls in there to create the initial structure of your save data.
		if(runningInFirstRunMode)
		{
			//Debug.Log ("OnFirstRun()------------------------------------------");
			foreach(KeyValuePair<Guid, PackObject> kvp in origPackage)
			{
				MethodInfo firstRunMethod = kvp.Value.GetType().GetMethod("OnFirstRun",  BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				
				if(firstRunMethod != null) firstRunMethod.Invoke(kvp.Value, null);
				
			}
		}

		//Debug.Log ("Start()------------------------------------------");
		foreach(KeyValuePair<Guid, PackObject> kvp in origPackage)
		{
			MethodInfo firstRunMethod = kvp.Value.GetType().GetMethod("Start",  BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			
			if(firstRunMethod != null) firstRunMethod.Invoke(kvp.Value, null);
			
		}


		if(mode == PackMode.FULL)
		foreach (KeyValuePair<Guid, PackObject> kvp in packageBuffer) 
		{
			if(!package.ContainsKey(kvp.Key))
			{
				package.Add(kvp.Key, kvp.Value);
			}
		}


		List<PackObject> pos = new List<PackObject> ();

		foreach (KeyValuePair<Guid, PackObject> k in packageBuffer) {

			pos.Add(k.Value);
				}
		
		return pos.ToArray();
		 
	}
	
	//Save
	public static void Pack(string slotName)
	{

		path = Application.persistentDataPath + "/GPacks";
		file = path+"\\"+slotName+".json";//string.IsNullOrEmpty(slotName) ? path + "/saved.json" : path + "/"+slotName+".json";
		cryptfile =  path+"\\"+slotName+".gpak";

		//<Guid, object>  -->  <Guid, json string>
		string json = PackageToJson(slotName);

		GregBugger.LogSystem ("PACKING file.. "+file);

		WriteToFile(json);
		//Debug.Log (json);
	}

	public static void PackDirty()
	{
		foreach(KeyValuePair<string, string> kvp in dirtySlots)
		{
			Pack (kvp.Key);
		}

		dirtySlots.Clear();
	}

	public static  void TraceDirties()
	{
		foreach(KeyValuePair<string, string> kvp in dirtySlots)
		{
			GregBugger.Log("Dirty: "+kvp.Key);
		}

		if(dirtySlots.Count == 0)
		{
			GregBugger.Log ("NO DIRTY PACKS");
		}
	}


	public static void PackSingle(PackObject po, string filename, string path)
	{
		Dictionary<string, object> dict = new Dictionary<string, object>();
		file = path+"\\"+filename+".json";//string.IsNullOrEmpty(slotName) ? path + "/saved.json" : path + "/"+slotName+".json";
		cryptfile =  path+"\\"+filename+".gpak";

		BreakDownObject (po, dict, null);

		string s = MiniJSON.Json.Serialize (dict);

		WriteToFile (s, filename, path);
	}

	private static string PackageToJson(string slotName)
	{
		Dictionary<string, object> dict = new Dictionary<string, object>();

		Dictionary<Guid, PackObject> package = GetPackage(slotName);

		foreach(KeyValuePair<Guid, PackObject> kvp in package)
		{
			BreakDownObject(kvp.Value, dict, null);
		}
		
		
		return MiniJSON.Json.Serialize(dict);
	}

	public static void MakeDirty(string slotname)
	{
		if(!dirtySlots.ContainsKey(slotname))
		{
			dirtySlots.Add(slotname, slotname);
		}
	}
	

	private static void WriteToFile(string json)
	{

		if(string.IsNullOrEmpty(slotName)) slotName = "";

		if (!Directory.Exists(path)) Directory.CreateDirectory(path);

		if(iomode == IOMode.JSON || iomode == IOMode.JSON_AND_CRYPT)
		{
			Debug.Log ("----SAVING to: "+file);
			System.IO.File.WriteAllText(file, json);
		}
		if(iomode == IOMode.CRYPT || iomode == IOMode.JSON_AND_CRYPT)
		{
			Debug.Log ("----CRYPTING to: "+cryptfile);
			System.IO.File.WriteAllText(cryptfile, MD5Crypt.Encrypt(json, "lol"+SystemInfo.deviceUniqueIdentifier, true));
		}
	}

	private static void WriteToFile(string json, string filename, string path)
	{

		string filepath = path + "\\" + filename;
		
		if (!Directory.Exists(path)) Directory.CreateDirectory(path);
		
		if(iomode == IOMode.JSON || iomode == IOMode.JSON_AND_CRYPT)
		{
			Debug.Log ("----SAVING to: "+filepath);
			System.IO.File.WriteAllText(filepath + ".json", json);
		}
		if(iomode == IOMode.CRYPT || iomode == IOMode.JSON_AND_CRYPT)
		{
			Debug.Log ("----CRYPTING to: "+filepath);
			System.IO.File.WriteAllText(filepath + ".gpak", MD5Crypt.Encrypt(json, "lol"+SystemInfo.deviceUniqueIdentifier, true));
		}
	}

	private static string ReadFromFile()
	{

		if(string.IsNullOrEmpty(slotName)) slotName = "";

		Debug.Log("path "+path);
		//string file = path +"/Gpacks/"+slotName;
		if (!Directory.Exists(path))
		{
		 
		}
		else{
			if(iomode == IOMode.JSON || iomode == IOMode.JSON_AND_CRYPT)
			{
				if(!File.Exists(file)) return null;

				return System.IO.File.ReadAllText(file);
			}
			if(iomode == IOMode.CRYPT || iomode == IOMode.JSON_AND_CRYPT)
			{
				if(!File.Exists(cryptfile)) return null;

				return MD5Crypt.Decrypt( System.IO.File.ReadAllText(cryptfile), "lol"+SystemInfo.deviceUniqueIdentifier, true);
			}
		}


		return null;
	}

	private static string ReadFromFile(string filename, string path)
	{



		if (!Directory.Exists(path))
		{
			
		}
		else{
			if(iomode == IOMode.JSON || iomode == IOMode.JSON_AND_CRYPT)
			{

				string filepath = path + "/"+filename+".json";
				if(!File.Exists(filepath)) return null;
				
				return System.IO.File.ReadAllText(filepath);
			}
			if(iomode == IOMode.CRYPT || iomode == IOMode.JSON_AND_CRYPT)
			{

				string filepath = path + "/"+filename+".gpak";
				if(!File.Exists(filepath)) return null;
				
				return MD5Crypt.Decrypt( System.IO.File.ReadAllText(filepath), "lol"+SystemInfo.deviceUniqueIdentifier, true);
			}
		}
		
		
		return null;
	}



	private static Type GetTypeOfList(PropertyInfo changedMember)
	{
		var listType = from i in changedMember.PropertyType.GetInterfaces()
				where i.IsGenericType
				let generic = i.GetGenericTypeDefinition()
				where generic == typeof (IEnumerable<>)
				select i.GetGenericArguments().Single();
		return listType.SingleOrDefault();
	}

	private static bool IsPrimitive(Type type)
	{
		if(type.IsPrimitive
			|| type == typeof(string)
		   	|| type == typeof(Decimal)) return true;

		return false;

	}

	private static bool IsList(Type type)
	{
		if(type.IsGenericType && 
			type.GetGenericTypeDefinition() == typeof(List<>))
			return true;

		return false;
	}

	private static bool IsDictionary(Type type)
	{
		if(type.IsGenericType && 
		   type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
			return true;
		
		return false;
	}
	
	//This is the BEEF, using Reflection we disassemble the object and get the cool stuff out that we need [Pack] attributes.
	private static Dictionary<string, object> BreakDownObject(PackObject po, Dictionary<string, object> maindict, string [] fragments)
	{
		//If we have this po in the dict, we have stored this object already.
		//This is quite a common case, happens when more than one object holds reference to a particular object.
		if(po.Guid == null || maindict.ContainsKey(po.Guid.ToString())) return maindict;	//-->So GTFO
		
		
		Dictionary<string, object> dict = new Dictionary<string, object> ();
		Dictionary<string, object> dict_info = new Dictionary<string, object> ();
		Dictionary<string, object> dict_params = new Dictionary<string, object> ();
		Dictionary<string, object> dict_refs = new Dictionary<string, object> ();
		Dictionary<string, object> dict_coll = new Dictionary<string, object> ();

		
		Type poType = po.GetType ();
		//Debug.Log ("---------- [BreakDown]: " + poType);
		dict_info.Add("fullname", poType.FullName);
		dict_info.Add("name", poType.Name);	

		PropertyInfo [] poProperties = poType.GetProperties ();


		foreach (PropertyInfo propertyInfo in poProperties) {

		 
			
			foreach (Attribute a in propertyInfo.GetCustomAttributes(true)) {
				
				if(a is Pack && propertyInfo.CanRead && FragmentPass(a, fragments))
				{
					var getMethod = propertyInfo.GetGetMethod();
					
					
					//----ARRAY
					if (getMethod.ReturnType.IsArray) {
						var arrayObject = getMethod.Invoke(po, null);
						foreach (object element in (Array) arrayObject)
						{
							//Debug.Log ("  [array]"+element.ToString());
							//foreach (PropertyInfo arrayObjPinfo in element.GetType().GetProperties())
							//{
								//Console.WriteLine(arrayObjPinfo.Name + ":" + arrayObjPinfo.GetGetMethod().Invoke(element, null).ToString());
							//}
						}
					}
					
					//---- LIst
					
					else if(IsList(getMethod.ReturnType))
					{

						IEnumerable copyFrom = propertyInfo.GetValue(po, null) as IEnumerable;
						
						if(copyFrom == null) continue; //GTFO
						 
					//--	
						var listObject = getMethod.Invoke(po, null);
						Type tempType  = GetTypeOfList(propertyInfo);

					//--
						// Create a List<> of unknown type at compile time.
						//Type customList = typeof(List<>).MakeGenericType(tempType);
						//IList objectList = (IList)Activator.CreateInstance(customList);
						
						// Copy items from a PropertyInfo list to the object just created
						//object o = objectThatContainsListToCopyFrom;
						//PropertyInfo p = o.GetType().GetProperty("PropertyName");
						//Dictionary <string, object> tempDict = new Dictionary<string, object>();
						Dictionary<string, object> tempDict = new Dictionary<string, object>();
						Dictionary<string, object> infoDict = new Dictionary<string, object>();
						tempDict.Add("info", infoDict);
						infoDict.Add("colltype", "List");
						infoDict.Add("elemtype", tempType.ToString());
						//tempDict.Add(propertyInfo.Name, tempList);

						//foreach(object item in copyFrom) objectList.Add(item); // Will throw exceptions if the types don't match.
						//objectList.Add(copyFrom[0]);
						// Iterate and access the items of "objectList"
						// (objectList declared above as non-generic IEnumerable)

					//--
						//--elem type PRIMITIVE
						if(IsPrimitive(tempType))
						{
							//foreach(object item in objectList) 
							//{ 
								tempDict.Add ("elements", getMethod.Invoke (po, null));
								//Debug.Log("   [primitive]"+item.ToString()); 
							//}
						}
						//--elem type LIST
						/*else if(IsList(tempType))
						{
							//You can't have a list in a list, FUCK YOU MAN!

							continue;
						}*/
						//--elem type OBJECT
						else
						{
							IList list = getMethod.Invoke (po, null) as IList;

							List<string> refs = new List<string>();
							tempDict.Add("elements", refs);

							foreach(object item in list) 
							{ 
								//Debug.LogWarning("   [object]"+item.ToString()); 
								PackObject p = item as PackObject;
								if(p!= null)
								{
									if(!tempDict.ContainsKey(p.Guid.ToString ()))
									{
										refs.Add( p.Guid.ToString());
										BreakDownObject(p, maindict, fragments);

									}
								}
							}

						}

						dict_coll.Add(propertyInfo.Name, tempDict);
						
					}
					
					//----PRMITIVE TYPE
					else if (IsPrimitive (getMethod.ReturnType)) 
					{
						dict_params.Add (propertyInfo.Name, getMethod.Invoke (po, null));
						//Debug.Log ("-" + propertyInfo.Name+ ": "+getMethod.Invoke (po, null));
					}
					
					
					
					//----OBJECT REFERENCE
					else {
						object obj = getMethod.Invoke (po, null);
						PackObject pobj = obj as PackObject;
						
						if (pobj == null)
						{
							if(obj != null)
								Debug.LogWarning ("Can't pack object, it must derive from PackObject: " + poType+"."+propertyInfo.Name);
							continue;
						}
						
						//add the reference of the target
						dict_refs.Add (propertyInfo.Name, pobj.Guid.ToString ());
						//Debug.Log ("-" + propertyInfo.Name+ ": "+getMethod.Invoke (po, null));
						
						BreakDownObject (pobj, maindict, fragments);
						
						
					}

				}
			}
		}
		dict.Add ("info", dict_info);
		dict.Add ("params", dict_params);
		dict.Add ("coll", dict_coll);
		dict.Add ("refs", dict_refs);
		
		maindict.Add (po.Guid.ToString(), dict);
		
		return maindict;
	}

	private static bool FragmentPass(Attribute a, string [] fragments)
	{
		PackFrag pf = a as PackFrag;

		if(pf == null || fragments == null) return true;

		if(fragments.Contains<string>(pf.frag)) return true;

		return false;
	}

	private static Dictionary<Guid, PackObject> GetPackage(string slotName)
	{
		if(packages.ContainsKey(slotName))
		{

			return packages[slotName];
		}
		else
		{


			Dictionary<Guid, PackObject> dict = new Dictionary<Guid, PackObject>();
			packages.Add(slotName, dict);

			return dict;
		}



	}
	
	
	
	//-------------------------------------------------------
	
	public sealed class PackEntry
	{
		List<PackField> fields = new List<PackField>();
		public List<PackField> Fields { get { return fields; } set { fields = value; } }
		
		public void AddField(PackField pf)
		{
			fields.Add(pf);
		}
	}
	
	public sealed class PackField
	{
		public string name;
		public object value;
	}

	public sealed class ObjectRefs
	{
		   
		public List<ObjectRefEntry> entries { get; private set; }

		public ObjectRefs()
		{
			entries = new List<ObjectRefEntry>();
		}

		public void AddEntry(PropertyInfo propertyInfo, Guid guid)
		{
			entries.Add(new ObjectRefEntry(propertyInfo, guid));
		}

		public sealed class ObjectRefEntry
		{
			public readonly Guid guid;
			public readonly PropertyInfo propertyInfo;

			public ObjectRefEntry(PropertyInfo propertyInfo, Guid guid)
			{
				this.guid = guid;
				this.propertyInfo = propertyInfo;
			}
		}
	}
	
}

//ATTRIBUTE
//---------
public class Pack : System.Attribute { /*TODO:sit back and relax*/ }
public class PackFrag : Pack { public readonly string frag; public PackFrag(string frag) { this.frag = frag; } }

//PACK OBJECT
//-----------
public class PackObject
{
	System.Guid guid { get; set; }
	public System.Guid Guid { get { return guid; } }
	
	internal PackObject()
	{
		//Debug.Log ("PAckObject created: "+this);
		if(!GregPacker._isUnpacking)
		{
			if (GregPacker._lock) {
				guid = System.Guid.NewGuid ();
				//Debug.Log ("   --> newly created guid: "+guid);

			} else {
				Debug.LogError ("You must not use the new keyword to instantiate PackObjects, use GregPacker.Create(..) instead! [ERROR in ctor : "+this+"]");
			}
		}
		else
		{
			// guid = from json
		}
	}

	public void OverrideGuid(string guid)
	{
		this.guid = new Guid(guid);
	}

	public void Destroy()
	{
		//GregPacker.Destroy(this);
	}

 
}

public class PackModul 
{
	//public PackModul(
}

//------------------------------------------------------
/*
  __  __ _____  _____  _____                  _   
 |  \/  |  __ \| ____|/ ____|                | |  
 | \  / | |  | | |__ | |     _ __ _   _ _ __ | |_ 
 | |\/| | |  | |___ \| |    | '__| | | | '_ \| __|
 | |  | | |__| |___) | |____| |  | |_| | |_) | |_ 
 |_|  |_|_____/|____/ \_____|_|   \__, | .__/ \__|
                                   __/ | |        
                                  |___/|_|        
 * */
//------------------------------------------------------
	public class MD5Crypt
	{

		public static string Encrypt(string toEncrypt, string securityKey, bool useHashing)
		{
			string retVal = string.Empty;
			try
			{
				byte[] keyArray;
				byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
				// Validate inputs  
				ValidateInput(toEncrypt);
				ValidateInput(securityKey);
				// If hashing use get hashcode regards to your key  
				if (useHashing)
				{
					MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
					keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
					// Always release the resources and flush data       
					// of the Cryptographic service provide. Best Practice   
					hashmd5.Clear();
				}
				else
				{
					keyArray = UTF8Encoding.UTF8.GetBytes(securityKey);
				}
				TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
				// Set the secret key for the tripleDES algorithm       
				tdes.Key = keyArray;
				// Mode of operation. there are other 4 modes.
				// We choose ECB (Electronic code Book)  
				tdes.Mode = CipherMode.ECB;
				// Padding mode (if any extra byte added)  
				tdes.Padding = PaddingMode.PKCS7;
				ICryptoTransform cTransform = tdes.CreateEncryptor();
				// Transform the specified region of bytes array to resultArray
				byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
				// Release resources held by TripleDes Encryptor      
				tdes.Clear();
				// Return the encrypted data into unreadable string format  
				retVal = Convert.ToBase64String(resultArray, 0, resultArray.Length);
			}
			catch (Exception ex)
			{
				throw;// new EncryptionException(EncryptionException.Code.EncryptionFailure, ex, MethodBase.GetCurrentMethod());   
			}
			return retVal;
		}
		
		


		public static string Decrypt(string cipherString, string securityKey, bool useHashing)
		{
			string retVal = string.Empty;
			try
			{
				byte[] keyArray;
				byte[] toEncryptArray = Convert.FromBase64String(cipherString);
				// Validate inputs                ValidateInput(cipherString);     
				ValidateInput(securityKey);
				if (useHashing)
				{
					// If hashing was used get the hash code with regards to your key    
					MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
					keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));
					// Release any resource held by the MD5CryptoServiceProvider       
					hashmd5.Clear();
				}
				else
				{
					// If hashing was not implemented get the byte code of the key      
					keyArray = UTF8Encoding.UTF8.GetBytes(securityKey);
				}
				TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
				// Set the secret key for the tripleDES algorithm       
				tdes.Key = keyArray;
				// Mode of operation. there are other 4 modes.   
				// We choose ECB(Electronic code Book)         
				tdes.Mode = CipherMode.ECB;
				// Padding mode(if any extra byte added)     
				tdes.Padding = PaddingMode.PKCS7;
				ICryptoTransform cTransform = tdes.CreateDecryptor();
				byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
				// Release resources held by TripleDes Encryptor    
				tdes.Clear();
				// Return the Clear decrypted TEXT  
				retVal = UTF8Encoding.UTF8.GetString(resultArray);
			}
			catch (Exception ex)
			{
				throw;//  new EncryptionException(EncryptionException.Code.DecryptionFailure, ex, MethodBase.GetCurrentMethod());
			}
			return retVal;
		}
		/// <summary>  
		/// Validates an input value  
		/// </summary>   
		/// <param name="inputValue">Specified input value</param>   
		/// <returns>True | Falue - Value is valid</returns>    
		private static bool ValidateInput(string inputValue)
		{
			bool notValid = string.IsNullOrEmpty(inputValue);
			if (notValid)
			{
				throw new Exception("Invalid Input");// new EncryptionException(EncryptionException.Code.InvalidInputValues, MethodBase.GetCurrentMethod());   
			}
			return notValid;
		}
		
	}




