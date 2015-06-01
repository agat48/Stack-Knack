using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string filename = "./levelsEnabled";
		int levelsAvailable = int.Parse(System.IO.File.ReadAllText(filename));
		foreach (Transform child in transform) {
			string[] name = child.name.Split(char.Parse("-"));
			int num = int.Parse(name[name.Length-1]);
			if(num <= levelsAvailable) {
				Sprite spr = Resources.Load<Sprite>("box_"+num);
				if(spr) {
					child.GetComponent<Button>().image.overrideSprite = spr;

				} 
			} else {
					child.GetComponent<Button>().interactable = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
