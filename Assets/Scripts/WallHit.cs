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

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Agent")) {		// one of the constrains was violated
			rewardManager.AgentConstrainViolation();
			gameController.UpdateState(TrainerState.IdleTraining);
		}
		else if (other.CompareTag("Puck")) {
			if (this.CompareTag("RightWall")) {
				rewardManager.RightWallHit();
			}
			else if (this.CompareTag("LeftWall")) {
				rewardManager.LeftWallHit();
			}
			else if (this.CompareTag("AgentWall")) {
				rewardManager.AgentWallHit();
				gameController.UpdateState(TrainerState.IdleTraining);
			}
			else if (this.CompareTag("OppWall")) {
				rewardManager.OppWallHit();
				gameController.UpdateState(TrainerState.IdleTraining);
			}
		}
	}

}
