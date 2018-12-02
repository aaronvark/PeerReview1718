 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBlock : Block {


    private void FixedUpdate() {

    }


    public void GameEndLock() {
        base.GameEndLock();
    }


    private void DestroySelf() {

    }


    private void OnCollisionEnter(Collider other) {
        
    }


    private void LockInPlace() {

    }


    public int GetScore() {
        return value;
    }
}
