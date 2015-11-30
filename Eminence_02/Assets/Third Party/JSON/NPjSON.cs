using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// JSON Extension. Extra convenience methods
/// </summary>
public class NPjSON : MonoBehaviour {

	/// <summary>
	/// Retrieve the entry stored in the JSON dictionary.
	/// </summary>
	/// <returns>
	/// The value. null if there is no value associated with the passed in key.
	/// </returns>
	/// <param name='dictionary'>
	/// Dictionary.
	/// </param>
	/// <param name='key'>
	/// Key.
	/// </param>
	public static object ValueFromJSONDictionary(Dictionary<string, object> dictionary, string key)
	{
		if (!dictionary.ContainsKey(key)) return null;
		
		object dictValue = dictionary[key];
		
		if (dictValue == null) return null;
		
		// Array
		if (typeof(List<object>) == dictValue.GetType()) 
		{
			// we have an array, but is it empty?
			List<object> values = (List<object>)dictValue;
			int numberOfValues = values.Count;
			if (numberOfValues == 0) return null;
			
			// do we have a populated list
			if (numberOfValues > 1) return dictValue;
			
			// is the only value stored null?
			object onlyEntry = values[0];
			if (onlyEntry == null) return null;
		}
		
		// we must have a valid object - the code extracting this data should know what class it is and handle it appropriately
		return dictValue;
	}
}
