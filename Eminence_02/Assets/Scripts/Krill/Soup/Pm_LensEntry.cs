using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
[ActionCategory(ActionCategory.Character)]
public class Pm_LensEntry : ActorOp 
{
	public FsmString id;
	public FsmString parentId;
	public FsmEvent onClick;
	public FsmString inventoryPrerequisite;

}