using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    
    private int amountEnemies;
    public float maxEnemies;

    public int deadEnemies;
    private int roundNum = 1;

    WaveManager waveManager;

	void Awake () {
        // Event Listners
        SpawnEvent.SendSpawned += EnemyCounter;
        Sergant.SendDeath += DeathCounter;
    }

    private void Start() {
        waveManager = GetComponent <WaveManager>();
        StartCoroutine(WaitForNextRound());
    }

    IEnumerator WaitForNextRound() {
        yield return new WaitForSeconds(5);
        StartSpawn();
    }

    // Update is called once per frame
    void Update () {
        if ( amountEnemies >= maxEnemies ) {
            waveManager.canSpawn = false;
            print(waveManager.canSpawn);
        }

        if ( deadEnemies == maxEnemies ) {
            roundNum += 1;
            amountEnemies = 0;
            deadEnemies = 0;

            if (waveManager.cooldown > 0.1f ) {
                waveManager.cooldown -= 0.1f;
            }

            if (roundNum % 3 == 0 ) {
                waveManager.Enemies += 1;
            }

            maxEnemies += 6 * roundNum;

            StartCoroutine(WaitForNextRound());
        }
    }

    void StartSpawn() {
        waveManager.canSpawn = true;
    }

    // Keeps track of the amount of enemies in the game. (used to keep it under a centain amount)
    void EnemyCounter(int amount) {
        amountEnemies += amount;
        print(amountEnemies);
    }

    void DeathCounter(int amount) {
        deadEnemies += amount;
        print(deadEnemies);
    }


}
