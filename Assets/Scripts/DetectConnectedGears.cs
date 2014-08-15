using UnityEngine;
using System.Collections;

public class DetectConnectedGears : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		GearBehavior gear = GetComponentInParent<GearBehavior> ();

		if (!gear)
			return;

		GearBehavior otherGear = collider.GetComponentInParent<GearBehavior> ();
		if (!otherGear)
			return;
				
		gear.ConnectWithGear (otherGear);
	}

	void OnTriggerExit(Collider collider) {
		GearBehavior gear = GetComponentInParent<GearBehavior> ();
		
		if (!gear)
			return;
		
		GearBehavior otherGear = collider.GetComponentInParent<GearBehavior> ();
		if (!otherGear)
			return;

		gear.DisconnectGear (otherGear);
	}

}
