using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public abstract class Guard 
{

	public BasePanel Ctrl {  get ; private set; }

	public T Controller<T> () where T : BasePanel
	{
		return Ctrl as T;
	}

	protected RootControl root; 
	public RootControl Root { get { return root; } }

	abstract public void OnPanelMessage (object ev, params  object[] obj);
	public Guard(){
		}
	public Guard(RootControl r){
		}
	public void Init(RootControl root)
	{
		this.root = root;


		string prefabName = GetType ().Name.Replace ("Guard", "Panel");

		if (!string.IsNullOrEmpty (prefabName)) 
		{
			Type type = Assembly.GetExecutingAssembly().GetType(prefabName);
			 

			Ctrl = GameObject.Instantiate(Resources.Load(type.Name, type)) as BasePanel;
			Ctrl.Guard = (this);
		}
	}

	public virtual void Destroy()
	{
		root = null;
		Ctrl = null;
	}



}
