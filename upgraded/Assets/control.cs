using UnityEngine;
using System.Collections;


public class control : MonoBehaviour {
	
	public GameObject ball;
	Gyroscope gyro;
	private float timer = 0;
	float rotZ;
	float step;
	bool opt = true;
	// The initials orientation
	private float initialOrientationZ;

	// Use this for initialization
	void Start () {
		gyro = Input.gyro; // Store the reference for Gyroscope sensor
		gyro.enabled = true;
		//ball = GameObject.Find ("Sphere");
		initialOrientationZ = Input.acceleration.x;
		ball.GetComponent<Rigidbody>().useGravity = false;

	}
	void OnGUI()
	{
		GUIStyle smallFont = new GUIStyle();
		smallFont.fontSize = 30;

		//GUILayout.Label ("Czas : " + timer);


		//GUI.Label(new Rect(0, 0, 200, 20), "Czas: "+timer, smallFont); //notice how the rect starts at 0/0 and the matrix handles the position!
		if (opt) {
			GUIStyle customButton = new GUIStyle("button");
			customButton.fontSize = 40;
			if (GUI.Button (new Rect (Screen.width/2-Screen.width/6, Screen.height/2-Screen.height/40, Screen.width/3, Screen.height/20), "Start",customButton)) {
				ball.GetComponent<Rigidbody>().useGravity = true;
				opt = ! opt;
			}
		}

	}
	// Update is called once per frame
	void Update () {

		//if (ball.transform.position.y > 1.0) {
		//	timer += Time.deltaTime;
		//}

		/*Quaternion transQuat = Quaternion.identity;
		//Adjust Unity output quaternion as per android SensorManager
		transQuat.w = gyro.attitude.x;
		transQuat.x = gyro.attitude.y;
		transQuat.y = gyro.attitude.z;
		transQuat.z = gyro.attitude.w;
		transQuat = Quaternion.Euler(90, 0, 0)*transQuat;//change axis around

		//transform.rotation (new Vector3(0, 0, 1), transQuat.eulerAngles.z - 180.0f);

		Vector3 temp = transform.rotation.eulerAngles;

		temp.z = Mathf.Round(Input.gyro.attitude.z*100)-45.0f;*/

		//transform.rotation = Quaternion.Euler(temp);
		if (ball.transform.position.y < 1.0f) {
			opt = true;

			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
			ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			ball.transform.position = new Vector3(1.65f, 3.1f, 0);
			ball.transform.rotation = Quaternion.Euler(0, 0, 0);


			transform.rotation = Quaternion.Euler(0, 0, 0);
			initialOrientationZ = Input.gyro.attitude.z;
		}

		if (!opt) {
			rotZ = transform.rotation.eulerAngles.z;
			step = (Input.acceleration.x - initialOrientationZ)*1.7f;

			if ((rotZ - step) < 30.0f || (rotZ - step) > 330.0f) {
				transform.Rotate (0, 0, -step);
			}
		}
		//Debug.Log (Mathf.Round((Input.acceleration.z) * 10.0f*100.0f)/100.0f);
		//Debug.Log (initialOrientationZ);
		//Debug.Log ("z: "+Input.acceleration.z);
		//Debug.Log ("x: "+Input.acceleration.x);
		//Debug.Log ("y: "+Input.acceleration.y);

	}
}
