using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour, EventListener {
	
	public Transform camPivot;
	public Transform camTarget;

	public Transform lookAtPointer;
	
	public float distanceFromTarget = 8;
	public float camTiltAngle = -45;
	public float camYAngle = 0;
	public Transform target;
	public float ambientIntensity = 0.1f;

	CameraInfo cameraInfo;
	public CameraInfo CameraInfo { get{return cameraInfo; } 
		set 
		{ 
			cameraInfo = value; 

			PointerCameraInfo pci = cameraInfo as PointerCameraInfo;

			if(pci != null)
			{
				distanceFromTarget = pci.distanceFromTarget;
				camTiltAngle = pci.camTiltAngle;
				camYAngle = pci.camYAngle;
			}
		} 
	}
	
	
	public enum ViewMode { POINTER_FOLLOW, FIXED_FOLLOW, ZONE_FOLLOW, NONE }
	
	[HideInInspector]
	public ViewMode viewMode;
	
	public Vector3 camTargetPosition;

	Vector3 offset = Vector3.zero;
	public Vector3 LookOffset{ get { return offset; } }

 
	Area activeArea;

	
	// Use this for initialization
	void Awake () {
		
		SwitchViewMode (ViewMode.POINTER_FOLLOW);
		camTargetPosition = Vector3.up * 1.5f; 

		RenderSettings.ambientIntensity = ambientIntensity;

		Eve.Listen<E_AreaEntered>(OnAreaEntered, this);


	}

	void Update()
	{


		Vector3 offset = Vector3.zero;

		if(SceneManager.Instance.ActivePC.ActiveArea != null)
			offset = SceneManager.Instance.ActivePC.ActiveArea.lookOffset;

		//iTween.MoveUpdate(lookAtPointer.gameObject, iTween.Hash("position", target.position + offset, "time", 2f, "easetype", iTween.EaseType.easeInCirc ));
		iTween.MoveUpdate(lookAtPointer.gameObject, iTween.Hash("x", target.position.x + offset.x, "z", target.position.z + offset.z, "time", 2f, "easetype", iTween.EaseType.easeInOutCubic ));
		iTween.MoveUpdate(lookAtPointer.gameObject, iTween.Hash("y", target.position.y + offset.y, "time", 5f, "easetype", iTween.EaseType.easeInOutCubic ));

	}

	void LateUpdate()
	{
		if(CameraInfo != null)
			CameraInfo.OnUpdate();
	}

	void OnAreaEntered(BaseEvent ev)
	{
		E_AreaEntered e = ev as E_AreaEntered;

	 
		if(e.Actor != SceneManager.Instance.activePC)
			return;

		CameraInfo.TransitionMode transMode = CameraInfo.TransitionMode.SMOOTH;
		//activeArea = e.Area;

		if(CameraInfo != null)
		{
			//If we walk from a portal to another
			if(e.Area.Portal != null && CameraInfo.area.Portal != null)
			{
				if(e.Area.Portal.Name != CameraInfo.area.Portal.Name)
					transMode = CameraInfo.TransitionMode.SNAP;
			}
			//If we walk from a non-portal to a portal
			else if(e.Area.Portal != null && CameraInfo.area.Portal == null)
			{
				transMode = CameraInfo.TransitionMode.SNAP;
			}
			//If we walk from a portal to a non-portal
			else if(e.Area.Portal == null && CameraInfo.area.Portal != null)
			{
				transMode = CameraInfo.TransitionMode.SNAP;
			}
		}

		CameraInfo = e.Area.camInfo;
		CameraInfo.Begin(this, transMode);

		//Refresh();
	}

	public void SwitchTarget()
	{
		CameraInfo.Begin(this, CameraInfo.TransitionMode.SNAP);
		//Refresh();
	}



	// Update is called once per frame
/*	public void Refresh () {



		Actor actor = target.GetComponent<Actor>();

		if(actor != null && actor.ActiveArea != null)
		{
			offset = actor.ActiveArea.lookOffset;
			activeArea = actor.ActiveArea;
		}

		if(CameraInfo != null)
			CameraInfo.Refresh (this);
				
		
	}*/
	
	
	
	public void SwitchViewMode(ViewMode mode)
	{
		switch (mode) 
		{
			
		case ViewMode.POINTER_FOLLOW:
			camTarget.transform.parent = camPivot;
			camTarget.localPosition = Vector3.zero;
			break;
		case ViewMode.FIXED_FOLLOW:
			camTarget.transform.parent = null;
			break;
		case ViewMode.ZONE_FOLLOW:
			camTarget.transform.parent = null;
			break;
			
		}
		
		viewMode = mode;
	}

	public Rect [] CalcSiblingAreaRects()
	{
		Vector3 camFromTop = new Vector3(transform.position.x, 0, transform.position.z);

		List<Rect> list = new List<Rect>();

		if(activeArea != null && activeArea.siblingAreas != null && activeArea.siblingAreas.Length > 0)
		foreach(Area a in activeArea.siblingAreas)
		{
			Vector3 targetSiblingFromTop = new Vector3(a.transform.position.x, 0, a.transform.position.z);

			Vector3 camDirVec = new Vector3(transform.forward.x, 0/*transform.forward.y*/, transform.forward.z).normalized;
			Vector3 targetDirVec = (a.transform.position - transform.position).normalized;
			Vector3 targetHorVec = (targetSiblingFromTop - camFromTop).normalized;

			Debug.DrawRay(transform.position, camDirVec, Color.red);
			Debug.DrawRay(transform.position, targetDirVec, Color.blue);
			Debug.DrawRay(transform.position, targetHorVec, Color.yellow);

			float ang = Vector3.Dot(camDirVec, targetDirVec);
			float ang2 = Vector3.Dot (camDirVec, targetHorVec);
			Vector3 cross = Vector3.Cross(camDirVec, targetHorVec);

			bool left= false;
			
			if (cross.z > 0)
				left = true;
				//ang = 360 - ang;

			Debug.Log (ang+", "+ang2);
		

		}


		return null;
	}
	
	void DoPointer()
	{
		
	}
	
	void DoFixed()
	{
		
	}
	
	void DoZone()
	{
		
	}

	void OnDrawGizmos()
	{

	}


}
