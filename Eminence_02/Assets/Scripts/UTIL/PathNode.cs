using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNode 
{

	public enum Directions { TOP, BOTTOM, RIGHT, LEFT, TOP_RIGHT, BOTTOM_RIGHT, BOTTOM_LEFT, TOP_LEFT }
	public enum BuildStatus { OPEN, PROCESSING, SETTLED }
	public BuildStatus buildStatus;
	public Vector3 Position { get; set; }
	Dictionary<Directions, PathNode> connections;

	public Dictionary<Directions, PathNode> Connections { get { return connections; } } 


	public PathNode(Vector3 pos)
	{
		this.Position = pos;

		connections = new Dictionary<Directions, PathNode> ();

	}

	public  void AddConnection(PathNode targetNode,int directionsIndex)
	{
		Directions d = (Directions)directionsIndex;
		if (!connections.ContainsKey (d))
			connections.Add (d, targetNode);
	}

	public void FeedExternalConnection(PathNode targetNode, int directionsIndex)
	{
		//Convert dir
		Directions dw = (Directions)directionsIndex;
		Directions d = GetOpposite (dw);

		if (!connections.ContainsKey (d)) 
		{
			connections.Add(d, targetNode);
		}

	}

	public static Directions GetOpposite(Directions dirFromTarget)
	{
		Directions d = Directions.BOTTOM;
		
		switch (dirFromTarget) 
		{
		case Directions.BOTTOM: d = Directions.TOP;
			break;
		case Directions.BOTTOM_LEFT: d = Directions.TOP_RIGHT;
			break;
		case Directions.BOTTOM_RIGHT: d = Directions.TOP_LEFT;
			break;
		case Directions.LEFT: d = Directions.RIGHT;
			break;
		case Directions.RIGHT: d = Directions.LEFT;
			break;
		case Directions.TOP: d = Directions.BOTTOM;
			break;
		case Directions.TOP_LEFT: d = Directions.BOTTOM_RIGHT;
			break;
		case Directions.TOP_RIGHT: d = Directions.BOTTOM_LEFT;
			break;
			
		}

		return d;
	}
 
}
