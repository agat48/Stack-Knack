using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableLevels : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		int levelsAvailable = PlayerPrefs.GetInt("Levels");
		
		//jeśli nie zostało ustawione
		if (levelsAvailable == 0)
			levelsAvailable = 1;
		
		
		foreach (Transform child in transform) {
			string[] name = child.name.Split(char.Parse("-"));
			int num = int.Parse(name[name.Length-1]);
			if(num <= levelsAvailable) {
				Sprite spr = Resources.Load<Sprite>("box_"+num);
				if(spr) {
					child.GetComponent<Button>().image.overrideSprite = spr;
					child.GetComponent<Button>().interactable = true;
				} 
			} else {
				// sth
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
