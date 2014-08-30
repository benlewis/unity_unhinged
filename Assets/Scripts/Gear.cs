using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gear : MonoBehaviour {

	public float startingPower = 0.0f;

	[HideInInspector]
	public float rotationSpeed = 0.0f;
	
	[HideInInspector]
	public float radius;
	
	[HideInInspector]
	public List<Gear> connectedGears = new List<Gear>();

	// Use this for initialization
	void Start () {
		radius = transform.FindChild("Gear Mesh").renderer.bounds.size.x;
		
		if (gameObject.activeSelf)
			GearManager.Instance().AddGear(this);		
	}
	
	// This is called during a GearManager.UpdateGears method
	public void powerConnectedGears() {
		// Only power other gears if we have power
		if (rotationSpeed == 0.0f)
			return;
			
		foreach (Gear g in connectedGears) {
			if (g.rotationSpeed == 0.0f) {
				g.rotationSpeed = -rotationSpeed * radius / g.radius;
					
				g.powerConnectedGears();
			}
		}
	}
	
	// FixedUpdate is called once every 0.2 seconds
	void Update () {
		//Debug.Log (gameObject.transform.rotation.ToString());
		//if (!GearManager.Instance().updateInProgress && Input.GetMouseButton(1))
		transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
	}
	
	public void AddToScene() {
		gameObject.SetActive(true);
		//GearManager.Instance().AddGear (this);
	}
	
	public void RemoveFromScene() {
		gameObject.SetActive(false);
		GearManager.Instance().RemoveGear (this);		
	}
	
	void OnTriggerEnter(Collider collider) {
		// print("Enter trigger with " + collider.transform.name);

		Gear otherGear = collider.GetComponentInParent<Gear> ();
		if (!otherGear)
			return;
					
		GearManager.Instance().ConnectGears(this, otherGear);
	}
	
	void OnTriggerExit(Collider collider) {
		// print("Exit trigger with " + collider.transform.name);
		
		Gear otherGear = collider.GetComponentInParent<Gear> ();
		if (!otherGear)
			return;
			
		GearManager.Instance().DisconnectGears(this, otherGear);	
	}	
}
