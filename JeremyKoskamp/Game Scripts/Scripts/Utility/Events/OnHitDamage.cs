using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDamage : MonoBehaviour {

    public delegate void EnemyHit(GameObject other, int hitDamage);
    public static event EnemyHit SendHit;

    public int damage;

    private void OnTriggerEnter(Collider other) {
        if ( other.tag != ("gun") ) {
            if ( other.tag != "Player" ) {
                if ( SendHit != null ) {
                    SendHit(other.gameObject, damage);
                }
            }
        }
    }
}
