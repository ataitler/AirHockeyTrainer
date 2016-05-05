using UnityEngine;
using System.Collections;

public class PuckController : MonoBehaviour {

	private GameController gameController;
	private RewardManager rewardManager;
	public float MaxSpeed;
	public Vector2 position;
	private Vector2 velocity;
	private float rotation;
	private float ratio;
	private bool reset;
	private bool first;

	public float GetMaxSpeed
	{
		get { return MaxSpeed; }
	}

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
		GameObject rewardManagerObject = GameObject.FindWithTag ("RewardManager");
		if (rewardManagerObject != null) {
			rewardManager = rewardManagerObject.GetComponent<RewardManager> ();
		}
		else {
			Debug.Log ("Error initializing agent, cannot find reward manager");
		}
	}

	void FixedUpdate() {
		if (reset) {
			this.rigidbody2D.velocity = new Vector2(0,0);
			this.rigidbody2D.angularVelocity = 0;
			this.rigidbody2D.position = new Vector2(1000,0);
			first = true;
			reset = false;
		}
		else if (gameController.State == TrainerState.Training) {
			if (first) {
				this.rigidbody2D.velocity = velocity;
				Debug.Log(velocity.ToString());
				this.rigidbody2D.position = position;
				this.rigidbody2D.angularVelocity = rotation;
				first = false;
			}
			// set puck speed
			ratio = rigidbody2D.velocity.magnitude / MaxSpeed;
			if (ratio > 1) {
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x / ratio, rigidbody2D.velocity.y / ratio);
			}
		}
	}

	public void Reset (float X, float Y, float Vx, float Vy, float R) {
		velocity.Set (Vx, Vy);
		position.Set (X, Y);
		rotation = R;
		reset = true;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.CompareTag ("Agent")) {
			rewardManager.AgentPuckCollision ();
		}
	}

}
