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
			StartSpinning (otherGear.spinDirection);
		} else if (otherGear.spinDirection == SpinDirection.SPIN_NONE && spinDirection != SpinDirection.SPIN_NONE) {
			otherGear.StartSpinning (spinDirection);
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

	void StopSpinning() {
		if (spinDirection == SpinDirection.SPIN_NONE)
			return;

		spinDirection = SpinDirection.SPIN_NONE;
	}

	void StartSpinning(SpinDirection otherDirection) {
		if (spinDirection != SpinDirection.SPIN_NONE)
			return;

		switch (otherDirection) {
		case SpinDirection.SPIN_CLOCKWISE: 
			spinDirection = SpinDirection.SPIN_COUNTERCLOCKWISE;
			transform.Rotate(0, 0, 24.0f); // give this a nice offset
			Debug.Log("Setting offset for " + name);
			break;
		case SpinDirection.SPIN_COUNTERCLOCKWISE: 
			spinDirection = SpinDirection.SPIN_CLOCKWISE; 
			break;
		case SpinDirection.SPIN_NONE: break;
		}

		foreach (GearBehavior g in connectedGears) {
			g.StartSpinning(spinDirection);
		}
	}
	

	// Use this for initialization
	void Start () {
		if (startingTorque != SpinDirection.SPIN_NONE) {
			powerGears.Add(this);
			StartSpinning(startingTorque);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (spinDirection == SpinDirection.SPIN_CLOCKWISE)
			transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
		else if (spinDirection == SpinDirection.SPIN_COUNTERCLOCKWISE)
			transform.Rotate(0,0,-rotationSpeed * Time.deltaTime);
	}
}
