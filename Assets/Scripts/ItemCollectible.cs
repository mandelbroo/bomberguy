using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectible : MonoBehaviour {

//	public void OnTriggerEnter(Collider other) {
//		if (other.CompareTag("Explosion")) {
//			Destroy(this);
//		}
//	}

	public enum Types { Fire, Speed, Bomb, Remote, BombPass, BoxPass }
	public Types Type;
	public GameObject GameObject;

	public static GameObject GetPrefab(Types type) {
		var gameObject = Resources.Load("Items/" + type) as GameObject;
		if (gameObject)
			gameObject.GetComponent<ItemCollectible>().Type = type;
		return gameObject;
	}
}
