
using System.Collections;
using UnityEngine;

public class TVector2 {
	public string Tag ="";
	private Vector2 _v = Vector2.zero;

	public TVector2(float x, float y, string tag){
		
		_v = new Vector2(x, y);
		Tag = tag;
		
	}
	
	public Vector2 Value{
		get {return _v;} 
		set {_v = value;}
	}

	public static Vector2 zero = Vector2.zero;
	
	public static TVector2 Tzero = new TVector2(0,0,"");

    public TVector2 Copy()
    {
        TVector2 ret = new TVector2(_v.x, _v.y, Tag);


        return ret;
    }
	
}
