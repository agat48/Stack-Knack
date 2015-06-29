using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public void LoadLevelByNumber (int num) {
		Application.LoadLevel("s-" + num);
	}
	public void Menu() {
		Application.LoadLevel("menu_16x9");
	}
	public void LoadLevelsScene () {
		Application.LoadLevel("levels_16x9");
	}
	public void LoadHowToScene() {
		Application.LoadLevel ("howTo_16x9");
	}
	public void LoadNextLevel() {
		string[] actLevelStr = Application.loadedLevelName.Split ('-');
		int actLevel = int.Parse(actLevelStr [actLevelStr.Length - 1]);
		LoadLevelByNumber (++actLevel);
	}
	public void DestroyCanvas() {
		Destroy (GameObject.Find ("LevelFail(Clone)").gameObject);
	}
	public void ReloadLevel() {
		Application.LoadLevel (Application.loadedLevelName);
	}
	public void Settings() {
		Application.LoadLevel("settings_16x9");
	}

	public void ClearGame() {
		string filename = "./levelsEnabled";
		if(System.IO.File.Exists(filename)) {
			System.IO.File.WriteAllText(filename, "1");
		}
	}
}
