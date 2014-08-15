using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(GearSupport))]public class GearTuner : Editor {
	private GearSupport _gs;
	private		HOEditorUndoManager		undoManager;

	// Use this for initialization
	void Start () {
	 
	}
 	void OnEnable(){
		_gs = (GearSupport) target;
		undoManager = new HOEditorUndoManager( _gs, "Gear Editor" );
		
	}
	
	
	
	override public void OnInspectorGUI  () {
		bool Error = false;
		//int OrigToothCount = _gs.QtyTeeth;
		//float ScaleFact = 1.0f;
		//float ToothHeight = 0.0f;
		undoManager.CheckUndo(); 
		GUILayout.Label ("Modify Gear (Gears By BrainTwinkie)", EditorStyles.boldLabel);            
	
		_gs.QtyTeeth = EditorGUILayout.IntSlider("Qty Teeth", _gs.QtyTeeth, 1, 1000,GUILayout.ExpandWidth(true) );               
		//_gs.LockRatio = EditorGUILayout.Toggle("Lock Tooth/Radius Ratio", _gs.LockRatio); 
		/*
		if(_gs.LockRatio){
			ScaleFact = (float)_gs.QtyTeeth / (float) OrigToothCount;
			ToothHeight = _gs.OutsideToothRadius - _gs.InnerToothRadius;
			
			
			_gs.InnerToothRadius *= ScaleFact;
			_gs.OutsideToothRadius = _gs.InnerToothRadius + ToothHeight;
			//_gs.InnerToothRadius *= ScaleFact;
		}
		*/
		_gs.OutsideToothRadius = EditorGUILayout.FloatField("Outside Tooth Radius", _gs.OutsideToothRadius);                
		
		_gs.InnerToothRadius = EditorGUILayout.FloatField("Base Tooth Radius", _gs.InnerToothRadius);               
		
		_gs.RingRadius = EditorGUILayout.FloatField("Ring Radius", _gs.RingRadius); 
		
		_gs.Height = EditorGUILayout.FloatField("Height", _gs.Height);
		
		if(_gs.InnerToothRadius != 0){
			_gs.SpokeType = (GearSupport.SpokeTypes) EditorGUILayout.EnumPopup("Spoke Type", _gs.SpokeType,GUILayout.ExpandWidth(true) );               
			
			if(_gs.SpokeType != GearSupport.SpokeTypes.None && _gs.SpokeType != GearSupport.SpokeTypes.Solid){
				_gs.SpokeCount = EditorGUILayout.IntSlider("Spoke Count", _gs.SpokeCount, 1, 10,GUILayout.ExpandWidth(true) );               
			}
			
			if(_gs.SpokeType != GearSupport.SpokeTypes.None){
				_gs.SpokeIndent = EditorGUILayout.FloatField("Spoke Indent", _gs.SpokeIndent);               
	
			}
			
			if(_gs.SpokeType == GearSupport.SpokeTypes.Straight){
				_gs.hubHeight = EditorGUILayout.FloatField("Hub Height", _gs.hubHeight);  
				_gs.HubRadius = EditorGUILayout.FloatField("Hub Radius", _gs.HubRadius);    
				_gs.HubSides = EditorGUILayout.IntSlider("Hub Sides", _gs.HubSides, 1, 100, GUILayout.ExpandWidth(true));    
				_gs.SpokeWidthInner = EditorGUILayout.FloatField("Spoke Width (Inner)", _gs.SpokeWidthInner);
				_gs.SpokeWidthOuter = EditorGUILayout.FloatField("Spoke Width (Outer)", _gs.SpokeWidthOuter);
				
				if(_gs.hubHeight >= _gs.Height){
					EditorGUILayout.LabelField("Warning! Hub Height should be less than Gear Height.", GUILayout.ExpandWidth(true));	
				}

				
			}
			
			
			EditorGUILayout.LabelField("", GUILayout.ExpandWidth(true));	
		}
		
		_gs.TopScalePct = EditorGUILayout.FloatField("Top Scale %", _gs.TopScalePct);  
		
		_gs.TopRotation = EditorGUILayout.FloatField("Top Rotation (Deg)", _gs.TopRotation);      
		
		_gs.ToothRotation = EditorGUILayout.FloatField("Tooth Rotation (Deg)", _gs.ToothRotation);               
		
		_gs.ToothWidthPct = EditorGUILayout.FloatField("Outside Tooth Width %", _gs.ToothWidthPct);  
			
		_gs.GearMaterial = (Material)EditorGUILayout.ObjectField("Gear Material",(Material)_gs.GearMaterial, typeof(Material), false, GUILayout.Height(16));
		//DebugTextMaterial = (Material)EditorGUILayout.ObjectField("DebugText Material",(Material)DebugTextMaterial, typeof(Material), false, GUILayout.Height(16));
		//DebugTextFont = (Font)EditorGUILayout.ObjectField("DebugText Font",(Font)DebugTextFont, typeof(Font), false, GUILayout.Height(16));
		//DebugTextMaterial = DebugTextFont.material;
		
		
		EditorGUILayout.LabelField("", GUILayout.ExpandWidth(true));	
		
		if(_gs.RingRadius > _gs.InnerToothRadius){
			EditorGUILayout.LabelField("ERROR! Ring Radius larger than the Inner tooth radius.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(_gs.RingRadius < 0){
			EditorGUILayout.LabelField("ERROR! Ring Radius must be greater than or 0.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(_gs.ToothWidthPct < 0){
			EditorGUILayout.LabelField("ERROR! Tooth Width % must be greater than or 0.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(_gs.OutsideToothRadius < _gs.InnerToothRadius){
			EditorGUILayout.LabelField("ERROR! Outside Tooth Radius must be greater than the Inner Tooth Radius.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(_gs.Height <=0){
			EditorGUILayout.LabelField("ERROR! Height must be greater than 0.", GUILayout.ExpandWidth(true));	
			Error = true;
		}
		
		if(_gs.SpokeType == GearSupport.SpokeTypes.Straight){
			float inctheta;
			float spokeadj;
			inctheta = 2.0f * (Mathf.PI / _gs.QtyTeeth);
			float r = Mathf.Sqrt((_gs.SpokeWidthInner) * (_gs.SpokeWidthInner) + (_gs.HubRadius * _gs.HubRadius));
			spokeadj = 2.0f * Mathf.Asin(_gs.SpokeWidthInner / (8.0f * r)); //Calc'd using cord of circ.
			if(inctheta - spokeadj <= spokeadj){
				EditorGUILayout.LabelField("Warning! Spoke width and hub radius may create overlapping verts.", GUILayout.ExpandWidth(true));	
				//Error = true;
			}
		}
		
		
		
		
		
		
		
		if(_gs.GearMaterial == null){
			EditorGUILayout.LabelField("Warning! Material Not Assigned.", GUILayout.ExpandWidth(true));	
		}
		
		EditorGUILayout.LabelField("", GUILayout.ExpandWidth(true));		
		if(!Error){
			if(GUILayout.Button ("Apply")){
				
				undoManager.CheckDirty(); // Check dirty  AFTER all GUI code.
				UpdateGear();
				
			}
		}else{
			EditorGUILayout.LabelField("Clear errors to create gears.", GUILayout.ExpandWidth(true));	
			undoManager.CheckDirty(); // Check dirty  AFTER all GUI code.

		}
		
		if(Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed"){
			UpdateGear();
		}

		
	}
	
	private void UpdateGear(){
		_gs.ProcessGear(_gs.gameObject, _gs.transform.position);

	}

}
