using UnityEngine;
using System.Collections;

public class GearSupport : MonoBehaviour {
	
	
	public float OutsideToothRadius = 1.2f;    
	public float InnerToothRadius = 1.0f;
	public float ToothRotation = 0.0f;
	public int QtyTeeth = 22;
	public float ToothWidthPct = 15.0f;
	public float RingRadius = 0.0f;
	public Material GearMaterial;
	public float Height = 0.2f;
	public float TopRotation = 0.0f;
	public float TopScalePct = 100.0f;
	
	public int SpokeCount = 0;
	public float SpokeIndent = 0.1f;
	public GearSupport.SpokeTypes SpokeType = GearSupport.SpokeTypes.None;
	public float hubHeight = 0.1f;
	public float HubRadius = 0.01f;
	public int HubSides = 12;
	public float SpokeWidthInner = 0.01f;
	public float SpokeWidthOuter = 0.01f;
	public bool LockRatio = true;
	
	public Material DebugTextMaterial;
	public Font DebugTextFont;
	public GearRenderer RenderedGear;
	
	public enum SpokeTypes{
			None,
			Solid,
			Straight
			//,Curved //Not Yet Implemented.
			
		}
	
	private GearRenderer _gr;
	
	
	public static string GetGearName(){
		string ret = "Gear9999";
		
		for (int i = 1; i < 10000; i++) {
			GameObject o = GameObject.Find("Gear" + i.ToString("000"));
			
			if (o == null){
				ret = "Gear" + i.ToString("000");	
				break;	
			}
			
		}
		
		return ret;
	}
	
	/*
	//For debugging verts & tris.
	private GameObject CreateTextMesh(GameObject parent, string Text, Vector3 pos){
		
		if(Text == null){
			Text = "null";	
		}
		
		GameObject gt = new GameObject(Text);
		TextMesh tm = (TextMesh)gt.AddComponent( typeof(TextMesh) );
		gt.AddComponent( typeof(MeshRenderer) );
		//gt.renderer.material = ;
		tm.text = Text;
		tm.fontSize = 0;
		tm.characterSize = 0.05f;
		tm.font = DebugTextFont;
		MeshRenderer mr = gt.GetComponent<MeshRenderer>();
		mr.material = DebugTextMaterial;
		gt.transform.parent = parent.transform;
		pos = new Vector3(pos.x, pos.y, Random.Range(0,1.0f) + pos.z);
		gt.transform.position = pos;
		tm.anchor = TextAnchor.MiddleCenter;
		//tm.font = guiFont;

		
		return gt;
	}
	*/
	
	public void ProcessGear(GameObject Gear, Vector3 origin){
		System.Collections.Generic.List<Vector3> verts = new System.Collections.Generic.List<Vector3>();
		System.Collections.Generic.List<int> triInt = new System.Collections.Generic.List<int>();
		System.Collections.Generic.List<Vector2> uvs = new System.Collections.Generic.List<Vector2>();
		
		GearRenderer g = new GearRenderer();
		
		g.Height = Height;
		g.HubSides = HubSides;
		g.HubHeight = Height;
		g.HubRadius = HubRadius;
		g.InnerRadius = InnerToothRadius;
		g.OuterRadius = OutsideToothRadius;
		g.RingRadius = RingRadius;
		g.OuterWidthPct = ToothWidthPct / 100.0f;
		g.ToothRotation = ToothRotation;
		g.TopScale = TopScalePct / 100.0f;
		g.TopRotation = TopRotation;
		g.SpokeCount = SpokeCount;
		g.QtyTeeth = QtyTeeth;
		g.SpokeWidthInner = SpokeWidthInner;
		g.SpokeWidthOuter = SpokeWidthOuter;
		g.SpokeType = SpokeType;
		g.SpokeIndent = SpokeIndent;
		g.HubHeight = hubHeight;
		
		
		
		var shape = g.GetRingGear();
		var tris = shape.tris;
		var tuvs = shape.UV;
		
		Mesh m = new Mesh();
		
		
		
		int i = 0;
		foreach(var tri in tris){
			//CreateTextMesh(o, tri.Tag + " " + tri.SortOrder.ToString(), tri.Center());
			
			
			//CreateTextMesh(o, tri.Tag + "-a:" + tri.a.Tag, tri.a.Value);
			//CreateTextMesh(o, tri.Tag + "-b:" + tri.b.Tag, tri.b.Value);
			//CreateTextMesh(o, tri.Tag + "-c:" + tri.c.Tag, tri.c.Value);
			
			
			/*
			if (!vertcache.ContainsKey(tri.a.Value)){
				vertcache.Add(tri.a.Value, i);
				verts.Add(tri.a.Value);
				i += 1;
			}
			triInt.Add(vertcache[tri.a.Value]);
			
			if (!vertcache.ContainsKey(tri.b.Value)){
				vertcache.Add(tri.b.Value, i);
				verts.Add(tri.b.Value);
				i += 1;
			}
			triInt.Add(vertcache[tri.b.Value]);
			
			if (!vertcache.ContainsKey(tri.c.Value)){
				vertcache.Add(tri.c.Value,i);
				verts.Add(tri.c.Value);
				i += 1;
			}
			triInt.Add(vertcache[tri.c.Value]);
			 */
			//i += 1;
			
			//Raw tris...
			verts.Add(tri.a.Value);
			triInt.Add(i);
			i += 1;
			
			verts.Add(tri.b.Value);
			triInt.Add(i);
			i += 1;
			
			verts.Add(tri.c.Value);
			triInt.Add(i);
			i += 1;
		
		}
		
		foreach(var uv in tuvs){
			uvs.Add(uv.Value);
		}
		
		MeshFilter f = Gear.GetComponent<MeshFilter>();
		
		if(f == null){
			f = Gear.AddComponent<MeshFilter>();
		}
		
		MeshRenderer r = Gear.GetComponent<MeshRenderer>();
		if(r == null){
			r = Gear.AddComponent<MeshRenderer>();
		}
				
		//Debug Stuff
		//GearDebugHelper h = o.AddComponent<GearDebugHelper>();
		/*
		if(Gear.GetComponent<GearTuner>() == null){
			var tuner = Gear.AddComponent<GearTuner>();
			
			tuner.RenderedGear = g;
			
			
			
		}
		*/
		
		
		//h.Tris = tris;
		//h.ShowDebugInfo = true;
		
		f.mesh = m;
		
		m.vertices = verts.ToArray();
		m.triangles = triInt.ToArray();
		m.uv = uvs.ToArray();
		m.RecalculateNormals();
		m.Optimize();
		r.material = this.GearMaterial;

		
	}
	
	
	
	
	
	
}
