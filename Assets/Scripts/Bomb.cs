using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public GameObject ExplosionPrefab;
	public LayerMask LevelMask;
	public bool CanExplode = true;
	[Range(2, 15)]
	public int FireRadius;
	private bool _exploded;
	private Collider _collider;

	void Start () {
		Invoke("Explode", 3f);
		_collider = transform.FindChild("Collider").GetComponent<Collider>();
	}

	private IEnumerator CreateExplosions(Vector3 direction) {
		var boxHit = false;
		var leave = false;
		for (int i = 1; i < FireRadius; i++) {
			RaycastHit hit;
			Vector3 position = transform.position + (i * direction);
			Physics.Raycast(transform.position, direction, out hit, i, LevelMask, QueryTriggerInteraction.Ignore);

			if (hit.collider) {
				switch (hit.collider.tag) {
					case "Bomb":
						var bomb = hit.collider.GetComponent<Bomb>();
						bomb.SelfDestroy(Opposite(direction));
						boxHit = true;
						break;
					case "Box":
						var box = hit.collider.gameObject.GetComponent<Box>();
						box.SelfDestroy();
						boxHit = true;
						break;
					case "Wall":
						leave = true;
						break;
				}
				if (leave) break;
			}
			Instantiate(ExplosionPrefab, position, ExplosionPrefab.transform.rotation);
			if (boxHit) break;
			yield return new WaitForSeconds(.05f); //Wait 50 milliseconds before checking the next location
		}
	}

	public void Explode() {
		SelfDestroy();
	}

	public void SelfDestroy(Vector3? ignoredDirection = null) {
		if (!CanExplode || _exploded) return;
		CancelInvoke("Explode");

		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

		if (!ignoredDirection.HasValue || ignoredDirection.Value != Vector3.forward)
			StartCoroutine(CreateExplosions(Vector3.forward));
		if (!ignoredDirection.HasValue || ignoredDirection != Vector3.right)
			StartCoroutine(CreateExplosions(Vector3.right));
		if (!ignoredDirection.HasValue || ignoredDirection != Vector3.back)
			StartCoroutine(CreateExplosions(Vector3.back));
		if (!ignoredDirection.HasValue || ignoredDirection != Vector3.left)
			StartCoroutine(CreateExplosions(Vector3.left));

		GetComponent<MeshRenderer>().enabled = false;
		_exploded = true;
		_collider.gameObject.SetActive(false);
		Destroy(gameObject, .3f);
	}

	public static Vector3 Opposite(Vector3 direction) {
		if (direction == Vector3.back)
			return Vector3.forward;
		if (direction == Vector3.forward)
			return Vector3.back;
		if (direction == Vector3.left)
			return Vector3.right;
		if (direction == Vector3.right)
			return Vector3.left;
		throw new Exception("Unsupported direction: " + direction);
	}
}
