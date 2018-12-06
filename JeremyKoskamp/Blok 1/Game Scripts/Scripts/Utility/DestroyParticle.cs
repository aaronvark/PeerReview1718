using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {
    public float wait;
    ParticleSystem pr;


    void Start() {
        pr = GetComponent<ParticleSystem>();
    }


    private void Update() {
        if(gameObject.transform.parent == null ) {
            StartCoroutine(Wait());
        }
    }


    IEnumerator Wait() {
        yield return new WaitForSeconds(wait);
        End();
    }


    void End() {
        Destroy(gameObject);
    }
}
