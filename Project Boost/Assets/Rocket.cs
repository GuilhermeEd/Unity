using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	public float boosterPower = 20f;
	public float rotationSensitivity = 80f;

	private Rigidbody rigidBody;
	private AudioSource audioSource;

	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		ProcessInput();
	}

	void ProcessInput () {
		if ( Input.GetKey ( KeyCode.Space ) ) {
			rigidBody.AddRelativeForce( Vector3.up * boosterPower );
			if ( !audioSource.isPlaying ) audioSource.Play();
		} else {
			audioSource.Stop();
		}

		if ( Input.GetKey ( KeyCode.A ) ) {
			transform.Rotate( Vector3.forward * rotationSensitivity * Time.deltaTime );
		} else if ( Input.GetKey ( KeyCode.D ) ) {
			transform.Rotate( -Vector3.forward * rotationSensitivity * Time.deltaTime );
		}
	}
}
