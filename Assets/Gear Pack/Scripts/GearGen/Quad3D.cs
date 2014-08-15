
using System.Collections;


public struct Quad3D
    {
        public TVector3 a;
        public TVector3 b;
        public TVector3 c;
		public TVector3 d;
        public string Tag;
	
	public Quad3D Create(Quad2D quad, float zOrigin){
		Quad3D q = new Quad3D();
		q.a = new TVector3(quad.a.Value.x, quad.a.Value.y, zOrigin, quad.a.Tag);
		q.b = new TVector3(quad.b.Value.x, quad.b.Value.y, zOrigin, quad.b.Tag);
		q.c = new TVector3(quad.c.Value.x, quad.c.Value.y, zOrigin, quad.c.Tag);
		q.d = new TVector3(quad.d.Value.x, quad.d.Value.y, zOrigin, quad.d.Tag);
		
		return q;
	}
	
	
		public TVector3 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.a;
                    case 1:
                        return this.b;
                    case 2:
                        return this.c;
				 	case 3:
                        return this.d;
                    default:
                        throw new System.IndexOutOfRangeException("Invalid Quad3D index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.a = value;
                        break;
                    case 1:
                        this.b = value;
                        break;
                    case 2:
                        this.c = value;
                        break;
					case 3:
                        this.d = value;
                        break;
                    default:
                        throw new System.IndexOutOfRangeException("Invalid Quad3D index!");
                }
            }
        }
	
		public Triangle3[] GetTris(){
				Triangle3[] tris = new Triangle3[2];
				Triangle3 t1 = new Triangle3();
				Triangle3 t2 = new Triangle3();
			
				t1.a = a;
				t1.b = b;
				t1.c = c;
				t1.Tag = this.Tag;
		
				t2.a = a;
				t2.b = c;
				t2.c = d;
				t2.Tag = this.Tag;
		
				tris[0] = t1;
				tris[1] = t2;
			
				return tris;
			}
	
	    }

		