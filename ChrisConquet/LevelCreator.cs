using UnityEngine;
using UnityEditor;

public class LevelCreator : EditorWindow {

    public Texture2D map;
    //public ColorToPrefab[] colorMappings;
    public bool Kill;
    public Color color;

    public GameObject Generator;
    public GameObject Object1;

    [MenuItem("Window/LevelGenerator")]
    public static void ShowWindow()
    {
        GetWindow<LevelCreator>("LevelGenerator");
    }

    void OnGUI()
    {
        //Fortnite GUI Button Skins
        var style = new GUIStyle(GUI.skin.button);
        style.hover.textColor = Color.red;
        style.normal.textColor = Color.red;
        style.active.textColor = Color.green;
        //style.fontSize = 18;


        //window code

        GUILayout.Space(20);
        GUILayout.Label("Please select your map", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Map");
        map = (Texture2D)EditorGUILayout.ObjectField(map, typeof(Texture2D),true);
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.Label("Now insert the spawner", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("LevelSpawn");
        Generator = (GameObject)EditorGUILayout.ObjectField(Generator, typeof(GameObject), true);
        GUILayout.EndHorizontal();


        GUILayout.Space(20);
        GUILayout.Label("Define your prefab color", EditorStyles.boldLabel);
        color = EditorGUILayout.ColorField("Color", color);
        GUILayout.Space(20);


        GUILayout.Label("Last but not least, insert your prefab", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        Object1 = (GameObject)EditorGUILayout.ObjectField(Object1, typeof(GameObject), false);
        GUILayout.EndHorizontal();

        GUILayout.Space(100);

        // Generate Level Button
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Level", GUILayout.Width(100)))
        {
            GenerateLevel();
            if (map == null)
            {
                ShowNotification(new GUIContent("No Map selected"));
            }
            else if (Help.HasHelpForObject(map))
            {
                Help.ShowHelpForObject(map);
            }
        }

        //GUILayout.Space(10);
        // Delete Level Button
        if (GUILayout.Button("Kill the kids", style, GUILayout.Width(100)))
        {
            Wipe();
        }
        GUILayout.EndHorizontal();
    }



    void GenerateLevel()
    {
            for (int x = 0; x < map.width; x++)
            {
                for (int y = 0; y < map.height; y++)
                {
                    GenerateTile(x, y);
                }
            }
    }

    public void KillButton()
    {
        Kill = false;
    }

    void Update()
    {
        if (Kill == true)
        {
            Wipe();

        }
    }


    [ContextMenu("Kill Kids")]
    public void Wipe()
    {
        int childs = Generator.transform.childCount;
        for (int i = childs - 1; i >=0; i--)
        {
            GameObject.DestroyImmediate(Generator.transform.GetChild(i).gameObject);
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            return;
        }
            if (color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                //Instantiate(Object1, position, Quaternion.identity);
                PrefabUtility.InstantiatePrefab(Generator);
                GameObject temp = Instantiate(Object1, position, Quaternion.identity);
                temp.transform.parent = Generator.transform;
                //Generator = Instantiate(colorMapping.prefab, position, Quaternion.identity) as GameObject;
                //Generator.transform.parent = transform;
            }
    }






}
