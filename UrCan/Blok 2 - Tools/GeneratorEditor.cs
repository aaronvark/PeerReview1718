using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Collections;

[CustomEditor(typeof(Generator))]
public class GeneratorEditor : Editor {
	private	string path = "", buttonName = "Load Directory";
	private List<string> files = new List<string>();

	public static string FILE_TYPE = "bruut";
	public static string TOOL_NAME = "AnyWalker";

	private Texture folderIcon;

	private bool hasDirectory = false;
	private DirectoryInfo currentDir;

	[System.Serializable]
	public struct SaveData {
		public int mode;
		public string dir;
		public string file;
	}
	private SaveData save;
	private string  pathJSON;
	private int lastMode = 0;
	private string lastFile = "";

	private Vector2 gameTypeScroll;

	private Preview preview;

	public 	static Color col_egg = new Color(), col_black = new Color(), col_semiblack = new Color();

	private void LoadSettings() {
		pathJSON = Application.persistentDataPath + "/settings";
		Debug.LogWarning("JSON file generated at: " + pathJSON);
		string content = File.ReadAllText(pathJSON);
		save = JsonUtility.FromJson<SaveData>(content);
		path = save.dir;
		lastMode = save.mode;
		lastFile = save.file;
		currentDir = new DirectoryInfo(path);
		hasDirectory = true;
	}

	private void SaveSettings() {
		save.dir = path;
		string content = JsonUtility.ToJson(save, true);
	    File.WriteAllText(pathJSON, content);
	}

	void OnEnable() {
		LoadSettings();
	}

	public string GetCurrentPath() {
		return path + "/" + save.file;
	}

	public override void OnInspectorGUI() {
		Generator tar = (Generator)target;
		
		//EditorGUI.ProgressBar(Rect, value:, label);

		//Colors
		ColorUtility.TryParseHtmlString("#eaf0ce", out col_egg);
		ColorUtility.TryParseHtmlString("#333232", out col_black);
		ColorUtility.TryParseHtmlString("#474647", out col_semiblack);

		//Styling
		GUIContent content = new GUIContent();
		GUIStyle style = new GUIStyle(GUI.skin.button), labelstyle = EditorStyles.boldLabel, generateButton = new GUIStyle(GUI.skin.button), elementBtn = new GUIStyle(GUI.skin.button);
		style.imagePosition = ImagePosition.ImageLeft;
		style.fixedHeight = style.fixedWidth = 32;
		elementBtn.fontStyle = FontStyle.Bold;
		elementBtn.onActive.textColor = col_egg;
		elementBtn.onNormal.textColor = col_egg;
		elementBtn.active.textColor = col_semiblack;
		generateButton.active.textColor = col_black;
		generateButton.normal.textColor = col_semiblack;
		generateButton.onHover.textColor = col_black;
		generateButton.fontStyle = FontStyle.Bold;
		generateButton.padding = new RectOffset(10, 10, 10, 5);
		labelstyle.normal.textColor = col_egg;

		//Load Icons
		folderIcon = Resources.Load(TOOL_NAME + "/Folder") as Texture;
		content.image = (Texture2D)folderIcon;
		content.tooltip = "Load a main directory folder where all the ."+FILE_TYPE+" files are located.";

		//File loading
		GUI.backgroundColor = col_semiblack;
		GUILayout.BeginVertical("Box");
		GUILayout.Label("File Loading", labelstyle);
		GUI.backgroundColor = col_black;
		GUILayout.BeginHorizontal("box");
		GUI.backgroundColor = col_egg;
		if(GUILayout.Button(content,style)) 
		{
			path = EditorUtility.OpenFolderPanel("Select ."+FILE_TYPE+" directory", "", FILE_TYPE);
			currentDir = new DirectoryInfo(path);
			SaveSettings();
			FileInfo[] info = currentDir.GetFiles("*."+FILE_TYPE);
			foreach(FileInfo f in info) files.Add(f.Name);
		}
		GUI.backgroundColor = Color.white;

		if(path.Length <= 1) {
			buttonName = "Load Directory\n <--";
			hasDirectory = false;
		} else {
			buttonName = "Directory loaded: \n" + Simplify(path);
			hasDirectory = true;
		}
		GUILayout.Box(buttonName, labelstyle);

		GUI.backgroundColor = col_egg;
		if(GUILayout.Button("Reset", elementBtn, GUILayout.Width(50))) {
			path = "";
			currentDir = null;
			hasDirectory = false;
		}
		GUILayout.EndHorizontal();
		
		//Open File Panel
		int labelSize = labelstyle.fontSize;
		if(hasDirectory) {
			GUILayout.BeginVertical("Box");
			FileInfo[] files = GetFileList();
			for(int i = 0; i < files.Length; i++) {
				if(save.file == files[i].Name) {
					GUI.backgroundColor = col_semiblack;
					labelstyle.normal.textColor = col_egg;
					labelstyle.fontSize = 12;
				}
				else {
					GUI.backgroundColor = col_egg;
					labelstyle.normal.textColor = col_semiblack;
					labelstyle.fontSize = labelSize;
				}
				GUILayout.BeginHorizontal("box");
				if(GUILayout.Button(files[i].Name, labelstyle)) {
					save.file = files[i].Name;
					ScanFileType();
				}
				GUILayout.EndHorizontal();
			}
			if(save.file != lastFile) SaveSettings();
			GUILayout.EndVertical();
		}
		GUILayout.EndVertical();

		//Reset style vars
		labelstyle.fontSize = labelSize;
		labelstyle.normal.textColor = col_egg;

		//GameType
		if(hasDirectory) {
			GUILayout.Space(10);
			GUI.backgroundColor = col_semiblack;
			GUILayout.BeginVertical("Box");
			GUILayout.Label("Game Type", EditorStyles.boldLabel);
			string[] names = new string[Generator.AMOUNT_GAMEMODES()];
			for(int i = 0; i < names.Length; i++) names[i] = ((Generator.GameType[])System.Enum.GetValues(typeof(Generator.GameType)))[i].ToString().Replace('_', ' ');

			GUI.backgroundColor = col_egg;
			GUIStyle gameTypeBtn = elementBtn;
			gameTypeBtn.imagePosition = ImagePosition.ImageAbove;
			gameTypeBtn.fixedHeight = style.fixedWidth = 120;
			gameTypeBtn.padding = new RectOffset(0, 0, 5, 5);
			
			//IMAGES LOADING
			GUIContent[] contents = new GUIContent[Generator.AMOUNT_GAMEMODES()];
			for(int i = 0; i < contents.Length; i++) {
				contents[i] = new GUIContent();
				contents[i].image = Resources.Load(TOOL_NAME + "/" + names[i].Replace(" ", "")) as Texture;
				contents[i].text = names[i];
			}
			gameTypeScroll = EditorGUILayout.BeginScrollView(gameTypeScroll, true, false);
			save.mode = GUILayout.SelectionGrid(save.mode, contents, names.Length, gameTypeBtn, GUILayout.Width(475));
			EditorGUILayout.EndScrollView();

			GUILayout.EndVertical();
			if(save.mode != lastMode) SaveSettings();
			GUILayout.Space(10);

			//Generate button
			GUI.backgroundColor = col_semiblack;
			GUILayout.BeginVertical("Box");
			GUILayout.Label("Preview", EditorStyles.boldLabel);
			GUI.backgroundColor = col_egg;
			if(GUILayout.Button("Generate", generateButton)) {
				if(preview == null && ScanFileType() != null) preview = Preview.Create(new Vector2(800, 620), tar, this);
			}
			GUILayout.EndVertical();
			GUILayout.Space(5);
		}
		labelstyle.normal.textColor = Color.black;
	}

	public FileInfo[] GetFileList() {
		List<FileInfo> files = new List<FileInfo>();
		foreach(FileInfo inf in currentDir.GetFiles()) {
			if(inf.Extension == ".meta") continue;
			files.Add(inf);
		}
		return files.ToArray();
	}

	public IConvertType ScanFileType() {
		DirectoryInfo currentFile = new DirectoryInfo(save.dir + "/" + save.file);
		string extension = currentFile.Extension.Replace('.', ' ').ToLower().Trim();
		try {
			if(IConvert.FILETYPES[extension] != null) return IConvert.FILETYPES[extension];
		}
		catch(KeyNotFoundException){}
		return null;
	}

	public string Simplify(string path) {
		string[] splits = path.Split('/');
		string[] data = Application.dataPath.Split('/');
		string fin = "";
		System.Array.Resize(ref data, 8);
		System.Array.Reverse(splits);
		System.Array.Reverse(data);
		bool lastShorten = false;
		for(int i = splits.Length - 1; i >= 0; i--) {
			bool shorten = false;
			if(splits[i] != "C:") {
			 foreach(string m in data) if(m.Trim() == splits[i].Trim()) {
				 if(!lastShorten) {
					 fin += "../"; 
					 lastShorten = true;
				 }
				 shorten = true; 
				 break;
			 }
			}
			if(!shorten) fin += splits[i] + "/";
		}
		return fin;
	}
}

public class Preview : EditorWindow {
	Editor editor;
	GameObject obj;
	Vector2 size;
	Generator tar;
	GeneratorEditor genEdit;

	private IConvertType settings;
	private object[] values;
	private bool collapse = false;

	public static Preview Create(Vector2 size, Generator target, GeneratorEditor genEdit) {
		Preview preview = EditorWindow.GetWindowWithRect<Preview>(new Rect(Screen.width / 2 - size.x / 2, (Screen.height / 2) - (size.y + 120) / 2, size.x, size.y + 60));
		preview.size = size;
		preview.tar = target;
		preview.genEdit = genEdit;
		preview.settings = genEdit.ScanFileType();

		//Base Material
		Material mat = Resources.Load(GeneratorEditor.TOOL_NAME + "/PreviewCube") as Material;
		mat.color = GeneratorEditor.col_egg;
		preview.tar.InitGenerator(mat);
		preview.obj = preview.settings.Convert(genEdit.GetCurrentPath());

		preview.values = new object[preview.settings.variables.Count];
		for(int i = 0; i < preview.values.Length; i++) preview.values[i] = preview.settings.variables[i].value;
		return preview;
	}

	void OnGUI() {
		GUIStyle generateButton = new GUIStyle(GUI.skin.button), label = EditorStyles.boldLabel;
		generateButton.active.textColor = GeneratorEditor.col_black;
		generateButton.normal.textColor = GeneratorEditor.col_semiblack;
		generateButton.onHover.textColor = GeneratorEditor.col_black;
		generateButton.fontStyle = FontStyle.Bold;
		generateButton.padding = new RectOffset(10, 10, 10, 5);

		label.normal.textColor = GeneratorEditor.col_egg;

		GUIStyle bgCol = new GUIStyle();
		bgCol.normal.background = Texture2D.blackTexture;

		if(obj == null) {
			Debug.LogError("Level could not be generated!");
			return;
		}
		if(editor == null) editor = Editor.CreateEditor(obj);
		editor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(size.x, size.y-30), bgCol);
		
		//Settings
		int settingAmount = settings.variables.Count + 1;
		int generateButtonWidth = 150;
		GUI.backgroundColor = GeneratorEditor.col_semiblack;
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical("Box", GUILayout.Width(size.x - (generateButtonWidth+10)));
		GUILayout.Label("Settings", EditorStyles.boldLabel);
		GUI.backgroundColor = GeneratorEditor.col_egg;
		
		GUIStyle setting = EditorStyles.boldLabel;
		int oldFontSize = setting.fontSize;
		float elementX = (size.x - (generateButtonWidth+10)) / settingAmount, elementY = 30;
		GUILayoutOption elementWidth = GUILayout.Width(elementX), elementHeight = GUILayout.Height(elementY);
		setting.fontSize = 10;
		GUILayout.BeginHorizontal();

		//Individual Settings
		for(int i = 0; i < settings.variables.Count; i++) {
			GUI.backgroundColor = GeneratorEditor.col_black;
			GUILayout.BeginVertical("Box");
			GUI.backgroundColor = GeneratorEditor.col_egg;
			GUILayout.Label(settings.variables[i].name, setting, elementWidth);

			if(values[i].GetType() == typeof(float)) {
				values[i] = EditorGUILayout.Slider((float)values[i], (float)settings.variables[i].options[0], (float)settings.variables[i].options[1], elementWidth, GUILayout.Height(elementY/2)) ;
				GUILayout.Space(15);
			}
			else if(values[i].GetType() == typeof(int)) {
				values[i] = EditorGUILayout.IntSlider((int)values[i], (int)settings.variables[i].options[0], (int)settings.variables[i].options[1], GUILayout.Height(elementY/2));
				GUILayout.Space(15);
			}
			else if(values[i].GetType() == typeof(bool)) {
				int val = ((bool)values[i])? 1 : 0;
				val = GUILayout.SelectionGrid(val, new string[]{settings.variables[i].options[0].ToString(), settings.variables[i].options[1].ToString()}, 2);
				values[i] = (val == 0)? true : false;
				GUILayout.Space(12.5f);
			}
			else if(values[i].GetType() == typeof(string)) {
				values[i] = GUILayout.TextField((string)values[i], elementWidth, GUILayout.Height(elementY / 2));
				GUILayout.Space(15);
			}
			GUILayout.EndVertical();
		}

		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		//Generate Button
		GUI.backgroundColor = GeneratorEditor.col_semiblack;
		GUILayout.BeginVertical("Box", GUILayout.Width(generateButtonWidth));
		GUILayout.Label("Finalise", EditorStyles.boldLabel);
		GUI.backgroundColor = GeneratorEditor.col_egg;
		GUILayout.Space(32);
		if(GUILayout.Button("Generate", generateButton)) tar.Generate();
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();

		setting.fontSize = oldFontSize;
		label.normal.textColor = Color.black;
	}
	
	void OnDestroy() {
		DestroyImmediate(obj);
		obj = null;
	}
}
