using UnityEngine;
using System.Collections;

public class Gear : MonoBehaviour {

	public enum SpinDirection {
		NONE,
		CLOCKWISE,
		COUNTERCLOCKWISE
	};
	private SpinDirection spin = SpinDirection.NONE;
	
	// Use the GearManager to do most of the gear logic
	private GearManager m;
	
	[HideInInspector]
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		m = GameObject.Find("Game Manager").GetComponent<GearManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (spin == SpinDirection.CLOCKWISE)
			transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
		else if (spin == SpinDirection.COUNTERCLOCKWISE)
			transform.Rotate(0,0,-rotationSpeed * Time.deltaTime);
	}
}
