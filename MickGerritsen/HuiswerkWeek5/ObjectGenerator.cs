using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {
    [SerializeField]
    private int amount;
    [SerializeField]
    private GameObject[] GameObjects;
    
    //The team can choose how many and what kinds of game objects need to be instantiated in scene
    void Start () {
        for (int i = 0; i < amount; i++)
		foreach(GameObject gameObject in GameObjects) {
            Instantiate(gameObject, transform.position, Quaternion.identity);
        }
	}
}
