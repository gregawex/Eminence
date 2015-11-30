/* _17_Scene_StartMenu.js ------------------------------------ by Henry Hsieh */

/* è®Šæ•¸å®£å‘Š */
//ä»‹é¢æ¨¡ç‰ˆ : åœ–å½¢åŒ–ä»‹é¢æ¨¡æ¿
//èƒŒæ™¯åœ–ç‰‡ : åœ–æª”
//æŒ‰éˆ•1åŠæŒ‰éˆ•2çš„ä½ç½® : 2ç¶­å€¼(x, y)
//æŒ‰éˆ•1åŠæŒ‰éˆ•2çš„å¤§å° : 2ç¶­å€¼(x, y)
//é è¼‰ç‹€æ…‹ : å¸ƒæž—å€¼(æ˜¯/å¦)(éš±åŒ¿å®£å‘Š)
var StartSkin : GUISkin;
var Button1Pos : Vector2 = Vector2(0.5, 0.5);
var Button1Size : Vector2 = Vector2(100, 40);

var Button2Pos : Vector2 = Vector2(0.5, 0.5);
var Button2Size : Vector2 = Vector2(100, 40);

var Button3Pos : Vector2 = Vector2(0.5, 0.5);
var Button3Size : Vector2 = Vector2(100, 40);

var Button4Pos : Vector2 = Vector2(0.5, 0.5);
var Button4Size : Vector2 = Vector2(100, 40);

var Button5Pos : Vector2 = Vector2(0.5, 0.5);
var Button6Pos : Vector2 = Vector2(0.5, 0.5);
var Button7Pos : Vector2 = Vector2(0.5, 0.5);
var Button8Pos : Vector2 = Vector2(0.5, 0.5);
var Button9Pos : Vector2 = Vector2(0.5, 0.5);
var Button10Pos : Vector2 = Vector2(0.5, 0.5);
var Button11Pos : Vector2 = Vector2(0.5, 0.5);
var Button12Pos : Vector2 = Vector2(0.5, 0.5);
var playAction : Transform;
/* åŠŸèƒ½ : åœ–å½¢åŒ–ä»‹é¢ */
function OnGUI ()
{	
	//ä½¿ç”¨ä»‹é¢æ¨¡ç‰ˆ StarSkin --> å»ºç«‹ä»‹é¢åœ–åƒ
	GUI.skin = StartSkin;
	//GUI.DrawTexture(Rect(0, 0, Screen.width, Screen.height), BackGround);

		
	
	//(å»ºç«‹é€²å…¥éŠæˆ²æŒ‰éˆ•)å¦‚æžœæŒ‰ä¸‹é€²å…¥éŠæˆ²æŒ‰éˆ•æ™‚ï¼Œå•Ÿå‹•é è¼‰æ¨¡å¼ï¼Œè®€å–ä¸¦åŸ·è¡Œ TheGame é—œå¡
	if(GUI.Button(Rect(Screen.width *Button1Pos.x -(Button1Size.x/2), Screen.height *Button1Pos.y -(Button1Size.y/2), Button1Size.x, Button1Size.y), "stand"))
	{
		playAction.GetComponent.<Animation>().Play("stand", PlayMode.StopAll);
	}
	
	if(GUI.Button(Rect(Screen.width *Button2Pos.x -(Button2Size.x/2), Screen.height *Button2Pos.y -(Button2Size.y/2), Button2Size.x, Button2Size.y), "walk"))
	{
		playAction.GetComponent.<Animation>().Play("walk", PlayMode.StopAll);
	}
	
	if(GUI.Button(Rect(Screen.width *Button3Pos.x -(Button3Size.x/2), Screen.height *Button3Pos.y -(Button3Size.y/2), Button3Size.x, Button3Size.y), "battle"))
	{
		playAction.GetComponent.<Animation>().Play("battle", PlayMode.StopAll);
	}
	
	if(GUI.Button(Rect(Screen.width *Button4Pos.x -(Button4Size.x/2), Screen.height *Button4Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "run"))
	{
		playAction.GetComponent.<Animation>().Play("run", PlayMode.StopAll);
	}
	
	if(GUI.Button(Rect(Screen.width *Button5Pos.x -(Button4Size.x/2), Screen.height *Button5Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "attack01"))
	{
		playAction.GetComponent.<Animation>().Play("attack01", PlayMode.StopAll);
	}
	
	if(GUI.Button(Rect(Screen.width *Button6Pos.x -(Button4Size.x/2), Screen.height *Button6Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "attack02"))
	{
		playAction.GetComponent.<Animation>().Play("attack02", PlayMode.StopAll);
	}
	
	if(GUI.Button(Rect(Screen.width *Button7Pos.x -(Button4Size.x/2), Screen.height *Button7Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "hurt"))
	{
		playAction.GetComponent.<Animation>().Play("hurt", PlayMode.StopAll);
	}
	
	if(GUI.Button(Rect(Screen.width *Button8Pos.x -(Button4Size.x/2), Screen.height *Button8Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "death"))
	{
		playAction.GetComponent.<Animation>().Play("death", PlayMode.StopAll);
	}
	
	if(GUI.Button(Rect(Screen.width *Button9Pos.x -(Button4Size.x/2), Screen.height *Button9Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "defense"))
	{
		playAction.GetComponent.<Animation>().Play("defense", PlayMode.StopAll);
	}

	if(GUI.Button(Rect(Screen.width *Button10Pos.x -(Button4Size.x/2), Screen.height *Button10Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "counter"))
	{
		playAction.GetComponent.<Animation>().Play("counter", PlayMode.StopAll);
	}

	if(GUI.Button(Rect(Screen.width *Button11Pos.x -(Button4Size.x/2), Screen.height *Button11Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "shield attack"))
	{
		playAction.GetComponent.<Animation>().Play("shieldattack", PlayMode.StopAll);
	}

	if(GUI.Button(Rect(Screen.width *Button12Pos.x -(Button4Size.x/2), Screen.height *Button12Pos.y -(Button4Size.y/2), Button4Size.x, Button4Size.y), "assault"))
	{
		
		playAction.GetComponent.<Animation>().Play("assault", PlayMode.StopAll);
	}

}

