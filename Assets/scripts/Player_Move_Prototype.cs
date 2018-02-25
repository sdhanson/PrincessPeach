using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prototype : MonoBehaviour {

	public int playerSpeed = 10;
	private bool facingRight = true;
	public int playerJumpPower = 1250;
	private float moveX;
	private int count = 0;
	public bool isGrounded;
	
	// Update is called once per frame
	void Update () {
		PlayerMove ();
		PlayerRaycast ();
	}

	void PlayerMove() {
		//CONTROLS
		moveX = Input.GetAxis ("Horizontal");
		if (Input.GetButtonDown ("Jump") && isGrounded == true){
			Jump();
		}
		//ANIMATIONS
		//PLAYER DIRECTIONS
		if (moveX < 0.0f && facingRight == false) {
			FlipPlayer();
		} else if(moveX > 0.0f && facingRight == true) {
			FlipPlayer();
		}
		//PHYSICS
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

	}

	void Jump() {
		GetComponent<Rigidbody2D> ().AddForce (Vector2.up * playerJumpPower);
		isGrounded = false;
	}

	void FlipPlayer() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		if (count != 0) {
			localScale.x *= -1;
		}
		transform.localScale = localScale;
		count++;
	}

	void OnCollisionEnter2D (Collision2D col) {
		
	}

	void PlayerRaycast() {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right);
		if (hit != null && hit.collider != null && hit.distance < 0.2f && hit.collider.tag == "winner") {
			Debug.Log ("you win");
			//Win
		}

		RaycastHit2D rayUp = Physics2D.Raycast (transform.position, Vector2.up);
		if (rayUp != null && rayUp.collider != null && rayUp.distance < 0.9f && rayUp.collider.tag == "box") {
			Destroy (rayUp.collider.gameObject);
		}

		RaycastHit2D rayDown = Physics2D.Raycast (transform.position, Vector2.down);
		if (rayDown != null && rayDown.collider != null && rayDown.distance < 0.9f && rayDown.collider.tag == "enemy") {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1000);
			rayDown.collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 200);
			Destroy (rayDown.collider.gameObject);
		}
		if (rayDown != null && rayDown.collider != null && rayDown.distance < 0.9f && rayDown.collider.tag != "enemy") {
			isGrounded = true;
		}

	}
}
