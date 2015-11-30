using UnityEngine;
using System.Collections;

public abstract class CameraInfo : MonoBehaviour {


	public Area area;

	protected CameraController camera;

	public enum TransitionMode { SNAP, SMOOTH }


	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	public  virtual void OnUpdate () {
	
	}

	public virtual void Begin(CameraController camera, TransitionMode transitionMode)
	{

		this.camera = camera;

		if(transitionMode == TransitionMode.SNAP)
		{
			iTween.Stop(SceneManager.Instance.activeCamera.camPivot.gameObject);
			iTween.Stop(SceneManager.Instance.activeCamera.lookAtPointer.gameObject);
		}
	}

	public virtual void Refresh(CameraController camera)
	{

	}
}
