using UnityEngine;
using System.Collections;

public class PointerCameraInfo : CameraInfo {


	public float distanceFromTarget;
	public float camTiltAngle;
	public float camYAngle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public override void Begin (CameraController camera, TransitionMode transitionMode)
	{
		base.Begin (camera, transitionMode);

		Vector3 t = CalculateTarget (camera);

		if (transitionMode == TransitionMode.SNAP) {
			camera.camTarget.position = t;
			camera.transform.position = t;
		}
	}

	public override void OnUpdate ()
	{
		base.OnUpdate ();


		base.camera.camPivot.position = base.camera.target.position;
		base.camera.camPivot.rotation = Quaternion.identity;
		base.camera.camPivot.RotateAround(base.camera.camPivot.transform.position, Vector3.up, base.camera.camYAngle);
		base.camera.camPivot.RotateAround(base.camera.camPivot.transform.position, base.camera.camPivot.transform.right, base.camera.camTiltAngle);
		
		//camTargetPosition = (Vector3.back * distanceFromTarget) + camPivot.position;
		
		base.camera.camTarget.localPosition = new Vector3(0,0,base.camera.distanceFromTarget);


 
		base.camera.transform.position = base.camera.camTarget.position;

		SceneManager.Instance.activeCamera.transform.LookAt(base.camera.camPivot.position/*SceneManager.Instance.activeCamera.lookAtPointer.position*/);
	}



	private Vector3 CalculateTarget(CameraController cam)
	{
		cam.camPivot.position = cam.target.position;
		cam.camPivot.rotation = Quaternion.identity;
		cam.camPivot.RotateAround(cam.camPivot.transform.position, Vector3.up, cam.camYAngle);
		cam.camPivot.RotateAround(cam.camPivot.transform.position, cam.camPivot.transform.right, cam.camTiltAngle);
		
		//camTargetPosition = (Vector3.back * distanceFromTarget) + camPivot.position;
		
		cam.camTarget.localPosition = new Vector3(0,0,cam.distanceFromTarget);

		return cam.target.position;
	}
}
