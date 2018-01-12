using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

	private const int bufferFrames = 100;
	private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
	private Rigidbody rigidBody;
	private GameManager manager;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		manager = FindObjectOfType<GameManager>();
	}

	void Update() {
		if ( manager.recording ) {
			Record();
		} else if (Time.frameCount > bufferFrames) {
			Playback();
		} else {
			Record();
		}
	}

	void Playback() {
		rigidBody.isKinematic = true;
		int frame = Time.frameCount % bufferFrames;
		transform.position = keyFrames[frame].position;
		transform.rotation = keyFrames[frame].rotation;
	}

	void Record(){
		rigidBody.isKinematic = false;
		int frame = Time.frameCount % bufferFrames;
       	float time = Time.time;
       	keyFrames[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
	}
}

/// A structure for storing time, positon and rotation.
public struct MyKeyFrame {

	public float frameTime;
	public Vector3 position;
	public Quaternion rotation;

	public MyKeyFrame(float time, Vector3 pos, Quaternion rot) {
		this.frameTime = time;
		this.position = pos;
		this.rotation = rot;
	}
}