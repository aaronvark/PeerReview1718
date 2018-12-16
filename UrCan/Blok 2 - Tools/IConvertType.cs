using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class IConvert {
	public static Dictionary<string, IConvertType> FILETYPES = new Dictionary<string, IConvertType>();
	
	/* THE IMPORTED TYPE CONVERSIONS */
	/*To add more file type conversions, simply include the implementation of the IConvertType interface and the respective file extension as a string */
	public static IConvertType[] types = new IConvertType[]{new IJPG(), new IMP3(), new IBMP()};

	static IConvert() {
		foreach(IConvertType conv in types) FILETYPES.Add(conv.extension.ToLower(), conv);
	}
}

[System.Serializable]
public class Setting {
	public string name {private set;get;}
	public object value {private set;get;}
	public object[] options {private set;get;}

	public Setting(string name, object value,  object[] options) {
		this.name = name;
		this.value = value;
		this.options = options;
	}
	public Setting(string name, object value) : this(name, value, null) {}
}

public interface IConvertType {
	List<Setting> variables {get;set;}

	string extension {
		get;
		set;
	}
	GameObject Convert(string path);
}

public class IJPG : IConvertType {
	public List<Setting> variables {
		get {
			List<Setting> dict = new List<Setting>();
			dict.Add(new Setting("Step", 5, new object[]{0, 100}));
			dict.Add(new Setting("Amplitude", 5f, new object[]{0f, 1f}));
			dict.Add(new Setting("Randomiziation", true, new object[]{"Yes", "No"}));
			dict.Add(new Setting("Seed", "randomStringWillGoHere"));
			return dict;}
		set {}
	}

	public string extension {
		get {return "JPG";}
		set {}
	}

    public GameObject Convert(string path) {
		Texture2D tex = Texture2D.whiteTexture;
		WWW www = new WWW(path);
		www.LoadImageIntoTexture(tex);

		for(int x = 0; x < tex.width; x++)
			for(int y = 0; y < tex.height; y++) {
				Color col = tex.GetPixel(x, y);
				if(col.r > 0 && col.g > 0 && col.b > 0) Generator.CreateCube(new Vector3(x, y, 0));
			}
			return Generator.Self.parent;
	}
}

public class IBMP : IConvertType {
	public List<Setting> variables {
		get {
			List<Setting> dict = new List<Setting>();
			dict.Add(new Setting("Step", 5, new object[]{0, 100}));
			dict.Add(new Setting("Amplitude", 5f, new object[]{0f, 1f}));
			dict.Add(new Setting("Randomiziation", true, new object[]{"Yes", "No"}));
			dict.Add(new Setting("Seed", "randomStringWillGoHere"));
			return dict;}
		set {}
	}

	public string extension {
		get {return "BMP";}
		set {}
	}

    public GameObject Convert(string path) {
		Texture2D tex = new Texture2D(100, 100);
		WWW www = new WWW(path);
		www.LoadImageIntoTexture(tex);

		for(int x = 0; x < tex.width; x++)
			for(int y = 0; y < tex.height; y++) {
				Color col = tex.GetPixel(x, y);
				Debug.Log(col + " | " + x  + "," + y);
				if(col.r > 0 && col.g <= 0 && col.b <= 0) Generator.CreateCube(new Vector3(x, y, 0));
			}

		return Generator.Self.parent;
	}
}

public class IMP3 : IConvertType {
		public List<Setting> variables {
		get {
			List<Setting> dict = new List<Setting>();
			dict.Add(new Setting("Low-Frequency Peaks", 5f, new object[]{0f, 10f}));
			dict.Add(new Setting("Mid-Frequency Peaks", 5f, new object[]{0f, 10f}));
			dict.Add(new Setting("High-Frequency Peaks", 5f, new object[]{0f, 10f}));
			dict.Add(new Setting("Length", 5, new object[]{1, 100}));
			return dict;}
		set {}
	}

	public string extension {
		get {return "mp3";}
		set {}
	}

	public GameObject Convert(string path) {
		return null;
	}
}