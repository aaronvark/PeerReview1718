using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public delegate void SpawnerObject(GameObject spawnerObject);
    public static event SpawnerObject SendSpawner;

    // Use this for initialization
    private void Start () {
        if ( SendSpawner != null ) {
            SendSpawner(gameObject);
            print("fokkin common");
        }
    }
}
