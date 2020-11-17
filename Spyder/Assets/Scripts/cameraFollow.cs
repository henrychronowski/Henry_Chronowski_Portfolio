using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
	public Transform target;

	[SerializeField]
	public float leftBoundX;

	[SerializeField]
	public float rightBoundX;

	[SerializeField]
	public float topBoundY;

	[SerializeField]
	public float bottomBoundY;

	[SerializeField]
	private float height = -10.0f;

	[SerializeField]
	private float followSpeed = 2.0f;

	private float stepWidth;

	private void FixedUpdate()
	{
		ClampedDampedMove();
	}

	private void ClampedDampedMove()
	{
		stepWidth = followSpeed * Time.deltaTime *
			Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, target.position.y));

		transform.position = Vector3.MoveTowards(transform.position, target.position, stepWidth);

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBoundX, rightBoundX), 
										 Mathf.Clamp(transform.position.y, bottomBoundY, topBoundY),
										 height);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		//Draw boundry lines in editor
		Gizmos.DrawLine(new Vector2(leftBoundX, topBoundY), new Vector2(rightBoundX, topBoundY));
		Gizmos.DrawLine(new Vector2(rightBoundX, topBoundY), new Vector2(rightBoundX, bottomBoundY));
		Gizmos.DrawLine(new Vector2(rightBoundX, bottomBoundY), new Vector2(leftBoundX, bottomBoundY));
		Gizmos.DrawLine(new Vector2(leftBoundX, bottomBoundY), new Vector2(leftBoundX, topBoundY));
	}
}
