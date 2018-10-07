using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomizer : MonoBehaviour {
    private Rigidbody rb;
    private float randomX;
    private float randomY;
    private float randomZ;
    [SerializeField]
    private float spawnBoxSize;
    [SerializeField]
    private float maxSpeed;

    //Setting things up at the start of the spawn
    void Start() {
        rb = GetComponent<Rigidbody>();
        Vector3 rndPosWithin;
        rndPosWithin = new Vector3(Random.Range(-spawnBoxSize, spawnBoxSize), Random.Range(-spawnBoxSize, spawnBoxSize), Random.Range(-spawnBoxSize, spawnBoxSize));
        rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
        transform.position = rndPosWithin;
        randomX = Random.Range(-maxSpeed, maxSpeed);
        randomY = Random.Range(-maxSpeed, maxSpeed);
        randomZ = Random.Range(-maxSpeed, maxSpeed);
    }
    private void FixedUpdate() {

        rb.velocity = new Vector3(randomX, randomY, randomZ);
        //To let the GameObject stay in a box, I want them to return on the other side of it
        if (transform.position.x < -spawnBoxSize / 2 || transform.position.x > spawnBoxSize / 2) {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -spawnBoxSize / 2 || transform.position.y > spawnBoxSize / 2) {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
        if (transform.position.z < -spawnBoxSize / 2 || transform.position.z > spawnBoxSize / 2) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z);
        }
    }
}
