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
		// ExploreNeighbours ();
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
		
		while ( queue.Count > 0 && isRunning ) {
			Waypoint searchCenter = queue.Dequeue();
			HaltIfEndFound(searchCenter);
			ExploreNeighbours(searchCenter);
			searchCenter.isExplored = true;
		}

		Debug.Log("Path Finding Done");
	}

	void ExploreNeighbours (Waypoint from) {
		if (!isRunning) { return; }
		foreach ( Vector2Int direction in directions ) {
			Vector2Int neighbourCoordinates = from.GetGridPos() + direction;
			if (grid.ContainsKey(neighbourCoordinates)){
				QueueNewNeighbours (neighbourCoordinates);
			}				
		}
	}

	void HaltIfEndFound (Waypoint searchCenter) {
		if ( searchCenter == endWaypoint ) {
			Debug.Log("HaltIfEndFound");
			isRunning = false;
		}
	}

	void QueueNewNeighbours (Vector2Int neighbourCoordinates) {
		Waypoint neighbour = grid[neighbourCoordinates];
		if (!neighbour.isExplored) {
			neighbour.SetTopColor(Color.blue);
			queue.Enqueue(neighbour);
		}
	}

}
