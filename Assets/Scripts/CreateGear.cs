using UnityEngine;
using System.Collections;

public class CreateGear : MonoBehaviour {
	
	public float range = 5.0f;
	public float delay = 0.2f;
	
	private float timeElapsed = 0.0f;
	
	void Start() {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed -= Time.deltaTime;
		
		if (timeElapsed > 0.0f)
			return;
			
		if (Input.GetButton("Fire1")) {
			timeElapsed = delay;				
			Transform camera = Camera.main.transform;
			Ray ray = new Ray(camera.position + camera.forward * 0.4f, camera.forward);
			RaycastHit hitInfo;
			LayerMask layer = LayerMask.GetMask("Facet with Peg");
			
			if (Physics.Raycast(ray, out hitInfo, range, layer)) {
				GameObject facet = hitInfo.collider.gameObject;
				Transform pegTransform = facet.transform.Find ("Peg");
				if (pegTransform) {
					Gear gear = pegTransform.gameObject.GetComponentsInChildren<Gear>(true)[0];
					gear.AddToScene();
				}
			}
		}
	}
}
