using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public Croc croc;
	public Croc crocLog;
	public Log log;
	public Car car;
	public Snake snake;

	void Update(){	
		for (int i = 0; i < EnemySpawner.spawnList.Count; i++){
			Enemy temp = EnemySpawner.spawnList[i];
			if (temp != null){
				temp.Move();
			}
		}				// Move for crocodiles and logs if the current scene is the "lake" level.
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			SceneManager.LoadScene(Random.Range(1, 4));
		} else return;
	}
}
