using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    private int scoreSize { get; set; }
    private Rigidbody2D rigid;
    public Sprite[] sprite;
    protected List<GameObject> localSprites;


    private void FixedUpdate() {

    }


    public void GameEndLock() {

    }


    private void DestroySelf() {

    }


    public int GetScore() {
        return value;
    }
}
