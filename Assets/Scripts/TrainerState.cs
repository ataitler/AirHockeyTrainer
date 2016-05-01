using UnityEngine;
using System.Collections;

public enum TrainerState {
	// neural agent is not connected
	Disconnected,
	// neural agent is being trained
	Trainning,
	// init new episode, this is a transition state
	Init,
	// idle training - between episodes
	IdleTrainning,
	// idle state.
	Idle
}
