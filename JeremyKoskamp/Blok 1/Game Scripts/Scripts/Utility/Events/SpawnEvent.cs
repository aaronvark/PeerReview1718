using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvent : MonoBehaviour {

    public delegate void Spawned(int Spawned);
    public static event Spawned SendSpawned;

    // Update is called once per frame
    void Awake () {
        if ( SendSpawned != null ) {
            SendSpawned(1);
        }
    }
}
