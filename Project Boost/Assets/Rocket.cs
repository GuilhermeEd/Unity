using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[Tooltip("Press L to Load next scene and C to toggle Collisions")]
	[SerializeField] bool debugMode = false;

	[SerializeField] float boosterPower = 20f;
	[SerializeField] float rotationSensitivity = 100f;
	[SerializeField] float levelLoadDelay = 2f;

	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip death;
	[SerializeField] AudioClip success;

	[SerializeField] ParticleSystem mainEngineParticles;
	[SerializeField] ParticleSystem deathParticles;
	[SerializeField] ParticleSystem successParticles;

	Rigidbody rigidBody;
	AudioSource audioSource;

	bool collisionsDisabled = false;
	bool isTransitioning = false;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Update () {
		if ( !isTransitioning ) {
			RespondThrustInput ();
			RespondRotateInput ();
		}
		if ( Debug.isDebugBuild ) {
			RespondDebugInput();
		}
	}

	void RespondDebugInput () {
		if ( debugMode ) {
			if ( Input.GetKey(KeyCode.L) ) {
				LoadNextScene();
				Debug.Log("Debug Mode: Next Scene Loaded");
			}
			if ( Input.GetKey(KeyCode.C) ) {
				collisionsDisabled = !collisionsDisabled;
				Debug.Log("Debug Mode: Collisions Toggled");
			}
		}
	}

	void OnCollisionEnter (Collision collision) {
		if ( isTransitioning || collisionsDisabled ) return;
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
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(success);
		successParticles.Play();
    Invoke("LoadNextScene", levelLoadDelay);
  }

	void StartDeathSequence () {
		isTransitioning = true;
		audioSource.Stop();
		audioSource.PlayOneShot(death);
		mainEngineParticles.Stop();
		deathParticles.Play();
		//Invoke ("LoadFirstScene", levelLoadDelay);
		Invoke ("ReloadScene", levelLoadDelay);
	}

  void LoadNextScene () {
    int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    SceneManager.LoadScene(nextScene);
  }

	void ReloadScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	void LoadFirstScene () {
		SceneManager.LoadScene (0);
	}

  void RespondThrustInput () {
    if (Input.GetKey(KeyCode.Space)) {
			Thrust();
		} else {
			StopThrust();
		}
  }

  void Thrust () {
    rigidBody.AddRelativeForce(Vector3.up * boosterPower * Time.deltaTime);
    if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
		mainEngineParticles.Play();
  }

	void StopThrust () {
		audioSource.Stop();
		mainEngineParticles.Stop();
	}

  void RespondRotateInput () {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
			rigidBody.angularVelocity = Vector3.zero;
      Rotate();
    }
  }

  void Rotate () {
    Vector3 rotation = Vector3.forward * rotationSensitivity * Time.deltaTime;
    if (Input.GetKey(KeyCode.A)) transform.Rotate(rotation);
    else if (Input.GetKey(KeyCode.D)) transform.Rotate(-rotation);
  }
}
