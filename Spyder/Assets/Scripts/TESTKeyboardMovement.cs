using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class governs how the player moves the avatar using the keyboard
public class TESTKeyboardMovement : MonoBehaviour
{
	float xMove = 0.0f, yMove = 0.0f;
	public float speed = 5.0f;
	Rigidbody2D myRigidbody2d;

	private void Start()
	{
		myRigidbody2d = gameObject.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		CheckInput();
	}

	private void FixedUpdate()
	{
		Move();
	}

	void CheckInput()
	{
		xMove = Input.GetAxis("Horizontal") * speed;
		//yMove = Input.GetAxis("Vertical") * speed;
	}

	void Move()
	{
		Vector2 temp = myRigidbody2d.velocity;
		temp.x = xMove;
		//temp.y = yMove;
		myRigidbody2d.velocity = temp;
	}
}
