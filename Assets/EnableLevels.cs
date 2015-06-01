using UnityEngine;
using System.Collections;

public class EnableLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string filename = "./levelsEnabled";
		int levelsAvailable = int.Parse(System.IO.File.ReadAllText(filename));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
