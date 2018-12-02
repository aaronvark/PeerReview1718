using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    private Transform playerCamera;
    public Transform followTarget;

    public float offset;
    float zPos;
    float xPos;
    float yPos;

    // Use this for initialization
    void Start () {
        playerCamera = this.transform;
        xPos = playerCamera.transform.position.x;
        zPos = playerCamera.transform.position.z;
        yPos = playerCamera.transform.position.y;

    }
	
	// Update is called once per frame
	void Update () {
        playerCamera.position = new Vector3(followTarget.position.x, yPos + offset, followTarget.position.z + zPos);
    }
}
