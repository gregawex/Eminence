using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {

	
		public GameObject text, loading;

		bool doLoad = false;


		void Awake()
		{
				GregBugger.on = false;
		}
	// Use this for initialization
	void Start () {
	
				text.SetActive (true);
				loading.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

				if (doLoad)
						Application.LoadLevel (1);
	
	}

		void pressed()
		{
				text.SetActive (false);
				loading.SetActive (true);

				doLoad = true;
		}
}
