using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public bool Destroyed = false;
	public GameObject Item;

	public void SelfDestroy() {
		if (Destroyed) return;
		Destroyed = true;
		Destroy(gameObject);
		if (Item != null)
			Instantiate(Item, transform.position, Quaternion.identity);
	}
}
