using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel_AC_Division : BasePanel {

	 
	public Image faderbg;
	public Text description;
	public Text divText;

	const float ANGLE_SEQ = -45f;

	public Transform wheel;
	public AnimationCurve curve;
	public AnimationCurve fadeCurve;
	public float speedMul = 1;


	float startAngle, targetAngle, startTime, setTo;
	bool isRotating
		, waitingForChange;

	string div;

	public Image bgImage;
	public Sprite aeternaBG, ixionBG, wilkurseBG, hollowsBG;

	// Use this for initialization
	void Start () {

		targetAngle = 135f;
		setTo = 135f;

		isRotating = false;
		waitingForChange = true;

		wheel.transform.rotation = Quaternion.identity;
		wheel.RotateAround (Vector3.forward, Mathf.Deg2Rad * 135f);

		div = "aeterna";
		DoChange();
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (!isRotating)
						return;

		float z = wheel.rotation.z;

		wheel.rotation = Quaternion.identity;

		float evalValue = (Time.time - startTime) * speedMul;

		float eval = curve.Evaluate (evalValue);

		float newz = (((targetAngle  ) * eval) + startAngle);

		GregBugger.Log ("newz " + newz);

		if (evalValue > 1) {
			
			isRotating = false;
			newz = startAngle + targetAngle;
			setTo = newz;
			faderbg.gameObject.SetActive (false);
		}

		if (evalValue > 0.5f && waitingForChange) {
						DoChange ();
		}


		wheel.RotateAround (Vector3.forward, Mathf.Deg2Rad * newz);

		faderbg.color = new Color (0, 0, 0, fadeCurve.Evaluate (evalValue));


	}

		void DoChange()
		{
				waitingForChange = false;
				//IntroSceneManager.Instance.SetBG("background-"+div);



				switch (div) {

				case "aeterna":
						divText.text = "AETERNA";
						bgImage.sprite = aeternaBG;
						description.text = "Once a small kingdom, Aeterna and its inhabitants lived peacefully in a secluded region of Artalys. An intelligent civilisation, they gave Artalys the modern writing and numerical system, while devoting much of its time into the study of Science and Alchemy.";
						break;
				case "ixion":
						divText.text = "IXION";
			
						bgImage.sprite = ixionBG;
						description.text = "The path of an Ixion is one of perpetual training aimed at mastering traditional techniques of hand-to-hand combat and proficiently controlling the body’s Chakra energy. It is known that Ixion warriors aim to end their battles within 60 seconds with a tendency to engage at close quarters. ";
						break;
				case "wilkurse":
						divText.text = "WILKURSE";
			
						bgImage.sprite = wilkurseBG;
						description.text = "The Wilkurse Empire has continually invested in the advancement of it’s technology, fuelled by artificially synthesising Alchemy which has enabled it’s economy to evolve and grow quicker than other nations.";
						break;
				case "hollows":
						divText.text = "HOLLOWS";
			
						bgImage.sprite = hollowsBG;
						description.text = "The goal of the Hollows are unknown, but one purpose seems to drive their quest for dominance - the complete annihilation of mortals. They are all which are unholy, resurrected warriors, murderous beasts and diabolical demons unified by a sinister darker power to bring destruction to Artalys.";
						break;
				}
		}


	void StartRotate()
	{

		isRotating = true;
		startTime = Time.time;
		faderbg.gameObject.SetActive (true);
		waitingForChange = true;

	}
	public void pressed_0()
	{
		startAngle = setTo;
		targetAngle =  135 - startAngle;
		//targetAngle = 0 - setTo;
		div = "aeterna";

		StartRotate ();

	}

	public void pressed_1()
	{
		startAngle = setTo;
		targetAngle =  90f - startAngle;
		//targetAngle = 45;
		div = "ixion";
		StartRotate ();
	}

	public void pressed_2()
	{
		startAngle = setTo;
		targetAngle =   45f - startAngle;
		//targetAngle = 45f + startAngle;
		div = "wilkurse";
		StartRotate ();
	}

	public void pressed_3()
	{
		startAngle = setTo;
		targetAngle =  0f- startAngle;
		//targetAngle = 0f - startAngle;
		div = "hollows";
		StartRotate ();
	}
}
