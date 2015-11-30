

using UnityEngine;

//[AddComponentMenu("NGUI/Interaction/Button Message")]
public class GregButtonMessage : MonoBehaviour
{
	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
		OnDoubleClick,
	}
	
	public BasePanel target;
	public string message;
  
	public Trigger trigger = Trigger.OnClick;
	public bool includeChildren = false;
	
	float mLastClick = 0f;
	bool mStarted = false;
	
	void Start () { mStarted = true; }
	
	void OnEnable () { /*if (mStarted) OnHover(UICamera.IsHighlighted(gameObject));*/ }
	
	void OnHover (bool isOver)
	{
		if (((isOver && trigger == Trigger.OnMouseOver) ||
		     (!isOver && trigger == Trigger.OnMouseOut))) Send();
	}
	
	void OnPress (bool isPressed)
	{
		if (((isPressed && trigger == Trigger.OnPress) ||
		     (!isPressed && trigger == Trigger.OnRelease))) Send();
	}
	
	void OnClick ()
	{
		float time = Time.realtimeSinceStartup;
		
		if (mLastClick + 0.2f > time)
		{
			if (trigger == Trigger.OnDoubleClick) Send();
		}
		else if (trigger == Trigger.OnClick) Send();
		
		mLastClick = time;
	}
	
	void Send ()
	{
		if (!enabled || !gameObject.active || string.IsNullOrEmpty(message)) return;
		if (target == null)
						return;
		
		if (includeChildren)
		{
			Transform[] transforms = target.GetComponentsInChildren<Transform>();
			
			for (int i = 0, imax = transforms.Length; i < imax; ++i)
			{
				//Transform t = transforms[i];
				target.Send(message);
			}
		}
		else
		{int a=0;
			GregBugger.Log ("UIButtonMessage triggered");
			target.Send (message);
		}
	}
}