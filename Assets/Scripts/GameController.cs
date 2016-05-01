using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public RewardManager reward;
	public CommunicationController communication;
	public PuckController puck;
	public AgentController agent;
	public Action action;

	public int max_speed;

	private TrainerState state;

	public TrainerState State
	{
		get { return state; }
	}

	// Use this for initialization
	void Start () {
		state = TrainerState.Disconnected;
		Random.seed = 42;
	}

	// FixedUpdate is called once every physical time step
	void FixedUpdate () {
		// intialize new episode
		if (state == TrainerState.IdleTrainning) {

			// only in the positive table positions
			puck.rigidbody2D.position = new Vector2(Random.value*1000, Random.value*550*2 - 550);
			// only negative velocities in X axis
			puck.rigidbody2D.velocity = new Vector2(-Random.value*max_speed, Random.value*max_speed - max_speed);
			// rotations
			puck.rigidbody2D.rotation = Random.value*Mathf.PI*2 - Mathf.PI;

			// update the game state and start new episode
			UpdateState(TrainerState.Trainning);
			return;
		}
		if (state == TrainerState.Trainning) {
			// send msg to the agent.
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) {
			// Quit simulator
		}

		if (state == TrainerState.IdleTrainning || state == TrainerState.Trainning) {
			if (Input.GetKeyDown (KeyCode.S)) {
				// stop game and move to idle state
				UpdateState (TrainerState.Idle);
			}
		}

		if (state == TrainerState.Idle) {
			if (Input.GetKeyDown(KeyCode.R)) {
				// Start trainning
				UpdateState(TrainerState.IdleTrainning);
			}
		}
	}
	
	public void UpdateState (TrainerState st) {
		// handle the state machine
		switch (st) {
			case TrainerState.Disconnected:
				break;
			case TrainerState.Idle:
				break;
			case TrainerState.Init:
				break;
			case TrainerState.Trainning:
				// start new episode
				break;
			case TrainerState.IdleTrainning:
				// Reset every object and calculation.
				break;
			default:
				break;
		}
		// update the state
		if (state != st)
			state = st;
	}
}

