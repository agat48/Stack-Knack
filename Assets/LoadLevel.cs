using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public void LoadLevelByNumber (int num) {
		Application.LoadLevel("s" + num);
	}
	public void Back() {
		Application.LoadLevel("menu_16x9");
	}
}
