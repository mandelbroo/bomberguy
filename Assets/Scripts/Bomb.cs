using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public GameObject ExplosionPrefab;
	public LayerMask LevelMask;
	public bool CanExplode = true;
	[Range(2, 15)]
	public int Range;
	private bool _exploded;
	private Collider _collider;

	void Start () {
		Invoke("Explode", 3f);
		_collider = transform.FindChild("Collider").GetComponent<Collider>();
	}

	private IEnumerator CreateExplosions(Vector3 direction) {
		for (int i = 1; i < Range; i++) {
			RaycastHit hit;
			Physics.Raycast(transform.position, direction, out hit, i, LevelMask);

			if (!hit.collider) {
				Instantiate(ExplosionPrefab, transform.position + (i * direction), ExplosionPrefab.transform.rotation);
			}
			else {//Hit a block, stop spawning in this direction
				break;
			}
			yield return new WaitForSeconds(.05f); //Wait 50 milliseconds before checking the next location
		}
	}

	public void Explode() {
		if (!CanExplode) return;
		
		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

		StartCoroutine(CreateExplosions(Vector3.forward));
		StartCoroutine(CreateExplosions(Vector3.right));
		StartCoroutine(CreateExplosions(Vector3.back));
		StartCoroutine(CreateExplosions(Vector3.left));

		GetComponent<MeshRenderer>().enabled = false;
		_exploded = true;
		_collider.gameObject.SetActive(false);
		Destroy(gameObject, .3f);
	}

	public void OnTriggerEnter(Collider other) {
		if (!_exploded && other.CompareTag("Explosion") && _collider.bounds.Intersects(other.bounds)) {
			CancelInvoke("Explode");
			Explode();
		}
	}
}
