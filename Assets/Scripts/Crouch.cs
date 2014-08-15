using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour {

	public float crouchingHeight = 1.0f;
	public float standingHeight = 2.0f;
	public float crouchTime = 0.3f; // 2 seconds to crouch

	// Update is called once per frame
	void Update () {
		CharacterController cc = GetComponent<CharacterController>();
		if (Input.GetButton("Crouch")) {
			if (cc.height > crouchingHeight) {
				float heightShift = Mathf.Min(Time.deltaTime / crouchTime * (standingHeight - crouchingHeight), 
				                              cc.height - crouchingHeight);
				cc.height -= heightShift;
				cc.transform.Translate(0, -heightShift / 2.0f, 0);
			}
		} else if (cc.height < standingHeight) {
			float heightShift = Mathf.Min(Time.deltaTime / crouchTime * (standingHeight - crouchingHeight), 
			                              standingHeight - cc.height);
			cc.height += heightShift;
			cc.transform.Translate(0, heightShift / 2.0f, 0);			                       	
		}
//		if (Input.GetKeyDown (KeyCode.LeftShift) && !isCrouching) {
//			isCrouching = true;
//			cc.height = 1.0f;
//			cc.transform.Translate(0, -0.5f, 0);
//		}
//
//		if (Input.GetKeyUp (KeyCode.LeftShift) && isCrouching) {
//			isCrouching = false;
//			cc.height = 2.0f;
//			cc.transform.Translate(0, 0.5f, 0);
//		}

	}

}
