using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class SceneManager : MonoBehaviour, EventListener {

	public bool doUnpackOnStart = true;
 


	 
	public bool showAreas;
	bool lastShowAreas;

	public CameraController activeCamera;
	//public AdventureController activeCtrl;
	public PlayableCharacter activePC;
	public PlayableCharacter ActivePC { get { return activePC; } }
	 

	public Transform testobj;

	public Texture2D mouse_normal, mouse_clickable;

	public CanvasScaler canvasScaler;
	public UICanvas uiCanvas;

	public HUDCanvas hudCanvas;
 

	GameItem itemInFocus;
	public GameItem ItemInFocus { get { return itemInFocus;  } }

	SceneLogic sceneLogic;
	SceneRoster sceneRoster;

	Dictionary<object, Action<object>> channels = new Dictionary<object, Action<object>>();


	//string is Guid
	Dictionary<string, GameItem> itemPool = new Dictionary<string, GameItem>();


	static SceneManager instance;
	public static SceneManager Instance { get { return instance; } }

 
	Dictionary<string, PlayableCharacter> allPlayableCharacters;
	 

	void Awake()
	{
		//////HACK 
		//GameManager.LandingInit(new IM_PC_Classic_PAC());
		//GameManager.LandingInit(new IM_Console_Controller());
		GameManager.LandingInit(new IM_PC_Keyboard());


		GregPacker.Unpack(GameItem.SLOTNAME);
		GregPacker.Unpack(SoupItem.SLOTNAME);



		//inventory = GameObject.FindObjectOfType<Inventory>();

		new TypeManager ();

		instance = this;
		allPlayableCharacters = new Dictionary<string, PlayableCharacter> ();

		Eve.Listen<E_AreaEntered> (AreaEntered, this);
		Eve.Listen<E_SoupRequest> (SoupRequest, this);



		PlayableCharacter [] allCtrls = GameObject.FindObjectsOfType<PlayableCharacter> ();

		foreach (PlayableCharacter ac in allCtrls) {
			allPlayableCharacters.Add(ac.gameObject.GetComponent<PlayableCharacter>().name, ac);
		}

		if (activePC != null)
						activePC.IsUserControlled = true;

		ToggleAreas (true);
	}



	// Use this for initialization
	void Start () {



		if(mouse_normal != null)
			Cursor.SetCursor (mouse_normal, Vector2.zero, CursorMode.Auto);

		 
	
	}


	void OnGUI()
	{
		/*
		int  i = 0;
		if(Buzz.Buzzes != null)
		foreach(BuzzUnit bu in Buzz.Buzzes)
		{
			GUI.Label(new Rect(100, 100 + i, 300, 20), bu.tag+" -> "+bu.Perc);

			                   i+=20;
		}

		GUI.Label(new Rect(10, 10, 300, 20), "Context: "+GameManager.Input.ActiveContext.ToString());
		GUI.Label(new Rect(10, 30, 300, 20), "ActivePC: "+ActivePC);
		if(this.activePC.ActiveItem != null)
			GUI.Label(new Rect(10, 50, 300, 20), "ActiveItem: "+ActivePC.ActiveItem);
		else
			GUI.Label(new Rect(10, 50, 300, 20), "[NO ActiveItem]");

		if(this.activePC.ActiveOp != null)
			GUI.Label(new Rect(10, 70, 300, 20), "ActiveOp: "+ActivePC.ActiveOp);
		else 
			GUI.Label(new Rect(10, 70, 300, 20), "[NO ActiveOp]");

		if(this.activePC.ActiveState != null)
			GUI.Label(new Rect(10, 90, 300, 20), "ActiveState: "+ActivePC.ActiveState);
		else 
			GUI.Label(new Rect(10, 90, 300, 20), "[NO ActiveState]");

		if(this.activePC.ActiveArea != null)
			GUI.Label(new Rect(10, 110, 300, 20), "ActiveArea: "+ActivePC.ActiveArea);
		else
			GUI.Label(new Rect(10, 110, 300, 20), "[NO ActiveArea]");

*/

		GUI.Label(new Rect(10, 10, 300, 20), "[Move] crsr keys");
		GUI.Label(new Rect(10, 30, 300, 20), "[Attack] x");
		GUI.Label(new Rect(10, 50, 300, 20), "[SpinAttack] hold x for 1 second");
		GUI.Label(new Rect(10, 70, 300, 20), "[Defense] hold y");
		GUI.Label(new Rect(10, 90, 300, 20), "[Run] crsr + hold b");
	}


	public void ItemEnabled(GameItem item)
	{
		if(!itemPool.ContainsKey(item.ItemPoolGuid))
		{
			itemPool.Add(item.ItemPoolGuid, item);
			GregBugger.LogSystem("item enabled "+item.itemName);

		}
	}

	public void ItemDisabled(GameItem item)
	{
		if(itemPool.ContainsKey(item.guid))
		{
			itemPool.Remove(item.guid);
			GregBugger.LogSystem("item disabled "+item.name);
		}
	}
 

	void AreaEntered(BaseEvent ev)
	{

		E_AreaEntered e = ev as E_AreaEntered;



		if (e.Actor == activePC) {
						Text text = uiCanvas.areaText.GetComponent<Text> ();
						text.text = (ev as E_AreaEntered).GetAreaName ();

			CameraInfo.TransitionMode vm = CameraInfo.TransitionMode.SMOOTH;

			if(activeCamera.CameraInfo is FixedCameraInfo)
			{
				vm = CameraInfo.TransitionMode.SNAP;
			}

			activeCamera.CalcSiblingAreaRects();
			//activeCamera.CameraInfo = e.Area.camInfo;
			//activeCamera.CameraInfo.Begin(activeCamera, vm);
		}
	}

	void LateUpdate()
	{

		//if (activeCamera != null)
		//	activeCamera.Refresh ();
	}
	float deltaTime;
	// Update is called once per frame

	void ToggleAreas(bool on)
	{
		Area [] areas = GameObject.FindObjectsOfType<Area>();
		
		foreach(Area a in areas)
		{
			a.gameObject.GetComponent<Collider>().enabled = on;
			Renderer r = a.gameObject.GetComponent<Renderer>();
			if(r != null)
				r.enabled = on;
			//a.gameObject.SetActive(on);

		}
	}

	public void ChangeContext(ContextType contextType)
	{
		GameManager.Input.ChangeContext(contextType);
	}

	public void SwitchControlTo(string name)
	{
		if (allPlayableCharacters.ContainsKey (name)) 
		{

			//activePC.Bot.target = null;
			PlayableCharacter pc = allPlayableCharacters[name];
			activePC = pc;
			activeCamera.CameraInfo = activePC.ActiveArea.camInfo;
			activeCamera.target = activePC.transform;
			//activeCamera.GetComponent<CamComponent>().SetDOFTargetDirectly(activePC.transform);
			//activeCamera.GetComponent<DepthOfField34>();
			activeCamera.SwitchTarget();

			//activeCamera.target = activePC.transform;
			//activePC.CurrentArea.camInfo.Begin
		}
	}

 

	void SoupRequest(BaseEvent ev)
	{
		//soup.OnRequest((ev as E_SoupRequest).soupName);
	}

	public void Say(string text, Actor actor)
	{


		uiCanvas.speechBox.gameObject.SetActive(true);

		uiCanvas.speechBox.text.color = actor.speechTextColor;
		uiCanvas.speechBox.text.text = "["+actor.name+"] "+text;


	}

	public void CloseLens()
	{
		//uiCanvas.actionSelector.mainLens.Close(Lens.CloseMode.CLOSE_ALL);
		activeCamera.target = activePC.transform;
	}
	


	void Update () {

		Buzz.Update();

		if (Application.isEditor && !Application.isPlaying && lastShowAreas != showAreas) 
		{
			ToggleAreas(showAreas);

			lastShowAreas = showAreas;
			
		}

		//----------------------
		//ITEM HIGHLIGHT
		//----------------------

		if(activePC != null)
		{
			itemInFocus = FindItemInFocus();
			
		}

		if(Input.GetKeyDown (KeyCode.F7))
		{
			GregPacker.TraceDirties();
		}
		if(Input.GetKeyDown (KeyCode.F8))
		{
			GregPacker.PackDirty();
	
		}
 
		if (Input.GetKeyDown (KeyCode.F10))
						GregBugger.Toggle ();

 
		
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
 
		if(uiCanvas.fpsText != null)
		uiCanvas.fpsText.text = "FPS: "+ (int)fps;


		bool clickedOn = false;
		Vector3 clickPos = Vector3.zero;

		if (Input.GetMouseButtonDown (0)) {
						clickedOn = true;
						clickPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
				} else if (Input.touchCount > 0) {

						clickedOn = true;
						clickPos = new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0);
				}


		if (clickedOn) 
		{
			GameManager.Input.ActiveContext.OnMouseClick(clickPos);
		} 

		else 
		{
			GameManager.Input.ActiveContext.OnMouseMove(Input.mousePosition, uiCanvas);
		}


		//----------------------------------
		// DIRECTIONAL
		//----------------------------------
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		float hor_right = Input.GetAxis ("RightAnalog_Horizontal");
		float ver_right = Input.GetAxis ("RightAnalog_Vertical");


		if(Mathf.Abs(hor) > 0.6f
		   || Mathf.Abs(ver) > 0.6f)
		{
			GameManager.Input.ActiveContext.OnAxis(AxisType.LEFT_THUMBSTICK, hor, ver);
		}
		else{
			GameManager.Input.ActiveContext.OnAxisUp(AxisType.LEFT_THUMBSTICK);
		}

		float crsr_hor = 0;
		float crsr_ver = 0;

		if(Input.GetKey(KeyCode.UpArrow))
			crsr_ver = -1;
		else if(Input.GetKey(KeyCode.DownArrow))
			crsr_ver = 1;

		if(Input.GetKey(KeyCode.RightArrow))
			crsr_hor = 1;
		else if(Input.GetKey(KeyCode.LeftArrow))
			crsr_hor = -1;



		if(crsr_hor != 0 || crsr_ver 
		   !=0)
		{
			GameManager.Input.ActiveContext.OnAxis(AxisType.CRSR_KEYS, crsr_hor, crsr_ver);
		}

		//----------------------------------
		// BUTTONS AND KEYS
		//----------------------------------
 
		ButtonType bt = ButtonType.NONE;


			if(Input.GetKeyDown(KeyCode.A))
			GameManager.Input.ActiveContext.OnButtonDown(ButtonType.A);
			else if(Input.GetKeyDown(KeyCode.B))
			GameManager.Input.ActiveContext.OnButtonDown(ButtonType.B);
			else if(Input.GetKeyDown(KeyCode.X))
			GameManager.Input.ActiveContext.OnButtonDown(ButtonType.X);
			else if(Input.GetKeyDown(KeyCode.Y))
			GameManager.Input.ActiveContext.OnButtonDown(ButtonType.Y);

		if(Input.GetKeyUp(KeyCode.A))
			GameManager.Input.ActiveContext.OnButtonUp(ButtonType.A);
		else if(Input.GetKeyUp(KeyCode.B))
			GameManager.Input.ActiveContext.OnButtonUp(ButtonType.B);
		else if(Input.GetKeyUp(KeyCode.X))
			GameManager.Input.ActiveContext.OnButtonUp(ButtonType.X);
		else if(Input.GetKeyUp(KeyCode.Y))
			GameManager.Input.ActiveContext.OnButtonUp(ButtonType.Y);

		

		/*if(Input.GetKeyDown("joystick button 0"))
		{
			Debug.Log ("BUTTON A");
			GameManager.Input.ActiveContext.OnButtonDown(ButtonType.A);
		}
		else if(Input.GetKeyDown("joystick button 1"))
		{
			GameManager.Input.ActiveContext.OnButtonDown(ButtonType.B);
		}

		if(Input.GetKeyUp("joystick button 0"))
		{
			GameManager.Input.ActiveContext.OnButtonUp(ButtonType.A);
		}
		else if(Input.GetKeyUp("joystick button 1"))
		{
			GameManager.Input.ActiveContext.OnButtonUp(ButtonType.B);
		}

		*/
	}

	GameItem FindItemInFocus()
	{
		foreach(KeyValuePair<string, GameItem> kvp in itemPool)
		{

			float dist = Vector3.Distance(new Vector3(kvp.Value.transform.position.x, activePC.transform.position.y, kvp.Value.transform.position.z), activePC.transform.position);
			float heightDiff = Mathf.Abs (kvp.Value.transform.position.y - activePC.transform.position.y);

			 

			if(dist < 2f
			   && heightDiff < 2)
			{
				// now check if we face the item
				float dot = Vector3.Dot(activePC.transform.forward, 
				                        //kvp.Value.transform.position
				                        ((new Vector3(kvp.Value.transform.position.x, activePC.transform.position.y, kvp.Value.transform.position.z) - activePC.transform.position).normalized)
				                        );
			 
				
				if(dot > 0.3f)
				{
					if(itemInFocus != kvp.Value)
					{
						GregBugger.LogHighlight("item active: "+kvp.Value.itemName);

						Transform focusPoint = kvp.Value.GetFocusPoint();

						if(focusPoint != null)
						{
							hudCanvas.FocusOn(focusPoint, kvp.Value.itemName);
						}
						else
						{
							//Fallback to the item's postioion. This will give rubbish results but at least
							//it won't crash if you forget to provide focus points
							hudCanvas.FocusOn(kvp.Value.transform, kvp.Value.itemName);

						}
					}

					return kvp.Value;
				}
			}
		}

		hudCanvas.FocusOff();
		return null;
	}
 
}
