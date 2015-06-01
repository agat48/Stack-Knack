using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		string filename = "./levelsEnabled";
		if(!System.IO.File.Exists(filename)) {
			System.IO.FileStream fs = System.IO.File.Create(filename);
			fs.Close();
			System.IO.File.WriteAllText(filename, "1");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
