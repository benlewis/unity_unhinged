using UnityEngine;
using System.Collections;

public class ForceRotate : MonoBehaviour {

	public bool forcedRotate = false;

	// Use this for initialization
	void Start () {
		if (forcedRotate)
			rigidbody.AddRelativeTorque (0.0f, 0.0f, 50.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(0,0,Time.deltaTime);
		//rigidbody.AddTorque (0, 1, 0);
		Debug.Log (rigidbody.angularVelocity);
		if (forcedRotate && rigidbody.angularVelocity.z < 2.0f) {
			float torqueDiff = (2.0f - rigidbody.angularVelocity.z) * 25.0f;
			rigidbody.AddRelativeTorque (0.0f, 0.0f, torqueDiff);
		}
	}
}
