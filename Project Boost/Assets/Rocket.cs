using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float boosterPower = 20f;
	[SerializeField] float rotationSensitivity = 100f;

	Rigidbody rigidBody;
	AudioSource audioSource;

	enum State { Alive, Dying, Transcending }
	State state = State.Alive;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Update () {
		if ( state == State.Alive ) {
			Thrust();
			Rotate();
		}
	}

	void OnCollisionEnter (Collision collision) {
		if ( state != State.Alive ) return;
		switch ( collision.gameObject.tag ) {
			case "Friendly":
				// do nothing
				break;
			case "End":
				state = State.Transcending;
				Invoke ("LoadNextScene", 1f);
				break;
			default:
				state = State.Dying;
				Invoke ("LoadFirstScene", 1f);
				break;
		}
	}

  void LoadNextScene () {
    int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    SceneManager.LoadScene(nextScene);
  }

	void LoadFirstScene () {
		SceneManager.LoadScene (0);
	}

  void Thrust () {
    if (Input.GetKey(KeyCode.Space)) {
      rigidBody.AddRelativeForce(Vector3.up * boosterPower);
      if (!audioSource.isPlaying) audioSource.Play();
    } else audioSource.Stop();

    
  }

	void Rotate () {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
			rigidBody.freezeRotation = true; // take manual control of rotation
			Vector3 rotation = Vector3.forward * rotationSensitivity * Time.deltaTime;
			if (Input.GetKey(KeyCode.A)) transform.Rotate(rotation);
			else if (Input.GetKey(KeyCode.D)) transform.Rotate(-rotation);
			rigidBody.freezeRotation = false; // resume physics control of rotation
		}
	}

}
