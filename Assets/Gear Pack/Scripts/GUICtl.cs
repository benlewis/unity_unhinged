using UnityEngine;
using System.Collections;

public class GUICtl : MonoBehaviour {
	
	public Texture Logo;
	
	void Start () {
	
	}
	
	void OnGUI () {
		
		int Offset = 30;
		int pos = 10;
		
		GUI.Box(new Rect(10,10,150,120), "Loader Menu");
		
		pos += Offset;
		if(GUI.Button(new Rect(20,pos,120,20), "Gallery")) {
			Application.LoadLevel(2);
		}
		
		pos += Offset;
		if(GUI.Button(new Rect(20,pos,120,20), "Gear Demo")) {
			Application.LoadLevel(0);
		}
		
		pos += Offset;
		if(GUI.Button(new Rect(20,pos,120,20), "Chain Demo")) {
			Application.LoadLevel(1);
		}
		
		if(Logo != null){
			float height = 255.0f;
			float width = 340.0f;
		 	GUI.DrawTexture(new Rect(Screen.width - width, Screen.height - height, width, height), Logo);
		}

	}

}
