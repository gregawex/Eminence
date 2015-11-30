using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public enum UIComm { POS_RESPONSE, NEG_RESPONSE }

public class Panel_Error : BasePanel {


	//public UILabel title;
	public Text body;

	public Action posCallback, negCallback; 

	public GameObject oneButtonPanel, twoButtonPanel;

	// Use this for initialization
	protected override void Start () {
	
		base.Start ();
		int a = 0;
	}

	void OnEnable()
	{
		oneButtonPanel.SetActive(false);
		/*if (negCallback == null) {
						oneButtonPanel.SetActive (true);
						twoButtonPanel.SetActive (false);
				} else {
						oneButtonPanel.SetActive(false);
						twoButtonPanel.SetActive(true);
				}
*/
		//guard.OnPanelMessage ("on_enable");
	}
	
	// Update is called once per frame
	protected override void Update () {
	
		base.Update ();
	}

	void PositiveResponse()
	{
		posCallback ();
		//gameObject.SetActive (false);
		//base.Close ();
		guard.OnPanelMessage ("positive");
	}

	void NegativeResponse()
	{
		//negCallback ();
		//gameObject.SetActive (false);
		//base.Close ();

		guard.OnPanelMessage ("negative");
	}


}
