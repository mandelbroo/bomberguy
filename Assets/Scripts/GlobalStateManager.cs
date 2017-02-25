using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalStateManager : MonoBehaviour
{
	public List<GameObject> Players = new List<GameObject>();

	private int deadPlayers = 0;
	private int deadPlayerNumber = -1;

	public void PlayerDied(int playerNumber)
	{
		deadPlayers++;

		if (deadPlayers == 1)
		{
			deadPlayerNumber = playerNumber;
			Invoke("CheckPlayersDeath", .3f);
		}
	}

	void CheckPlayersDeath()
	{
		// todo
	}
}