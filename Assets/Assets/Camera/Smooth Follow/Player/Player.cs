using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float acceleration = 20f;
	public float jumpPower = 400f;
	public float maxSpeed = 100f;

	private Rigidbody rb;
	private bool isJumping;

	void Start () {
		rb = GetComponent<Rigidbody>();
		isJumping = false;
	}
	
	void FixedUpdate () {
		// Handle horizontal movement
		if( Input.GetKey( KeyCode.RightArrow ) || Input.GetKey(KeyCode.D)){
			if( Mathf.Abs ( rb.velocity.x ) < maxSpeed )	{
				rb.AddForce( Vector3.right * acceleration * Time.deltaTime );
			}
		}
		if( Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey(KeyCode.A) ){
			if( Mathf.Abs ( rb.velocity.x ) < maxSpeed )	{
				rb.AddForce( Vector3.left * acceleration * Time.deltaTime );
			}
		}

		// Handle jumps
		if( ( Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) ){
			if ( !isJumping ) {
				rb.AddForce( Vector3.up * jumpPower * Time.deltaTime );
			}
		}

		// Detect jump
		float offset = (GetComponent<Collider>().bounds.size.y / 2) - 0.1f;
		Vector3 origin = transform.position - Vector3.up * offset;
		bool isHittingGround = Physics.Raycast(origin, Vector3.down, 0.2f);
		isJumping = !isHittingGround;
	}
}
