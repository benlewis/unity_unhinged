using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearManager : MonoBehaviour {

	private List<Gear> gears = ArrayList<Gear>();

	public void AddGear(Gear g) {
		// Keep track of all gears in the scene
		if (!gears.Contains(g))
			gears.Add(g);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
