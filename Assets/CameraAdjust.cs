using UnityEngine;
using System.Collections;

public class CameraAdjust : MonoBehaviour {

	private Vector3 initialCameraOrientation;
	float speed = 30.0f;
	// Use this for initialization
	void Start () {
		Vector3 platform = GameObject.Find ("Platform").transform.position;
		float levelY = GameObject.Find ("Level").transform.position.y;
		float middleY = (platform.y + levelY) / 2;
		transform.LookAt (new Vector3(platform.x, middleY, platform.z));
		initialCameraOrientation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void rotateHorizontal(float angle) {
		float cameraY = transform.eulerAngles.y;
		transform.RotateAround (Vector3.zero, Vector3.up, angle - cameraY);
	}

	public void rotateVertical(float angle) {
		float cameraX = transform.eulerAngles.x;
		transform.RotateAround (Vector3.zero, transform.right, angle - cameraX + initialCameraOrientation.x);
		if (transform.eulerAngles.x >= 90) {
			Debug.Log (angle);
		}
	}

	public void zoomIn() {

		transform.position += transform.forward * Time.deltaTime * speed;
	}

	public void zoomOut() {
		transform.position -= transform.forward * Time.deltaTime * speed;
	}
}
