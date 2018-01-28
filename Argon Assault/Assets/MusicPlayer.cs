using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	static GameObject singleton;

	void Awake () {
		if ( singleton ) {
			Destroy( gameObject );
		} else {
			singleton = gameObject;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start () {
		Invoke("LoadFirstScene", 2f);
	}

	void LoadFirstScene () {
		SceneManager.LoadScene(1);
	}
}
