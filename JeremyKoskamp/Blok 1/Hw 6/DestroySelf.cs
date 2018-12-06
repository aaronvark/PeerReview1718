using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

    private ParticleSystem pr;

    private void Start() {
        pr = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        if ( other.tag != ("gun") ) {
            if ( other.tag != ("Player") ) { 
                pr.transform.parent = null;
                Destroy(gameObject);
            }
        }
    }
}
