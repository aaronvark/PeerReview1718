using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D map;
    public ColorToPrefab[] colorMappings;

	void Start () {
        GenerateLevel();
	}

    void GenerateLevel() {

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x,y);
            }
        }
    }

    void GenerateTile(int x, int y) {
        Color pixelColor = map.GetPixel(x,y);

        if (pixelColor.a == 0)
        {
            return;
        }
        Debug.Log(pixelColor);
        foreach (ColorToPrefab colorMapping in colorMappings) {

            if (colorMapping.color.Equals(pixelColor)) {
                Vector2 position = new Vector2(x,y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }

    public void SaveLevel()
    {
        SaveSystem.SavePlayer(this);
        //Debug.Log(this);
    }

    public void LoadLevel()
    {
        LevelData data = SaveSystem.LoadData();

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        transform.position = position;
    }
	
	
}
