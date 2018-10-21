using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public List<Transform> spawners;

    private int amountEnemies;
    public int Enemies;
    public int maxEnemies;

    public float cooldown;
    private float spawnCooldown;

    private bool canSpawn;


    public GameObject sergant;


	void Awake () {
        // Event Listners
        SpawnEvent.SendSpawned += EnemyCounter;
        Spawner.SendSpawner += AddSpawners;
    }

    private void Start() {
        spawnCooldown = cooldown;
        StartCoroutine(Wait());

    }


    IEnumerator Wait() {
        print(Time.time);
        yield return new WaitForSeconds(2f);
        canSpawn = true;
    }


    // Update is called once per frame
    void Update () {
        if ( canSpawn ) {
            SpawnEnemys();

            if (spawnCooldown > 0 ) {
                spawnCooldown -= Time.deltaTime;
            }
            else {
                spawnCooldown = cooldown;
            }
        }
	}

    // Keeps track of the amount of enemies in the game. (used to keep it under a centain amount)
    void EnemyCounter(int amount) {
        amountEnemies += amount;
        print(amountEnemies);
    }

    // Adds spawners to spawner list for control.
    void AddSpawners(GameObject spawnerObject) {
        spawners.Add(spawnerObject.transform);
        print(spawners);
    }


    void SpawnEnemys() {

        for (int i = 0; i < Enemies; i++ ) {
            if ( spawnCooldown < 0 ) {
                int spawnPointIndex = Random.Range(0, spawners.Capacity - 1);
                Instantiate(sergant, spawners[spawnPointIndex].transform);
            }
        }
    }
}
