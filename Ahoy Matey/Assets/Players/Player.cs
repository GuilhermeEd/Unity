using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	public float mouseSensitivity = 3f;

	private Vector3 inputValue;
	private Camera cam;
	private float mouseX, mouseY;

	void Update () {
		if(!isLocalPlayer) return;

		inputValue.x = CrossPlatformInputManager.GetAxis("Horizontal");
		inputValue.y = 0f;;
		inputValue.z = CrossPlatformInputManager.GetAxis("Vertical");
		
		transform.Translate(inputValue);
		
		mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
		mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity;

		cam.transform.eulerAngles = new Vector3(-mouseY, mouseX ,0f);
	}

	public override void OnStartLocalPlayer(){
		if(isLocalPlayer){
			cam = GetComponentInChildren<Camera>();
			cam.enabled = true;
		}
	}
	
}
