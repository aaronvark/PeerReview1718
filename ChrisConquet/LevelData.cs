using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public float[] position;

    public LevelData(LevelGenerator cords)
    {

        position = new float[2];
        position[0] = cords.transform.position.x;
        position[1] = cords.transform.position.y;
    }

}
