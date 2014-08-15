
using System.Collections;


public class Triangle3
    {
        public TVector3 a;
        public TVector3 b;
        public TVector3 c;
		public string Tag;
		public int SortOrder = 0;
	
        public Triangle3 Empty
        {
            get
            {
                Triangle3 ret = new Triangle3();

                ret.a = TVector3.Tzero;
                ret.b = TVector3.Tzero;
                ret.c = TVector3.Tzero;
				ret .SortOrder = 0;
                ret.Tag = "";

                ret.Complement = null;

                return ret;

            }
        }

        public Triangle3 Complement;

        public Triangle3()
        {
            this.a = TVector3.Tzero;
            this.b = TVector3.Tzero;
            this.c = TVector3.Tzero;

            this.Tag = "";

            this.Complement = null;

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
                    default:
                        throw new System.IndexOutOfRangeException("Invalid Triangle3 index!");
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
                    default:
                        throw new System.IndexOutOfRangeException("Invalid Triangle3 index!");
                }
            }
        }

        public override bool Equals(object obj)
        {
            Triangle3 comp;

            comp = (Triangle3)obj;

            if (comp.a.Equals(this.a) && comp.b.Equals(this.b) && comp.c.Equals(this.c))
            {
                return true;

            }

            return false;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Triangle3 Copy()
        {
            Triangle3 ret = new Triangle3();

            ret.a = this.a.Copy();
            ret.b = this.b.Copy();
            ret.c = this.c.Copy();
            ret.Tag = this.Tag;
			ret.SortOrder = this.SortOrder;
            ret.Complement = new Triangle3();

            return ret;
        }
	
		public TVector3[] GetVerts(){
			return (new TVector3[]{this.a, this.b, this.c});
		}
	
	
		public UnityEngine.Vector3 Center(){
			UnityEngine.Vector3 c = UnityEngine.Vector3.zero;
		
			c = new UnityEngine.Vector3((this.a.Value.x + this.b.Value.x + this.c.Value.x) / 3.0f, (this.a.Value.y + this.b.Value.y + this.c.Value.y) / 3.0f, (this.a.Value.z + this.b.Value.z + this.c.Value.z) / 3.0f);
		
			return c;
		
		}
		public UnityEngine.Vector3 GetNormal(){
			var n = UnityEngine.Vector3.Cross((this.b.Value - this.a.Value), (this.c.Value - this.a.Value));
			n.Normalize();
			return n;
		}
	
	
    }

