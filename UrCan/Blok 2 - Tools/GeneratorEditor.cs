using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
[CustomEditor(typeof(Generator))]
public class GeneratorEditor : Editor {
	private int mode;

	public static string FILE_TYPE = "bruut";
	private	string path = "", buttonName = "Load Directory";
	private List<string> files = new List<string>();

	private Texture folderIcon;

	public override void OnInspectorGUI() {
		Generator tar = (Generator)target;

		//Load Icons
		folderIcon = Resources.Load("Folder") as Texture;
		GUIContent content = new GUIContent();
		content.image = (Texture2D)folderIcon;
		content.tooltip = "Load a main directory folder where all the ."+FILE_TYPE+" files are located.";
		GUIStyle style = new GUIStyle(GUI.skin.button);
		style.imagePosition = ImagePosition.ImageLeft;
		//style.font = Resources.Load("BM") as Font;
		style.fixedHeight = style.fixedWidth = 32;

		//File loading
		GUILayout.BeginVertical("Box");
		GUILayout.Label("File Loading", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal("");
		Color col = new Color();
		ColorUtility.TryParseHtmlString("#eaf0ce", out col);
		GUI.backgroundColor = col;
		if(GUILayout.Button(content,style)) 
		{
			path = EditorUtility.OpenFolderPanel("Select ."+FILE_TYPE+" directory", "", FILE_TYPE);
			DirectoryInfo dir = new DirectoryInfo(path);
			FileInfo[] info = dir.GetFiles("*."+FILE_TYPE);
			foreach(FileInfo f in info) files.Add(f.Name);
		}
		GUI.backgroundColor = Color.white;

		if(path.Length <= 1) buttonName = "Load Directory";
		else buttonName = "File loaded: \n" + Simplify(path);
		
		GUILayout.TextField(buttonName, GUILayout.Width(204));
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		//GameType
		GUILayout.Space(10);
		GUILayout.BeginVertical("Box");
		GUILayout.Label("Game Type", EditorStyles.boldLabel);
		string[] names = new string[Generator.AMOUNT_GAMEMODES()];
		for(int i = 0; i < names.Length; i++) names[i] = ((Generator.GameType[])System.Enum.GetValues(typeof(Generator.GameType)))[i].ToString().Replace('_', ' ');

		mode = GUILayout.SelectionGrid(mode, names, 2);
		GUILayout.EndVertical();

		GUILayout.Space(20);

		if(GUILayout.Button("Generate Level")) tar.Generate();

		GUILayout.Space(5);
	}

	public string Simplify(string path) {
		string[] splits = path.Split('/');
		string fin = "";
		System.Array.Reverse(splits);
		for(int i = splits.Length - 1; i > 4; i--) fin += splits[i] + "/";
		return fin;
	}
}
