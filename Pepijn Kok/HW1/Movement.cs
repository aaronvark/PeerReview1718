using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float speed {
		get; set = 1f;
	}
	public float camSpeed {
		get; set = 1.0f;
	}
	private float z = 0;
	private float x = 0;
	private float xCam = 0;
	private Vector3 eulerAngleVelocity;


	private void Update () {

		z = Input.GetAxis("Vertical") * speed;
		x = Input.GetAxis("Horizontal") * speed;
		xCam = Input.GetAxis("CameraHorizontal") * camSpeed;

		GetComponent<Rigidbody>().AddForce(this.transform.forward * z);
		GetComponent<Rigidbody>().AddForce(this.transform.right * x);

		eulerAngleVelocity = new Vector3(0, xCam, 0);
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
	}
}
