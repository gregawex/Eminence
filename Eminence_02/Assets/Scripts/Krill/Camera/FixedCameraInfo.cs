using UnityEngine;
using System.Collections;

public class FixedCameraInfo : CameraInfo
{

	public Transform cameraPosition;


	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(cameraPosition.position, 0.1f);
	}

	public override void Begin (CameraController camera, TransitionMode transitionMode)
	{
		base.Begin (camera, transitionMode);

		camera.camTarget.position = cameraPosition.position;
		camera.transform.position = cameraPosition.position;
	}

	public override void OnUpdate()
	{

		//iTween.LookUpdate(SceneManager.Instance.activeCamera.gameObject, SceneManager.Instance.activeCamera.target.position, 2f);
		//iTween.MoveUpdate(SceneManager.Instance.activeCamera.camPivot.gameObject, targetVec,  2f );
		SceneManager.Instance.activeCamera.transform.LookAt(SceneManager.Instance.activeCamera.lookAtPointer.position);
	}

	public override void Refresh (CameraController camera)
	{
		base.Refresh (camera);

		camera.camTarget.position = cameraPosition.position;
		camera.transform.position = cameraPosition.position;

		camera.transform.LookAt(camera.target.position + camera.camTargetPosition);
	}
 
}
