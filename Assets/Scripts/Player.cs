using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Range(1, 8)] //Enables a nifty slider in the editor
	public int PlayerNumber = 1;
	public bool CanDropBombs = true;
	public bool CanDie = true;
	public bool CanMove = true;
	public float MoveSpeed = 5f;
	public bool Active = true;
	public bool Dead;
	public GameObject BombPrefab;
	public GlobalStateManager GlobalManager; 
	public int FireRange = 2;

	private Animator _animator;
	private Collider _collider;
	private Transform _myTransform;
	private GameObject _recentBomb;
	private Rigidbody _rigidBody;

	public Collider GetCollider() {
		if (!_collider)
			_collider = GetComponent<Collider>();
		return _collider;
	}

	void Start() {
		gameObject.SetActive(Active);
		if (Active) {
			_rigidBody = GetComponent<Rigidbody>();
			_myTransform = transform;
			_animator = _myTransform.FindChild("PlayerModel").GetComponent<Animator>();
		}
	}

	void Update() {
		UpdateMovement();
	}

	public void OnTriggerEnter(Collider other) {
		if (CanDie && !Dead && other.CompareTag("Explosion")) {
			Debug.Log("P" + PlayerNumber + " hit by explosion!");
			Dead = true;
			GlobalManager.PlayerDied(PlayerNumber);
			Destroy(gameObject);
		}
		else if (other.CompareTag("Item")) {
			Destroy(other.gameObject);

			if (other.gameObject.GetComponent<ItemCollectible>().Type == ItemCollectible.Types.Fire) {
				FireRange++;
			}
		}
	}

	void UpdateMovement() {
		_animator.SetBool("Walking", false);

		if (CanMove)
			UpdatePlayerMovement(PlayerNumber);

		if (_recentBomb && !Intersects(_recentBomb))
		{
			_recentBomb.AddComponent<SphereCollider>();
			_recentBomb = null;
		}
	}

	private void UpdatePlayerMovement(int playerNumber) {

		float translation = Input.GetAxis("Joy" + playerNumber + "X");
		float rotation = Input.GetAxis("Joy" + playerNumber + "Y");

		if (Input.GetKey(KeyCode.W) || rotation == -1) { //move up
			_rigidBody.velocity = new Vector3(_rigidBody.velocity.x, _rigidBody.velocity.y, MoveSpeed);
			_myTransform.rotation = Quaternion.Euler(0, 0, 0);
			_animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.A) || translation == -1) { //move down
			_rigidBody.velocity = new Vector3(-MoveSpeed, _rigidBody.velocity.y, _rigidBody.velocity.z);
			_myTransform.rotation = Quaternion.Euler(0, 270, 0);
			_animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.S) || rotation == 1) { //move left
			_rigidBody.velocity = new Vector3(_rigidBody.velocity.x, _rigidBody.velocity.y, -MoveSpeed);
			_myTransform.rotation = Quaternion.Euler(0, 180, 0);
			_animator.SetBool("Walking", true);
		}

		if (Input.GetKey(KeyCode.D) || translation == 1) { //move right
			_rigidBody.velocity = new Vector3(MoveSpeed, _rigidBody.velocity.y, _rigidBody.velocity.z);
			_myTransform.rotation = Quaternion.Euler(0, 90, 0);
			_animator.SetBool("Walking", true);
		}

		if (CanDropBombs && (Input.GetKeyDown(KeyCode.Space) || IsButtonPressed(0, playerNumber))) {
			DropBomb();
		}
	}

	void DropBomb() {
		if (BombPrefab) {
			Vector3 position = new Vector3(Mathf.RoundToInt(_myTransform.position.x), 0.3f, Mathf.RoundToInt(_myTransform.position.z));
			_recentBomb = Instantiate(BombPrefab, position, BombPrefab.transform.rotation);
			_recentBomb.GetComponent<Bomb>().Range = FireRange;
		}
	}

	bool Intersects(GameObject gameObject) {
		return GetCollider().bounds.Intersects(gameObject.GetComponent<Renderer>().bounds);
	}

	bool IsButtonPressed(int button, int playerNumber) {
		KeyCode enumValue = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + playerNumber + "Button" + button);
		return Input.GetKeyDown(enumValue);
	}
}
