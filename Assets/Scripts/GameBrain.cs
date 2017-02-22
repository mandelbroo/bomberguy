using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameBrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//BuildWalls();
		//PlaceBoxes();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void BuildWalls()
	{
		GameObject prefab = Resources.Load("Wall") as GameObject;
		for (int i = 0; i < 14; i++)
			for (int x = 0; x < 10; x++)
				if (i % 2 == 0 && x % 2 == 0)
					Instantiate(prefab, new Vector3(i + 1.5f, 0.5f, -x + -1.5f), Quaternion.identity);
	}

	void PlaceBoxes()
	{
		GameObject prefab = Resources.Load("Box") as GameObject;
		for (int i = 0; i < 15; i++)
			for (int x = 0; x < 11; x++)
				if ((x % 2 == 0 || i % 2 == 0) && TrueOrFalse())
					Instantiate(prefab, new Vector3(i + 0.5f, 0.5f, -x + -0.5f), Quaternion.identity);
	}

	bool TrueOrFalse()
	{
		if (rand == null)
			rand = new System.Random();
		return rand.Next(9) % 2 == 0;
	}

	private System.Random rand;
}
