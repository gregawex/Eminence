using UnityEngine;
using System.Collections;

#if UNITY_EDITOR	
using UnityEditor;
#endif

public class ZoneCameraInfo : CameraInfo 
{

	public BoxCollider camMoveArea;

	public enum LookAtMode { TARGET, LEFT, RIGHT, FWD, DOWN }
	public LookAtMode lookAtMode;

	bool snapPending;

	// Use this for initialization
	void Start () 
	{
	
	}

	public override void Begin (CameraController cam, TransitionMode transitionMode)
	{
		base.Begin (cam, transitionMode);

		SceneManager.Instance.activeCamera.transform.parent = SceneManager.Instance.activeCamera.camPivot;
		SceneManager.Instance.activeCamera.transform.localPosition = Vector3.zero;

		targetVec = CalculateTarget (cam);

		if(transitionMode == TransitionMode.SNAP)
		{
			Actor actor = SceneManager.Instance.ActivePC;



			SceneManager.Instance.activeCamera.camPivot.position = targetVec;
			SceneManager.Instance.activeCamera.lookAtPointer.position = actor.transform.position + actor.ActiveArea.lookOffset;
			SceneManager.Instance.activeCamera.transform.LookAt(SceneManager.Instance.activeCamera.lookAtPointer.position);
		}
		else{
			iTween.MoveTo(SceneManager.Instance.activeCamera.camPivot.gameObject, iTween.Hash("position", targetVec, "time", 5f, "easetype", iTween.EaseType.linear ));

		}
 
		//iTween.LookTo(SceneManager.Instance.activeCamera.gameObject, iTween.Hash("time", 1,  "looktarget", cam.target.position));
		//iTween.MoveTo(SceneManager.Instance.activeCamera.camPivot.gameObject, iTween.Hash("position", targetVec,  "time", 2f));

		//SceneManager.Instance.activeCamera.camPivot.position = targetVec;




	}

	public override void OnUpdate()
	{
		targetVec = CalculateTarget (SceneManager.Instance.activeCamera);
		//iTween.LookUpdate(SceneManager.Instance.activeCamera.gameObject, SceneManager.Instance.activeCamera.target.position, 2f);
		//iTween.MoveUpdate(SceneManager.Instance.activeCamera.camPivot.gameObject, targetVec,  2f );
		SceneManager.Instance.activeCamera.transform.LookAt(SceneManager.Instance.activeCamera.lookAtPointer.position);
	}
	
 
	public override void Refresh (CameraController cam)
	{
		base.Refresh (cam);

		targetVec =CalculateTarget (cam);

		iTween.MoveTo(SceneManager.Instance.activeCamera.camPivot.gameObject, iTween.Hash("position", targetVec, "time", 5f, "easetype", iTween.EaseType.linear ));

		 
		Vector3 offset = Vector3.zero;

		/*if(cam.ActiveArea != null)
			offset = cam.ActiveArea.lookOffset;
		iTween.MoveTo(cam.lookAtPointer.gameObject, iTween.Hash("position", cam.target.position + offset, "time", 2f, "easetype", iTween.EaseType.linear ));

*/

		//iTween.LookUpdate(SceneManager.Instance.activeCamera.gameObject, SceneManager.Instance.activeCamera.target.position, 2f);
		//iTween.LookTo(SceneManager.Instance.activeCamera.gameObject, iTween.Hash("looktarget", SceneManager.Instance.activeCamera.target, "time", 2f ));

		//SceneManager.Instance.activeCamera.transform.LookAt(SceneManager.Instance.activeCamera.lookAtPointer.position);
   
	}

	Vector3 targetVec;
	#if UNITY_EDITOR	
	void OnDrawGizmos()
	{

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(targetVec, 0.3f);	 
		//Handles.Label (targetVec, gameObject.transform.parent.gameObject.name);
		Gizmos.color = Color.white;
	}
#endif

	Vector3 CalculateTarget(CameraController cam)
	{
		Vector3 playerPos = SceneManager.Instance.activePC.transform.position;
		
		Bounds b = area.GetComponent<Collider>().bounds;
		Bounds b2 = camMoveArea.bounds;
		
		
		Vector3 v = playerPos - b.min;
		
		float percX = v.x / b.size.x;
		float percY = v.y / b.size.y;
		float percZ = v.z / b.size.z;
		
		cam.camTarget.position = new Vector3 (b2.min.x + (b2.size.x * percX), b2.min.y + (b2.size.y * percY), b2.min.z + (b2.size.z * percZ));


		return cam.camTarget.position;
	}

 



}
