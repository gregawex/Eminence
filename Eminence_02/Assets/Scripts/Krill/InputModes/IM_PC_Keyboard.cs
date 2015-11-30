using UnityEngine;
using System.Collections;

public class IM_PC_Keyboard : InputMode {
	
	public IM_PC_Keyboard()
	{
		contexts.Add(ContextType.GAMEPLAY, 	new Context_Gameplay(this));
		contexts.Add(ContextType.UI, 		new Context_UI(this));
		contexts.Add(ContextType.PC_BUSY, 	new Context_PCBusy(this));
		
		ActiveContext = contexts[ContextType.GAMEPLAY];
	}

	public static Vector3 inputVec;

	public override bool IsDirectionalInputPressed ()
	{

		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
			return true;

		return base.IsDirectionalInputPressed ();
	}
	/*
========================================================
┌─┐┌─┐┌┬┐┌─┐┌─┐┬  ┌─┐┬ ┬
│ ┬├─┤│││├┤ ├─┘│  ├─┤└┬┘
└─┘┴ ┴┴ ┴└─┘┴  ┴─┘┴ ┴ ┴ 
========================================================
*/
	public class Context_Gameplay : SceneContext
	{
		InputMode input;

		public Context_Gameplay(InputMode input)
			:base (input)
		{
			this.input = input;
		}

		public override void OnAxis (AxisType axisType, float x, float y)
		{
			base.OnAxis (axisType, x, y);

			IM_PC_Keyboard.inputVec = new Vector3(x, 0, y);
			
			if(axisType == AxisType.CRSR_KEYS)
			{
				float angle = ((Mathf.Atan2(y, x) * Mathf.Rad2Deg)- 90f + SceneManager.Instance.activeCamera.transform.rotation.eulerAngles.y);
				//Debug.Log ("x: "+x+" y:"+y);
				//Debug.Log ("angle: "+angle);
				
				SceneManager.Instance.testobj.position = (SceneManager.Instance.ActivePC.transform.position - (Vector3.forward * 1.2f));
				SceneManager.Instance.testobj.RotateAround(SceneManager.Instance.ActivePC.transform.position, Vector3.up, angle);
				
				if(!(SceneManager.Instance.ActivePC.ActiveOp is Op_ControlledWalk))
					SceneManager.Instance.ActivePC.ActiveOp.OnOpInstruction(ActorOp.OpInstruction.DIRECT_FLOOR, null);
				
			}
		}

		public override void OnButtonDown (ButtonType buttonType)
		{
			base.OnButtonDown (buttonType);

			if(SceneManager.Instance.ItemInFocus != null)
			{
				if(buttonType == ButtonType.Y)
					SceneManager.Instance.ItemInFocus.OnUse(SceneManager.Instance.ActivePC, ItemEvent.USE);
			}



			
		}

		public override void OnButtonUp (ButtonType buttonType)
		{
			base.OnButtonUp (buttonType);


		}
	}
	/*
========================================================
┬ ┬┬
│ ││
└─┘┴
========================================================
*/
	public class Context_UI : SceneContext
	{
		public Context_UI(InputMode input)
			:base (input)
		{
			
		}
	}
	
	/*
========================================================
┌─┐┌─┐    ┌┐ ┬ ┬┌─┐┬ ┬
├─┘│      ├┴┐│ │└─┐└┬┘
┴  └─┘────└─┘└─┘└─┘ ┴ 
========================================================
*/
	public class Context_PCBusy : SceneContext
	{
		public Context_PCBusy(InputMode input)
			:base (input)
		{
			
		}
	}
	
	
}
