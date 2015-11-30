using UnityEngine;
using System.Collections;

public class RC_MainMenu : RootControl 
{
		Guard_Main guardMain;
		Guard_Social guardSocial;

		protected override void Awake()
		{
				base.Awake ();

				guardMain = this.AddController<Guard_Main> ();
				guardSocial = this.AddController<Guard_Social> ();
		}

		protected override void Start()
		{
				Show (guardMain.Ctrl);
		}

}
