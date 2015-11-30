using UnityEngine;
using System.Collections;

public class HUDCanvas : MonoBehaviour {


	public HUDFocus hudFocus;

	public void FocusOn(Transform target, string text)
	{
		this.transform.position = target.position;
		this.transform.rotation = target.rotation;

		hudFocus.gameObject.SetActive(true);
		hudFocus.descriptorText.text = text;
	}

	public void FocusOff()
	{
		hudFocus.gameObject.SetActive(false);
	}
}
