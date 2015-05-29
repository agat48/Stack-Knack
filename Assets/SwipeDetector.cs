using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour 
{
	
	public float minSwipeDistY;
	public float minSwipeDistX;
	private Vector2 startPos;

	public Camera camera;

	private Rect zoomIn;
	private Rect zoomOut;

	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	private float minZ;
	private float maxZ;


	void Start(){
	
		zoomIn = new Rect (Screen.width/100, Screen.height- 50, Screen.width/5, Screen.height/10);
		zoomOut = new Rect (Screen.width-Screen.width/5-Screen.width/100, Screen.height- 50, Screen.width/5, Screen.height/10);

		minX = -10;
		maxX = 10;
		minY = 0;
		maxY = 20;
		minZ = -10;
		maxZ = 10;
	}
	void OnGUI()
	{
		GUIStyle smallFont = new GUIStyle("button");
		smallFont.fontSize = 30;
		


		GUIStyle customButton = new GUIStyle("button");
		customButton.fontSize = 40;

		if (Camera.main.transform.position.y < maxY) {
			//rysowanie przycisków i obsługa myszki
			if (GUI.RepeatButton (zoomIn, "+", customButton)) {//zoom in
				Camera.main.transform.position += Camera.main.transform.forward * Time.deltaTime;
			}

			if (GUI.RepeatButton (zoomOut, "-", customButton)) {//zoom out
				Camera.main.transform.position -= Camera.main.transform.forward * Time.deltaTime;
			}
		}

		if (GUI.RepeatButton (new Rect (Screen.width/100, Screen.height/100, Screen.width/3, Screen.height/10), "Front",smallFont)) {//zoom out
			Camera.main.transform.position = new Vector3(0, 6,-6);
			Camera.main.transform.transform.localEulerAngles = new Vector3(10,0,0);
		}
		if (GUI.RepeatButton (new Rect (Screen.width-Screen.width/100-Screen.width/3, Screen.height/100, Screen.width/3, Screen.height/10), "Top",smallFont)) {//zoom out
			Camera.main.transform.position = new Vector3(0, 20,0);
			Camera.main.transform.transform.localEulerAngles = new Vector3(90,0,0);
		}
		
	}

	void Update()
	{
		//#if UNITY_ANDROID
		if (Input.touchCount > 0) 	
		{
			Touch touch = Input.touches[0];

			//obsługa zoomu dotykiem
			Vector2 vec = touch.position;
			vec.y = Screen.height - vec.y;
			if(zoomIn.Contains(vec)){

				//ograniczenie pozycji
				Vector3 v3 = Camera.main.transform.position;
				Vector3 trans = Camera.main.transform.forward* Time.deltaTime;
				v3.z = Mathf.Clamp(v3.z+trans.z,minZ,maxZ);
				v3.y = Mathf.Clamp(v3.y+trans.y,minY,maxY);
				v3.x = Mathf.Clamp(v3.x+trans.x,minX,maxX);
				Camera.main.transform.position = v3;
			}
			if(zoomOut.Contains(vec)){

				Vector3 v3 = Camera.main.transform.position;
				Vector3 trans = Camera.main.transform.forward* Time.deltaTime;
				v3.z = Mathf.Clamp(v3.z-trans.z,minZ,maxZ);
				v3.y = Mathf.Clamp(v3.y-trans.y,minY,maxY);
				v3.x = Mathf.Clamp(v3.x-trans.x,minX,maxX);
				Camera.main.transform.position = v3;
			}



			if(touch.phase == TouchPhase.Began){
				startPos = touch.position;
			}

			float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
			float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

			if (swipeDistVertical > minSwipeDistY) 
			{
				
				float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
				Vector3 localRight = Camera.main.transform.worldToLocalMatrix.MultiplyVector(Camera.main.transform.right);

				if (swipeValue > 0){//up swipe

					if(Camera.main.transform.position.y < maxY){
						//jak mniejsza niż maxY to możemy obracać
						Camera.main.transform.RotateAround(Vector3.zero, Camera.main.transform.right, swipeValue);
						
						Vector3 v3 = Camera.main.transform.position;
						v3.z = Mathf.Clamp(v3.z,minZ,maxZ);
						v3.y = Mathf.Clamp(v3.y,minY,maxY);
						v3.x = Mathf.Clamp(v3.x,minX,maxX);
						Camera.main.transform.position = v3;
					}else{
						//inaczej możemy przesuwać
						Vector3 v3 = Camera.main.transform.position;
						Vector3 trans = Camera.main.transform.up* 2*Time.deltaTime;
						v3.z = Mathf.Clamp(v3.z-trans.z,minZ,maxZ);
						v3.y = Mathf.Clamp(v3.y-trans.y,minY,maxY);
						v3.x = Mathf.Clamp(v3.x-trans.x,minX,maxX);
						Camera.main.transform.position = v3;
					}


					Debug.Log("up"+swipeValue);	
					
				}else if (swipeValue < 0){//down swipe
					if(Camera.main.transform.position.y < maxY){
						Camera.main.transform.RotateAround(Vector3.zero, Camera.main.transform.right, swipeValue);

						Vector3 v3 = Camera.main.transform.position;
						v3.z = Mathf.Clamp(v3.z,minZ,maxZ);
						v3.y = Mathf.Clamp(v3.y,minY,maxY);
						v3.x = Mathf.Clamp(v3.x,minX,maxX);
						Camera.main.transform.position = v3;

						Debug.Log("down"+swipeValue);
					}else{
						//inaczej możemy przesuwać
						Vector3 v3 = Camera.main.transform.position;
						Vector3 trans = Camera.main.transform.up*2* Time.deltaTime;
						v3.z = Mathf.Clamp(v3.z+trans.z,minZ,maxZ);
						v3.y = Mathf.Clamp(v3.y+trans.y,minY,maxY);
						v3.x = Mathf.Clamp(v3.x+trans.x,minX,maxX);
						Camera.main.transform.position = v3;
					}
				}
				
			}
			if (swipeDistHorizontal > minSwipeDistX) 
			{	
				float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
				
				if (swipeValue > 0){//right swipe
					if(Camera.main.transform.position.y < maxY){
						Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, swipeValue);

						Vector3 v3 = Camera.main.transform.position;
						v3.z = Mathf.Clamp(v3.z,minZ,maxZ);
						v3.y = Mathf.Clamp(v3.y,minY,maxY);
						v3.x = Mathf.Clamp(v3.x,minX,maxX);
						Camera.main.transform.position = v3;

						Debug.Log("right"+swipeValue);
					}else{

						//inaczej możemy przesuwać
						Vector3 v3 = Camera.main.transform.position;
						Vector3 trans = Camera.main.transform.right*2* Time.deltaTime;
						v3.z = Mathf.Clamp(v3.z-trans.z,minZ,maxZ);
						v3.y = Mathf.Clamp(v3.y-trans.y,minY,maxY);
						v3.x = Mathf.Clamp(v3.x-trans.x,minX,maxX);
						Camera.main.transform.position = v3;
					}
					
				}else if (swipeValue < 0){//left swipe


					if(Camera.main.transform.position.y < maxY){
						Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, swipeValue);

						Vector3 v3 = Camera.main.transform.position;
						v3.z = Mathf.Clamp(v3.z,minZ,maxZ);
						v3.y = Mathf.Clamp(v3.y,minY,maxY);
						v3.x = Mathf.Clamp(v3.x,minX,maxX);
						Camera.main.transform.position = v3;

						Debug.Log("left"+swipeValue);
					}else{
						
						//inaczej możemy przesuwać
						Vector3 v3 = Camera.main.transform.position;
						Vector3 trans = Camera.main.transform.right*2* Time.deltaTime;
						v3.z = Mathf.Clamp(v3.z+trans.z,minZ,maxZ);
						v3.y = Mathf.Clamp(v3.y+trans.y,minY,maxY);
						v3.x = Mathf.Clamp(v3.x+trans.x,minX,maxX);
						Camera.main.transform.position = v3;
					}
				}
			}
		}
	}
}