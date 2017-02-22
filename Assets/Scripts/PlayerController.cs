using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//Player parameters
	[Range(1, 8)] //Enables a nifty slider in the editor
	public int playerNumber = 1;
	public bool canDropBombs = true; //Can the player drop bombs?
	public bool canMove = true; //Can the player move?
	public GameObject bombPrefab;
	public float moveSpeed = 5f;

	private Animator animator;
	private Rigidbody rigidBody;
	private Transform myTransform;
	
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		myTransform = transform;
		animator = myTransform.FindChild("PlayerModel").GetComponent<Animator>();
		// dynamic color change
		//Color brown = new Color(139f / 255f, 69f / 255f, 19f / 255f, 1f);
		//Renderer renderer = GetComponent<Renderer>();
		//renderer.material.color = brown;
	}

	void Update()
	{
		UpdateMovement();
	}

	void UpdateMovement()
	{
		animator.SetBool("Walking", false);

		if (canMove)
			UpdatePlayerMovement(playerNumber);
	}

	private void UpdatePlayerMovement(int playerNumber)
	{
		if (Input.GetKey(KeyCode.W))
		{ //Up movement
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
			myTransform.rotation = Quaternion.Euler(0, 0, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.A))
		{ //Left movement
			rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
			myTransform.rotation = Quaternion.Euler(0, 270, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.S))
		{ //Down movement
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
			myTransform.rotation = Quaternion.Euler(0, 180, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.D))
		{ //Right movement
			rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
			myTransform.rotation = Quaternion.Euler(0, 90, 0);
			animator.SetBool("Walking", true);
		}

		if (canDropBombs && Input.GetKeyDown(KeyCode.Space))
		{
			DropBomb();
		}
	}

	void DropBomb()
	{
		if (bombPrefab)
			Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x),
				bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z)),
				bombPrefab.transform.rotation);
	}
}
