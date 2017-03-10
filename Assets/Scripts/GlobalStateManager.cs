using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GlobalStateManager : MonoBehaviour
{
	public List<GameObject> Players = new List<GameObject>();
	public const int FireCount = 5;
	public bool ShowBoxes = true;

	private List<int> _deadPlayers;
	private int _playerCount = 4;

	public void Start() {
		_deadPlayers = new List<int>();
		if (ShowBoxes)
			PlaceBoxes();
		fireCount = 0;
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

	void BuildWalls() {
		GameObject prefab = Resources.Load("Wall") as GameObject;
		for (int i = 0; i < 14; i++)
			for (int x = 0; x < 10; x++)
				if (i % 2 == 0 && x % 2 == 0)
					Instantiate(prefab, new Vector3(i + 1f, 0.5f, -x + -1f), Quaternion.identity);
	}

	void PlaceBoxes() {
		GameObject prefab = Resources.Load("Box") as GameObject;
		GameObject fire = ItemCollectible.GetPrefab(ItemCollectible.Types.Fire);
		for (var i = 0; i < 15; i++)
			for (var x = 0; x < 11; x++)
				if ((x % 2 == 0 || i % 2 == 0) && TrueOrFalse()) {
					var box = Instantiate(prefab, new Vector3(i, 0.5f, -x), Quaternion.identity);

					var randCondition = _rand.Next(300) % 13 == 0;
					if (fireCount != FireCount && randCondition) {
						fireCount++;
						box.gameObject.GetComponent<Box>().Item = fire;
					}
				}
	}

	private int fireCount = 0;

	private bool TrueOrFalse() {
		if (_rand == null)
			_rand = new System.Random();
		return _rand.Next(9) % 2 == 0;
	}

	private System.Random _rand;
}