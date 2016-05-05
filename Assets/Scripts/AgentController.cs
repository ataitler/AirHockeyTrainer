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
		this.rigidbody2D.position = position;
		this.rigidbody2D.velocity = velocity;
		reset = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (gameController.State == TrainerState.Training) {
			this.rigidbody2D.velocity = velocity;
			this.rigidbody2D.position = new Vector2(
				Mathf.Clamp(rigidbody2D.position.x, -1180, -10), Mathf.Clamp(rigidbody2D.position.y, -590, 590)
				);
		}
		else {
			if (reset) {
				this.rigidbody2D.velocity = velocity;
				this.rigidbody2D.position = position;
				reset = false;
				Debug.Log("reseting agent's values to position:" + position.ToString());
			}
		}
	}

	public void SetAction(float Vx, float Vy) {
		// TODO: enforce physical constrains
		if (!reset) {
			velocity.Set (Vx, Vy);
			Debug.Log ("action updated");
		}
	}

	public void Reset() {
		reset = true;
		velocity.Set (0, 0);
	}

}
