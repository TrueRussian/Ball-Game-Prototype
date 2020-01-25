using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{// a bit of premature code refactoring i guess
	public float moveSpeed = 5f;
	public float jumpSpeedMult = 5f;
	public float limitSpeed = 9.3f;
	Vector3 initialPosition;
	Rigidbody rigidBody;

	void Start()
	{
		initialPosition = transform.position;
		rigidBody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Move();
		ClampVelocity();
	}

	void LateUpdate()
	{
		CallJump();
		CallResetPosition();
		Check();
	}

	void Move()
	{
		float MovementV = Input.GetAxis("Vertical");
		float MovementH = Input.GetAxis("Horizontal");

		Vector3 move = new Vector3(MovementH, 0.0f , MovementV);

		if (!ClampVelocity())
			rigidBody.AddForce(move * moveSpeed);
	}

	void Check()
	{
		if (rigidBody.velocity.x > limitSpeed)//we've dun goof'd here
			Debug.Log("X 2fast: " + rigidBody.velocity.x + "|" + rigidBody.velocity.sqrMagnitude);

		if (rigidBody.velocity.z > limitSpeed)
			Debug.Log("Y 2fast: " + rigidBody.velocity.z + "|" + rigidBody.velocity.sqrMagnitude);
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
		if (Input.GetKeyDown(KeyCode.Space))
			Jump();
	}
	void Jump()
	{
		rigidBody.AddForce(new Vector3(0, 10f * jumpSpeedMult, 0) * moveSpeed);
	}

	bool ClampVelocity()
	{
		//if (rigidBody.velocity.x > limitSpeed | rigidBody.velocity.y > limitSpeed)
		return false;
	}
}
