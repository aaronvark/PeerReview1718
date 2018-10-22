using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitPlayer : MonoBehaviour {

    public delegate void PlayerHit(int hitDamage);
    public static event PlayerHit SendHit;

    public int damage;

    private void OnTriggerEnter(Collider other) {
        if(SendHit != null ) {
            if(other.tag == "Player" ) {
                SendHit(damage);
            }
        }
    }
}
