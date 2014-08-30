using UnityEngine;
using System.Collections;

public class VRSwitch : MonoBehaviour {

	public GameObject screenPlayerController;
	public GameObject ovrPlayerController;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("z")) {

			
			if (screenPlayerController.activeSelf) {
				screenPlayerController.SetActive(false);
				ovrPlayerController.SetActive(true);
			} else {
				ovrPlayerController.SetActive(false);
				screenPlayerController.SetActive(true);
			}
		}
	}
}
