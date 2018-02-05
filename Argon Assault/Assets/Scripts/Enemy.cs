using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour {

	[SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;

	void Start () {
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.isTrigger = false;
	}

	void OnParticleCollision ( GameObject other ) {
		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
		fx.transform.parent = parent;
		Destroy(gameObject);
	}
}
