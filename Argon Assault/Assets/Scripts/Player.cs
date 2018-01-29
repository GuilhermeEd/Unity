using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[Tooltip("In ms^-1")] [SerializeField] float xSpeed = 20f;
	[Tooltip("In ms^-1")] [SerializeField] float ySpeed = 20f;
	[Tooltip("In m")] [SerializeField] float xRange = 5f;
	[Tooltip("In m")] [SerializeField] float yRange = 3f;
	[SerializeField] float positionPitchFactor = -5f;
	[SerializeField] float controlPitchFactor = -20f;
	[SerializeField] float positionYawFactor = 5f;
	[SerializeField] float controlRollFactor = -20f;

	float xThrow, yThrow;

	void OnTriggerEnter (Collider collider) {
		Debug.Log("Player triggered something.");
	}

	void Update ()
  {
    ProcessTranslation ();
		ProcessRotation ();
  }

  void ProcessTranslation () {
    xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    yThrow = CrossPlatformInputManager.GetAxis("Vertical");

    float xOffset = xThrow * xSpeed * Time.deltaTime;
    float yOffset = yThrow * ySpeed * Time.deltaTime;

    float rawNewX = transform.localPosition.x + xOffset;
    float rawNewY = transform.localPosition.y + yOffset;

    float clampedNewX = Mathf.Clamp(rawNewX, -xRange, xRange);
    float clampedNewY = Mathf.Clamp(rawNewY, -yRange, yRange);

    transform.localPosition = new Vector3(clampedNewX, clampedNewY, transform.localPosition.z);
  }

	void ProcessRotation () {
		float positionPitch = transform.localPosition.y * positionPitchFactor;
		float controlPitch = yThrow * controlPitchFactor;
		float pitch = positionPitch + controlPitch;
		
		float positionYaw = transform.localPosition.x * positionYawFactor;
		float yaw = positionYaw;

		float controlRoll = xThrow * controlRollFactor;
		float roll = controlRoll;

		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

}
