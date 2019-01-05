using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor {
    private ReorderableList list;

    private void OnEnable(){
        //draws the thing
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("Waves"), true, true, true, true);
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Type"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("enemyFormation"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("formationEnemyCount"), GUIContent.none);
        };
        //header name
        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Enemy Waves");
        };

        list.onSelectCallback = (ReorderableList l) => {
            var prefab = l.serializedProperty.GetArrayElementAtIndex(l.index).FindPropertyRelative("enemyFormation").objectReferenceValue as GameObject;
            if (prefab) EditorGUIUtility.PingObject(prefab.gameObject);
        };

        list.onCanRemoveCallback = (ReorderableList l) => {
            return l.count > 1;
        };
        //remove wave
        list.onRemoveCallback = (ReorderableList l) => {
            if (EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete the wave?", "Yes", "No"))
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(l);
            }
        };
        //add wave
        list.onAddCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("Type").enumValueIndex = 0;
            element.FindPropertyRelative("formationEnemyCount").intValue = 20;
            element.FindPropertyRelative("enemyFormation").objectReferenceValue = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Formations/E_CircleFormation.prefab", typeof(GameObject)) as GameObject;
        };

        list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {
            var menu = new GenericMenu();
            //create the submenus for dropdown
           
            //Circle
            var guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Circle" });
            foreach (var guid in guids){
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Circle/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Formations, Path = path });
            }
           
            //Hexagon
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Hexagon" });
            foreach (var guid in guids){
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Hexagon/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Formations, Path = path });
            }
            
            //Pentagon
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Pentagon" });
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Pentagon/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Formations, Path = path });
            }
            
            // Square
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Square" });
            foreach (var guid in guids){
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Square/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Formations, Path = path });
            }
           
            //Triangle
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Triangle" });
            foreach (var guid in guids){
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Triangle/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Formations, Path = path });
            }

            menu.ShowAsContext();
        };
    }

    public override void OnInspectorGUI(){
        //base.OnInspectorGUI();

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        GameController gameControl = (GameController)target;

        GUILayout.Label("amount of time to start spawning waves:");
        gameControl.startWaitTime = EditorGUILayout.Slider(gameControl.startWaitTime, 1, 10);

        GUILayout.Label("time inbetween spawning waves:");
        gameControl.spawnWaitTime = EditorGUILayout.Slider(gameControl.spawnWaitTime,1,10);


    }

    private void clickHandler(object target){
        var data = (WaveCreationParams)target;
        var index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        element.FindPropertyRelative("Type").enumValueIndex = (int)data.Type;
        element.FindPropertyRelative("formationEnemyCount").intValue = data.Type == MobWave.WaveType.Formations ? 1 : 20;
        element.FindPropertyRelative("enemyFormation").objectReferenceValue = AssetDatabase.LoadAssetAtPath(data.Path, typeof(GameObject)) as GameObject;
        serializedObject.ApplyModifiedProperties();
    }

    private struct WaveCreationParams{
        public MobWave.WaveType Type;
        public string Path;
    }

}
