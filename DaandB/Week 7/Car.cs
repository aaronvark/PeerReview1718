using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Enemy {
	
	void Awake(){
		StartCoroutine(CarRot());
	}

	public override void Move(){ base.Move(); }

	private IEnumerator CarRot(){
		yield return new WaitForSeconds(0.25f);
		transform.Rotate(0f, 0f, Random.Range(-15f, 15f));
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			PlayerDeath.DeathBy(this);
		} else if (other.tag == "Car") transform.Rotate(0f, 0f, Random.Range(-30, 30));
	}
} 
