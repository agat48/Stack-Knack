﻿using UnityEngine;
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
}
