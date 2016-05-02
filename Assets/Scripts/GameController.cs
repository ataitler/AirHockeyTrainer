using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public RewardManager reward;
	public CommunicationController communication;
	public PuckController puck;
	public AgentController agent;
	public Action action;

	public int maxSpeed;

	private TrainerState state;

	public TrainerState State
	{
		get { return state; }
	}

	// Use this for initialization
	void Start () {
		state = TrainerState.Disconnected;
		UpdateState (TrainerState.Disconnected);
		Random.seed = 42;
	}

	// FixedUpdate is called once every physical time step
	void FixedUpdate () {
		if (state == TrainerState.Training) {
			// send msg to the agent.
			string msg = string.Empty;
			communication.Send(msg);
		}
		// intialize new episode
		else if (state == TrainerState.IdleTraining) {

			// reset all positions and velocities
			puck.Reset(Random.value*1000, Random.value*550*2 - 550, -Random.value*maxSpeed, Random.value*maxSpeed - maxSpeed, Random.value*Mathf.PI*2 - Mathf.PI);
			agent.Reset();

			// send reward

			// start training
			UpdateState(TrainerState.Training);
		}
		else if (state == TrainerState.Disconnected)
		{
		}
	}
	
	// Update is called once per frame
	void Update () {

		// move to disconnected state
		if (Input.GetKeyDown (KeyCode.Q)) {
			//disconnect communication
			communication.ShutDown(false);
			// Quit simulator
			UpdateState(TrainerState.Disconnected);
		}

		if (state == TrainerState.IdleTraining || state == TrainerState.Training) {
			// stop game and move to idle state
			if (Input.GetKeyDown (KeyCode.S)) {
				UpdateState (TrainerState.Idle);
			}
		}
		if (state == TrainerState.Idle) {
			// start training
			if (Input.GetKeyDown(KeyCode.R)) {
				// Start trainning
				UpdateState(TrainerState.IdleTraining);
			}
		}
	}
	
	public void UpdateState (TrainerState st) {
		// handle the state machine
		switch (st) {
			case TrainerState.Disconnected:
				Debug.Log("Disconnected state");
				break;
			case TrainerState.Idle:
				// wait to start a new training session
				Debug.Log("Idle state");
				break;
			case TrainerState.Init:
				// obselete
				break;
			case TrainerState.Training:
				Debug.Log("Training state");
				// start new episode
				break;
			case TrainerState.IdleTraining:
				Debug.Log("IdleTraining state");
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

