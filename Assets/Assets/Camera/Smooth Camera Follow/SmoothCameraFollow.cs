using UnityEngine;

[DisallowMultipleComponent]
public class SmoothCameraFollow : MonoBehaviour {

	[SerializeField] Transform target;
	[SerializeField] float smoothSpeed = 10f;
	[SerializeField] Vector3 offset = new Vector3(0, 2, -10);

	void Start () {
		if ( !target ) {
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	void FixedUpdate () {
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothPosition;
		transform.LookAt(target);
	}	
}
