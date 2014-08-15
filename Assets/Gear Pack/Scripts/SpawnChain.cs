using UnityEngine;
using System.Collections;

public class SpawnChain : MonoBehaviour {
	public GameObject OddLink;
	public GameObject EvenLink;
	
	public int LinkCount = 10;
	
	public Vector3 LinkOffset;
	
	// Use this for initialization
	void Start () {
		
		
		GameObject lastlink = null;
		
		for (int i = 0; i < LinkCount; i++) {
			Vector3 pos = this.transform.position;
			pos = new Vector3(pos.x + LinkOffset.x * i, pos.y + LinkOffset.y * i, pos.z + LinkOffset.z * i);
			GameObject o;
			
			if (i % 2 == 0) {
				o= (GameObject)Instantiate(EvenLink, pos, this.transform.rotation);	
			}else{
				o= (GameObject)Instantiate(OddLink, pos, this.transform.rotation);	
			}
			o.name = (i.ToString() + "-link");
			o.transform.parent = this.transform;
			ConfigurableJoint j = o.GetComponent<ConfigurableJoint>();
			
			j.anchor = new Vector3(0.0f,0.05f,0.905f);
			
			
			if (lastlink != null){
				j.connectedBody = lastlink.GetComponent<Rigidbody>();
				
			}
			lastlink = o;
			
			if(i == 0){
				//Rigidbody r = o.GetComponent<Rigidbody>();
				//r.useGravity = false;
				//r.isKinematic = true;
				Destroy(j);	
			}
			
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
