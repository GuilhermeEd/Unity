using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	void Start () {
		PathFinder pathfinder = FindObjectOfType<PathFinder>();
		List<Waypoint> path = pathfinder.GetPath();
		StartCoroutine ( FollowPath (path) );
	}

	IEnumerator FollowPath (List<Waypoint> path) {
		foreach ( Waypoint waypoint in path ) {
			transform.position = waypoint.transform.position;
			yield return new WaitForSeconds(1f);
		}		
	} 

}
