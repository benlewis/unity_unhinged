using UnityEngine;
using System.Collections;

public class CreateGear : MonoBehaviour {
	
	public float range = 5.0f;
	
	void Start() {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Transform camera = Camera.main.transform;
			Ray ray = new Ray(camera.position + camera.forward * 0.4f, camera.forward);
			RaycastHit hitInfo;
			
			if (Physics.Raycast(ray, out hitInfo, range)) {
				GameObject peg = hitInfo.collider.gameObject;
				Debug.Log ("Hit " + peg.name);
				if (peg.name == "Full Facet") {
					Transform pegTransform = peg.transform.Find ("Peg");
					if (pegTransform)
						peg = pegTransform.gameObject;
				}
				if (peg.tag == "Peg") {
					Transform gear = peg.transform.FindChild("Gear");
					if (gear) {
						gear.gameObject.SetActive(true);
					}
				}
			}
		}
	}
}
