using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwipeDetector : MonoBehaviour 
{
	
	public float minSwipeDistY;
	public float minSwipeDistX;
	
	private float swipeDistVertical;
	private float swipeDistHorizontal;
	private Vector2 startPos;

	private Rect zoomIn;
	private Rect zoomOut;
	private Rect rotationField;

	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	private float minZ;
	private float maxZ;
	

	private Color objColor;
	private Rigidbody obj;

	private float doubleTapTime;
	private bool objectSelected=false;

	public Rigidbody[] objects;
	private List<ObjectData> objectsBck = new List<ObjectData>();

	private bool simulate = false;
	private bool start = false;
	private bool stop = false;

	public GameObject paddle;
	private float rotZ;
	private float step;
	private float initialOrientationZ;


	private float timer; 
	void Start(){
	
		zoomIn = new Rect (Screen.width/100, Screen.height- 50, Screen.width/5, Screen.height/10);
		zoomOut = new Rect (Screen.width/50 + Screen.width/5, Screen.height- 50, Screen.width/5, Screen.height/10);
		rotationField= new Rect (Screen.width-Screen.width/100- Screen.width/2.5f, Screen.height- Screen.height/7, Screen.width/2.5f, Screen.height/7);

		minX = -10;
		maxX = 10;
		minY = 0;
		maxY = 20;
		minZ = -10;
		maxZ = 10;

		Input.gyro.enabled = true;

		for (int i = 0; i < objects.Length; i++)
		{
			objects[i].constraints = RigidbodyConstraints.FreezeAll;
		}

		timer = 5;

	}
	void OnGUI()
	{
		GUIStyle smallFont = new GUIStyle("button");
		smallFont.fontSize = 30;

		GUIStyle customButton = new GUIStyle("button");
		customButton.fontSize = 40;

		if (!simulate) {
			//edycja
			if (objectSelected) {
				//wybrany obiekt do obrotu
				if (GUI.RepeatButton (new Rect (Screen.width / 100, Screen.height / 100, Screen.width / 3, Screen.height / 10), "Done", smallFont)) {//zoom out
					objectSelected = false;
					obj.GetComponent<Renderer> ().material.SetColor ("_Color", objColor);
				}
				return;
			}
			
			if (Camera.main.transform.position.y < maxY) {
				//rysowanie przycisków i obsługa myszki
				if (GUI.RepeatButton (zoomIn, "+", customButton)) {//zoom in
					Camera.main.transform.position += Camera.main.transform.forward * Time.deltaTime;
				}
				
				if (GUI.RepeatButton (zoomOut, "-", customButton)) {//zoom out
					Camera.main.transform.position -= Camera.main.transform.forward * Time.deltaTime;
				}



			}

			if (GUI.Button (rotationField, "o", customButton)) {//zoom out
				
			}

			if (GUI.Button (new Rect (Screen.width / 100, Screen.height / 100, Screen.width / 3, Screen.height / 10), "Front", smallFont)) {//zoom out
				Camera.main.transform.position = new Vector3 (0, 7.2f, -7.5f);
				Camera.main.transform.transform.localEulerAngles = new Vector3 (10, 0, 0);
			}
			if (GUI.Button (new Rect (Screen.width - Screen.width / 100 - Screen.width / 3, Screen.height / 100, Screen.width / 3, Screen.height / 10), "Top", smallFont)) {//zoom out
				Camera.main.transform.position = new Vector3 (0, 20, 0);
				Camera.main.transform.transform.localEulerAngles = new Vector3 (90, 0, 0);
			}
			if (GUI.Button (new Rect (Screen.width / 50 + Screen.width / 3, Screen.height / 100, Screen.width / 3.2f - Screen.width / 100, Screen.height / 10), "Sim", smallFont)) {//zoom out
				simulate = true;

				Camera.main.orthographic = true;
				Camera.main.transform.position = new Vector3 (0, 7.7f, -6);
				Camera.main.transform.transform.localEulerAngles = new Vector3 (10, 0, 0);
			}
		} else {
			//symulacja
			if(start){
				if (GUI.Button (new Rect (Screen.width / 100, Screen.height / 100, Screen.width / 3, Screen.height / 10), "Stop", smallFont)) {//zoom out

					for (int i = 0; i < objects.Length; i++)
					{
						objects[i].useGravity = false;
						objects[i].velocity = Vector3.zero;
						objects[i].angularVelocity = Vector3.zero;
						objects[i].transform.position = objectsBck[i].pos;
						objects[i].rotation = objectsBck[i].rot;
					}
					paddle.transform.rotation=Quaternion.Euler(Vector3.zero);
					start = false;

					timer = 5;
				}

				GUI.Label(new Rect(Screen.width- 100,10,90,40),""+Mathf.Round(timer), smallFont);
			}else{
				if (GUI.Button (new Rect (Screen.width / 100, Screen.height / 100, Screen.width / 3, Screen.height / 10), "Start", smallFont)) {//zoom out
					start = true;

					objectsBck.Clear();
					for (int i = 0; i < objects.Length; i++)
					{
						objectsBck.Add( new ObjectData(objects[i].transform.position, objects[i].rotation));
						objects[i].useGravity = true;
						objects[i].constraints = RigidbodyConstraints.None;
						objects[i].constraints = RigidbodyConstraints.FreezeRotationX;
						objects[i].constraints = RigidbodyConstraints.FreezePositionZ;
					}

					initialOrientationZ = Input.acceleration.x;
				}
				if (GUI.Button (new Rect (Screen.width - Screen.width / 100 - Screen.width / 3, Screen.height / 100, Screen.width / 3, Screen.height / 10), "Edit", smallFont)) {//zoom out
					
					if(objectsBck.Count>0){
						
						for (int i = 0; i < objects.Length; i++)
						{
							objects[i].useGravity = false;
							objects[i].velocity = Vector3.zero;
							objects[i].angularVelocity = Vector3.zero;
							objects[i].transform.position = objectsBck[i].pos;
							objects[i].rotation = objectsBck[i].rot;
							objects[i].constraints = RigidbodyConstraints.FreezeAll;

						}
					}
					//zmiana kamery
					Camera.main.orthographic = false;
					Camera.main.transform.position = new Vector3 (0, 7.2f, -7.5f);
					
					paddle.transform.rotation=Quaternion.Euler(Vector3.zero);
					simulate = false;
				}
			}


		}


		
	}

	void Update()
	{
		//#if UNITY_ANDROID
		if (start) {
			timer -= Time.deltaTime;

			rotZ = paddle.transform.rotation.eulerAngles.z;
			step = (Input.acceleration.x - initialOrientationZ)*1.7f;
			
			if ((rotZ - step) < 30.0f || (rotZ - step) > 330.0f) {
				paddle.transform.Rotate (0, 0, -step);
			}
			bool complete = true;

			for (int i = 0; i < objects.Length; i++)
			{
				if(objects[i].transform.position.y <= 0)
				{
					complete = false;
					break;
				}
				
			}

			if(timer <= 0){

				if(complete){
					//next lvl
					Application.LoadLevel("levels_16x9");
				}else{
					//porazka

					for (int i = 0; i < objects.Length; i++)
					{
						objects[i].useGravity = false;
						objects[i].velocity = Vector3.zero;
						objects[i].angularVelocity = Vector3.zero;
						objects[i].transform.position = objectsBck[i].pos;
						objects[i].rotation = objectsBck[i].rot;
					}
					paddle.transform.rotation=Quaternion.Euler(Vector3.zero);
					start = false;

					timer = 5;
				}

			}
		}

		if (obj && !simulate) {
			obj.velocity = Vector3.zero;
			obj.angularVelocity = Vector3.zero;
		}

		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			if (touch.phase == TouchPhase.Began) {
				startPos = touch.position;
			}

			if (Camera.main.transform.position.y < maxY) {
				//obsługa zoomu dotykiem
				Vector2 vec = touch.position;
				vec.y = Screen.height - vec.y;
				if (zoomIn.Contains (vec)) {
					
					//ograniczenie pozycji
					Vector3 v3 = Camera.main.transform.position;
					Vector3 trans = Camera.main.transform.forward * Time.deltaTime;
					v3.z = Mathf.Clamp (v3.z + trans.z, minZ, maxZ);
					v3.y = Mathf.Clamp (v3.y + trans.y, minY, maxY);
					v3.x = Mathf.Clamp (v3.x + trans.x, minX, maxX);
					Camera.main.transform.position = v3;
					return;
				}
				if (zoomOut.Contains (vec)) {
					
					Vector3 v3 = Camera.main.transform.position;
					Vector3 trans = Camera.main.transform.forward * Time.deltaTime;
					v3.z = Mathf.Clamp (v3.z - trans.z, minZ, maxZ);
					v3.y = Mathf.Clamp (v3.y - trans.y, minY, maxY);
					v3.x = Mathf.Clamp (v3.x - trans.x, minX, maxX);
					Camera.main.transform.position = v3;
					return;
				}
				if (rotationField.Contains (vec)) {
					
					float swipeValueY = Mathf.Sign (touch.position.y - startPos.y);
					float swipeValueX = Mathf.Sign (touch.position.x - startPos.x);

					swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
					swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;



					if (swipeDistVertical > minSwipeDistY) {
						Camera.main.transform.RotateAround (Vector3.zero, Camera.main.transform.right, swipeValueY);
					}else if (swipeDistHorizontal > minSwipeDistX) {
						Camera.main.transform.RotateAround (Vector3.zero, Vector3.up, swipeValueX);
					}

					Vector3 v3 = Camera.main.transform.position;
					v3.z = Mathf.Clamp (v3.z, minZ, maxZ);
					v3.y = Mathf.Clamp (v3.y, minY, maxY);
					v3.x = Mathf.Clamp (v3.x, minX, maxX);
					Camera.main.transform.position = v3;

					Debug.Log(swipeValueY+" + "+swipeValueX);
					return;
				}

			}else{
				Vector2 vec = touch.position;
				vec.y = Screen.height - vec.y;
				if (rotationField.Contains (vec)) {
					
					float swipeValueY = Mathf.Sign (touch.position.y - startPos.y);
					float swipeValueX = Mathf.Sign (touch.position.x - startPos.x);
					
					swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
					swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
					

					//inaczej możemy przesuwać
					Vector3 v3 = Camera.main.transform.position;
					Vector3 trans = Camera.main.transform.right * 2 * Time.deltaTime;
					v3.z = Mathf.Clamp (v3.z + swipeValueX * trans.z, minZ, maxZ);
					v3.y = Mathf.Clamp (v3.y + swipeValueX * trans.y, minY, maxY);
					v3.x = Mathf.Clamp (v3.x + swipeValueX * trans.x, minX, maxX);
					Camera.main.transform.position = v3;

					//inaczej możemy przesuwać
					v3 = Camera.main.transform.position;
					trans = Camera.main.transform.up * 2 * Time.deltaTime;
					v3.z = Mathf.Clamp (v3.z + swipeValueY * trans.z, minZ, maxZ);
					v3.y = Mathf.Clamp (v3.y + swipeValueY * trans.y, minY, maxY);
					v3.x = Mathf.Clamp (v3.x + swipeValueY * trans.x, minX, maxX);
					Camera.main.transform.position = v3;


					return;
				}
			}
			if(objectSelected){
				//rotacja wybranego obiektu
				swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
				swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;

				float swipeValueY = Mathf.Sign (touch.position.y - startPos.y);
				float swipeValueX = Mathf.Sign (touch.position.x - startPos.x);
				Quaternion deltaRotationY, deltaRotationX;

				deltaRotationY = Quaternion.Euler(Camera.main.transform.right * swipeValueY );
				deltaRotationX = Quaternion.Euler(Camera.main.transform.up * -swipeValueX );


				if (swipeDistVertical > minSwipeDistY) {
					obj.MoveRotation( deltaRotationY * obj.rotation );
				}else if (swipeDistHorizontal > minSwipeDistX) {
					obj.MoveRotation( deltaRotationX *obj.rotation  );
				}

				Debug.Log(swipeValueY);

				return;
			}

			//poruszanie obiektów
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, 100.0f)) {


				

				if (hit.rigidbody) {

					//sprawdzenie ostatniego obiektu
					if(obj == hit.rigidbody && Time.time < doubleTapTime + .2f){
						objectSelected= true;
						hit.rigidbody.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);

						Debug.Log("Double Tap"+Time.time);
						return;
					}

					if (touch.phase == TouchPhase.Began ) {
						obj = hit.rigidbody;
						objColor = hit.rigidbody.GetComponent<Renderer> ().material.color;
						hit.rigidbody.constraints = RigidbodyConstraints.None;
					}
					if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began) {


						Vector2 touchDelta = Input.GetTouch (0).deltaPosition;
						hit.rigidbody.useGravity = false;
						hit.rigidbody.velocity = Vector3.zero;
						hit.rigidbody.angularVelocity = Vector3.zero;

						Vector3 cameraTransform = Camera.main.transform.InverseTransformPoint (0, 0, 0);
						float dist = Vector3.Distance (hit.transform.position, Camera.main.transform.position);



						hit.rigidbody.MovePosition (hit.transform.position + (Camera.main.transform.right * Time.deltaTime * touchDelta.x * dist / 15) + 
							(Camera.main.transform.up * Time.deltaTime * touchDelta.y * dist / 15));



						//Debug.Log (objColor)
						hit.rigidbody.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
					}
					if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
						obj.GetComponent<Renderer> ().material.SetColor ("_Color", objColor);
						doubleTapTime = Time.time;
						hit.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
						obj.constraints = RigidbodyConstraints.FreezeAll;
						obj.velocity = Vector3.zero;
						obj.angularVelocity = Vector3.zero;
					}

					return;
				}
			}
			if(obj){
				obj.GetComponent<Renderer> ().material.SetColor ("_Color", objColor);
			}

		}
	}
}