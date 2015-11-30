using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ListButton : MonoBehaviour 
{

	public ListBox listBox;

	void Awake()
	{

	}

	public void Clicked()
	{
		listBox.ButtonClicked(GetComponent<Button>());
		listBox.gameObject.SetActive(false);
	}

}
