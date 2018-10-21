using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Version3D {
    public class FollowPlayerController : MonoBehaviour {
        GameObject player;
        public float minAmount;
        public Camera cam;
        private float divider = 1;
        private float rotation = 0;
        // Use this for initialization
        void Start() {
            PlayerController.OnRotateCamera += RotateAll;
            player = PlayerController.Instance.gameObject;

        }

        // Update is called once per frame
        void Update() {
            this.transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player.transform.position.y, 0.1f) + minAmount, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(transform.eulerAngles.y, rotation, 0.25f), this.transform.eulerAngles.z);
            if (cam != null) {                
                cam.orthographicSize = Mathf.Lerp(transform.position.y, player.transform.position.y, 0.1f) + minAmount -1;
            }
        }

        private void RotateAll(float rotationY) {
            
            if(rotation + rotationY == 360) {
                rotation = 0;
            }
            else {
                rotation += rotationY;
            }
            print(rotation);
        }
    }
}
