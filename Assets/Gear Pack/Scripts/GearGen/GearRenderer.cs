using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

public class GearRenderer
{
	public const string VERT_ToothBorderTop = "toothbordertop";
	public const string VERT_ToothBorderBottom = "toothborderbottom";
	
	public const string VERT_OuterRingBorderTop = "outerringtop";
	public const string VERT_OuterRingBorderBottom = "outerringbottom";
	
	public const string VERT_InnerRingBorderTop = "innerringtop";
	public const string VERT_InnerRingBorderBottom = "innerringbottom";
	
	public const string VERT_SpokeEdgeTop = "spokeedgetop";
	public const string VERT_SpokeEdgeBottom = "spokeedgebottom";
	
	public const string VERT_HubPerimeterTop = "hubperimtop";
	public const string VERT_HubPerimeterBottom = "hubperimbottom";
	
	
	public const string VERT_SpokePerimeterTop = "spokeperimetertop";
	public const string VERT_SpokePerimeterBottom = "spokeperimeterbottom";
	
	public const string VERT_origin = "origin";
	
	public const string TRI_GearPlaneTopA1 = "gpt-a1";
	public const string TRI_GearPlaneTopA2 = "gpt-a2";
	public const string TRI_GearPlaneTopB = "gpt-b";
	public const string TRI_GearPlaneTopC = "gpt-c";
	public const string TRI_GearPlaneTopD = "gpt-d";
	public const string TRI_GearPlaneTopE = "gpt-e";
	
	public const string TRI_GearPlaneBottomA1 = "gpb-a1";
	public const string TRI_GearPlaneBottomA2 = "gpb-a2";
	public const string TRI_GearPlaneBottomB = "gpb-b";
	public const string TRI_GearPlaneBottomC = "gpb-c";
	public const string TRI_GearPlaneBottomD = "gpb-d";
	public const string TRI_GearPlaneBottomE = "gpb-e";
	
	
	public const string TRI_SolidSpokePlaneTop = "solidspokeplanetop";
	public const string TRI_SolidSpokePlaneBottom = "solidspokeplanebottom";
	public const string TRI_SpokeTop1 = "spoketop1";
    public const string TRI_SpokeBottom1 = "spokebottom1";
	
	public const string TRI_SpokeTop2 = "spoketop2";
    public const string TRI_SpokeBottom2 = "spokebottom2";
	
    public const string TRI_HubTop = "hubtop";
    public const string TRI_HubBottom = "hubbottom";

    public const string TRI_SideOuterTop = "sideoutertop";
	public const string TRI_SideOuterBottom = "sideouterbottom";
    public const string TRI_SideInnerTop = "sideinnertop";
    public const string TRI_SideInnerBottom = "sideinnerbottom";
    public const string TRI_SpokeSideTop = "spokesidetop";
	public const string TRI_SpokeSideBottom = "spokesidebottom";
    public const string TRI_HubSideTop = "hubsidetop";
	public const string TRI_HubSideBottom = "hubsidebottom";
	
    private TVector3 _origin;
    private float _innerRadius = 1.0f;
    private float _outerRadius = 1.2f;
    private int _qtyTeeth = 10;
    private float _outerWidthPct = 1.0f;
    private float _toothRot = 0.0f;
    private int _spokeCount = 6;
    private float _ringRadius = 0.0f;
    private float _height = 0.1f;
    private float _topScale = 1.0f;
    private float _topRotation = 0.0f;
    
    private float _spokeWidthInner = 0.1f;
    private float _spokeWidthOuter = 0.15f;
    private int _hubSides = 12;
    private float _hubHeight = 0.1f;
    private float _hubRadius = 0.15f;
    private float _spokeIndent = 0.05f;
    private GearSupport.SpokeTypes _spokeType = GearSupport.SpokeTypes.None;
	private float _ToothPerim = 0.0f;
	
	private Dictionary<string, string> _VertMap = new Dictionary<string, string>();
	
	private int _triOrder = 0;
	
    public TVector3 Origin
    {
        get { return _origin; }
        set { _origin = value; }
    }

    public float InnerRadius{
        get{return _innerRadius;}
        set{_innerRadius = value;}

    }

    public float OuterRadius{
        get{return _outerRadius;}
        set{_outerRadius = value;}

    }

    public int QtyTeeth{
        get{return _qtyTeeth;}
        set{_qtyTeeth = value;}

    }

    public float OuterWidthPct{
        get{return _outerWidthPct;}
        set{_outerWidthPct = value;}

    }

    public float ToothRotation{
        get{return _toothRot;}
        set{_toothRot = value;}

    }

    public int SpokeCount{
        get{return _spokeCount;}
        set{_spokeCount = value;}

    }

    public float RingRadius{
        get{return _ringRadius;}
        set{_ringRadius = value;}

    }

    public float Height{
        get{return _height;}
        set{_height = value;}
    }

    public float TopScale{
        get{return _topScale;}
        set{_topScale = value;}

    }

    public float TopRotation{
        get{return _topRotation;}
        set{_topRotation = value;}

    }

    public float SpokeWidthInner{
        get{return _spokeWidthInner;}
        set{_spokeWidthInner = value;}
    }

    public float SpokeWidthOuter{
        get{return _spokeWidthOuter;}
        set{_spokeWidthOuter = value;}
    }

    public int HubSides{
        get{return _hubSides;}
        set{_hubSides = value;}
    }

    public float HubHeight{
        get{return _hubHeight;}
        set{_hubHeight = value;}
    }

    public float HubRadius{
        get{return _hubRadius;}
        set{_hubRadius = value;}
    }

    public float SpokeIndent
    {
        get { return _spokeIndent; }
        set { _spokeIndent = value; }
    }
	
	public float HubIndent
    {
        get { return _hubHeight; }
        set { _hubHeight = value; }
    }
	
    public GearSupport.SpokeTypes SpokeType
    {
        get { return _spokeType; }
        set { _spokeType = value; }

    }
	
	public GearRenderer(){
		_origin = TVector3.Tzero.Copy();	
		
		_VertMap.Clear();
		_VertMap.Add(VERT_HubPerimeterBottom, VERT_HubPerimeterTop);
		_VertMap.Add(VERT_HubPerimeterTop, VERT_HubPerimeterBottom);
		_VertMap.Add(VERT_InnerRingBorderBottom, VERT_InnerRingBorderTop);
		_VertMap.Add(VERT_InnerRingBorderTop, VERT_InnerRingBorderBottom);
		_VertMap.Add(VERT_OuterRingBorderBottom, VERT_OuterRingBorderTop);
		_VertMap.Add(VERT_OuterRingBorderTop, VERT_OuterRingBorderBottom);
		_VertMap.Add(VERT_SpokeEdgeBottom, VERT_SpokeEdgeTop);
		_VertMap.Add(VERT_SpokeEdgeTop, VERT_SpokeEdgeBottom);
		_VertMap.Add(VERT_SpokePerimeterBottom, VERT_SpokePerimeterTop);
		_VertMap.Add(VERT_SpokePerimeterTop, VERT_SpokePerimeterBottom);
		_VertMap.Add(VERT_ToothBorderBottom, VERT_ToothBorderTop);
		_VertMap.Add(VERT_ToothBorderTop, VERT_ToothBorderBottom);
		_VertMap.Add(VERT_origin, VERT_origin);
	}

	public Shape3D GetRingGear()
	{
		System.Collections.Generic.List<Triangle3> tris = new List<Triangle3> ();

		float OuterWidth = 0.0f;
		float ToothBaseWidth = 0.0f;
		
		
		Shape3D ret;
		
		_origin.Tag = VERT_origin;
		
		ToothBaseWidth = (float)(_innerRadius * 2 * Mathf.PI / (((float)(_qtyTeeth)) * (_outerWidthPct + 1)));
		OuterWidth = ToothBaseWidth * _outerWidthPct;

		//Calc the angle to sweep...
		float inctheta = 2 * Mathf.PI / ((float)_qtyTeeth * (_outerWidthPct + 1));
		float theta = 0.0f;
		float ToothRotAngle = _toothRot * Mathf.PI / 360.0f;
		float phi = (inctheta *_outerRadius - OuterWidth) / (2 *_outerRadius);
		
		
		for (int i = 0; i <  _qtyTeeth * 2; i++) { 
			TVector3 a;
			TVector3 b;
			TVector3 c;
			
			TVector3 ta; 
			TVector3 tb;
			TVector3 tc;
			
			float adjAngle = 0.0f;
			float angleBuffera = 0;
            float angleBufferc = 0;
            Triangle3 t = new Triangle3();
			
			if (i % 2 == 0) {
				adjAngle = inctheta;
                angleBuffera = inctheta / 2.0f; //LargePoints
                angleBufferc = inctheta + _outerWidthPct * inctheta / 2.0f;
			} else {
				adjAngle = _outerWidthPct * inctheta;
                angleBuffera = _outerWidthPct * inctheta / 2.0f; // Small points
                angleBufferc = inctheta / 2.0f + inctheta * _outerWidthPct;
			}


			b = new TVector3 (_innerRadius * Mathf.Cos (theta) +_origin.Value.x, _innerRadius * Mathf.Sin (theta) +_origin.Value.y,_origin.Value.z, VERT_OuterRingBorderTop);
			c = new TVector3 (_innerRadius * Mathf.Cos (theta + adjAngle) +_origin.Value.x, _innerRadius * Mathf.Sin (theta + adjAngle) +_origin.Value.y,_origin.Value.z, VERT_OuterRingBorderTop);

			a =_origin.Copy();
            
			if (_ringRadius != 0) {
                if (_outerWidthPct != 0)
                {
                    a = new TVector3(_ringRadius * Mathf.Cos(theta + angleBuffera) + a.Value.x, _ringRadius * Mathf.Sin(theta + angleBuffera) + a.Value.y, a.Value.z, VERT_InnerRingBorderTop);
                }
                else
                {
                    a = new TVector3(_ringRadius * Mathf.Cos(theta + angleBuffera) + a.Value.x, _ringRadius * Mathf.Sin(theta + angleBuffera) + a.Value.y, a.Value.z, VERT_InnerRingBorderTop);
                }
                
            }
			
			t.a = a;
			t.b = b;
			t.c = c;
			t.SortOrder = _triOrder;
			_triOrder += 2;
			
			if (i % 2 == 0) {
				t.Tag = TRI_GearPlaneTopA1;
			}else{
				t.Tag = TRI_GearPlaneTopA2;
			}
            tris.Add(t);
            t = new Triangle3();

            if (_ringRadius != 0)
            {
                if (_outerWidthPct != 0)
                {
                    Triangle3 tFill = new Triangle3();
                    tFill.a = a.Copy();

                    tFill.b = c.Copy();

                    tFill.c = new TVector3(_ringRadius * Mathf.Cos(theta + angleBufferc) + _origin.Value.x, _ringRadius * Mathf.Sin(theta + angleBufferc) + _origin.Value.y, _origin.Value.z, VERT_InnerRingBorderTop);

                    tFill.Tag = TRI_GearPlaneTopB;
					tFill.SortOrder = _triOrder;
					_triOrder += 2;
                    tris.Add(tFill);

                }else{
                    Triangle3 tFill = new Triangle3();
                    tFill.a = a.Copy();

                    tFill.b = c.Copy();

                    tFill.c = new TVector3(_ringRadius * Mathf.Cos(theta + angleBufferc) + _origin.Value.x, _ringRadius * Mathf.Sin(theta + angleBufferc) + _origin.Value.y, _origin.Value.z, VERT_InnerRingBorderTop);

                    tFill.Tag = TRI_GearPlaneTopC;
					tFill.SortOrder = _triOrder;
					_triOrder += 2;
                    tris.Add(tFill);

                }
            }

            if (_outerWidthPct == 0 && i % 2 != 0)
            {
                continue; //If no 'squared' teeth, then bail quickly.
            }

			if (i % 2 == 0) {
				Triangle3 t1 = new Triangle3();
                Triangle3 t2 = new Triangle3();
				ta = b.Copy(); //First corner of tooth.

				tb = new TVector3 (_outerRadius * Mathf.Cos (theta + phi + ToothRotAngle) +_origin.Value.x,_outerRadius * Mathf.Sin (theta + phi + ToothRotAngle) +_origin.Value.y,_origin.Value.z, VERT_ToothBorderTop);
				tc = c.Copy();
					
				t1.a = ta;
				t1.b = tb;
				t1.c = tc;
				t1.Tag = TRI_GearPlaneTopD;
				t1.SortOrder = _triOrder;
				_triOrder += 2;
			    tris.Add (t1);
					
				tb = new TVector3 (_outerRadius * Mathf.Cos (theta + inctheta - phi + ToothRotAngle) +_origin.Value.x,_outerRadius * Mathf.Sin (theta + inctheta - phi + ToothRotAngle) +_origin.Value.y,_origin.Value.z, VERT_ToothBorderTop); //Second Corner of Outer Tooth.
				ta = new TVector3 (_outerRadius * Mathf.Cos (theta + phi + ToothRotAngle) +_origin.Value.x,_outerRadius * Mathf.Sin (theta + phi + ToothRotAngle) +_origin.Value.y,_origin.Value.z, VERT_ToothBorderTop); //First corner of outer tooth.
				tc = c.Copy();
				
				t2.a = ta;
				t2.b = tb;
				t2.c = tc;
				t2.Tag = TRI_GearPlaneTopE;
				t2.SortOrder = _triOrder;
				_triOrder += 2;
				tris.Add (t2);

			}

			theta += adjAngle;
			
			//If next to last tooth then set theta = 0 (for full circle...);
			if (i == (_qtyTeeth * 2 - 1)) {
				theta = 0;	
				
			}
			
			
		}
		
		
		var opptris = BuildOppositePlane(tris.ToArray(), new string[] { TRI_GearPlaneTopA1 }, TRI_GearPlaneBottomA1, _topScale, _topRotation);
        tris.AddRange(opptris);
		
		opptris = BuildOppositePlane(tris.ToArray(), new string[] { TRI_GearPlaneTopA2 }, TRI_GearPlaneBottomA2, _topScale, _topRotation);
		tris.AddRange(opptris);
		
		opptris = BuildOppositePlane(tris.ToArray(), new string[] { TRI_GearPlaneTopE }, TRI_GearPlaneBottomE, _topScale, _topRotation);
		tris.AddRange(opptris);
		
		opptris = BuildOppositePlane(tris.ToArray(), new string[] { TRI_GearPlaneTopD }, TRI_GearPlaneBottomD, _topScale, _topRotation);
		tris.AddRange(opptris);
		
		opptris = BuildOppositePlane(tris.ToArray(), new string[] { TRI_GearPlaneTopB }, TRI_GearPlaneBottomB, _topScale, _topRotation);
		tris.AddRange(opptris);
	
		opptris = BuildOppositePlane(tris.ToArray(), new string[] { TRI_GearPlaneTopC }, TRI_GearPlaneBottomC, _topScale, _topRotation);
		tris.AddRange(opptris);

		tris.AddRange(GetSpokes());
		
		//Stitch Teeth
		System.Collections.Generic.List<Triangle3> stitching = new List<Triangle3>();
		System.Collections.Generic.SortedList<int, Triangle3> sortedTris = new SortedList<int, Triangle3>();
		
		foreach (Triangle3 item in tris) {
			if(sortedTris.ContainsKey(item.SortOrder)){
				Debug.Log("Item exists: " + 	item.SortOrder.ToString() + " " + item.Tag);
			}
			sortedTris.Add(item.SortOrder, item);			
		}

		foreach (Triangle3 item in sortedTris.Values) {
			
			
			stitching.AddRange(StitchEdge(item, TRI_GearPlaneTopA2, TRI_GearPlaneBottomA2, "b", "c", "a", "b", TRI_SideOuterTop, TRI_SideOuterBottom));
			stitching.AddRange(StitchEdge(item, TRI_GearPlaneTopD, TRI_GearPlaneBottomD, "a", "b", "b", "c", TRI_SideOuterTop, TRI_SideOuterBottom));
			stitching.AddRange(StitchEdge(item, TRI_GearPlaneTopE, TRI_GearPlaneBottomE, "a", "b", "b", "c", TRI_SideOuterTop, TRI_SideOuterBottom));
			stitching.AddRange(StitchEdge(item, TRI_GearPlaneTopE, TRI_GearPlaneBottomE, "b", "c", "a", "b", TRI_SideOuterTop, TRI_SideOuterBottom));
			
			//Stitch Inner Ring.
			stitching.AddRange(StitchEdge(item, TRI_GearPlaneTopB, TRI_GearPlaneBottomB, "c", "a", "c", "a", TRI_SideInnerTop, TRI_SideInnerBottom));
			stitching.AddRange(StitchEdge(item, TRI_GearPlaneTopC, TRI_GearPlaneBottomC, "c", "a", "c", "a", TRI_SideInnerTop, TRI_SideInnerBottom));
			
			//Stitch Straight Spokes
			stitching.AddRange(StitchEdge(item, TRI_SpokeTop1, TRI_SpokeBottom1, "a", "b", "b", "c", TRI_SpokeSideTop, TRI_SpokeSideBottom));
			stitching.AddRange(StitchEdge(item, TRI_SpokeTop2, TRI_SpokeBottom2, "b", "c", "a", "b", TRI_SpokeSideTop, TRI_SpokeSideBottom));
			
			//Stitch Hub
			stitching.AddRange(StitchEdge(item, TRI_HubTop, TRI_HubBottom, "b", "c", "a", "b", TRI_HubSideTop, TRI_HubSideBottom));

		}
		tris.AddRange(stitching.ToArray());
		
		ret.UV = BuildUVs(tris);
		ret.tris = tris.ToArray();
		
		return ret;
	}
	
		
	TVector2[] BuildUVs (System.Collections.Generic.List<Triangle3> tris)
	{
			_ToothPerim = GetToothPermiter(tris, new string[]{TRI_SideOuterTop});
		
			//Build Uv's
			var uvs = new List<TVector2>();
			
			//Build uvs for planes...
			var tbounds = GetUVBoundaries(tris,new string[]{TRI_GearPlaneTopA1, TRI_GearPlaneTopA2, TRI_GearPlaneTopB, TRI_GearPlaneTopC, TRI_GearPlaneTopD, TRI_GearPlaneTopE}); //Get max extents for the verticies.
			
			var tmin = tbounds.min;
			var tbsz = tbounds.size;
			
			var bbounds = GetUVBoundaries(tris,new string[]{TRI_GearPlaneBottomA1, TRI_GearPlaneBottomA2, TRI_GearPlaneBottomB, TRI_GearPlaneBottomC, TRI_GearPlaneBottomD, TRI_GearPlaneBottomE}); //Get max extents for the verticies.
			
			var bmin = bbounds.min;
			var bbsz = bbounds.size;
		
		
			var hubRange = GetUVBoundaries(tris, new string[]{TRI_HubBottom, TRI_HubSideBottom, TRI_HubTop, TRI_HubSideTop});
			var hubMin = hubRange.min;
			var hubSz = hubRange.size;
			
			var spokeRange = GetUVBoundaries(tris, new string[]{TRI_SolidSpokePlaneTop, TRI_SolidSpokePlaneBottom, TRI_SpokeTop1, TRI_SpokeTop2, TRI_SpokeBottom1, TRI_SpokeBottom2, TRI_SpokeSideBottom, TRI_SpokeSideTop});
			var spokeMin = spokeRange.min;
			var spokeSz = spokeRange.size;

			//Get UV 'Buckets'
			float pad = .01f; //Padding
			float hs = 1.0f / (_ToothPerim);
			float vs = hs;
			float toph = Mathf.Max(tbsz.x, tbsz.y) * vs; //(2.0f * _outerRadius * vs); // Top Height Scaled
			float bottomh = Mathf.Max(bbsz.x, bbsz.y) * vs;//(2.0f * _outerRadius * vs * _topScale); // BottomHeight Scaled
			float gh = _height * vs;//_height * vs; //Gear Height Scaled
			float hh = _hubHeight * vs; //Hub Height Scaled
			float spokeh = spokeSz.y * vs; //(2.0f * _ringRadius * vs);
			float spokew = spokeSz.x * vs;
			float sidespokeh = (_height - (_spokeIndent)) * vs * 2.0f;
			//float hubcaph = (2.0f * Mathf.Sqrt((_spokeWidthInner) * (_spokeWidthInner) + (_hubRadius * _hubRadius)) * vs); //???
			float hubcaph = Mathf.Max(hubSz.x, hubSz.y) * vs;
			Rect topbounds = new Rect(pad, 1.0f - pad - toph, toph, toph);
			Rect bottombounds = new Rect(pad, topbounds.y - bottomh - pad, bottomh, bottomh);
			
			Rect topspokebounds = new Rect(topbounds.x + topbounds.width + pad, 1.0f - pad - spokeh, spokew, spokeh);
			Rect bottomspokebounds = new Rect(topspokebounds.x, topspokebounds.y - pad - spokeh, spokew, spokeh);
			
			float perimstarty = bottombounds.y;
			
			if (perimstarty > bottomspokebounds.y){
				perimstarty = bottomspokebounds.y;	
			}
			
			Rect perimbounds = new Rect(pad, perimstarty - 2.0f * pad - gh, 1.0f - 2.0f * pad, gh);
			Rect ringbounds = new Rect(pad, perimbounds.y - pad - gh, 1.0f - 2.0f * pad, gh);
			Rect hubbounds = new Rect(pad, ringbounds.y - pad - hh, 1.0f - 2.0f * pad, hh);
	
			Rect tophubcapbounds = new Rect(topspokebounds.x + topspokebounds.width + pad, topspokebounds.y, hubcaph, hubcaph);
			Rect bottomhubcapbounds = new Rect(tophubcapbounds.x, tophubcapbounds.y - pad - hubcaph, hubcaph, hubcaph);
			Rect spokesidebounds = new Rect(pad, pad, 1.0f - 2.0f * pad, sidespokeh + pad);
			
			
			float lastoutertopx = perimbounds.x;
			float lastouterbottomx = perimbounds.x;
			float lastinnertopx = ringbounds.x;
			float lastinnerbottomx = ringbounds.x;
			float lasthubbottomx = hubbounds.x;
			float lasthubtopx = hubbounds.x;
			
			float lastspokesidetopx = spokesidebounds.x;
			float lastspokesidebottomx = spokesidebounds.x;
			
			//For top plane verts, go through and calc the UV space.
			foreach (Triangle3 tri in tris) {
				float x = 0.0f;
				float y = 0.0f;
				//float xScale = (_ToothPerim);
				//float yScale = xScale;

				var BottomTagList = new string[] {TRI_GearPlaneBottomA1, TRI_GearPlaneBottomA2, TRI_GearPlaneBottomB, TRI_GearPlaneBottomC, TRI_GearPlaneBottomD, TRI_GearPlaneBottomE};
				if(TagContains(tri.Tag, BottomTagList)){
					TVector2 uvi;
					
					Rect itembounds = bottombounds;
					float widthscalex = (1.0f / bbsz.x) * itembounds.width;
					float widthscaley = (1.0f / bbsz.y) * itembounds.height;
					
					uvi = new TVector2((tri.a.Value.x - bmin.x) * widthscalex + itembounds.x, (tri.a.Value.y - bmin.y) * widthscaley + itembounds.y, tri.a.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.b.Value.x - bmin.x) * widthscalex + itembounds.x, (tri.b.Value.y - bmin.y) * widthscaley + itembounds.y, tri.b.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.c.Value.x - bmin.x) * widthscalex + itembounds.x, (tri.c.Value.y - bmin.y) * widthscaley + itembounds.y, tri.c.Tag);
					uvs.Add(uvi);
					
					
				}
				
				var TopTagList = new string[] {TRI_GearPlaneTopA1, TRI_GearPlaneTopA2, TRI_GearPlaneTopB, TRI_GearPlaneTopC, TRI_GearPlaneTopD, TRI_GearPlaneTopE};
				if(TagContains(tri.Tag, TopTagList)){
					TVector2 uvi;
					
					Rect itembounds = topbounds;
					float widthscalex = (1.0f / tbsz.x) * itembounds.width; 
					float widthscaley = (1.0f / tbsz.y) * itembounds.height;
					
					uvi = new TVector2((tri.a.Value.x - tmin.x) * widthscalex + itembounds.x, (tri.a.Value.y - tmin.y) * widthscaley + itembounds.y, tri.a.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.b.Value.x - tmin.x) * widthscalex + itembounds.x, (tri.b.Value.y - tmin.y) * widthscaley + itembounds.y, tri.b.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.c.Value.x - tmin.x) * widthscalex + itembounds.x, (tri.c.Value.y - tmin.y) * widthscaley + itembounds.y, tri.c.Tag);
					uvs.Add(uvi);
					
				}
				
				
				//Spoke Layouts
				var SpokeBottomTagList = new string[] {TRI_SolidSpokePlaneBottom, TRI_SpokeBottom1, TRI_SpokeBottom2};
				if(TagContains(tri.Tag, SpokeBottomTagList)){
					TVector2 uvi;
					
					Rect itembounds = bottomspokebounds;
					float widthscalex = (1.0f / spokeSz.x) * itembounds.width;
					float widthscaley = (1.0f / spokeSz.y) * itembounds.height;
					
					uvi = new TVector2((tri.a.Value.x - spokeMin.x) * widthscalex + itembounds.x, (tri.a.Value.y - spokeMin.y) * widthscaley + itembounds.y, tri.a.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.b.Value.x - spokeMin.x) * widthscalex + itembounds.x, (tri.b.Value.y - spokeMin.y) * widthscaley + itembounds.y, tri.b.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.c.Value.x - spokeMin.x) * widthscalex + itembounds.x, (tri.c.Value.y - spokeMin.y) * widthscaley + itembounds.y, tri.c.Tag);
					uvs.Add(uvi);
					
					
				}
				
				var SpokeTopTagList = new string[] {TRI_SolidSpokePlaneTop, TRI_SpokeTop1, TRI_SpokeTop2};
				if(TagContains(tri.Tag, SpokeTopTagList)){
					TVector2 uvi;
					
					Rect itembounds = topspokebounds;
					float widthscalex = (1.0f / spokeSz.x) * itembounds.width; 
					float widthscaley = (1.0f / spokeSz.y) * itembounds.height;
					
					uvi = new TVector2((tri.a.Value.x - spokeMin.x) * widthscalex + itembounds.x, (tri.a.Value.y - spokeMin.y) * widthscaley + itembounds.y, tri.a.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.b.Value.x - spokeMin.x) * widthscalex + itembounds.x, (tri.b.Value.y - spokeMin.y) * widthscaley + itembounds.y, tri.b.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.c.Value.x - spokeMin.x) * widthscalex + itembounds.x, (tri.c.Value.y - spokeMin.y) * widthscaley + itembounds.y, tri.c.Tag);
					uvs.Add(uvi);
					
				}
				
				//Side Spokes...
				var TopSideSpokeList = new string[] {TRI_SpokeSideTop};
				if(TagContains(tri.Tag, TopSideSpokeList)){
					Rect itembounds = spokesidebounds;
					
					TVector2 uvi1;
					TVector2 uvi2;
					TVector2 uvi3;
					
					float w = Vector3.Distance(tri.c.Value, tri.b.Value) * vs;
					float row = Mathf.Floor((w + lastspokesidetopx) / itembounds.width);
					//float rowCount = Mathf.Ceil((_spokeCount * 2.0f * w) / itembounds.width);
					float h = Vector3.Distance(tri.a.Value, tri.b.Value) * vs; 
					float adj = itembounds.width -  (Mathf.Floor(itembounds.width / w) * w);
					
					x = w + lastspokesidetopx - itembounds.width * row + (adj * row);
	
					y = itembounds.y + ((h) * row) + (pad * row);
					uvi1 = new TVector2(x, y, tri.Tag);
	
					y = y + h;
					uvi2 = new TVector2(x, y, tri.Tag);
	
					x = lastspokesidetopx - itembounds.width * row + (adj * row);
					uvi3 = new TVector2(x, y, tri.Tag);
					
					lastspokesidetopx +=(w);
					uvs.Add(uvi1);
					uvs.Add(uvi2);
					uvs.Add(uvi3);
	
				}
				
				var BottomSideSpokeList = new string[] {TRI_SpokeSideBottom};
				if(TagContains(tri.Tag, BottomSideSpokeList)){
					Rect itembounds = spokesidebounds;
					
					TVector2 uvi1;
					TVector2 uvi2;
					TVector2 uvi3;
					
					float w = Vector3.Distance(tri.a.Value, tri.c.Value) * vs;
					float row = Mathf.Floor((w + lastspokesidebottomx) / itembounds.width);
					//float rowCount = Mathf.Ceil((_spokeCount * 2.0f * w) / itembounds.width);
					float h = Vector3.Distance(tri.b.Value, tri.c.Value) * vs; 
					float adj = itembounds.width -  (Mathf.Floor(itembounds.width / w) * w);
					
					x = w + lastspokesidebottomx - itembounds.width * row + (adj * row);
					
					y = itembounds.y + ((h) * row) + (pad * row);
					uvi1 = new TVector2(x, y, tri.Tag);
					
					x = lastspokesidebottomx - itembounds.width * row + (adj * row);
					uvi2 = new TVector2(x, y, tri.Tag);
					
					y = y + h;
					uvi3 = new TVector2(x, y, tri.Tag);
					
					lastspokesidebottomx +=(w);
	
					
					uvs.Add(uvi1);
					uvs.Add(uvi3);
					uvs.Add(uvi2);
					
				}
				
				
				//Hub Cap Layouts
				var HubCapBottomTagList = new string[] {TRI_HubBottom};
				if(TagContains(tri.Tag, HubCapBottomTagList)){
					TVector2 uvi;
					
					Rect itembounds = bottomhubcapbounds;
					float widthscalex = (1.0f / hubSz.x) * itembounds.width;
					float widthscaley = (1.0f / hubSz.y) * itembounds.height;
	
					uvi = new TVector2((tri.a.Value.x - hubMin.x) * widthscalex + itembounds.x, (tri.a.Value.y - hubMin.y) * widthscaley + itembounds.y, tri.a.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.b.Value.x - hubMin.x) * widthscalex + itembounds.x, (tri.b.Value.y - hubMin.y) * widthscaley + itembounds.y, tri.b.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.c.Value.x - hubMin.x) * widthscalex + itembounds.x, (tri.c.Value.y - hubMin.y) * widthscaley + itembounds.y, tri.c.Tag);
					uvs.Add(uvi);
					
					
				}
				
				var HubCapTopTagList = new string[] {TRI_HubTop};
				if(TagContains(tri.Tag, HubCapTopTagList)){
					TVector2 uvi;
					
					Rect itembounds = tophubcapbounds;
					float widthscalex = (1.0f / hubSz.x) * itembounds.width;
					float widthscaley = (1.0f / hubSz.y) * itembounds.height;
					
					uvi = new TVector2((tri.a.Value.x - hubMin.x) * widthscalex + itembounds.x, (tri.a.Value.y - hubMin.y) * widthscaley + itembounds.y, tri.a.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.b.Value.x - hubMin.x) * widthscalex + itembounds.x, (tri.b.Value.y - hubMin.y) * widthscaley + itembounds.y, tri.b.Tag);
					uvs.Add(uvi);
					
					uvi = new TVector2((tri.c.Value.x - hubMin.x) * widthscalex + itembounds.x, (tri.c.Value.y - hubMin.y) * widthscaley + itembounds.y, tri.c.Tag);
					uvs.Add(uvi);
					
				}
				
				
				
				//Tooth Perim 1st Half
				var OuterListTop = new string[] {TRI_SideOuterTop};
				if(TagContains(tri.Tag, OuterListTop)){
					TVector2 uvi1;
					TVector2 uvi2;
					TVector2 uvi3;
					Rect itembounds = perimbounds;
					
					float w = Vector3.Distance(tri.c.Value, tri.b.Value) * vs * itembounds.width; /// xScale * _topScale;
					//float h = itembounds.height; // _height /  yScale; 
					
					x = w + lastoutertopx;
					y = itembounds.y;
					uvi1 = new TVector2(x, y, tri.Tag);
					
					x = lastoutertopx;
					uvi2 = new TVector2(x, y, tri.Tag);
				
					y = itembounds.y + itembounds.height;
					uvi3 = new TVector2(x, y, tri.Tag);
					
					lastoutertopx +=w;
	
					uvs.Add(uvi3);
					uvs.Add(uvi2);
					uvs.Add(uvi1);	
					
				}
				
				//Tooth Perim 2nd Half
				var OuterListBottom = new string[] {TRI_SideOuterBottom};
				if(TagContains(tri.Tag, OuterListBottom)){
					TVector2 uvi1;
					TVector2 uvi2;
					TVector2 uvi3;
					Rect itembounds = perimbounds;
					float w = Vector3.Distance(tri.a.Value, tri.c.Value) * vs * itembounds.width *_topScale; /// xScale * _topScale;
					//float h = itembounds.height; // _height /  yScale; 
					
					x = w + lastouterbottomx;
					y = itembounds.y;
					uvi1 = new TVector2(x, y, tri.Tag);
	
					y = itembounds.y + itembounds.height;
					uvi2 = new TVector2(x, y, tri.Tag);
	
					x = lastouterbottomx;
					uvi3 = new TVector2(x, y, tri.Tag);
					
					
					lastouterbottomx +=w;
							
					uvs.Add(uvi3);
					uvs.Add(uvi1);
					uvs.Add(uvi2);
					
				}
	
				var InnerListTop = new string[] {TRI_SideInnerTop};
				if(TagContains(tri.Tag, InnerListTop)){
					TVector2 uvi1;
					TVector2 uvi2;
					TVector2 uvi3;
					Rect itembounds = ringbounds;
					float w = Vector3.Distance(tri.b.Value, tri.c.Value) * vs * itembounds.width;
					//float h = itembounds.height;
					
					x = w + lastinnertopx;
					y = itembounds.y;
					uvi1 = new TVector2(x, y, tri.Tag);
	
					y = itembounds.y + itembounds.height;
					uvi2 = new TVector2(x, y, tri.Tag);
	
					x = lastinnertopx;
					uvi3 = new TVector2(x, y, tri.Tag);
					
					lastinnertopx +=w;
							
					uvs.Add(uvi1);
					uvs.Add(uvi2);
					uvs.Add(uvi3);	
	
				}
				
				var InnerListBottom = new string[] {TRI_SideInnerBottom};
				if(TagContains(tri.Tag, InnerListBottom)){
					TVector2 uvi1;
					TVector2 uvi2;
					TVector2 uvi3;
					Rect itembounds = ringbounds;
					float w = Vector3.Distance(tri.a.Value, tri.c.Value) * vs * itembounds.width;
					//float h = _height / yScale;
					
					x = lastinnerbottomx;
					y = itembounds.y;
					uvi1 = new TVector2(x, y, tri.Tag);
	
					x = w + lastinnerbottomx;
					uvi2 = new TVector2(x, y, tri.Tag);
					
					y = itembounds.y + itembounds.height;
					x = lastinnerbottomx;
					uvi3 = new TVector2(x, y, tri.Tag);
					
					lastinnerbottomx +=w;
							
					uvs.Add(uvi2);
					uvs.Add(uvi3);
					uvs.Add(uvi1);
	
				}
				
				var HubListTop = new string[] {TRI_HubSideTop};
				if(TagContains(tri.Tag, HubListTop)){
					TVector2 uvi1;
					TVector2 uvi2;
					TVector2 uvi3;
					Rect itembounds = hubbounds;
					
					float w = Vector3.Distance(tri.c.Value, tri.b.Value) * vs * itembounds.width;
					//float h = itembounds.height;
					
					x = w + lasthubtopx;
					y = itembounds.y;
					uvi1 = new TVector2(x, y, tri.Tag);
					
					x = lasthubtopx;
					uvi2 = new TVector2(x, y, tri.Tag);
				
					y = itembounds.y + itembounds.height;
					uvi3 = new TVector2(x, y, tri.Tag);
					
					lasthubtopx +=w;
	
					uvs.Add(uvi3);
					uvs.Add(uvi2);
					uvs.Add(uvi1);	
					
				}
				
				var HubListBottom = new string[] {TRI_HubSideBottom};
				if(TagContains(tri.Tag, HubListBottom)){
					TVector2 uvi1;
					TVector2 uvi2;
					TVector2 uvi3;
					Rect itembounds = hubbounds;
					float w = Vector3.Distance(tri.a.Value, tri.c.Value) * vs * itembounds.width;
					//float h = _hubHeight / yScale;
					
					x = w + lasthubbottomx;
					y = itembounds.y;
					uvi1 = new TVector2(x, y, tri.Tag);
	
					y = itembounds.y + itembounds.height;
					uvi2 = new TVector2(x, y, tri.Tag);
	
					x = lasthubbottomx;
					uvi3 = new TVector2(x, y, tri.Tag);
					
					
					lasthubbottomx +=w;
							
					uvs.Add(uvi3);
					uvs.Add(uvi1);
					uvs.Add(uvi2);
					
				}
				
				
			}
	
			return uvs.ToArray();
	}
	
	float GetToothPermiter(System.Collections.Generic.List<Triangle3> tris, string[] TriTags)
	{
		
		//List<TVector3> VertList = new List<TVector3>();
		var trilist = new SortedDictionary<Int32, Triangle3>();
		//TVector3[] ov;
		float perimeterout = 0.0f;
		
		foreach (var item in tris) {
			if(trilist.ContainsKey(item.SortOrder)){
				Debug.Log("Sort Order Already Present:" + item.SortOrder.ToString());	
			}
			trilist.Add(item.SortOrder, item);
		}
		
		foreach (var tri in trilist.Values) {
			if(TagContains(tri.Tag, TriTags)){
				float w = Vector3.Distance(tri.c.Value, tri.b.Value);
				
				perimeterout +=w;

				
			}
		}
		
		return perimeterout;
	}
	
	public static bool TagContains(string Value, string[] CompareList){
		bool ret = false;
		
		foreach (string item in CompareList) {
			if(Value.ToLower() == item.ToLower()){
				return true;
			}
		}
		
		return ret;
	}
	
	private Boundary GetUVBoundaries(System.Collections.Generic.List<Triangle3> tris, string[] TagList){
		Boundary bounds;
		
		Vector3 min = Vector3.zero;
		Vector3 max = Vector3.zero;
		
		//Get boundary verts
		foreach (var tri in tris) {
			if (TagList.Length == 0 || TagContains(tri.Tag, TagList)){
				for (int i = 0; i < 3; i++) {
					var vert = tri[i].Value;
	
					if (min.x > vert.x){
						min = new Vector3(vert.x, min.y, min.z);	
					}
					if (min.y > vert.y){
						min = new Vector3(min.x, vert.y,min.z);	
					}
					if (min.z > vert.z){
						min = new Vector3(min.x, min.y,vert.z);	
					}
					if (max.x < vert.x){
						max = new Vector3(vert.x, max.y,max.z);	
					}
					if (max.y < vert.y){
						max = new Vector3(max.x, vert.y, max.z);	
					}
					if (max.z < vert.z){
						max = new Vector3(max.x, max.y,vert.z);	
					}
					
				}
			}
		}
		
		Vector3 uvLen = Vector3.zero;
		
		uvLen = new Vector3(max.x - min.x, max.y - min.y, max.z - min.z);
		
		bounds.size = uvLen;
		bounds.min = min;
		bounds.max = max;
		
		return bounds;
	}
	
	private Triangle3[] BuildOppositePlane (Triangle3[] tris, string[] TriTags, string Tag, float scaleFactor, float rotation)
	{
        float scale = 0.0f;
		float rot = rotation;
		System.Collections.Generic.List<Triangle3> ret = new List<Triangle3> ();
		var ScaleIgnoreList = new string[]{VERT_origin, VERT_InnerRingBorderTop, VERT_HubPerimeterTop, VERT_HubPerimeterBottom};
		
		
		//Build other plane for extrusion...  (Unwinding back to forwards)
		for (int j = tris.Length-1; j >= 0; j--) {
            var tri = tris[j].Copy();
            var triOrig = tri.Copy();
			
			if(TriTags.Contains(tri.Tag)){
				var a = tri.a.Copy().Value;
                var b = tri.b.Copy().Value;
                var c = tri.c.Copy().Value;
				string aTag = tri.a.Tag;
				string bTag = tri.b.Tag;
				string cTag = tri.c.Tag;
				
				/////////////////////////////////////////////
				if (! TagContains(triOrig.c.Tag, ScaleIgnoreList)){
                    scale = scaleFactor;
				}else{
					scale = 1.0f;
				}
				tri.a = new TVector3(c.x*scale, c.y*scale, c.z - _height, cTag);
                tri.a = RotatePoint(rot, tri.a);
				
				/////////////////////////////////////////////
				if (! TagContains(triOrig.b.Tag, ScaleIgnoreList)){
                    scale = scaleFactor;;
				}else{
					scale = 1.0f;
				}
                tri.b = new TVector3(b.x * scale, b.y * scale, b.z - _height, bTag);
                tri.b = RotatePoint(rot, tri.b);
				
				/////////////////////////////////////////////
				if (! TagContains(triOrig.a.Tag, ScaleIgnoreList)){
                    scale = scaleFactor;
				}else{
					scale = 1.0f;
				}
                tri.c = new TVector3(a.x * scale, a.y * scale, a.z - _height, aTag);
                tri.c = RotatePoint(rot, tri.c);
				
				/////////////////////////////////////////////
                tri.Tag = Tag;
				tri.SortOrder = triOrig.SortOrder + 1;
                tri.Complement = triOrig;

				ret.Add(tri);
			}
		}
		
		MirrorVertTags(ret);
		
		return ret.ToArray();
	}

	public Triangle3[] GetSolidSpokePlane (TVector3 origin, string Tag, float scale)
	{
		System.Collections.Generic.List<Triangle3> tris = new List<Triangle3> ();

		//Calc the angle to sweep...
		float inctheta = 2 * Mathf.PI / ((float)_qtyTeeth * 2.0f);
		float theta = 0.0f;
		_origin.Tag = VERT_origin;
        float width = _ringRadius * 1.05f;
		for (int i = 0; i < (_qtyTeeth * 2); i++) {
			TVector3 a;
			TVector3 b;
			TVector3 c;
			
			float adjAngle = 0.0f;

            Triangle3 t = new Triangle3();

 
			adjAngle = inctheta;
			a =origin.Copy();
			
			
			
            b = new TVector3(width * Mathf.Cos(theta) + origin.Value.x, width * Mathf.Sin(theta) + origin.Value.y, a.Value.z, VERT_SpokePerimeterTop);
			
 			b = new TVector3(b.Value.x * scale, b.Value.y * scale, b.Value.z, b.Tag);
			
            c = new TVector3(width * Mathf.Cos(theta + adjAngle) + origin.Value.x, width * Mathf.Sin(theta + adjAngle) + origin.Value.y, a.Value.z, VERT_SpokePerimeterTop);
            
 			c = new TVector3(c.Value.x * scale, c.Value.y * scale, c.Value.z, c.Tag);
			
			t.a = a;
			t.b = b;
			t.c = c;
			t.Tag = Tag;
			t.SortOrder = _triOrder;
			_triOrder += 2;
			tris.Add (t);

			theta += adjAngle;
			
			//If next to last tooth then set theta = 0 (for full circle...);
			if (i == (_qtyTeeth * 2 - 1)) {
				theta = 0;	
				
			}

		}
		
		return tris.ToArray ();
	}

	public Triangle3[] GetStraightSpokePlane (TVector3 origin, float zOffset, string Tag1, string Tag2)
	{
		System.Collections.Generic.List<Triangle3> tris = new List<Triangle3> ();

		//Calc the angle to sweep...
		float inctheta = 2 * Mathf.PI / ((float)_spokeCount);
		float theta = 0.0f;
		float adjAngle = 0.0f;
		System.Collections.Generic.List<LineSegment2D> spokelines = new System.Collections.Generic.List<LineSegment2D> ();
		
		for (int i = 0; i < (_spokeCount + 1); i++) {
			adjAngle = inctheta;
			TVector2 pt1;
			TVector2 pt2;
			
			if(_hubRadius !=0){
	            pt1 = new TVector2((_hubRadius) * Mathf.Cos(theta) + origin.Value.x, (_hubRadius) * Mathf.Sin(theta) + origin.Value.y, VERT_SpokeEdgeTop);
	            
			}else{
				pt1 = new TVector2(origin.Value.x, origin.Value.y, VERT_SpokeEdgeTop);
				
			}
			pt2 = new TVector2(_ringRadius * Mathf.Cos(theta) + origin.Value.x, _ringRadius * Mathf.Sin(theta) + origin.Value.y, VERT_SpokeEdgeTop);
			LineSegment2D ls = new LineSegment2D ();
				
			ls.a = pt1;
			ls.b = pt2;

			spokelines.Add (ls);
			theta += (adjAngle);
		}
		theta = 0.0f;
		
		for (int i = 0; i < (_spokeCount); i++) {
			Quad3D q3;

			var q2 = CreateQuadFromLineSegment (spokelines [i], _spokeWidthInner, _spokeWidthOuter);
            

            q3 = new Quad3D().Create(q2, origin.Value.z);
			q3.Tag = Tag1;
			var qtris = q3.GetTris ();
			qtris[0].Tag = q3.Tag;
			qtris[0].SortOrder = _triOrder;
			_triOrder += 2;
			qtris[1].Tag = Tag2;
			qtris[1].SortOrder = _triOrder;
			_triOrder += 2;
			tris.AddRange (qtris);

		}	
		
		return tris.ToArray ();
	}
	
	public Triangle3[] GetHubPlane(TVector3 Origin, float zOffset, string Tag){
		System.Collections.Generic.List<Triangle3> tris = new List<Triangle3> ();
		float inctheta = 2 * Mathf.PI / ((float) _hubSides);
		float theta = 0.0f;
		float adjAngle = 0.0f;
		float r = Mathf.Sqrt((_spokeWidthInner) * (_spokeWidthInner) + (_hubRadius * _hubRadius)); //Get dist to corner of spoke.
		for (int i = 0; i < (_hubSides); i++) {
			Triangle3 tri = new Triangle3();
			adjAngle = inctheta;
            tri.a = new TVector3(Origin.Value.x, Origin.Value.y, Origin.Value.z + zOffset, VERT_origin);
            tri.b = new TVector3(r * Mathf.Cos(theta) + Origin.Value.x, r * Mathf.Sin(theta) + Origin.Value.y, Origin.Value.z + zOffset, VERT_HubPerimeterTop);
            tri.c = new TVector3(r * Mathf.Cos(theta + adjAngle) + Origin.Value.x, r * Mathf.Sin(theta + adjAngle) + Origin.Value.y, Origin.Value.z + zOffset, VERT_HubPerimeterTop);
			
			tri.SortOrder = _triOrder;
			_triOrder += 2;
			
			tri.Tag = Tag;
			tris.Add(tri);
			
			theta += (adjAngle);
		}
		
		return tris.ToArray();
	}
	
	Triangle3[] GetSpokes ()
	{
		System.Collections.Generic.List<Triangle3> tris = new List<Triangle3> ();
		string TopTag1;
        string BottomTag1;
		string TopTag2;
        string BottomTag2;
		switch (_spokeType) {
			case GearSupport.SpokeTypes.Solid:
                TopTag1 = TRI_SolidSpokePlaneTop;
                BottomTag1 = TRI_SolidSpokePlaneBottom;
				tris.AddRange(GetSolidSpokes(TopTag1, BottomTag1));
				break;
				
		case GearSupport.SpokeTypes.Straight:
                TopTag1 = TRI_SpokeTop1;
                BottomTag1 = TRI_SpokeBottom1;
				TopTag2 = TRI_SpokeTop2;
                BottomTag2 = TRI_SpokeBottom2;
			
			
                tris.AddRange(GetStraightSpokes(TopTag1, TopTag2, BottomTag1, BottomTag2));
			break;
			/*
		case SpokeTypes.Curved:
			//Not yet guys ;)
			break;
			*/
			default:
			break;
			
		}
		
		
		return tris.ToArray();
		
	}
	
	private void MirrorVertTags(System.Collections.Generic.List<Triangle3> tris){
		MirrorVertTags(tris.ToArray());
		
	}
	
	private void MirrorVertTags(Triangle3[] tris){	
		foreach (Triangle3 item in tris) {
			item.a.Tag = _VertMap[item.a.Tag];
			item.b.Tag = _VertMap[item.b.Tag];
			item.c.Tag = _VertMap[item.c.Tag];
		}
	}
	
	Triangle3[] GetSolidSpokes (string TagTop, string TagBottom)
	{
		TVector3 origin1 = new TVector3(0,0, ((-_spokeIndent/2)), GearRenderer.VERT_origin);
		TVector3 origin2 = new TVector3(0,0, ((-_height + _spokeIndent/2)), GearRenderer.VERT_origin);
        System.Collections.Generic.List<Triangle3> tris = new System.Collections.Generic.List<Triangle3>();
        Triangle3 newtri;

		Triangle3[] p1tris = GetSolidSpokePlane(origin1, TagTop, 1.0f);
		Triangle3[] p2tris = GetSolidSpokePlane(origin2, TagBottom,1.0f);
		MirrorVertTags(p2tris);
		
		foreach(var tri in p1tris){
			var t1 = tri.Copy();
            newtri = new Triangle3();
            newtri.a = t1.a;
            newtri.b = t1.b;
            newtri.c = t1.c;
            newtri.Tag = TagTop;
			newtri.SortOrder = _triOrder;
			_triOrder += 2;
            tris.Add(newtri);
		}
		
		foreach(var tri in p2tris){
			var t2 = tri.Copy();
            newtri = new Triangle3();
            newtri.a = t2.c;
            newtri.b = t2.b;
            newtri.c = t2.a;
            newtri.Tag = TagBottom;
			newtri.SortOrder = tri.SortOrder + 1;
            tris.Add(newtri);


		}

		return tris.ToArray();
		
	}
	
	Triangle3[] GetStraightSpokes (string TagTop1,string TagTop2, string TagBottom1, string TagBottom2)
	{
		TVector3 origin1 = new TVector3(0,0, ((-_spokeIndent/2)), GearRenderer.VERT_origin);
        TVector3 origin2 = new TVector3(0, 0, ((-_height + _spokeIndent / 2)), GearRenderer.VERT_origin);
        System.Collections.Generic.List<Triangle3> tris = new System.Collections.Generic.List<Triangle3>();
        
        Triangle3[] p1tris = GetStraightSpokePlane(origin1.Copy(), (+_spokeIndent / 2), TagTop1, TagTop2);
        Triangle3[] p2tris = GetStraightSpokePlane(origin2.Copy(), (-_spokeIndent / 2), TagBottom1, TagBottom2);
		MirrorVertTags(p2tris);
        Triangle3 trinew;

        for (int i = 0; i < p1tris.Length; i++)  //Works since both arrays are same len.
        {
            var tri1 = p1tris[i].Copy();
            var tri2 = p2tris[i].Copy();
            trinew = new Triangle3();
            trinew.a = tri1.a;
            trinew.b = tri1.b;
            trinew.c = tri1.c;
            trinew.Tag = tri1.Tag;
			trinew.SortOrder = _triOrder;
			_triOrder += 2;
            tris.Add(trinew);

            trinew = new Triangle3();
            trinew.a = tri2.c;
            trinew.b = tri2.b;
            trinew.c = tri2.a;
            trinew.Tag = tri2.Tag;
			trinew.SortOrder = _triOrder + 1;
            trinew.Complement = tri1;

            tris.Add(trinew);

        }
		
		if(_hubRadius != 0){
        	Triangle3[] p3tris = GetHubPlane(origin1.Copy(), (+_hubHeight / 2.0f), TRI_HubTop);
        	Triangle3[] p4tris = GetHubPlane(origin2.Copy(), (-_hubHeight / 2.0f), TRI_HubBottom);
			MirrorVertTags(p4tris);
		
	        for (int i = 0; i < p3tris.Length; i++)  //Works since both arrays are same len.
	        {
	            var tri1 = p3tris[i].Copy();
	            var tri2 = p4tris[i].Copy();
	            trinew = new Triangle3();
	            trinew.a = tri1.a;
	            trinew.b = tri1.b;
	            trinew.c = tri1.c;
	            trinew.Tag = tri1.Tag;
				trinew.SortOrder = _triOrder;
				_triOrder += 2;
	            tris.Add(trinew);
	             
	            trinew = new Triangle3();
	            trinew.a = tri2.c;
	            trinew.b = tri2.b;
	            trinew.c = tri2.a;
	            trinew.Tag = tri2.Tag;
	            trinew.Complement = tri1;
				trinew.SortOrder = _triOrder + 1;
	            tris.Add(trinew);		
	
	        }
			
			
		}
			
        return tris.ToArray();
	}
	
	private Triangle3[] StitchEdges(Triangle3[] tris, string TopTriTag, string BottomTriTag, string StitchTopVertName1, string StitchTopVertName2, string StitchBottomVertName1, string StitchBottomVertName2, string TopTag, string BottomTag)
    {
        System.Collections.Generic.List<Triangle3> ret = new List<Triangle3>();
		
		foreach (var item in tris)
        {
			ret.AddRange(StitchEdge(item, TopTriTag, BottomTriTag, StitchTopVertName1, StitchTopVertName2, StitchBottomVertName1, StitchBottomVertName2,TopTag, BottomTag));
			
		}
		
		return ret.ToArray();
	}
	
    private Triangle3[] StitchEdge(Triangle3 item, string TopTriTag, string BottomTriTag, string StitchTopVertName1, string StitchTopVertName2, string StitchBottomVertName1, string StitchBottomVertName2, string TopTag, string BottomTag)
    {
        System.Collections.Generic.List<Triangle3> ret = new List<Triangle3>();
       
        //Get first tri from top, get first tri from bottom...
        int i = 0;

            Triangle3 topTri;
            Triangle3 bottomTri;
            i += 1;

            if (item.Complement != null && item.Complement.Tag == TopTriTag)
            {
                bottomTri = item;
                topTri = item.Complement;
                Quad3D q3 = new Quad3D();

				TVector3 t1 = TVector3.Tzero.Copy();
				TVector3 t2 = TVector3.Tzero.Copy();
				TVector3 b1 = TVector3.Tzero.Copy();
				TVector3 b2 = TVector3.Tzero.Copy();
				
				switch (StitchTopVertName1.ToLower()) {
					case "a":
						t1 = topTri.a.Copy();
						
					break;
				case "b":
					t1 = topTri.b.Copy();
					
					break;
					
				case "c":
					t1 = topTri.c.Copy();
					
					break;
				default:
				break;
				}
				
				switch (StitchTopVertName2.ToLower()) {
					case "a":
						t2 = topTri.a.Copy();
						
					break;
				case "b":
					t2 = topTri.b.Copy();
					
					break;
					
				case "c":
					t2 = topTri.c.Copy();
					
					break;
				default:
				break;
				}
				
				
				switch (StitchBottomVertName1.ToLower()) {
					case "a":
						b1 = bottomTri.a.Copy();
					break;
				case "b":
					b1 = bottomTri.b.Copy();
					break;
					
				case "c":
					b1 = bottomTri.c.Copy();
					break;
				default:
				break;
				}
				
				switch (StitchBottomVertName2.ToLower()) {
					case "a":
						b2 = bottomTri.a.Copy();
					break;
				case "b":
					b2 = bottomTri.b.Copy();
					break;
					
				case "c":
					b2 = bottomTri.c.Copy();
					break;
				default:
				break;
				}
				
				
				
					q3.a = t1;
	                q3.b = b2;
	                q3.c = b1;
	                q3.d = t2;
					q3.Tag = TopTag;
				
					var newtris = q3.GetTris();
					newtris[0].SortOrder = _triOrder;
					_triOrder += 2;		
					newtris[1].SortOrder = _triOrder;
					_triOrder += 2;		
			
					newtris[1].Tag = BottomTag;
				
					ret.AddRange(newtris);
					
				}

        return ret.ToArray();
    }
	
	TVector3 RotatePoint(float Rotation, TVector3 pt){
		TVector3 ret;	
		float Rot = Mathf.Deg2Rad * Rotation;
		
		if (Rotation == 0 || pt.Value == Vector3.zero){
			ret = pt.Copy();	

		}else{
			
			
		
			ret = new TVector3(pt.Value.x * Mathf.Cos(Rot) - pt.Value.y * Mathf.Sin(Rot), 
				pt.Value.x * Mathf.Sin(Rot) + pt.Value.y * Mathf.Cos(Rot), pt.Value.z, pt.Tag);
			
			
			
		}
		
		
		return ret;
	}
	
	public struct Shape3D
	{
		public Triangle3[] tris;
		public TVector2[] UV; 
	}
	
	public struct Boundary
	{
		public Vector3 min;
		public Vector3 max; 
		public Vector3 size;
	}
	
	private Quad2D CreateQuadFromLineSegment (LineSegment2D ls, float TailLen, float HeadLen)
	{	
		float pitch = ls.Pitch ();
		Quad2D ret = new Quad2D ();

		
		float theta = pitch;
		ret.a = new TVector2(TailLen * Mathf.Sin(theta) + ls.a.Value.x, -TailLen * Mathf.Cos(theta) + ls.a.Value.y, ls.a.Tag);
		ret.d = new TVector2(-TailLen * Mathf.Sin(theta) + ls.a.Value.x, TailLen * Mathf.Cos(theta) + ls.a.Value.y, ls.a.Tag);
		
		ret.b = new TVector2(HeadLen * Mathf.Sin(theta) + ls.b.Value.x, -HeadLen * Mathf.Cos(theta) + ls.b.Value.y, ls.b.Tag);
		ret.c = new TVector2(-HeadLen * Mathf.Sin(theta) + ls.b.Value.x, HeadLen * Mathf.Cos(theta) + ls.b.Value.y, ls.b.Tag);


		return ret;
	}

}
