using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GlobalStateManager : MonoBehaviour
{
	public List<GameObject> Players = new List<GameObject>();

	private List<int> _deadPlayers;
	private int _playerCount = 4;

	public void Start() {
		_deadPlayers = new List<int>();
	}

	public void PlayerDied(int playerNumber) {
		_deadPlayers.Add(playerNumber);
		Invoke("CheckPlayersDeath", .3f);
	}

	void CheckPlayersDeath() {
		if (_deadPlayers.Count >= _playerCount)
			Debug.Log("Draw");
		else if (_deadPlayers.Count == _playerCount - 1)
			Debug.Log("Player " + (10 - _deadPlayers.Sum(i => i)) + " Win!");
	}
}