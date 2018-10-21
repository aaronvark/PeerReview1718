using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy {

	[SerializeField]
	public GameObject venom;

	private float fireRate;
	private float nextFire;

	// Use this for initialization
	void Start () {
		fireRate = 0.75f;
		nextFire = Time.time;
	}

	void Update(){
		CheckFireStatus();
	}

	public void SetSpeed(){ base.SetSpeed(); }

	public override void Move(){ base.Move(); }

	void CheckFireStatus(){
		if (Time.time > nextFire){
			Instantiate(venom, transform.position, Quaternion.identity);
			nextFire = Time.time + fireRate;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			PlayerDeath.DeathBy(this);
		} else if (other.tag == "Snake") return;
	}
}
