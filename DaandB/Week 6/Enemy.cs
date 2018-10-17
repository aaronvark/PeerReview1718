using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public static Enemy instance = null;

	public Rigidbody2D rigBod;

	public float minSpeed = 1;
	public float maxSpeed = 5;

	public float speed;

	// SetSpeed sets the speed variable and assigns the object's rigidbody to the rigBod variable.
	public void SetSpeed () {
		rigBod = GetComponent<Rigidbody2D>();

		speed = Random.Range(minSpeed, maxSpeed);
	}
	
	// Move moves the object's rigidbody.
	public virtual void Move () {
		Vector2 forward = new Vector2(transform.right.x, transform.right.y);

		rigBod.MovePosition(rigBod.position + forward * Time.fixedDeltaTime * speed);
		
	}
}
