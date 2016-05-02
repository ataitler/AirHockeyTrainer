using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

public class CommunicationController : MonoBehaviour {

	// Thread signal.
	private static ManualResetEvent allDone = new ManualResetEvent(false);
	private Socket connection;
	
	public AgentController agent;
	public PuckController puck;
	public GameController gameManager;
	public RewardManager rewardManager;
	public int port;

	// Use this for initialization
	void Start () {
		// init socker parameters
		IPAddress IpAddress = IPAddress.Loopback;
		IPEndPoint localendPoint = new IPEndPoint (IpAddress, port);

		// create a TCP socket
		connection = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		// bind the socket to the end point and listen for incoming connections
		try {
			connection.Bind(localendPoint);
			connection.Listen(100);
			connection.BeginAccept(new AsyncCallback(AcceptCallback), connection);
		}
		catch (Exception) {
			Debug.Log("Couldn't setup the TCP listner");
		}
	}

	void FixedUpdate () {
		if (IsConnected(connection)) {
			gameManager.UpdateState(TrainerState.Disconnected);
		}


		if (gameManager.State == TrainerState.Training) {

			/*string reward = rewardManager.GetReward ().ToString ();
			string puckData = puck.rigidbody2D.position.x.ToString () + "," + puck.rigidbody2D.position.y.ToString () + "," +
					puck.rigidbody2D.velocity.x.ToString () + "," + puck.rigidbody2D.velocity.y.ToString () + "," +
					puck.rigidbody2D.angularVelocity.ToString () + ",";
			string agentData = agent.rigidbody2D.position.x.ToString () + "," + agent.rigidbody2D.position.y.ToString () + "," +
					agent.rigidbody2D.velocity.x.ToString () + "," + agent.rigidbody2D.velocity.y.ToString () + ",";

			string msg = agentData + puckData + reward;	// agentX, agentY, agentVx, agentVy, puckX, puckY, puckVx, puckVy, puckR

			// send information to agent
			Send (connection, msg);*/
		}
	}

	private void AcceptCallback(IAsyncResult ar) {
		// Get the socket that handles the client request.
		Socket listener = (Socket)ar.AsyncState;
		Socket handler = listener.EndAccept(ar);

		try {
			// Create the state object.
			StateObject state = new StateObject();
			state.workSocket = handler;
			handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);

			// notify game manager an agents has connected
			gameManager.UpdateState (TrainerState.Idle);
		}
		catch {
			Debug.Log("Error listenning to incoming connection");
		}
	}
	
	private void ReadCallback(IAsyncResult ar) {
		String content = String.Empty;

		// Retrieve the state object and the handler socket
		// from the asynchronous state object.
		StateObject state = (StateObject)ar.AsyncState;
		Socket handler = state.workSocket;

		if (handler.Connected) {
			// Read data from the client socket. 
			int bytesRead = handler.EndReceive (ar);

			// send action to agent
			//agent.

			// Continue listenning
			handler.BeginReceive (state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback (ReadCallback), state);
		}
	}
	
	public void Send(Socket handler, String data) {
		// Convert the string data to byte data using ASCII encoding.
		byte[] byteData = Encoding.ASCII.GetBytes(data);

		try {
			// Begin sending the data to the remote device.
			handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
		}
		catch {
			Debug.Log("Dropped connection");
		}
	}

	public void Send(String data) {
		// Convert the string data to byte data using ASCII encoding.
		byte[] byteData = Encoding.ASCII.GetBytes(data);
		
		try {
			// Begin sending the data to the remote device.
			this.connection.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), this.connection);
		}
		catch {
			Debug.Log("Dropped connection");
		}
	}
	
	private void SendCallback(IAsyncResult ar) {
		try {
			// Retrieve the socket from the state object.
			Socket handler = (Socket)ar.AsyncState;

			if (handler.Connected) {
				// Complete sending the data to the remote device.
				int bytesSent = handler.EndSend(ar);
				Debug.Log("Sent " + bytesSent.ToString() + " bytes to client.");
			}
		}
		catch (Exception e) {
			Debug.Log(e.ToString());
		}
	}

	/// <summary>
	/// Shuts down.
	/// </summary>
	/// <param name="onlyListen">If set to <c>true</c> only listen.</param>
	public void ShutDown(bool onlyListen) {
		try {
		if (onlyListen == false) {
			connection.Shutdown (SocketShutdown.Both);
			connection.Close ();
		}
		}
		catch {
			Debug.Log("Error shutting down connection on the server side");
		}

		// relisten for incomming connections
		IPAddress IpAddress = IPAddress.Loopback;
		IPEndPoint localendPoint = new IPEndPoint (IpAddress, port);
		connection = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		try {
			connection.Bind(localendPoint);
			connection.Listen(100);
			connection.BeginAccept(new AsyncCallback(AcceptCallback), connection);
		}
		catch (Exception) {
			Debug.Log("Couldn't setup the TCP listner");
		}
	}

	/// <summary>
	/// Determines whether this instance is connected the specified socket.
	/// </summary>
	/// <returns><c>true</c> if this instance is connected the specified socket; otherwise, <c>false</c>.</returns>
	/// <param name="socket">Socket.</param>
	private bool IsConnected(Socket socket) {
		try {
			return !(socket.Poll(1,SelectMode.SelectRead) && socket.Available == 0);
		}
		catch (SocketException) {
			return false;
		}
	}

}