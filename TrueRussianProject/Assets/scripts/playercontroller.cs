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
	float range = 80;
	GameObject[] oldfloors;
	public float speedpost = 30;
	int x = 0;
	public float limitSpeedpost = 40;
	public float dragmul;
	float speedabs;
	float speedx;
	float speedy;
	float time;
	
	void Start()
		
	
	{
		GameObject startfloor = Instantiate(floorprefab, new Vector3(0, 0,0 ), Quaternion.identity);
		GameObject startfloor2 = Instantiate(floorprefab, new Vector3(0, 0, 40), Quaternion.identity);
		
		initialPosition = transform.position;
		rigidBody = GetComponent<Rigidbody>();
		SetInfoBoard();
		collSetup = gameObject.AddComponent<ColliderSetup>();
		time = Time.deltaTime;
	}



	void Update()
	{
		speedpost = speedpost + time / 5;
		limitSpeedpost = limitSpeedpost + time / 5;
		getVelocity();
		
		InfoBoardUpdate();
		
	}
	void FixedUpdate()
    {
		Check();
		
		Move();
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
		InfoBoard.text = "Info ";
		VectorSpeed.text = "Vector3 - " + speed;
		floatSpeed.text = "Float - " + speed.sqrMagnitude;
		coliding.text = "Coliding Status - " + collSetup.isColliding;
		coliding.text = "Score: " + points;

	}


	void LateUpdate()
	{
		CallJump();
		CallResetPosition();

	}

	void Move()
	{
		float MovementV = Input.GetAxis("Vertical") / 100;
		float MovementH = Input.GetAxis("Horizontal") * dragmul;
		
		
		Vector3 move = new Vector3(MovementH, 0.0f, MovementV).normalized;
		if (GetColliderStatus(true))
		{
			rigidBody.AddForce(move * moveSpeed);

			
		}

		rigidBody.AddForce(0, 0, speedpost);
	}

	void Check()
	{

		var x = Mathf.Clamp(speed.x, limitSpeed * -1, limitSpeed);
		var y = Mathf.Clamp(speed.y, limitSpeed * -1, limitSpeed);
		var z = Mathf.Clamp(speed.z, limitSpeedpost * -1, limitSpeedpost);
		rigidBody.velocity = new Vector3(x, y, z);
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
		rigidBody.AddForce(new Vector3(0, 10f * jumpSpeedMult, 0));



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
			x = x + 1;
			GameObject clone = Instantiate(floorprefab, new Vector3(0, 0, range), Quaternion.identity);
			Instantiate(triggercube, new Vector3(0, 0, range - 40), Quaternion.identity);
			clone.name = ("clone" + x);
			Destroy(other.gameObject);
			range = range + 40;
			GameObject.Find("clone" + (x - 3)).SetActive(false);



		}

	}
}



