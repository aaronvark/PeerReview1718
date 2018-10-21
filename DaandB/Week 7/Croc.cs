using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Croc : Enemy {

	public override void Move(){ base.Move(); }

	IEnumerator OnTriggerStay2D(Collider2D other){
		if (other.tag == "WaterTile"){
			other.enabled = false;
//			Debug.Log("Destroyed");
			yield return new WaitForSeconds(1f);
			other.enabled = true;
		} 
		if (other.tag == "Player"){
			PlayerDeath.DeathBy(this);
		}
	}
}
