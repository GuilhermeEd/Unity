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
			if ( isPaused ) { ResumeGame(); }
			else { PauseGame(); }
		}

	}

	void PauseGame() {
		Time.timeScale = 0f;
		Time.fixedDeltaTime = 0f;
		isPaused = true;
	}

	void ResumeGame() {
		Time.timeScale = 1f;
		Time.fixedDeltaTime = initialFixedDeltaTime;
		isPaused = false;
	}

	void OnApplicationPause( bool pauseStatus ) {
		if(pauseStatus) PauseGame();
	}
}
