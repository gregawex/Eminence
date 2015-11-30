using UnityEngine;
using System.Collections;

public class ListOpener : MonoBehaviour 
{

	public ListBox listBox;


	protected virtual void Awake()
	{

	}

	protected virtual void Start()
	{

	}

	protected virtual void Update()
	{

	}

	public virtual void Clicked()
	{
		listBox.gameObject.SetActive(true);
	}
}
