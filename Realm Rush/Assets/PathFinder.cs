using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int,Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	[SerializeField] bool isRunning = true;

	Vector2Int[] directions = {
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
	};

	void Start () {
		LoadBlocks ();
		ColorStartAndEnd ();
		PathFind ();
		ExploreNeighbours ();
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

	void PathFind () {
		queue.Enqueue(startWaypoint);
		
		while ( queue.Count > 0 ) {
			Waypoint searchCenter = queue.Dequeue();
			HaltIfEndFound(searchCenter);
		}

		Debug.Log("Path Finding Done");
	}

	void ExploreNeighbours () {
		foreach ( Vector2Int direction in directions ) {
			Vector2Int explorationCoordinates = startWaypoint.GetGridPos() + direction;
			if (grid.ContainsKey(explorationCoordinates)){
				grid[explorationCoordinates].SetTopColor(Color.blue);
			}				
		}
	}

	void HaltIfEndFound (Waypoint searchCenter) {
		if ( searchCenter == endWaypoint ) {
			Debug.Log("HaltIfEndFound");
			isRunning = false;
		}
	}

}
