using UnityEngine;
using System.Collections;

public class DetectConnectedGears : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		print("Enter trigger with " + collider.transform.name);
		GearBehavior gear = GetComponentInParent<GearBehavior> ();

		if (!gear)
			return;

		GearBehavior otherGear = collider.GetComponentInParent<GearBehavior> ();
		if (!otherGear)
			return;
				
		gear.ConnectWithGear (otherGear);
	}

	void OnTriggerExit(Collider collider) {
		print("Exit trigger with " + collider.transform.name);
		
		GearBehavior gear = GetComponentInParent<GearBehavior> ();
		
		if (!gear)
			return;
		
		GearBehavior otherGear = collider.GetComponentInParent<GearBehavior> ();
		if (!otherGear)
			return;

		gear.DisconnectGear (otherGear);
	}

}
