using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlock : Block {

    public int speed { get; set; }


    private void FixedUpdate() {
        
    }


    public void GameEndLock() {
        base.GameEndLock();
    }


    private void DestroySelf() {
        
    }


    public int GetScore() {
        return value;
    }
}
