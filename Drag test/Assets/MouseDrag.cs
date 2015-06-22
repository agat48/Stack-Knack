using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {
	
	private float dist;
	private Transform toDrag;
	public static Transform activeTransform;
	private bool dragging = false;
	private Vector3 offset;
	private GameObject activeObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		drag ();
	}
	void drag() {
		
		Vector3 v3;
		if(Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit hit = new RaycastHit();
			Ray ray  = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); 
			if(Physics.Raycast(ray, out hit))
			{

				activeObj = hit.transform.gameObject;
				activeObj.GetComponent<Rigidbody>().isKinematic = false;
				{
					toDrag = hit.transform;
					dist = hit.transform.position.z - Camera.main.transform.position.z;
					v3 = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, dist);
					v3 = Camera.main.ScreenToWorldPoint(v3);
					offset = toDrag.position - v3;
					dragging = true;
				}
			}
		}
		if (Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			if (dragging)
			{
				v3 = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, dist);
				v3 = Camera.main.ScreenToWorldPoint(v3);
				toDrag.position = v3 + offset;
			}
		}
		if (Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			dragging = false;
			activeObj.GetComponent<Rigidbody>().isKinematic = true;
		}
	}
}
	