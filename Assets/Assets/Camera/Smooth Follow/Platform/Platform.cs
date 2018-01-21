using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public float range = 1.0f;
    public float duration = 3.0f;

    private float startY;
    private float startTime;
		private bool isMovingUp;

    void Start() {
			startY = transform.position.y;
			startTime = Time.time;
			isMovingUp = true;
    }
    void LateUpdate() {
        float t = (Time.time - startTime) / duration;
				if ( isMovingUp ) {
        	transform.position = new Vector3(0f, Mathf.SmoothStep(startY, startY + range, t), 0f);
					if ( transform.position.y >= startY + range ) {
						isMovingUp = false;
						startTime = Time.time;
					}
				} else {
					transform.position = new Vector3(0f, Mathf.SmoothStep(startY + range, startY, t), 0f);
					if ( transform.position.y <= startY ) {
						isMovingUp = true;
						startTime = Time.time;
					}
				}
    }

}
