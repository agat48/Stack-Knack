using UnityEngine;
using System.Collections;

public class MainScr : MonoBehaviour {

	Rigidbody obj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {

						Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
						RaycastHit hit = new RaycastHit ();
						if (Physics.Raycast (ray, out hit, 100.0f)) {
					
								if (Input.GetTouch (0).phase == TouchPhase.Moved || Input.GetTouch (0).phase == TouchPhase.Began) {
										//float speed = 0.1f;
										Vector2 touchDelta = Input.GetTouch (0).deltaPosition;
										//hit.transform.Translate (touchDelta.x * speed, touchDelta.y * speed, 0);
										hit.rigidbody.useGravity = false;
										hit.rigidbody.velocity = Vector3.zero;
										hit.rigidbody.angularVelocity = Vector3.zero;
										//Debug.Log (hit.rigidbody.gameObject.name);
										Vector3 cameraTransform = Camera.main.transform.InverseTransformPoint (0, 0, 0);
										//hit.transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, cameraTransform.z-1.41f));
										//hit.rigidbody.AddRelativeForce (-touchDelta.x * 1.5f, touchDelta.y * 1.5f, 0);
										hit.rigidbody.MovePosition(new Vector3 (hit.transform.position.x + touchDelta.x*0.02f,hit.transform.position.y+ touchDelta.y*0.02f, 0));
					obj = hit.rigidbody;
										Debug.Log(touchDelta.x);
								}
						}
				}else{
			if(obj != null){
				obj.useGravity = true;
			}
		}
	}
}
