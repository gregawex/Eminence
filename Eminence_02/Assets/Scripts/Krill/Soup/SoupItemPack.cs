using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoupItemPack : PackObject
{

	[Pack]
	public List<SoupItemPackEntry> PackEntries { get; set; }



	public SoupItemPackEntry GetActiveEntry(string name)
	{
		SoupItemPackEntry found = PackEntries.Find(entry => entry.Name.ToLower() == name.ToLower());

		foreach(SoupItemPackEntry en in PackEntries)
			GregBugger.Log ("--- "+en.Name+", "+en.ActiveStateName);

		return found;
	}

	public void AddEntry(string entryname, string statename)
	{
		if(PackEntries == null)
			PackEntries = new List<SoupItemPackEntry>();

		SoupItemPackEntry entry = GregPacker.Create<SoupItemPackEntry>(SoupItem.SLOTNAME);
		entry.Name = entryname;
		entry.ActiveStateName = statename;
		PackEntries.Add(entry);

	
	}

	public void SetStateName(string entryname, string statename)
	{
		SoupItemPackEntry entry = GetActiveEntry(entryname);

		if(entry != null )
		{

		
				entry.ActiveStateName = statename;
		 
			

		}
		else
		{
			GregBugger.LogError("Currently Executing Entry ["+entryname+"] doesn't exist in soupitem");
		 
		}
	}
}

public class SoupItemPackEntry : PackObject
{
	[Pack]
	public string Name { get; set; }

	[Pack]
	public string ActiveStateName { get; set; }


}
