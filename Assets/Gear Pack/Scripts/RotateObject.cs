using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {
	
	public float xRotSpeed = 0.0f;
	public float yRotSpeed = 0.0f;
	public float zRotSpeed = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float dt =Time.deltaTime;
		
		transform.rotation = transform.rotation * Quaternion.AngleAxis(xRotSpeed * dt, Vector3.forward);
		transform.rotation = transform.rotation * Quaternion.AngleAxis(yRotSpeed * dt, Vector3.left);
		transform.rotation = transform.rotation * Quaternion.AngleAxis(zRotSpeed * dt, Vector3.up);
		
	}
		
	
}
