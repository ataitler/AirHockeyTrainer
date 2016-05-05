using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public RewardManager reward;
	public CommunicationController communication;
	public PuckController puck;
	public AgentController agent;
	public Action action;
	public bool trainingDelay;
	public float Timer;
	private float _Timer;

	private float maxSpeed;

	private TrainerState state;

	public TrainerState State
	{
		get { return state; }
	}

	// Use this for initialization
	void Start () {
		//state = TrainerState.Disconnected;
		state = TrainerState.Init;
		trainingDelay = true;
		UpdateState (TrainerState.Disconnected);
		Random.seed = 42;
		maxSpeed = puck.GetMaxSpeed;
		_Timer = Timer;
	}

	// FixedUpdate is called once every physical time step
	void FixedUpdate () {
		if (state == TrainerState.Training) {
			string puckData = puck.rigidbody2D.position.x.ToString () + "," + puck.rigidbody2D.position.y.ToString () + "," +
							  puck.rigidbody2D.velocity.x.ToString () + "," + puck.rigidbody2D.velocity.y.ToString () + "," +
							  puck.rigidbody2D.angularVelocity.ToString ();
			string agentData = agent.rigidbody2D.position.x.ToString () + "," + agent.rigidbody2D.position.y.ToString () + "," +
						       agent.rigidbody2D.velocity.x.ToString () + "," + agent.rigidbody2D.velocity.y.ToString ();
			
			string msg = "<Message>:" + agentData + "," + puckData;	// agentX, agentY, agentVx, agentVy, puckX, puckY, puckVx, puckVy, puckR
			communication.Send(msg);

			_Timer -= Time.fixedDeltaTime;
			if (_Timer <= 0) {
				reward.Timeout();
				UpdateState(TrainerState.IdleTraining);
			}
		}
		// intialize new episode
		else if (state == TrainerState.IdleTraining) {
			if (trainingDelay) {
				// reset all positions and velocities
				puck.Reset(Random.value*1000, Random.value*550*2 - 550, -Random.value*maxSpeed, Random.value*maxSpeed - maxSpeed, Random.value*Mathf.PI*2 - Mathf.PI);
				agent.Reset();
				Debug.Log("reseting objects");
				// send reward

				trainingDelay = false;

				_Timer = Timer;
			}
			else {
				UpdateState(TrainerState.Training);
				trainingDelay = true;
			}
		}
		else if (state == TrainerState.Idle)
		{
			//agent.Reset();
			//puck.Reset(0,0,0,0,0);
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
				if (state != TrainerState.Disconnected)
					Debug.Log("Disconnected state");
				break;
			case TrainerState.Idle:
				puck.Reset(1000,0,0,0,0);
				agent.Reset();
				if (state == TrainerState.Disconnected) {
					// send action type to train
					Debug.Log("Sending action: " + action.ToString());
					communication.Send(action.ToString());
				}
				// wait to start a new training session
				Debug.Log("Idle state");
				break;
			case TrainerState.Init:
				// obselete
				break;
			case TrainerState.Training:
				Debug.Log("Training state");
				// start new episode
				reward.Reset();
				break;
			case TrainerState.IdleTraining:
				Debug.Log("IdleTraining state");
				if (state == TrainerState.Training) {
					// send reward
					communication.Send("<Reward>: " + reward.GetReward().ToString());
					reward.Reset();
				}
				break;
			default:
				break;
		}
		// update the state
		if (state != st)
			state = st;
	}
}

