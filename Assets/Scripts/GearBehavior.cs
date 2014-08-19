using UnityEngine;
using System.Collections.Generic;

public class GearBehavior : MonoBehaviour {
	
	public enum SpinDirection {
		SPIN_NONE,
		SPIN_CLOCKWISE,
		SPIN_COUNTERCLOCKWISE
	}

	public SpinDirection startingTorque = SpinDirection.SPIN_NONE;

	public static List<GearBehavior> powerGears = new List<GearBehavior>();

	private SpinDirection spinDirection = SpinDirection.SPIN_NONE;
	private float rotationSpeed = 45.0f;
	private List<GearBehavior> connectedGears = new List<GearBehavior>();

	public void ConnectWithGear(GearBehavior otherGear) {
		if (connectedGears.Contains (otherGear))
			return;

		if (otherGear == this)
			return;

		connectedGears.Add (otherGear);
		if (spinDirection == SpinDirection.SPIN_NONE && otherGear.spinDirection != SpinDirection.SPIN_NONE) {
			StartSpinning (otherGear);
		} else if (otherGear.spinDirection == SpinDirection.SPIN_NONE && spinDirection != SpinDirection.SPIN_NONE) {
			otherGear.StartSpinning (this);
		}
	}

	public void DisconnectGear(GearBehavior otherGear) {
		if (!connectedGears.Contains (otherGear))
			return;

		connectedGears.Remove (otherGear);

		bool anotherConnectedSpinningGear = false;
		foreach (GearBehavior g in connectedGears) {
			if (g.spinDirection != SpinDirection.SPIN_NONE) {
				anotherConnectedSpinningGear = true;
				break;
			}
		}

		if (!anotherConnectedSpinningGear)
			StopSpinning ();
	}

	public void StopSpinning() {
		if (spinDirection == SpinDirection.SPIN_NONE)
			return;

		spinDirection = SpinDirection.SPIN_NONE;
	}

	void StartSpinning(GearBehavior poweredGear) {
		SpinDirection otherDirection;
		if (poweredGear == null) {
			otherDirection = startingTorque;
		} else {
			otherDirection = poweredGear.spinDirection;
			float poweredGearRotation = poweredGear.transform.localEulerAngles.y;
			transform.Rotate(0, 0, poweredGearRotation - transform.localEulerAngles.y + 24.0f);
		}
		
		if (spinDirection != SpinDirection.SPIN_NONE)
			throw new UnityException("Shouldn't StartSpinning without a SpinDirection");
		
		switch (otherDirection) {
		case SpinDirection.SPIN_CLOCKWISE: 
			spinDirection = SpinDirection.SPIN_COUNTERCLOCKWISE;
			break;
		case SpinDirection.SPIN_COUNTERCLOCKWISE: 
			spinDirection = SpinDirection.SPIN_CLOCKWISE; 
			break;
		case SpinDirection.SPIN_NONE: throw new UnityException("Shouldn't start spinning with SPIN_NONE");
		}

		foreach (GearBehavior g in connectedGears) {
			if (g.spinDirection == SpinDirection.SPIN_NONE)
				g.StartSpinning(this);
		}
	}
	

	// Use this for initialization
	void Start () {
		if (startingTorque != SpinDirection.SPIN_NONE) {
			powerGears.Add(this);
			StartSpinning(null);
		}
	}
	
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
	// Update is called once per frame
	void Update () {
		// Only spin if right mouse button down
//		if (!Input.GetMouseButton(1))
//			return;
		if (spinDirection == SpinDirection.SPIN_CLOCKWISE)
			transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
		else if (spinDirection == SpinDirection.SPIN_COUNTERCLOCKWISE)
			transform.Rotate(0,0,-rotationSpeed * Time.deltaTime);
	}
}
