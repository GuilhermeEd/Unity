using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[Tooltip("In ms^-1")] [SerializeField] float xSpeed = 10f;
	[Tooltip("In ms^-1")] [SerializeField] float ySpeed = 10f;
	[Tooltip("In m")] [SerializeField] float xRange = 5f;
	[Tooltip("In m")] [SerializeField] float yRange = 3f;

	void Start () {
		
	}
	
	void Update () {
		float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

		float xOffset = xThrow * xSpeed * Time.deltaTime;
		float yOffset = yThrow * ySpeed * Time.deltaTime;

		float rawNewX = transform.localPosition.x + xOffset;
		float rawNewY = transform.localPosition.y + yOffset;

		float clampedNewX = Mathf.Clamp(rawNewX, -xRange, xRange);
		float clampedNewY = Mathf.Clamp(rawNewY, -yRange, yRange);

		transform.localPosition = new Vector3(clampedNewX, clampedNewY, transform.localPosition.z);
	}

}
