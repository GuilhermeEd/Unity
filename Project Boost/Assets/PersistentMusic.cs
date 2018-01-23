using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentMusic : MonoBehaviour {

	private static PersistentMusic singleton;

	void Awake () {
		if ( !singleton ) {
			singleton = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

}
