using UnityEngine;
using System.Collections;

public class SpawnChainPath : MonoBehaviour {
	
	private System.Collections.Generic.SortedList<string, GameObject> _nodes = new System.Collections.Generic.SortedList<string, GameObject>();
	private LineRenderer _LineRenderer;
	
	public GameObject OddLink;
	public GameObject EvenLink;
	
	private GameObject[] _Links;
	
	private int _CurrentNode = 0;
	
	public float Speed = 0.5f;
	public float NextLinkDistance = 0.08f;
	
	// Use this for initialization
	void Start () {
		
		//Get Objects that represent Path Nodes...
		int ChildCount = 0;
		int LinkCount = 0;
		GameObject lookobj;
		System.Collections.Generic.List<GameObject> links = new System.Collections.Generic.List<GameObject>();
		
		
		ChildCount = transform.childCount;
		
		for (int i = 0; i < ChildCount; i++) {
			Transform t = this.transform.GetChild(i);
			if (t.gameObject.name.ToLower().StartsWith("node")) {
				_nodes.Add(t.gameObject.name,t.gameObject);
			}
		}
		
		LinkCount = _nodes.Count;
		
		for (int i = 0; i < LinkCount; i++) {
			Vector3 pos = this.transform.position;
			
			pos = _nodes.Values[i].transform.position;
			
			GameObject o;
		
			
			if (i % 2 == 0) {
				o= (GameObject)Instantiate(EvenLink, pos, Quaternion.identity);	
			}else{
				o= (GameObject)Instantiate(OddLink, pos, Quaternion.identity);	
			}
			o.name = (i.ToString() + "-link");
			o.transform.parent = this.transform;

			//Calc the 'look ahead'
			if(i!= LinkCount-1){
				lookobj = _nodes.Values[i + 1];
			}else{
				lookobj =_nodes.Values[0];
			}
			
			//Set Link Orientation.
			o.transform.LookAt(lookobj.transform, o.transform.up);
			
			links.Add(o);
			
			
		}
			
			_Links = links.ToArray();
	
	}
		
	// Update is called once per frame
	void Update () {
	
		//Get 0'th node...
		GameObject anchor;
		float curdistance = 0;
		float nxtdistance = 0;
		int Links = _Links.Length-1;

		Vector3 center;
		float dir = 1;
		float dt = Time.deltaTime;
		int NextNode = 0;
		
		anchor = _Links[0];
		
		if(Speed == 0){
			dir = 0;
		}else{
			dir = Speed / Mathf.Abs(Speed);
		}
		
		NextNode = _CurrentNode + (int)dir;	
		NextNode = Clamp(NextNode, 0, Links);
		
		//Calc the distance to the current node...
		try {
			curdistance = Vector3.Distance(anchor.transform.position,_nodes.Values[_CurrentNode].transform.position);
			nxtdistance = Vector3.Distance(anchor.transform.position,_nodes.Values[NextNode].transform.position);
		} catch (System.Exception ex) {
			Debug.Log("CurrentNode :" + _CurrentNode + " Next Node:" + NextNode + " " + ex.Message);	
		}

		//When the target node gets close, then bump to next position...
		if (curdistance < NextLinkDistance || curdistance > nxtdistance) {// || distance > _LastDistance) {
			_CurrentNode = NextNode;
		}

		//Move everything...
		for (int i = 0; i < Links + 1; i++) {
			int SubNodePos = 0;
			int LinkPos = 0;
			
			GameObject link;
			GameObject linktarget;
			
			LinkPos = i;
			SubNodePos = _CurrentNode + i;	
			
			LinkPos = Clamp(LinkPos, 0, Links);
			SubNodePos = Clamp(SubNodePos, 0, Links);
			
			link = _Links[LinkPos];
			linktarget = _nodes.Values[SubNodePos];
			
			link.transform.position = Vector3.MoveTowards(link.transform.position, linktarget.transform.position, dt * Mathf.Abs(Speed));
			
		}
		
		center = GetCenter(); //Assuming a semi uniform closed loop.
		
		//Rotate Everything...
		for (int i = 0; i < Links + 1; i++) {
			int LookNodePos = i +1;
			GameObject link;
			GameObject linktarget;
			
			if (LookNodePos > Links){
				LookNodePos = LookNodePos % (Links + 1);	
			}
			
			link = _Links[i];
			linktarget = _Links[LookNodePos];
			
			link.transform.LookAt(linktarget.transform.position, this.transform.TransformDirection(link.transform.position - center));

		}
		
	}
		
	private int Clamp(int Current, int Min, int Max){
		int ret = 0;
		
		ret = Current;
		
		if (Current < Min) {
			ret = Max - (Current % (Max + 1));
		}
		
		if (Current > Max){
			ret = Min + (Current % (Max + 1));	
		}
		
		return ret;
		
	}
		
	private Vector3 GetCenter(){
		Vector3 ret = Vector3.zero;
		
		for (int i = 0; i < _Links.Length; i++) {
			ret += _Links[i].transform.position;
			
			
		}
		
		ret /= _Links.Length;
		
		return ret;
	}
	
	
	
	
}
