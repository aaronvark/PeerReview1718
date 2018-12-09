using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Spawner : EditorWindow {

    public string t;
    private Minion otherMin;

    [MenuItem("Tools/Spawner")]
	static void Create()
    {
        GetWindow<Spawner>();
    }

    private void OnGUI()
    {
        t = EditorGUILayout.TextField(t);
        if (GUILayout.Button("spawn"))
        {
            otherMin = CreateInstance<Minion>();
            otherMin.Show();
        }
    }
}

public class Minion : EditorWindow
{
    public string t;

    static void Create()
    {
        GetWindow<Spawner>();
    }

    private void OnGUI()
    {
        t = EditorGUILayout.TextField(t);
    }
}
