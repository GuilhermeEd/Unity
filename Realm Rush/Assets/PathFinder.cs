using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int,Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;
	Waypoint searchCenter;
	List<Waypoint> path = new List<Waypoint>();

	Vector2Int[] directions = {
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
	};

	public List<Waypoint> GetPath () {
		LoadBlocks ();
		ColorStartAndEnd ();
		BreadthFirstSearch ();
		CreatePath ();
		return path;
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

	void BreadthFirstSearch () {
		queue.Enqueue(startWaypoint);
		
		while ( queue.Count > 0 && isRunning ) {
			searchCenter = queue.Dequeue();
			HaltIfEndFound();
			ExploreNeighbours();
			searchCenter.isExplored = true;
		}

		Debug.Log("Path Finding Done");
	}

	void ExploreNeighbours () {
		if (!isRunning) { return; }
		foreach ( Vector2Int direction in directions ) {
			Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
			if (grid.ContainsKey(neighbourCoordinates)){
				QueueNewNeighbours (neighbourCoordinates);
			}				
		}
	}

	void HaltIfEndFound () {
		if ( searchCenter == endWaypoint ) {
			isRunning = false;
		}
	}

	void QueueNewNeighbours (Vector2Int neighbourCoordinates) {
		Waypoint neighbour = grid[neighbourCoordinates];
		if ( !(neighbour.isExplored || queue.Contains(neighbour)) ) {
			queue.Enqueue(neighbour);
			neighbour.exploredFrom = searchCenter;
		}
	}

	void CreatePath () {
		path.Add(endWaypoint);
		Waypoint previous = endWaypoint.exploredFrom;
		while ( previous != startWaypoint ) {
			path.Add(previous);
			previous = previous.exploredFrom;
		}
		path.Add(startWaypoint);
		path.Reverse();
	}

}
