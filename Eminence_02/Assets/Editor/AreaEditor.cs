using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Area))]
public class AreaEditor : Editor
{

	public override void OnInspectorGUI()
	{
		Area area = target as Area;



		area.name = EditorGUILayout.TextField ("Area Name", area.name);
		area.lookOffset = EditorGUILayout.Vector3Field("Look Offset", area.lookOffset);


		if (area.camInfo == null) {
			CameraController.ViewMode picked = CameraController.ViewMode.NONE;



			if(GUILayout.Button("Add Zone Cam")) picked = CameraController.ViewMode.ZONE_FOLLOW;
			if(GUILayout.Button("Add Pointer Cam")) picked =CameraController.ViewMode.POINTER_FOLLOW;
			if(GUILayout.Button("Add Fixed Cam")) picked = CameraController.ViewMode.FIXED_FOLLOW;
				
		
			switch(picked)
			{
			case CameraController.ViewMode.ZONE_FOLLOW:
				CreateCamInfo<ZoneCameraInfo>(area);

				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.parent = area.camInfo.gameObject.transform;
				cube.transform.localPosition = Vector3.zero;
				cube.transform.rotation = area.transform.rotation;
				(cube.GetComponent<Collider>() as BoxCollider).isTrigger = true;
				cube.GetComponent<Renderer>().enabled = false;
				cube.gameObject.layer = 2;
				(area.camInfo as ZoneCameraInfo).camMoveArea = (cube.GetComponent<Collider>() as BoxCollider);
				//cube.transform.localScale = area.transform.localScale / 2;

				break;
			case CameraController.ViewMode.POINTER_FOLLOW:
				CreateCamInfo<PointerCameraInfo>(area);
				break;
			case CameraController.ViewMode.FIXED_FOLLOW:
				CreateCamInfo<FixedCameraInfo>(area);
				GameObject g = new GameObject();
				(area.camInfo as FixedCameraInfo).cameraPosition = g.transform;
				g.transform.parent = area.transform;
				g.transform.localPosition = Vector3.zero;
				break;
			}

		}
	}

	void CreateCamInfo<T> (Area area) where T : CameraInfo
	{
		GameObject go = new GameObject (typeof(T).Name);
		go.transform.parent = area.transform;
		go.transform.localPosition = Vector3.zero;
		go.AddComponent<T> ();

		area.camInfo = go.GetComponent<CameraInfo> ();
		area.camInfo.area = area;

	}
 
}
