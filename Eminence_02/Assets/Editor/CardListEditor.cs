using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.IO;

public class CardListEditor : EditorWindow {
	
	 
	//public parameter
	public CardList cardItemList;
	private int viewIndex = 1;


	//private parameters
	string activeCardList;

	static List<FieldInfo> fields;
 

	void OnEnable()
	{
		if(EditorPrefs.HasKey("ObjectPath")) {
			string objectpath = EditorPrefs.GetString("ObjectPath");
			cardItemList = AssetDatabase.LoadAssetAtPath(objectpath,typeof(CardList)) as CardList;

			activeCardList = Path.GetFileNameWithoutExtension(objectpath);
		}
	}
	
	void OnGUI()
	{



		GUILayout.BeginHorizontal();
		GUILayout.Label("Active Item List",EditorStyles.boldLabel);
		GUILayout.Label(""+activeCardList);

		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Card Item Editor",EditorStyles.boldLabel);
		
		if(cardItemList != null) {
			if(GUILayout.Button("Select Card List File")) {
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = cardItemList;
			}
		}
		
		if(GUILayout.Button("Open Card List")) {
			OpenCardList();
		}


		
		GUILayout.EndHorizontal();

		GUILayout.Space(70);
		
		if(cardItemList == null){
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			
			if(GUILayout.Button("Create Card List",GUILayout.ExpandWidth(false))){
				CreateNewCardList();
			}
			
			
			if(GUILayout.Button("Open existing Card List",GUILayout.ExpandWidth(false))){
				OpenCardList();
			}
			
			GUILayout.EndHorizontal();
		}
		
		GUILayout.Space(20);
		
		if(cardItemList != null){
			GUILayout.BeginHorizontal();
			
			GUILayout.Space(5);
			
			if(viewIndex > 1){
				if(GUILayout.Button("First",GUILayout.ExpandWidth(false))){
					viewIndex = 1;
				}
				
				if(GUILayout.Button("Previous",GUILayout.ExpandWidth(false))){
					if(viewIndex > 1){
						viewIndex--;
					}
				}
			}
			else {
				GUILayout.Space(100);
			}
			
			if(viewIndex < cardItemList.cardList.Count){
				if(GUILayout.Button("Next",GUILayout.ExpandWidth(false))){
					if(viewIndex < cardItemList.cardList.Count){
						viewIndex++;
					}
				}
				
				if(GUILayout.Button("Last",GUILayout.ExpandWidth(false))){
					viewIndex = cardItemList.cardList.Count;
				}
			}
			else {
				GUILayout.Space(70);
			}
			
			GUILayout.Space(40);
			
			if(GUILayout.Button("Add Card",GUILayout.ExpandWidth(false))){
				AddCard();
			}
			
			if(cardItemList.cardList.Count > 0) {
				if(GUILayout.Button("Delete Card",GUILayout.ExpandWidth(false))){
					if(viewIndex > 0){
						DeleteCard(viewIndex -1);
					}
				}
			}
			
			GUILayout.EndHorizontal();


			//================================================================================
			//REFL

			if(cardItemList.cardList.Count == 0)
				return;




			if(cardItemList.cardList.Count > 0) {
				GUILayout.BeginHorizontal();
				viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Card",viewIndex,GUILayout.ExpandWidth(false)),1,cardItemList.cardList.Count);
				EditorGUILayout.LabelField("of   " + cardItemList.cardList.Count.ToString() + "   cards", "",GUILayout.ExpandWidth(false));
				GUILayout.EndHorizontal();
				
				GUILayout.Space(10);

				GUILayout.Space(10);
				
				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Card ID");
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].name = EditorGUILayout.TextField("Card Name",cardItemList.cardList[viewIndex-1].name);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				 EditorGUILayout.LabelField("GUID",cardItemList.cardList[viewIndex-1].guid);
				GUILayout.EndHorizontal();

				GUILayout.Space(20);
				
			

				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].division = (Division)EditorGUILayout.EnumPopup("Division",cardItemList.cardList[viewIndex-1].division);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity",cardItemList.cardList[viewIndex-1].rarity);
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].stars = EditorGUILayout.IntSlider("Stars",cardItemList.cardList[viewIndex-1].stars, 1, 5);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].topValue = EditorGUILayout.IntSlider("Top Value",cardItemList.cardList[viewIndex-1].topValue, 1, 10);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].bottomValue = EditorGUILayout.IntSlider("Bottom Value",cardItemList.cardList[viewIndex-1].bottomValue, 1, 10);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].leftValue = EditorGUILayout.IntSlider("Left Value",cardItemList.cardList[viewIndex-1].leftValue, 1, 10);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].rightValue = EditorGUILayout.IntSlider("Right Value",cardItemList.cardList[viewIndex-1].rightValue, 1, 10);
				GUILayout.EndHorizontal();

				GUILayout.Space(20);
				


				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].cardSprite = EditorGUILayout.ObjectField("Main Image",cardItemList.cardList[viewIndex-1].cardSprite,typeof(Texture2D),false) as Texture2D;
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].promoSprite = EditorGUILayout.ObjectField("Promo Image",cardItemList.cardList[viewIndex-1].promoSprite,typeof(Texture2D),false) as Texture2D;
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].teaserSprite = EditorGUILayout.ObjectField("Teaser Image",cardItemList.cardList[viewIndex-1].teaserSprite,typeof(Texture2D),false) as Texture2D;
				GUILayout.EndHorizontal();
		
				
				GUILayout.BeginHorizontal();
				//bundleName = EditorGUILayout.TextField("Bundle Name",bundleName);
				bool doBuild = false;

				if(GUILayout.Button("BUILD ASSET BUNDLES",GUILayout.ExpandWidth(true)))
				{
					doBuild = true;
				}
				GUILayout.EndHorizontal();

				if(doBuild)
					BuildBundle(activeCardList);
			}
				
				/*	


				
				GUILayout.Space(10);
				
				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("In App Purchases IDs");
				GUILayout.EndHorizontal();
				
				cardItemList.cardList[viewIndex-1].NormalPriceId = EditorGUILayout.TextField("Normal Price ID",cardItemList.cardList[viewIndex-1].NormalPriceId);
				cardItemList.cardList[viewIndex-1].PromotionalPriceId = EditorGUILayout.TextField("Promotional Price ID",cardItemList.cardList[viewIndex-1].PromotionalPriceId);
				
				GUILayout.Space(10);
				
				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].cardSprite = EditorGUILayout.ObjectField("Left Hand",cardItemList.cardList[viewIndex-1].cardSprite,typeof(Sprite),false) as Sprite;
				
				if(cardItemList.cardList[viewIndex-1].cardType == CardType.Fist){
					cardItemList.cardList[viewIndex-1].secondHandSprite = EditorGUILayout.ObjectField("Right Hand",cardItemList.cardList[viewIndex-1].secondHandSprite,typeof(Sprite),false) as Sprite;
				}
				
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				if(cardItemList.cardList[viewIndex-1].cardType != CardType.Throwable){
					cardItemList.cardList[viewIndex-1].cardBrokenSprite = EditorGUILayout.ObjectField("Broken Card",cardItemList.cardList[viewIndex-1].cardBrokenSprite,typeof(Sprite),false) as Sprite;
				}
				GUILayout.EndHorizontal();
				
				GUILayout.Space(10);
				
				GUILayout.BeginHorizontal();
				cardItemList.cardList[viewIndex-1].cardIcon = EditorGUILayout.ObjectField("Card Icon",cardItemList.cardList[viewIndex-1].cardIcon,typeof(Sprite),false) as Sprite;
				GUILayout.EndHorizontal();
				
				GUILayout.BeginVertical();
				cardItemList.cardList[viewIndex-1].cardPrefab = EditorGUILayout.ObjectField("CardPrefab",cardItemList.cardList[viewIndex-1].cardPrefab,typeof(Card),false) as Card;
				cardItemList.cardList[viewIndex-1].cardSFX = EditorGUILayout.ObjectField("Card SFX",cardItemList.cardList[viewIndex-1].cardSFX,typeof(AudioClip),false) as AudioClip;
				GUILayout.EndVertical();
			}
			else{
				GUILayout.Label("This Card List is Empty");
			}

			*/
			
			if(GUI.changed){
				EditorUtility.SetDirty(cardItemList);
			}
		}
	}
	
	//----------------------------------------------------------------------------
	// Menu Methods:
	//----------------------------------------------------------------------------



	[MenuItem("Stoker/Card Item Editor %#e")]
	static void Init()
	{
		EditorWindow.GetWindow(typeof(CardListEditor));

		if(fields == null)
			fields = new List<FieldInfo>();
		else
			fields.Clear();

		//Reflect CardITem
		Type t = typeof(CardItem);
		FieldInfo[] fieldInfos = t.GetFields(BindingFlags.Instance | BindingFlags.Public);
		foreach(FieldInfo fi in fieldInfos)
		{
			object [] attObjs = fi.GetCustomAttributes(false);

			if(attObjs.Length > 0)
			{
				if(attObjs[0].GetType().IsSubclassOf(typeof(CardItem)))
				{
					fields.Add(fi);
				}
			}
		}

	}
	
	//----------------------------------------------------------------------------
	// Private Methods:
	//----------------------------------------------------------------------------

	private void BuildBundle(string str)
	{
		//this.Close();


		foreach(CardItem ci in cardItemList.cardList)
		{

			MarkBundle(str, cardItemList);
			MarkBundle(str, ci);

			MarkBundle(str, ci.cardSprite);
			MarkBundle(str, ci.promoSprite);
			MarkBundle(str, ci.teaserSprite);

		}
		string outputPath = Application.dataPath+"/Bundles";
		if(!Directory.Exists(outputPath))
		{
			Directory.CreateDirectory(outputPath);
		}


		CreateAssetBundles.BuildAllAssetBundles(outputPath, false);
		//BuildPipeline.BuildAssetBundles(outputPath);



	}

	private void MarkBundle(string str, UnityEngine.Object obj)
	{
		if(obj == null) return;

		string path = AssetDatabase.GetAssetPath(obj);
		AssetImporter importer = AssetImporter.GetAtPath(path);

		importer.assetBundleName = str;
		//Debug.Log(""+importer.assetPath+" ->> ["+str+"]");

	}
	
	private void AddCard()
	{
		CardItem card = ScriptableObject.CreateInstance<CardItem>();
		//AssetDatabase.AddObjectToAsset(card,cardItemList);
			string guid = Guid.NewGuid().ToString();
		AssetDatabase.CreateAsset(card,"Assets/Resources/Cards/"+guid+".asset");
		card.guid = guid;

		cardItemList.cardList.Add(card);
		viewIndex = cardItemList.cardList.Count;
	}
	
	private void DeleteCard(int index)
	{
		cardItemList.cardList.RemoveAt(index);
	}
	
	private void CreateNewCardList()
	{
		viewIndex = 1;
		cardItemList = Create();
		
		if(cardItemList){
			string relPath = AssetDatabase.GetAssetPath(cardItemList);
			EditorPrefs.SetString("ObjectPath",relPath);
		}
	}
	
	private CardList Create()
	{
		CardList asset = ScriptableObject.CreateInstance<CardList>();
		AssetDatabase.CreateAsset(asset,"Assets/Resources/Cards/CardList.asset");
		AssetDatabase.SaveAssets();

		return asset;
	}
	
	private void OpenCardList()
	{
		string absPath = EditorUtility.OpenFilePanel("Select Card List","","");
		
		if(absPath.StartsWith(Application.dataPath)){
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			cardItemList = AssetDatabase.LoadAssetAtPath(relPath,typeof(CardList)) as CardList;

				
				//cardItemList = AssetDatabase.LoadAssetAtPath(objectpath,typeof(CardList)) as CardList;
				
				activeCardList = Path.GetFileNameWithoutExtension(relPath);
			

			viewIndex = cardItemList.cardList.Count;
		}
	}
}

