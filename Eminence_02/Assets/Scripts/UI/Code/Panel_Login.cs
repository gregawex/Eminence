using UnityEngine;
using System.Collections;

public class Panel_Login : BasePanel 
{



	protected override void TransRequest (string eventName)
	{

		if(eventName == "Trans_Channel1")
		{
			base.TransRequest(eventName);
		}
		else{
			bool isOk = false;
			if(isOk)
				base.TransRequest (eventName);
			else
				base.TransRequest("Trans_Error");
		}
	}

	public override void FinalizeProcess()
	{

	}


}
