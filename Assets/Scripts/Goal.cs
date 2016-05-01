using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private GameController gameController;
	private PuckController puckController;

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
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Puck"){
			// determine which goal is it
			// inform rewarder and end episode
		}
	}

}
