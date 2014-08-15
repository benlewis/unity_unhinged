using UnityEngine;
using System.Collections;
using UnityEditor;

class GearWorx : EditorWindow {
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
	
	//public Material DebugTextMaterial;
	//public Font DebugTextFont;
	
	// Add menu link... 
	[MenuItem ("GameObject/Create Other/Gears By BrainTwinkie/Create Gear")]    static void Init () {        
		// Get existing open window or if none, make a new one:        
		GearWorx window = (GearWorx)EditorWindow.GetWindow (typeof (GearWorx));    
		window.title = "Create Gear";
	}
	
	void OnGUI () {
		bool Error = false;
		//int OrigToothCount = QtyTeeth;
		//float ScaleFact = 1.0f;
		//float ToothHeight = 0.0f;
		
		GUILayout.Label ("Create Gear (Gears By BrainTwinkie)", EditorStyles.boldLabel);            
	
		QtyTeeth = EditorGUILayout.IntSlider("Qty Teeth", QtyTeeth, 1, 1000,GUILayout.ExpandWidth(true) );               
		//LockRatio = EditorGUILayout.Toggle("Lock Tooth/Radius Ratio", LockRatio); 
		/*
		if(LockRatio){
			ScaleFact = (float)QtyTeeth / (float) OrigToothCount;
			ToothHeight = OutsideToothRadius - InnerToothRadius;
			
			
			InnerToothRadius *= ScaleFact;
			OutsideToothRadius = InnerToothRadius + ToothHeight;
			//InnerToothRadius *= ScaleFact;
		}
		*/
		OutsideToothRadius = EditorGUILayout.FloatField("Outside Tooth Radius", OutsideToothRadius);                
		
		InnerToothRadius = EditorGUILayout.FloatField("Base Tooth Radius", InnerToothRadius);               
		
		RingRadius = EditorGUILayout.FloatField("Ring Radius", RingRadius); 
		
		Height = EditorGUILayout.FloatField("Height", Height);
		
		if(InnerToothRadius != 0){
			SpokeType = (GearSupport.SpokeTypes) EditorGUILayout.EnumPopup("Spoke Type", SpokeType,GUILayout.ExpandWidth(true) );               
			
			if(SpokeType != GearSupport.SpokeTypes.None && SpokeType != GearSupport.SpokeTypes.Solid){
				SpokeCount = EditorGUILayout.IntSlider("Spoke Count", SpokeCount, 1, 10,GUILayout.ExpandWidth(true) );               
			}
			
			if(SpokeType != GearSupport.SpokeTypes.None){
				SpokeIndent = EditorGUILayout.FloatField("Spoke Indent", SpokeIndent);               
	
			}
			
			if(SpokeType == GearSupport.SpokeTypes.Straight){
				hubHeight = EditorGUILayout.FloatField("Hub Height", hubHeight);  
				HubRadius = EditorGUILayout.FloatField("Hub Radius", HubRadius);    
				HubSides = EditorGUILayout.IntSlider("Hub Sides", HubSides, 1, 100, GUILayout.ExpandWidth(true));    
				SpokeWidthInner = EditorGUILayout.FloatField("Spoke Width (Inner)", SpokeWidthInner);
				SpokeWidthOuter = EditorGUILayout.FloatField("Spoke Width (Outer)", SpokeWidthOuter);
				
				if(hubHeight >= Height){
					EditorGUILayout.LabelField("Warning! Hub Height should be less than Gear Height.", GUILayout.ExpandWidth(true));	
				}
				
			}
			
			
			EditorGUILayout.LabelField("", GUILayout.ExpandWidth(true));	
		}
		
		TopScalePct = EditorGUILayout.FloatField("Top Scale %", TopScalePct);  
		
		TopRotation = EditorGUILayout.FloatField("Top Rotation (Deg)", TopRotation);      
		
		ToothRotation = EditorGUILayout.FloatField("Tooth Rotation (Deg)", ToothRotation);               
		
		ToothWidthPct = EditorGUILayout.FloatField("Outside Tooth Width %", ToothWidthPct);  
			
		GearMaterial = (Material)EditorGUILayout.ObjectField("Gear Material",(Material)GearMaterial, typeof(Material), false, GUILayout.Height(16));
		//DebugTextMaterial = (Material)EditorGUILayout.ObjectField("DebugText Material",(Material)DebugTextMaterial, typeof(Material), false, GUILayout.Height(16));
		//DebugTextFont = (Font)EditorGUILayout.ObjectField("DebugText Font",(Font)DebugTextFont, typeof(Font), false, GUILayout.Height(16));
		//DebugTextMaterial = DebugTextFont.material;
		
		
		EditorGUILayout.LabelField("", GUILayout.ExpandWidth(true));	
		
		if(RingRadius > InnerToothRadius){
			EditorGUILayout.LabelField("ERROR! Ring Radius larger than the Inner tooth radius.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(RingRadius < 0){
			EditorGUILayout.LabelField("ERROR! Ring Radius must be greater than or 0.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(ToothWidthPct < 0){
			EditorGUILayout.LabelField("ERROR! Tooth Width % must be greater than or 0.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(OutsideToothRadius < InnerToothRadius){
			EditorGUILayout.LabelField("ERROR! Outside Tooth Radius must be greater than the Inner Tooth Radius.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(Height <=0){
			EditorGUILayout.LabelField("ERROR! Height must be greater than 0.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(SpokeType == GearSupport.SpokeTypes.Straight){
			float inctheta;
			float spokeadj;
			inctheta = 2.0f * (Mathf.PI / QtyTeeth);
			spokeadj = 2.0f * Mathf.Asin(SpokeWidthInner / (8.0f * HubRadius)); //Calc'd using cord of circ.
			
			if(inctheta - spokeadj <= spokeadj){
				EditorGUILayout.LabelField("Warning! Spoke width and hub radius may create overlapping verts.", GUILayout.ExpandWidth(true));	
				//Error = true;
			}
		}
		
		if(GearMaterial == null){
			EditorGUILayout.LabelField("Warning! Material Not Assigned.", GUILayout.ExpandWidth(true));	
		}
		
		EditorGUILayout.LabelField("", GUILayout.ExpandWidth(true));	
		if(!Error){
			if(GUILayout.Button ("Create")){
				CreateGear();
			}
		}else{
			EditorGUILayout.LabelField("Clear errors to create gears.", GUILayout.ExpandWidth(true));	
		}
		
	}
	
	public void CreateGear(){
		GameObject o = new GameObject(GearSupport.GetGearName());
		CreateGear(o);
		
	}	
	
	public void CreateGear(GameObject Gear){
		GearRenderer g = new GearRenderer();
		Vector3 origin;
		
		var sv = SceneView.currentDrawingSceneView;
		
		if(sv == null && SceneView.lastActiveSceneView != null){
			sv = SceneView.lastActiveSceneView;
		}
		
		if(sv == null){
			Debug.LogError("No Scene present!");
			return;
		}
		
		Camera cam = SceneView.lastActiveSceneView.camera;
		
		Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth / 2.0f, cam.pixelHeight / 2.0f,0));
			
		origin = ray.GetPoint(5.0f);
		
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
		
		
		GearSupport gs = Gear.GetComponent<GearSupport>();
		if(gs == null){
			gs = Gear.AddComponent<GearSupport>();
			
		}
		
		gs.GearMaterial = GearMaterial;
		gs.RenderedGear = g;
		
		gs.OutsideToothRadius = this.OutsideToothRadius;    
		gs.InnerToothRadius = this.InnerToothRadius;
		gs.ToothRotation = this.ToothRotation;
		gs.QtyTeeth = this.QtyTeeth;
		gs.ToothWidthPct = this.ToothWidthPct;
		gs.RingRadius = this.RingRadius;
		gs.GearMaterial = this.GearMaterial;
		gs.Height = this.Height;
		gs.TopRotation = this.TopRotation;
		gs.TopScalePct = this.TopScalePct;
		
		gs.SpokeCount = this.SpokeCount;
		gs.SpokeIndent = this.SpokeIndent;
		gs.SpokeType = this.SpokeType;
		gs.hubHeight = this.hubHeight;
		gs.HubRadius = this.HubRadius;
		gs.HubSides = this.HubSides;
		gs.SpokeWidthInner = this.SpokeWidthInner;
		gs.SpokeWidthOuter = this.SpokeWidthOuter;
		gs.LockRatio = this.LockRatio;
		
		//gs.DebugTextMaterial = this.DebugTextMaterial;
		//gs.DebugTextFont = this.DebugTextFont;

		gs.ProcessGear(Gear, origin);
		
		Gear.transform.position = origin;

		Debug.Log("Gear Rendered " + origin.ToString());
		
	}
	
	
}
