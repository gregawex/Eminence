using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
██████╗ ██████╗  ██████╗ ███████╗██╗██╗     ███████╗
██╔══██╗██╔══██╗██╔═══██╗██╔════╝██║██║     ██╔════╝
██████╔╝██████╔╝██║   ██║█████╗  ██║██║     █████╗  
██╔═══╝ ██╔══██╗██║   ██║██╔══╝  ██║██║     ██╔══╝  
██║     ██║  ██║╚██████╔╝██║     ██║███████╗███████╗
╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝╚══════╝

 * */

public class Profile : PackObject
{



	//public List<CardBundle> cards; 

	[Pack]
	public Inventory Inventory { get; set; }

	[Pack]
	public PersonalInfo Personal { get; set; }


	public static Profile ProcessJson(string profileJson)
	{
		//PLEASE NOTE incoming json has to be decrypted


		//If the validation of the incoming json fails we send an error message back to the user
		if (profileJson == null) 
		{
			ErrorMsg.LogError(ErrorCode.SERVER_RETURNED_NULLJSON);
			return null;
		} 
		//The validation succeeded, Create the profile
		else 
		{
			//====================================
			//CREATE PROFILE
			//====================================
			// 1. Shared data 
			// 2. Account details (eg. name, ID, avatar etc.)
			// 3. Inventory (card refs, soft/ hard currency etc)

			Profile profile = new Profile();

			return profile;
		}
	}


}
