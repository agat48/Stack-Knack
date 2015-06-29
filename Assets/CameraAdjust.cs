using UnityEngine;
using System.Collections;

public class CameraAdjust : MonoBehaviour {

	private Vector3 initialCameraOrientation;
	float speed = 30.0f;
	// Use this for initialization
	void Start () {
		initialCameraOrientation = Camera.main.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void rotateHorizontal(float angle) {
		float cameraY = Camera.main.transform.eulerAngles.y;
		Camera.main.transform.RotateAround (Vector3.zero, Vector3.up, angle - cameraY);
	}

	public void rotateVertical(float angle) {
		float cameraX = Camera.main.transform.eulerAngles.x;
		Camera.main.transform.RotateAround (Vector3.zero, Camera.main.transform.right, angle - cameraX + initialCameraOrientation.x);
		if (transform.eulerAngles.x >= 90) {
			Debug.Log (angle);
		}
	}

	public void zoomIn() {

		Camera.main.transform.position += Camera.main.transform.forward * Time.deltaTime * speed;
	}

	public void zoomOut() {
		Camera.main.transform.position -= Camera.main.transform.forward * Time.deltaTime * speed;
	}

}
