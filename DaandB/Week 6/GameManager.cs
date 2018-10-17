using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Croc croc;
	public Log log;
	public Car car;
	public Snake snake;

	void Start(){
		croc.SetSpeed();
		log.SetSpeed();
		car.SetSpeed();
		snake.SetSpeed();
	}

	void Update(){	
		croc.Move();	
		log.Move();
		car.Move();
		snake.Move();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			SceneManager.LoadScene(Random.Range(1, 3));
		} else return;
	}
}
