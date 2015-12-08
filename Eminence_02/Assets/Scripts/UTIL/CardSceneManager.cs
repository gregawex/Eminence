using UnityEngine;
using System.Collections;

public class CardSceneManager : MonoBehaviour 
{

	public CardInstance tempCard;

	public static CardSceneManager Instance { get ; private set; }

	public CGPlayer Player_RED { get; private set; }
	public CGPlayer Player_BLU { get; private set; }

	public Transform blu_holder, red_holder;

	void Awake()
	{
		Instance = this;
	}
	
	void Start () 
	{

	}

	void Update () 
	{
	
	}

	public void Init(CGPlayer player_red, CGPlayer player_blu)
	{
		this.Player_RED = player_red;
		this.Player_BLU = player_blu;
	}


}
