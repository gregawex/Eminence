using UnityEngine;
using System.Collections;

public class Guard_DownloadBundle : Guard //<Guard_DownloadBundle> 
{
	public Guard_DownloadBundle()
	{
	}
	public Guard_DownloadBundle(RootControl root)
		:base(root)
	{

	}

	public override void OnPanelMessage (object ev, params object[] obj)
	{
		throw new System.NotImplementedException ();
	}
 
}
