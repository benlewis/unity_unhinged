
using System.Collections;


public struct Quad2D
    {
        public TVector2 a;
        public TVector2 b;
        public TVector2 c;
		public TVector2 d;
		public string Tag;
	
        public TVector2 this[int index]
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
                        throw new System.IndexOutOfRangeException("Invalid Quad2D index!");
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
                        throw new System.IndexOutOfRangeException("Invalid Quad2D index!");
                }
            }	
        }
	
		public Triangle2[] GetTris(){
			Triangle2[] tris = new Triangle2[2];
			Triangle2 t1 = new Triangle2();
			Triangle2 t2 = new Triangle2();
		
			t1.a = a;
			t1.b = b;
			t1.c = c;
			t1.Tag = this.Tag;
			t2.a = a;
			t2.b = b;
			t2.c = d;
			t2.Tag = this.Tag;
			tris[0] = t1;
			tris[1] = t2;
		
			return tris;
		}
	
	
	


    }

