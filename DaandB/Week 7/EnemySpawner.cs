using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner instance = null;

	public Enemy car, snake, croc, log, crocLog;
	public static List<Enemy> spawnList = new List<Enemy>();

	[SerializeField]
	private float spawnRate = 1.5f;

	void Start(){
		Scene currentScene = SceneManager.GetActiveScene();
		int buildIndex = currentScene.buildIndex;

		switch(buildIndex){								// This spawns enemies depending on the current scene's index.
			case 1:
				StartCoroutine(CarSpawner());			// Spawns cars if the current scene is the "road" level.
				break;

			case 2:
				StartCoroutine(SnakeSpawner());			// Spawns snakes if the current scene is the "forest" level.
				break;
			
			case 3:
				StartCoroutine(CrocLogSpawner());		// Spawns crocodiles and logs if the current scene is the "lake" level.
				break;
		}
	}
	
	private IEnumerator CarSpawner(){													// --- CARS ---
		while (true){
			Enemy temp;
			temp = Instantiate(car, transform.position, transform.rotation) as Enemy;
			spawnList.Add(temp);
			temp.SetSpeed();
			yield return new WaitForSeconds(1f);										// Every second a new car is spawned...
			Destroy(temp, 2);															// ...and destroyed after 2 seconds.
		}
	}

	private IEnumerator SnakeSpawner(){													// --- SNAKES --- 
		while (true){
			Enemy temp;
			temp = Instantiate(snake, transform.position, transform.rotation) as Enemy;
			spawnList.Add(temp);
			temp.SetSpeed();
			yield return new WaitForSeconds(5f);										// Every 5 seconds a new snake is spawned (because they move slower and fire projectiles)...
			Destroy(temp, 6);															// ...and destroyed after 6 seconds.
		}
	}

	private IEnumerator CrocLogSpawner(){												// --- CROCS 'N LOGS ---
		while (true){
			Enemy temp;
			int crocOrLog = Random.Range(1, 21);											// Wether a croc, a log or a crocLog is being spawned is chosen randomly.

			if (crocOrLog > 0 && crocOrLog < 7){														// Spawns a croc...
				temp = Instantiate(croc, transform.position, transform.rotation) as Enemy;
				spawnList.Add(temp);
				temp.SetSpeed();
				//Destroy(temp, 1f);

			}
			else if (crocOrLog > 7 && crocOrLog < 18){													// ...or a log...
				temp = Instantiate(log, transform.position, transform.rotation) as Enemy;
				spawnList.Add(temp);
				temp.SetSpeed();
				//Destroy(temp, 1f);
			}
			else if (crocOrLog > 18 && crocOrLog < 21){													// ...or a crocodile in disguise!
				temp = Instantiate(crocLog, transform.position, transform.rotation) as Enemy;
				spawnList.Add(temp);
				temp.SetSpeed();
				//Destroy(temp, 1f);
			}
			yield return new WaitForSeconds(spawnRate);
		}
	}
}
