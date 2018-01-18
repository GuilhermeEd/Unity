using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {
	
	public void MyStartHost () {
		Debug.Log(Time.timeSinceLevelLoad + ": Starting Host");
		StartHost();
	}

	public override void OnStartHost () {
		Debug.Log(Time.timeSinceLevelLoad + ": Host Started");
	}
	
	public override void OnStartClient(NetworkClient myClient){
		Debug.Log(Time.timeSinceLevelLoad + ": Client Start Requested");
		InvokeRepeating("PrintDots", 0f, 1f);
	}

	public override void OnClientConnect(NetworkConnection conn){
		Debug.Log(Time.timeSinceLevelLoad + ": Client is Connected to IP: " + conn.address);
		CancelInvoke();
	}

	public void PrintDots(){
		Debug.Log(".");
	}

}
