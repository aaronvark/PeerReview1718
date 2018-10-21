using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pepijn {

    public class Block : MonoBehaviour {
        public GameObject CurrentBlock { get; set; }
        public GameObject TetrisBlock { get; set; }


        public void DropBlock() {
            
        }


        public void PlaceBlock() {
            
        }


        public IEnumerator FallingBlock() {
            yield return new WaitForSeconds(0);
        }


    }
}