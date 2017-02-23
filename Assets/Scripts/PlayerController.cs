using System;
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
	public bool active = true;

	private Animator animator;
	private Rigidbody rigidBody;
	private Transform myTransform;
	
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		myTransform = transform;
		animator = myTransform.FindChild("PlayerModel").GetComponent<Animator>();
		//string[] jnames = Input.GetJoystickNames();
		//if (jnames.Length > 0)
		//{
		//	Debug.Log(jnames.Length);
		//	Debug.Log(jnames[0]);
		//	Debug.Log(jnames[1]);
		//}
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
		
		float translation = Input.GetAxis("Joy" + playerNumber + "X");
		float rotation = Input.GetAxis("Joy" + playerNumber + "Y");

		if (Input.GetKey(KeyCode.W) || rotation == -1)
		{ //move up
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
			myTransform.rotation = Quaternion.Euler(0, 0, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.A) || translation == -1)
		{ //move down
			rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
			myTransform.rotation = Quaternion.Euler(0, 270, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.S) || rotation == 1)
		{ //move left
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
			myTransform.rotation = Quaternion.Euler(0, 180, 0);
			animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.D) || translation == 1)
		{ //move right
			rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
			myTransform.rotation = Quaternion.Euler(0, 90, 0);
			animator.SetBool("Walking", true);
		}

		if (canDropBombs && (Input.GetKeyDown(KeyCode.Space) || IsButtonPressed(0, playerNumber)))
		{
			DropBomb();
		}
	}

	void DropBomb()
	{
		if (bombPrefab)
		{
			GameObject recentBomb = Instantiate(bombPrefab, new Vector3(
				myTransform.position.x,
				bombPrefab.transform.position.y, 
				myTransform.position.z),
				bombPrefab.transform.rotation);
		}
	}

	bool IsButtonPressed(int button, int playerNumber)
	{
		KeyCode enumValue = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + playerNumber + "Button" + button);
		return Input.GetKeyDown(enumValue);
	}
}
