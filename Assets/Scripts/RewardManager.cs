using UnityEngine;
using System.Collections;

public class RewardManager : MonoBehaviour {

	private bool isPuckAgentHit;
	private bool isConstrainViolatied;
	private bool isSelfGoal;
	private bool isScoredGoal;
	private bool isTimeout;
	private bool isAgentWallHit;
	private bool isOppWallHit;
	private int rightWallHits;
	private int leftWallHits;

	// Use this for initialization
	void Start () {
		isPuckAgentHit = false;
		isConstrainViolatied = false;
		isSelfGoal = false;
		isScoredGoal = false;
		isTimeout = false;
		isAgentWallHit = false;
		isOppWallHit = false;
		rightWallHits = 0;
		leftWallHits = 0;
	}
	
	public double GetReward() {
		double reward = 0;
		// reward formula...
		return reward;
	}

	public void SelfGoal() {
		isSelfGoal = true;
	}

	public void ScoredGoal() {
		isScoredGoal = true;
	}

	public void RightWallHit() {
		rightWallHits++;
	}

	public void LeftWallHit() {
		leftWallHits++;
	}

	public void AgentWallHit() {
		isAgentWallHit = true;
	}

	public void OppWallHit() {
		isOppWallHit = true;
	}

	public void AgentConstrainViolation() {
		isConstrainViolatied = true;
	}

	public void AgentPuckCollision() {
		isPuckAgentHit = true;
	}

	public void Timeout() {
		isTimeout = true;
	}

	public void Reset() {
		isPuckAgentHit = false;
		isConstrainViolatied = false;
		isSelfGoal = false;
		isScoredGoal = false;
		isTimeout = false;
		isAgentWallHit = false;
		isOppWallHit = false;
		rightWallHits = 0;
		leftWallHits = 0;
	}

}
