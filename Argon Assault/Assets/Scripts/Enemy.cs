using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour {

	[SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;
	[SerializeField] int scorePetHit = 12;
	[SerializeField] int hits = 10;

	ScoreBoard scoreBoard;

	void Start ()
  {
    AddBoxCollider ();
		scoreBoard = FindObjectOfType<ScoreBoard>();
  }

  void AddBoxCollider () {
    BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
    boxCollider.isTrigger = false;
  }

  void OnParticleCollision ( GameObject other )
  {
    scoreBoard.ScoreHit(scorePetHit);
		hits--;
		if ( hits <= 0 ) {
			KillEnemy ();
		}
  }

  void KillEnemy () {
    GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
    fx.transform.parent = parent;
    Destroy(gameObject);
  }
}
