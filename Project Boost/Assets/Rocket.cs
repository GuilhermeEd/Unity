using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float boosterPower = 20f;
	[SerializeField] float rotationSensitivity = 100f;

	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip death;
	[SerializeField] AudioClip success;

	[SerializeField] ParticleSystem mainEngineParticles;
	[SerializeField] ParticleSystem deathParticles;
	[SerializeField] ParticleSystem successParticles;

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
			RespondThrustInput ();
			RespondRotateInput ();
		}
	}

	void OnCollisionEnter (Collision collision) {
		if ( state != State.Alive ) return;
		switch ( collision.gameObject.tag ) {
			case "Friendly":
				// do nothing
				break;
      case "End":
        StartSuccessSequence ();
        break;
      default:
				StartDeathSequence ();
				break;
		}
	}

  void StartSuccessSequence () {
    state = State.Transcending;
    audioSource.Stop();
    audioSource.PlayOneShot(success);
		successParticles.Play();
    Invoke("LoadNextScene", 1.5f);
  }

	void StartDeathSequence () {
		state = State.Dying;
		audioSource.Stop();
		audioSource.PlayOneShot(death);
		deathParticles.Play();
		Invoke ("LoadFirstScene", 1.5f);
	}

  void LoadNextScene () {
    int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    SceneManager.LoadScene(nextScene);
  }

	void LoadFirstScene () {
		SceneManager.LoadScene (0);
	}

  void RespondThrustInput () {
    if (Input.GetKey(KeyCode.Space)) {
			Thrust();
		} else {
			audioSource.Stop();
			mainEngineParticles.Stop();
		}
  }

  void Thrust () {
    rigidBody.AddRelativeForce(Vector3.up * boosterPower * Time.deltaTime);
    if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
		mainEngineParticles.Play();
  }

  void RespondRotateInput () {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
      rigidBody.freezeRotation = true; // take manual control of rotation
      Rotate();
      rigidBody.freezeRotation = false; // resume physics control of rotation
    }
  }

  void Rotate () {
    Vector3 rotation = Vector3.forward * rotationSensitivity * Time.deltaTime;
    if (Input.GetKey(KeyCode.A)) transform.Rotate(rotation);
    else if (Input.GetKey(KeyCode.D)) transform.Rotate(-rotation);
  }
}
