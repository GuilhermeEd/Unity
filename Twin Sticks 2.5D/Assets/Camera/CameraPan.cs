using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {

	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void LateUpdate () {
		transform.LookAt(player.transform);
	}
}
