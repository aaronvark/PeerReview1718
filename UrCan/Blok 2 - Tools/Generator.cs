using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Generator : MonoBehaviour {
	public Texture load;

	[System.Serializable]
	public enum GameType {
		Layered, Runner, Landscape
	};

	[SerializeField]
	public GameType gameType;

	protected Material cubeMaterial;
	public GameObject parent;

	public static Generator Self;

	public void Generate() {
		
	}

	public void InitGenerator(Material material) {
		Self = this;
		cubeMaterial = material;
		parent = GameObject.CreatePrimitive(PrimitiveType.Cube);
		parent.name = "Preview Model";
		parent.hideFlags = HideFlags.HideAndDontSave;
		parent.GetComponent<MeshRenderer>().sharedMaterial = Self.cubeMaterial;
	}

	public static GameObject CreateCube(Vector3 pos) {
		GameObject obj;
		obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		obj.name = "Part";
		//obj.hideFlags = HideFlags.HideAndDontSave;
		obj.GetComponent<MeshRenderer>().sharedMaterial = Self.cubeMaterial;
		obj.transform.SetParent(Self.parent.transform);
		obj.transform.localPosition = new Vector3(pos.x *  obj.transform.localScale.x, pos.y *  obj.transform.localScale.y, pos.z *  obj.transform.localScale.z);
		return obj;
	}

	public static int AMOUNT_GAMEMODES() {
		return System.Enum.GetNames(typeof(GameType)).Length;
	}
}
