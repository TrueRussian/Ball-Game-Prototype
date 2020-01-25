using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playercontroller : MonoBehaviour
{// a bit of premature code refactoring i guess
	public float moveSpeed = 10f;
	public float jumpSpeedMult = 5f;
	public float limitSpeed = 6.3f;
	Vector3 initialPosition;
	Rigidbody rigidBody;
	Vector3 speed;
	public float SpeedComp = 3.3f;
	public float UserDrag = 2;
	public Text InfoBoard;
	public Text floatSpeed;
	public Text VectorSpeed;
	public Text coliding;
	public Text score;
	public int points = 0;
	ColliderSetup collSetup;
	Vector3 MoveMoment;
	public GameObject floorprefab;
	public GameObject triggercube;
	Vector3 floorposition = new Vector3(1, 1, 1);
	float range = 40;
	void Start()
	{

		initialPosition = transform.position;
		rigidBody = GetComponent<Rigidbody>();
		SetInfoBoard();
		collSetup = gameObject.AddComponent<ColliderSetup>();
	}



	void Update()
	{
		Move();
		getVelocity();
		Check();
		InfoBoardUpdate();
		CallJump();
	}

	void SetInfoBoard()
	{
		InfoBoard.text = "";
		floatSpeed.text = "";
		VectorSpeed.text = "";
		coliding.text = "";
		score.text = "";
	}
	void InfoBoardUpdate()
	{
		InfoBoard.text = "Your speed: ";
		VectorSpeed.text = "Vector3 - " + speed;
		floatSpeed.text = "Float - " + speed.sqrMagnitude;
		coliding.text = "Coliding Status - " + collSetup.isColliding;
		coliding.text = "Score: " + points;

	}


	void LateUpdate()
	{

		CallResetPosition();

	}

	void Move()
	{
		float MovementV = Input.GetAxis("Vertical");
		float MovementH = Input.GetAxis("Horizontal");

		Vector3 move = new Vector3(MovementH, 0.0f, MovementV);
		if (GetColliderStatus(true))
		{
			rigidBody.AddForce(move * moveSpeed);
		}
		MoveMoment = move * moveSpeed;
	}

	void Check()
	{

		if (speed.sqrMagnitude > limitSpeed && GetColliderStatus(true))//we've dun goof'd here
		{

			Debug.Log(" 2fast: ");
			rigidBody.velocity = speed.normalized * SpeedComp;

		}

	}

	//only call it from LateUpdate
	void CallResetPosition()
	{
		if (Input.GetKeyDown(KeyCode.Backspace))//using literal keycode rn bcuz fuck input managers and stuff
			ResetPosition();
	}
	void ResetPosition()
	{
		rigidBody.velocity = Vector3.zero;//stoping all forces just for a good measure
		rigidBody.angularVelocity = Vector3.zero;
		transform.position = initialPosition;
	}
	//only call it from LateUpdate
	void CallJump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && GetColliderStatus(true))
			Jump();

	}
	void Jump()
	{
		rigidBody.AddForce(new Vector3(MoveMoment.x, 10f * jumpSpeedMult, MoveMoment.z));



	}

	void getVelocity()
	{
		speed = rigidBody.velocity;

	}

	bool GetColliderStatus(bool iscol)
	{
		iscol = collSetup.isColliding;
		return iscol;

	}


	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("Coin"))
		{
			points++;
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag("trigger"))
		{
			Instantiate(floorprefab, new Vector3(0, 0, range), Quaternion.identity);
			Instantiate(triggercube, new Vector3(0, 0, range), Quaternion.identity);
			Destroy(other.gameObject);
			range = range + 40;



		}

	}
}



