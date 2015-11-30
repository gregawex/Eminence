using UnityEngine;
using System.Collections;
using System.IO;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

[ActionCategory(ActionCategory.Character)]
public class Action_LoadCards : FsmStateAction
{
	public FsmString bundleName;
	public BundleLoading loading;
	public FsmEvent finished;

	public override void OnEnter ()
	{
		base.OnEnter ();

		switch(loading)
		{
		case BundleLoading.EDITOR:

			string path = Application.dataPath+"/Bundles/"+bundleName.Value;
			if(!File.Exists(path))
			{
				Debug.LogError("Asset bundle is not on path "+path);
			}

			AssetBundle ab = AssetBundle.CreateFromFile(path);

			CardList [] ts = ab.LoadAllAssets<CardList>();

			foreach(CardList t in ts)
			{
				CardRoster.Instance.FeedCardList(t);
			}



		break;

		case BundleLoading.REMOTE:
			break;

		}

		Fsm.Event(finished);

	}

}
