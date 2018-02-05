using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour {

	ParticleSystem particles;

	void Start () {
		particles = GetComponent<ParticleSystem>();
		float delay = particles.main.duration;
		Destroy( gameObject, delay );
	}

}
