using UnityEngine;
using System.Collections;

public class CreateGear : MonoBehaviour {
	
	public float range = 5.0f;
	public float delay = 0.2f;
	
	private float timeElapsed = 0.0f;
	
	public GameObject selectedFacet = null;
	
	void Start() {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		// Highlight any facets we are facing
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;
		LayerMask layer = LayerMask.GetMask("Facet with Peg");
		
		GameObject newSelectedFacet = null;
		if (Physics.Raycast(ray, out hit, range, layer))
			newSelectedFacet = hit.collider.gameObject;
		
		if (newSelectedFacet != selectedFacet) {
			if (selectedFacet != null)
				selectedFacet.GetComponent<HighlightSelectedFacet>().TurnOffHighlight();
				
			selectedFacet = newSelectedFacet;
			
			if (selectedFacet != null)
				selectedFacet.GetComponent<HighlightSelectedFacet>().TurnOnHighlight();
		}
		
		timeElapsed -= Time.deltaTime;
		
		if (timeElapsed > 0.0f)
			return;
			
		if (Input.GetButton("Fire1") && selectedFacet != null) {
			timeElapsed = delay;				
			Transform pegTransform = selectedFacet.transform.Find ("Peg");
			if (pegTransform) {
				Gear gear = pegTransform.gameObject.GetComponentsInChildren<Gear>(true)[0];
				if (gear)
					gear.AddToScene();
			}
		}
	}
}
