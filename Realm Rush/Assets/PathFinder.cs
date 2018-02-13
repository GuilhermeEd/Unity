using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int,Waypoint>();

	void Start () {
		LoadBlocks ();
		ColorStartAndEnd ();
	}
	
	void LoadBlocks () {
		Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
		foreach ( Waypoint waypoint in waypoints ) {
			bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
			if ( isOverlapping ) {
				Debug.LogWarning("Skipping Overlapping Block " + waypoint);
			} else {
				grid.Add(waypoint.GetGridPos(), waypoint);
			}
		}
	}

	void ColorStartAndEnd () {
		startWaypoint.SetTopColor(Color.green);
		endWaypoint.SetTopColor(Color.red);
	}

}
