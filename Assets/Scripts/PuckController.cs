using UnityEngine;
using System.Collections;

public class PuckController : MonoBehaviour {

	private GameController gameController;
	public float MaxSpeed;
	public Vector2 position;
	private Vector2 velocity;
	private float rotation;
	private float ratio;
	private bool reset;
	private bool first;

	// Use this for initialization
	void Start () {
		reset = false;
		first = true;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		else {
			Debug.Log ("Error initializing agent, cannot find game controller");
		}
	}

	void FixedUpdate() {
		if (gameController.State == TrainerState.Training) {
			if (first) {
				this.rigidbody2D.velocity.Set (velocity.x, velocity.y);
				first = false;
			}
			else {
				// set puck speed
				ratio = rigidbody2D.velocity.magnitude / MaxSpeed;
				if (ratio > 1) {
					rigidbody2D.velocity.Set(rigidbody2D.velocity.x / ratio, rigidbody2D.velocity.y / ratio);
				}
			}
		}
		else {
			if (reset) {
				//this.rigidbody2D.velocity.Set(0,0);
				this.rigidbody2D.position.Set (position.x, position.y);
			}
		}
	}

	public void Reset() {
		//velocity.Set random
		//position.Set random
		reset = true;
	}

	public void Reset (float X, float Y, float Vx, float Vy, float R) {
		velocity.Set (Vx, Vy);
		position.Set (X, Y);
		rotation = R;
		reset = true;
	}
}
