using UnityEngine;
using System.Collections;

public class SceneContext 
{
	protected InputMode input;

	public SceneContext(InputMode input)
	{
		this.input = input;
	}

	public virtual void Begin()
	{

	}

	public virtual void End()
	{

	}

	public virtual void OnMouseClick(Vector3 clickPos)
	{


	}

	public virtual void OnMouseMove(Vector3 mouse, UICanvas hudCanvas)
	{

	}

	public virtual void OnAxis(AxisType axisType, float x, float y)
	{

	}

	public virtual void OnAxisUp(AxisType axisType)
	{

	}

	public virtual void OnButtonDown(ButtonType buttonType)
	{
		input.SetButton(buttonType, true);
	}

	public virtual void OnButtonUp(ButtonType buttonType)
	{
		input.SetButton(buttonType, false);
	}



}
