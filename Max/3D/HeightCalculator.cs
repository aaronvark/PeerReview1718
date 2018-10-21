using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Version3D {
    public class HeightCalculator : MonoBehaviour {
        private List<Transform> positions = new List<Transform>();
        private float averageResult;
        public List<float> distances = new List<float>();
        List<int> duplcates = new List<int>();

        public delegate void HeightCheck(float height, int amount);
        public static event HeightCheck AfterHeightCheck;

        private void Awake() {
            foreach (Transform child in transform) {
                positions.Add(child);
            }
            PlayerController.StartTestHeight += TestHeight;  //Subscribe to <- PlayerController StartTestHeight
        }



        private void TestHeight() {
            distances.Clear();
            foreach (Transform pos in positions) {
                Ray downRay = new Ray(pos.transform.position, Vector3.down);
                RaycastHit[] allHits;
                List<float> localDistances = new List<float>();
                allHits = Physics.RaycastAll(downRay, 40);

                foreach (RaycastHit bong in allHits) {
                    Debug.DrawLine(pos.transform.position, bong.point, Color.red);
                    if (bong.transform.tag == "Cube") {
                        localDistances.Add(bong.distance);
                    }
                }
                if (positions.Count > 1 && localDistances.Count > 1) {
                    distances.Add(localDistances.Min());
                }
            }

            
            List<float> places = new List<float>();
            duplcates.Clear();
            int i = 0;
            foreach (float dis in distances) {               
                duplcates.Add(0);
                places.Add(dis);
                foreach(float disX in distances) {
                    if (disX == dis) {
                        duplcates[i] += 1;
                    }
                }
                i++;
            }
            int index = duplcates.IndexOf(duplcates.Max());
            AfterHeightCheck(places[index], duplcates.Max());
        }
    }
}

