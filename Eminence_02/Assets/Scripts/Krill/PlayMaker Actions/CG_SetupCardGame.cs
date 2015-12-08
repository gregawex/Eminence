using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
public class CG_SetupCardGame : FsmStateAction 
{

	public FsmEvent finished;
	

	public override void OnEnter ()
	{

		//HACK this will have to be replaced by server pull code

		//HACK.. 
		CardPack pack_red = GregPacker.CreateSingle<CardPack>();
		pack_red.CardTokens = new System.Collections.Generic.List<string>();
		pack_red.CardTokens.Add("2d574ada-1f71-4ff8-a4d7-382e4d16346d");
		pack_red.CardTokens.Add("5f75e91f-05d0-4fee-837d-0418b4f0f0a3");
		pack_red.CardTokens.Add("6d91beae-7460-488b-b05d-adb74300f3f4");
		pack_red.CardTokens.Add("6f8ed173-06bc-4971-95ab-06f71627869d");
		pack_red.CardTokens.Add("28e045d5-aedd-4015-80bf-8e1df7e14a43");

		CardPack pack_blu = GregPacker.CreateSingle<CardPack>();
		pack_blu.CardTokens = new System.Collections.Generic.List<string>();
		pack_blu.CardTokens.Add("887404c3-26b8-43fb-90ad-fc0f6783f1d0");
		pack_blu.CardTokens.Add("bed46c7a-7a0f-4fb3-b1cc-706995e0536b");
		pack_blu.CardTokens.Add("d7068df3-e157-466f-a4e9-97ed90edbc92");
		pack_blu.CardTokens.Add("f9a5a4ad-5f35-4168-afbb-8889fa17bf2c");
		pack_blu.CardTokens.Add("f9a5a4ad-5f35-4168-afbb-8889fa17bf2c");

		CGPlayer player_red = new CGPlayer("userID_43784279", pack_red, PlayerColor.RED);
		CGPlayer player_blu = new CGPlayer("userID_63765327", pack_blu, PlayerColor.BLUE);

		//REAL..
		 
		List<CardInstance> redInstances = new List<CardInstance>();
		List<CardInstance> bluInstances = new List<CardInstance>();

		foreach(CardItem cardItem in player_red.CardItems)
		{
			redInstances.Add(CreateCardInstance(cardItem, player_red.Color));
		}

		foreach(CardItem cardItem in player_blu.CardItems)
		{
			bluInstances.Add(CreateCardInstance(cardItem, player_blu.Color));
		}

		CardSceneManager.Instance.tempCard.gameObject.SetActive(false);

		player_red.FeedCardInstances(redInstances.ToArray());
		player_blu.FeedCardInstances(bluInstances.ToArray());

		CardSceneManager.Instance.Init(player_red, player_blu);

		Fsm.Event(finished);
	}

	private CardInstance CreateCardInstance(CardItem cardItem, PlayerColor color)
	{
		CardInstance tempCard = CardSceneManager.Instance.tempCard;
		GameObject go = GameObject.Instantiate(tempCard.gameObject) as GameObject;
		go.transform.parent = tempCard.transform.parent;
		go.transform.position = tempCard.transform.position;
		go.transform.localScale = tempCard.transform.localScale;

		go.name = "card_"+cardItem.name;

		CardInstance inst = go.GetComponent<CardInstance>();

		inst.Init(cardItem, color);

		return inst;
	}

	public override void OnUpdate ()
	{
		base.OnUpdate ();


	}

}
