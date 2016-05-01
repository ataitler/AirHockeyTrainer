using UnityEngine;
using System.Collections;

public class WallHit : MonoBehaviour {

	private GameController gameController;
	private PuckController puckController;
	private RewardManager rewardManager;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log("Cannot find 'GameController' script");
		}
		
		// find the puck
		GameObject puckControllerObject = GameObject.FindWithTag ("Puck");
		if (puckControllerObject != null) {
			puckController = puckControllerObject.GetComponent<PuckController>();
		}
		if (puckController == null) {
			Debug.Log("Cannot find 'PuckController' script");
		}

		// find the reward manger
		GameObject rewardManagerObject = GameObject.FindWithTag ("RewardManager");
		if (rewardManagerObject != null) {
			rewardManager = rewardManagerObject.GetComponent<RewardManager> ();
		}
		if (rewardManager == null) {
			Debug.Log("Cannot find 'RewardManager' script");
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Agent") {
			//need to inform the rewarder and end episode.
			return;
		}
		if (coll.gameObject.tag == "Puck") {
			// determind which collider trigered the event
			// recored data with the rewarder
		}
	}

}
