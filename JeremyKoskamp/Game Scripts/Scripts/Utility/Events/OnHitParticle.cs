using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitParticle : MonoBehaviour {

    public GameObject hitParticle;
    public Transform particleSpawn;

    void Start () {
    }

    private void OnTriggerEnter(Collider other) {
        if ( other.tag != ("gun") ) {
            if ( other.tag != "Player" ) {
                particleSpawn = gameObject.transform;
                Instantiate(hitParticle, particleSpawn.position, particleSpawn.rotation, hitParticle.transform.parent = null);
            }      
        }
    }
}
