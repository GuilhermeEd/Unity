using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	public bool recording = true;
	private bool isPaused = false;
	private float initialFixedDeltaTime;

	void Start() {
		initialFixedDeltaTime = Time.fixedDeltaTime;
	}

	void Update () {
		if ( CrossPlatformInputManager.GetButton("Fire1") ){
			recording = false;
		} else {
			recording = true;
		}

		if ( Input.GetKeyDown( KeyCode.P ) ) {
			if ( isPaused ) { isPaused = false; ResumeGame(); }
			else { isPaused = true; PauseGame(); }
		}

	}

	void PauseGame() {
		Time.timeScale = 0f;
		Time.fixedDeltaTime = 0f;
	}

	void ResumeGame() {
		Time.timeScale = 1f;
		Time.fixedDeltaTime = initialFixedDeltaTime;
	}

	void OnApplicationPause( bool pauseStatus ) {
		isPaused = pauseStatus;
	}
}
