using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ListBox : MonoBehaviour 
{

	public Button tempButton;

	List<GameObject> listOfItems = new List<GameObject>();


	 

	public void SetupList(string [] list)
	{
		foreach(GameObject go in listOfItems)
		{
			GameObject.DestroyImmediate(go);
		}

		listOfItems.Clear();

		int y = 0;
		tempButton.gameObject.SetActive(true);

		foreach(string s in list)
		{

			GameObject go = Instantiate(tempButton.gameObject) as GameObject;

			go.transform.localScale = Vector3.zero;

			RectTransform rt = go.GetComponent<RectTransform>();
			rt.parent = go.GetComponent<RectTransform>();
			//rt.right = 0;
		 	


			Transform child = go.transform.GetChild(0);
			Text buttonName = child.gameObject.GetComponent<Text>();
			buttonName.text = s;

			listOfItems.Add(go);

			y -= 60;
		}

		tempButton.gameObject.SetActive(false);
	}

	public void ButtonClicked(Button button)
	{

	}

 
}
