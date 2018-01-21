using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	public float boosterPower = 20f;
	public float rotationSensitivity = 80f;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		ProcessInput();
	}

	void ProcessInput () {
		if ( Input.GetKey ( KeyCode.Space ) ) {
			rb.AddRelativeForce( Vector3.up * boosterPower );
		}

		if ( Input.GetKey ( KeyCode.A ) ) {
			transform.Rotate( Vector3.forward * rotationSensitivity * Time.deltaTime );
		} else if ( Input.GetKey ( KeyCode.D ) ) {
			transform.Rotate( -Vector3.forward * rotationSensitivity * Time.deltaTime );
		}
	}
}
