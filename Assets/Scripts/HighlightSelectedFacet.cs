using UnityEngine;
using System.Collections;

public class HighlightSelectedFacet : MonoBehaviour {

	public void TurnOnHighlight() {
		transform.GetComponent<MeshRenderer>().enabled = true;
	}
	
	public void TurnOffHighlight() {
		transform.GetComponent<MeshRenderer>().enabled = false;
	}
}
