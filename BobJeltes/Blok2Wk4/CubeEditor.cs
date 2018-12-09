using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cube))]
public class CubeEditor : Editor {
    private float baseSizePrevious;

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        Cube cube = (Cube)target;

        GUILayout.Label(
            "Oscillates around a base size"
            );
        
        // This allows the designer to change the size of the cube both inside and outside of playmode
        // It also makes sure that the cube doesn't glitch between the editor size and the simulated size (old version did this)
        // This is done by preventing the editor from applying the baseSize value every frame, and only allowing it to do so when it is changed
        cube.baseSize = EditorGUILayout.Slider("Size", cube.baseSize, .1f, 2f);
        if (baseSizePrevious != cube.baseSize) {
            cube.transform.localScale = Vector3.one * cube.baseSize;
            baseSizePrevious = cube.baseSize;
        }

        GUILayout.Space(4);

        GUILayout.BeginVertical("box");

        GUILayout.Label(
            "Pick a random color or reset to white"
            );

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Color")) {
            cube.GenerateColor();
            Debug.Log("Generated new color");
        }
        if (GUILayout.Button("Reset")) {
            cube.Reset();
            Debug.Log("Reset color to white");
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
}