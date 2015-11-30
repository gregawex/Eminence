using UnityEngine;
using System.Collections;

public class Portal_Warehouse : PortalManager {



	string cachedPortal;
 
	public override void Open (Portal portal)
	{
		base.Open (portal);

		SceneManager.Instance.ActivePC.CharacterController.enabled = false;
		SceneManager.Instance.ActivePC.Bot.enabled = false;
		foreach(Portal p in portals)
		{
			if(!p.alwaysVisible)
				p.gameObject.SetActive(false);
		}

		portal.gameObject.SetActive(true);

		SceneManager.Instance.ActivePC.Bot.enabled = true;
		SceneManager.Instance.ActivePC.CharacterController.enabled = true;


	}


}
