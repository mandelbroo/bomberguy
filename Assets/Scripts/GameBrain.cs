using UnityEngine;

public class GameBrain : MonoBehaviour {

	public bool ShowBoxes = true;

	void Start () {
		if (ShowBoxes)
			PlaceBoxes();
		fireCount = 0;
	}
	public const int FireCount = 5;
	
	void Update () {
		
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
				if ((x%2 == 0 || i%2 == 0) && TrueOrFalse()) {
					var box = Instantiate(prefab, new Vector3(i, 0.5f, -x), Quaternion.identity);

					var randCondition = rand.Next(300) % 13 == 0;
					if (fireCount != FireCount && randCondition) {
						fireCount++;
						box.gameObject.GetComponent<Box>().Item = fire;
					}
				}
	}

	private int fireCount = 0;

	private bool TrueOrFalse() {
		if (rand == null)
			rand = new System.Random();
		return rand.Next(9) % 2 == 0;
	}

	private System.Random rand;
}
