
using System.Collections;
using UnityEngine;

public class TVector3 {

	public string Tag ="";
	private Vector3 _v = Vector3.zero;

	public TVector3(float x, float y, float z, string tag){
		
		_v = new Vector3(x, y, z);
		Tag = tag;
		
	}
	
	public Vector3 Value{
		get {return _v;} 
		set {_v = value;}
	}

	public static Vector3 zero = Vector3.zero;
	
	public static TVector3 Tzero = new TVector3(0,0,0,"");

    public TVector3 Copy()
    {
        TVector3 ret = new TVector3(_v.x, _v.y, _v.z, this.Tag);


        return ret;
    }


    public override bool Equals(object obj)
    {
        bool ret = false;
        TVector3 comp = (TVector3)obj;

        if (comp.Value.x == this.Value.x && comp.Value.y == this.Value.y && comp.Value.z == this.Value.z)
        {
            ret = true;

        }

        return ret;
    }

    public override int GetHashCode()
    {
 	     return base.GetHashCode();
    }

    public bool TagContains(string[] Tags)
    {
        for (int i = 0; i < Tags.Length; i++)
        {
            if (this.Tag == Tags[i])
            {
                return true;
            }
        }

        return false;

    }
	

	


	
}
