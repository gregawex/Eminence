using UnityEngine;
using System.Collections;


public enum ItemEvent { SHOW, USE, EXAMINE }

public enum AxisType {LEFT_THUMBSTICK, RIGHT_THUMBSTICK, LEFT_TRIGGER, RIGHT_TRIGGER, CRSR_KEYS }
public enum ButtonType {NONE, A, B, X, Y}

public enum GroundType {FLOOR, STAIRS}
public enum FloorMaterial { WOOD, GRAVEL, CONCRETE, CARPET, GRASS, DIRT, MUD, WATER }
 
public enum Side { LEFT, RIGHT }
public enum ButtonState { PRESSED, RELEASED }

public enum ActorEvent { DAMAGE }

public enum PopupButtonSetup { SINGLE, TWO_CHOICE }

public enum BundleLoading {EDITOR, REMOTE }
 
public enum _State { BEGIN, EXECUTE, END }

public enum CGResultMethod { LOCAL, SERVER }

public enum PlayerColor {RED, BLUE }

public class Constants 
{
	public const string BUNDLE_DESCRIPTOR = "bundle_desc";

	public const float PC_NO_CLICK_RADIUS = 0.8f;
	
	public const float MAX_STEEPNESS = 0.7f;

	public const string COMPANY_NAME_CASUAL = "Stoker Games";
	public const string COMPANY_NAME_LONG = "Stoker Games Ltd.";
	public const string GAME_TITLE_CASUAL = "Eminence";
	public const string GAME_TITLE_LONG = "Eminence - ...";

}

public enum Countries
{
	Albania,
	Andorra,
	Hungary
}
public enum SecretQuestion
{
	What_is_your_favourite_food,
	Who_is_your_favourite_famous_person
}
