using UnityEngine;

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

}
