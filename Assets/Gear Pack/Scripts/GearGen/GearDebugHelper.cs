using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GearDebugHelper : MonoBehaviour {
	
	public Triangle3[] Tris = new Triangle3[]{};
	public bool ShowDebugInfo = false;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	    void OnDrawGizmos() {
       
		
		if(ShowDebugInfo){
			foreach (var tri in Tris) {

				var InnerList = new string[] {GearRenderer.TRI_SideInnerTop, GearRenderer.TRI_SideInnerBottom};
				if(GearRenderer.TagContains(tri.Tag, InnerList)){
					//Calc Normal of tri...
					Vector3 d = tri.Center();
					Vector3 n = tri.GetNormal();

					float norma = Mathf.Atan2(n.y, n.x);
					float xdira = norma + Mathf.PI / 2.0f;
						
					Vector3 xdir = new Vector3(n.magnitude * Mathf.Cos(xdira), n.magnitude * Mathf.Sin(xdira), n.z);

					Gizmos.color = Color.red;
					Gizmos.DrawRay(d, xdir * 1.0f);
					Gizmos.color = Color.yellow;
					Gizmos.DrawRay(d, n * 1.0f);
		
					
				}

			
			var OuterList = new string[] {GearRenderer.TRI_SideOuterTop,GearRenderer.TRI_SideOuterBottom };
				if(GearRenderer.TagContains(tri.Tag, OuterList)){
					//Calc Normal of tri...
					Vector3 d = tri.Center();
					Vector3 n = tri.GetNormal();
					//Vector3 xdir = ???
					//x = (p-o) dot x
					//y=(p-o) dot (n cross x)
					float norma = Mathf.Atan2(n.y, n.x);
					float xdira = norma + Mathf.PI / 2.0f;
						
					Vector3 xdir = new Vector3(n.magnitude * Mathf.Cos(xdira), n.magnitude * Mathf.Sin(xdira), n.z);
					
					Gizmos.color = Color.red;
					Gizmos.DrawRay(d, xdir * 1.0f);
					Gizmos.color = Color.yellow;
					Gizmos.DrawRay(d, n * 1.0f);
					
					
				}
			
			
			
			
			
			
			}
		}
	
	}
}
