using UnityEngine;
using System.Collections;

public class AgentController : MonoBehaviour {

	private GameController gameController = null;
	private Vector2 velocity;
	public Vector2 position;
	private bool reset = false;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		else {
			Debug.Log ("Error initializing agent, cannot find game controller");
		}
		velocity = new Vector2 (0, 0);
		this.rigidbody2D.velocity.Set (velocity.x, velocity.y);
		this.rigidbody2D.position.Set (position.x, position.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (gameController.State == TrainerState.Training) {
			this.rigidbody2D.velocity.Set (velocity.x, velocity.y);
		}
		else {
			if (reset) {
				this.rigidbody2D.velocity.Set (velocity.x, velocity.y);
				this.rigidbody2D.position.Set (position.x, position.y);
			}
		}
	}

	void SetAction(float Vx, float Vy) {
		// TODO: enforce physical constrains
		velocity.x = Vx;
		velocity.y = Vy;
	}

	public void Reset() {
		velocity.Set (0, 0);
		reset = true;
	}

	public void Reset(float X, float Y) {
		position.Set (X, Y);
		velocity.Set (0, 0);
		reset = true;
	}
}
