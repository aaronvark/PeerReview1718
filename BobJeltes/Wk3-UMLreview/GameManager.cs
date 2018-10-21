using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pepijn {

    public class GameManager : MonoBehaviour {
        public TetrisChecker TetrisChecker { get; set; }
        public Block Block { get; set; }

        public GameObject RowPrefab { get; set; }
        public GameObject Roof { get; set; }
        public GameObject RightWall { get; set; }
        public GameObject LeftWall { get; set; }
        public GameObject BackWall { get; set; }
        public GameObject Ground { get; set; }
        public GameObject LatestRow { get; set; }

        public bool BlockIsFallin { get; set; }


        private void Start() {
            
        }


        private void Update() {

        }


        public void CreateBlock() {
            
        }


        public void Tetris() {

        }


    }
}