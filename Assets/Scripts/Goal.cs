using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private GameController gameController;
	private PuckController puckController;
	private RewardManager rewardManager;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null)
		{
			Debug.Log("Cannot find 'GameController' script");
		}
		
		// find the puck
		GameObject puckControllerObject = GameObject.FindWithTag ("Puck");
		if (puckControllerObject != null)
		{
			puckController = puckControllerObject.GetComponent<PuckController>();
		}
		if (puckController == null)
		{
			Debug.Log("Cannot find 'PuckController' script");
		}

		// find the reward manager
		GameObject RewardManagerObject = GameObject.FindWithTag ("RewardManager");
		if (RewardManagerObject != null) {
			rewardManager = RewardManagerObject.GetComponent<RewardManager>();
		}
		if (rewardManager == null) { 
			Debug.Log("Cannot find 'RewardManager' script");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log (this.tag);
		if (other.tag == "Puck"){
			if (this.CompareTag("AgentGoal")) {
				rewardManager.SelfGoal();							// we got a goal, BAD!
			}
			else {
				rewardManager.ScoredGoal();							// we scored!
			}
			gameController.UpdateState(TrainerState.IdleTraining);
		}
	}

}
