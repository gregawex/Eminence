using UnityEngine;
using System.Collections;

public class IM_PC_Classic_PAC : InputMode 
{

	public IM_PC_Classic_PAC()
	{
		contexts.Add(ContextType.GAMEPLAY, 	new Context_Gameplay(this));
		contexts.Add(ContextType.UI, 		new Context_UI(this));
		contexts.Add(ContextType.PC_BUSY, 	new Context_PCBusy(this));
		
		ActiveContext = contexts[ContextType.GAMEPLAY];
	}

	public class Context_Gameplay : SceneContext
	{
		public Context_Gameplay(InputMode input)
			:base (input)
		{
			
		}
		
		
		public override void Begin ()
		{
			base.Begin ();
		}
		
		public override void OnMouseClick (Vector3 clickPos)
		{
			base.OnMouseClick (clickPos);
			
			if(SceneManager.Instance.ActivePC.ActiveOp == null)
			{
				GregBugger.LogError("no active Op in ["+SceneManager.Instance.ActivePC.name+"]");
				return;
			}
			
			Ray r = SceneManager.Instance.activeCamera.GetComponent<Camera>().ScreenPointToRay (clickPos);
			
			RaycastHit hitInfo;
			Physics.Raycast (r, out hitInfo, 1000);
			
			
			if (hitInfo.collider != null) 
			{
				
				int layer = hitInfo.collider.gameObject.layer;
				
				float dist = Vector3.Distance(hitInfo.point, SceneManager.Instance.ActivePC.transform.position);
				if(dist < Constants.PC_NO_CLICK_RADIUS && hitInfo.collider.gameObject.tag == "Floor")
					return;
				
				if(SceneManager.Instance.ActivePC.ActiveOp is Op_Use)
					return;
				
				//========================
				//Clickable/ Stairs/ Floor/ Character
				if (layer == 8 || layer == 9 || layer == 10 || layer == 13) 
				{
						
					
					if(hitInfo.collider.gameObject.tag == "ClickableObject")
					{
						GameItem co = hitInfo.collider.gameObject.GetComponent<GameItem>();
						//Eve.TriggerEvent(new E_ItemEvent(ItemEvent.USE, co, SceneManager.Instance.ActivePC));
						co.OnUse(SceneManager.Instance.ActivePC, ItemEvent.SHOW);
						SceneManager.Instance.activeCamera.target = co.transform;
						SceneManager.Instance.activeCamera.camTargetPosition = Vector3.zero;
						
						SceneManager.Instance.ChangeContext(ContextType.UI);
						
					}
					else if(hitInfo.collider.gameObject.tag == "InstantClickable")
					{
						GameItem co = hitInfo.collider.gameObject.GetComponent<GameItem>();
						//Eve.TriggerEvent(new E_ItemEvent(ItemEvent.USE, co, SceneManager.Instance.ActivePC));
						co.OnUse(SceneManager.Instance.ActivePC, ItemEvent.USE);
					}
					else if(hitInfo.collider.gameObject.tag == "Character"
					        || hitInfo.collider.gameObject.tag == "PC")
					{
						Actor actor = hitInfo.collider.gameObject.GetComponent<Actor>();
						Eve.TriggerEvent(new E_SoupRequest(actor.name));
						SceneManager.Instance.ChangeContext(ContextType.UI);
					}
					else if(hitInfo.collider.gameObject.tag == "Floor")
					{
						
						
						SceneManager.Instance.testobj.position = hitInfo.point;
						
						SceneManager.Instance.ActivePC.ActiveOp.OnOpInstruction(ActorOp.OpInstruction.ON_FLOOR, null);
					}
				}
				else if(hitInfo.collider.gameObject.tag == "ClickableObject")
				{
					GameItem co = hitInfo.collider.gameObject.GetComponent<GameItem>();
					//Eve.TriggerEvent(new E_ItemEvent(ItemEvent.USE, hitInfo.collider.gameObject.GetComponent<GameItem>(), SceneManager.Instance.ActivePC));
					co.OnUse(SceneManager.Instance.ActivePC, ItemEvent.SHOW);
					SceneManager.Instance.ChangeContext(ContextType.UI);
				}
				/*else if(hitInfo.collider.gameObject.tag == "Trigger")
				{
					hitInfo.collider.gameObject.GetComponent<Trigger>().OnClick();
				}*/
				
			}
		}
		
		public override void OnMouseMove (Vector3 mouse, UICanvas hudCanvas)
		{
			base.OnMouseMove (mouse, hudCanvas);
			
			Ray r = SceneManager.Instance.activeCamera.GetComponent<Camera>().ScreenPointToRay(mouse);
			
			
			
			RaycastHit hitInfo;
			Physics.Raycast (r, out hitInfo, 1000);
			
			if(hitInfo.collider != null)
			{
				string tag = hitInfo.collider.gameObject.tag;
				if(tag == "ClickableObject")
				{
					GameItem gi = hitInfo.collider.gameObject.GetComponent<GameItem>();
					if (gi == null) return;
					
					hudCanvas.descriptorLabel.gameObject.SetActive(true);
					hudCanvas.descriptorLabel.text = gi.name;
					Cursor.SetCursor(SceneManager.Instance.mouse_clickable, Vector2.zero, CursorMode.Auto);
					
					float x = Input.mousePosition.x ;//+ hudCanvas.descriptorLabel.preferredWidth ;//- (Screen.width / 2);
					float y = Input.mousePosition.y + 20 ;//- (Screen.height / 2);
					hudCanvas.descriptorLabel.rectTransform.position = new Vector3(x, y, 0);
				}
				else if(tag == "PC" && hitInfo.collider.gameObject != SceneManager.Instance.gameObject)
				{
					hudCanvas.descriptorLabel.gameObject.SetActive(true);
					hudCanvas.descriptorLabel.text = hitInfo.collider.gameObject.GetComponent<Actor>().name;
					Cursor.SetCursor(SceneManager.Instance.mouse_clickable, Vector2.zero, CursorMode.Auto);
					
					float x = Input.mousePosition.x ;//+ hudCanvas.descriptorLabel.preferredWidth ;//- (Screen.width / 2);
					float y = Input.mousePosition.y + 20 ;//- (Screen.height / 2);
					hudCanvas.descriptorLabel.rectTransform.position = new Vector3(x, y, 0);
				}
				else{
					Cursor.SetCursor(SceneManager.Instance.mouse_normal, Vector2.zero, CursorMode.Auto);
					hudCanvas.descriptorLabel.gameObject.SetActive(false);
				}
			}
		}
		
		public override void End ()
		{
			base.End ();
		}
		
		
		
	}



	
	//==================================================	
	public class Context_PCBusy : SceneContext 
	{
	
		public Context_PCBusy(InputMode input)
			:base (input)
		{
			
		}
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		public override void Begin ()
		{
			base.Begin ();
			
			Cursor.visible = false;
		}
		
		public override void End ()
		{
			base.End ();
			
			Cursor.visible = true;
		}
	}


	//==================================================
	public class Context_UI : SceneContext 
	{
		
		public Context_UI(InputMode input)
			:base (input)
		{
			
		}
		public override void Begin ()
		{
			base.Begin ();
		}
		
		public override void OnMouseClick (Vector3 clickPos)
		{
			base.OnMouseClick (clickPos);
		}
		
		public override void OnMouseMove (Vector3 mouse, UICanvas hudCanvas)
		{
			base.OnMouseMove (mouse, hudCanvas);
		}
		
		public override void End ()
		{
			base.End ();
		}
	}

}
