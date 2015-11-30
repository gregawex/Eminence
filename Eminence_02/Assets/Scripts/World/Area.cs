using UnityEngine;
using System.Collections;

public class Area : MonoBehaviour {


	public string name;
	public Area [] siblingAreas;
	// Use this for initialization
	public CameraInfo camInfo;
	public Vector3 lookOffset;

	Portal portal;
	public Portal Portal { get { return portal; } }


	[ExecuteInEditMode]
	void Awake()
	{
#if UNITY_EDITOR
		if(!Application.isPlaying)
		gameObject.AddComponent<AreaSiblings>();
#endif
	}

	void Start () {

		portal = LookUpPortal(this.transform);
	
		if(Application.isPlaying)
		{
			Renderer r = GetComponent<Renderer>();
			if(r != null)
				r.enabled = false;

			AreaSiblings asib = GetComponent<AreaSiblings>();

			if(asib != null)
				siblingAreas = asib.areas;
		}
	}
	
	// Update is called once per frame
	public void Update () {
	
		if (camInfo == null) 
		{

			Debug.LogError ("Area " + name + " without Camera Info");
			return ;
		}


	}

	Portal LookUpPortal(Transform tr)
	{
		Portal p = null;

		if(tr.gameObject.tag == "Portal")
			p = tr.gameObject.GetComponent<Portal>();
		else{

			if(tr.parent != null)
			{
				p = LookUpPortal(tr.parent);
			}
			else{
				return null;
			}
		}

		return p;

	}

	void OnTriggerEnter(Collider other)
	{
		Actor ctrl = other.gameObject.GetComponent<Actor> ();

		//Debug.Log ("Something entered the area");
		if (ctrl != null) 
		{

			//Debug.Log ("player entered");
			 


			ctrl.SetActiveArea(this);



			Eve.TriggerEvent(new E_AreaEntered(this, ctrl));

			//Eve.Trace();
		}
	}
}
