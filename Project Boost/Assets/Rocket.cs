using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	[SerializeField] float boosterPower = 20f;
	[SerializeField] float rotationSensitivity = 100f;

	Rigidbody rb;
	AudioSource audioSource;

	void Start () {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		Thrust();
		Rotate();
	}

  void Thrust () {
    if (Input.GetKey(KeyCode.Space)) {
      rb.AddRelativeForce(Vector3.up * boosterPower);
      if (!audioSource.isPlaying) audioSource.Play();
    } else audioSource.Stop();

    
  }

	void Rotate () {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
			rb.freezeRotation = true; // take manual control of rotation
			Vector3 rotation = Vector3.forward * rotationSensitivity * Time.deltaTime;
			if (Input.GetKey(KeyCode.A)) transform.Rotate(rotation);
			else if (Input.GetKey(KeyCode.D)) transform.Rotate(-rotation);
			rb.freezeRotation = false; // resume physics control of rotation
		}
	}
}
