using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractForce : MonoBehaviour {

    public static List<AttractForce> Attractors;

    const float G = 1.6674f;

    [SerializeField]
    private Rigidbody2D rb;


    //Searches for attractors in list
    private void FixedUpdate() {
        AttractForce[] attractors = FindObjectsOfType<AttractForce>();
        foreach (AttractForce attractor in attractors) {
            if (attractor != this)
                Atrract(attractor);
        }
    }


    void Atrract(AttractForce objToAttract) {
        Rigidbody2D rbToAttract = objToAttract.rb;

        Vector2 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0f)
            return;
        //Newtons law of gravity
        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector2 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
