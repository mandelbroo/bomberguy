using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public GameObject explosionPrefab;
	public LayerMask levelMask;
	private bool exploded = false;

	void Start () {
		Invoke("Explode", 3f);
	}
	
	void Update () {
		
	}

	private IEnumerator CreateExplosions(Vector3 direction)
	{
		for (int i = 1; i < 3; i++)
		{ //The 3 here dictates how far the raycasts will check, in this case 3 tiles far
			RaycastHit hit; //Holds all information about what the raycast hits

			Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i, levelMask); //Raycast in the specified direction at i distance, because of the layer mask it'll only hit blocks, not players or bombs

			if (!hit.collider)
			{ // Free space, make a new explosion
				Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
			}
			else
			{ //Hit a block, stop spawning in this direction
				break;
			}
			yield return new WaitForSeconds(.05f); //Wait 50 milliseconds before checking the next location
		}

	}

	void Explode()
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);

		StartCoroutine(CreateExplosions(Vector3.forward));
		StartCoroutine(CreateExplosions(Vector3.right));
		StartCoroutine(CreateExplosions(Vector3.back));
		StartCoroutine(CreateExplosions(Vector3.left));

		GetComponent<MeshRenderer>().enabled = false;
		exploded = true;
		Transform collider = transform.FindChild("Collider");
		if (collider)
			collider.gameObject.SetActive(false);
		Destroy(gameObject, .3f);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (!exploded && other.CompareTag("Explosion"))
		{
			CancelInvoke("Explode");
			Explode();
		}
	}
}
