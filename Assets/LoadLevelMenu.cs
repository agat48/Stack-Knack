using UnityEngine;
using System.Collections;

public class LoadLevelMenu : MonoBehaviour {
	

	// Update is called once per frame
	public void LoadLevelsScene () {
		Application.LoadLevel("levels_16x9");
	}
	public void LoadHowToScene() {
		Application.LoadLevel ("howTo_16x9");
	}
}
