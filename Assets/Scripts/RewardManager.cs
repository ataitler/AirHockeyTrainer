using UnityEngine;
using System.Collections;

public class RewardManager : MonoBehaviour {

	private bool isPuckAgentHit;
	private bool isConstrainViolated;
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
		isConstrainViolated = false;
		isSelfGoal = false;
		isScoredGoal = false;
		isTimeout = false;
		isAgentWallHit = false;
		isOppWallHit = false;
		rightWallHits = 0;
		leftWallHits = 0;
	}
	
	public double GetReward() {
		double reward = CalculateReward ();
		Reset ();
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
		isConstrainViolated = true;
	}

	public void AgentPuckCollision() {
		isPuckAgentHit = true;
	}

	public void Timeout() {
		isTimeout = true;
	}

	public void Reset() {
		isPuckAgentHit = false;
		isConstrainViolated = false;
		isSelfGoal = false;
		isScoredGoal = false;
		isTimeout = false;
		isAgentWallHit = false;
		isOppWallHit = false;
		rightWallHits = 0;
		leftWallHits = 0;
	}

	private double CalculateReward()
	{
		double reward = 0;

		if (isPuckAgentHit == false)
		{
			reward -= 1;
		} 
		if (isConstrainViolated)
		{
			reward -=1;
		}
		if ((isAgentWallHit) || (rightWallHits>0) || (leftWallHits>0))
		{
			reward -= 10;
		}
		if (isTimeout)
		{
			reward -= 1;
		}
		if (isSelfGoal)
		{
			reward -= 100;
		}
		if (isScoredGoal)
		{
			reward += 100;
		}
		if (isPuckAgentHit){
			reward += 50;
		}
		if (isOppWallHit)
		{
			reward -= 5;
		}

		return reward;
	}
}
