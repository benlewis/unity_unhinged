
using System.Collections;
using UnityEngine;

    public struct LineSegment2D
    {
        public TVector2 a;
        public TVector2 b;

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

                    default:
                        throw new System.IndexOutOfRangeException("Invalid LineSegment2D index!");
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
                    default:
                        throw new System.IndexOutOfRangeException("Invalid LineSegment2D index!");
                }
            }
        }

		public float Length(){
			return Vector2.Distance(this.a.Value, this.b.Value);
		}
	
		public float Pitch(){
			float angle = 0.0f;
		
		
			angle = Mathf.Atan2(this.b.Value.y - this.a.Value.y, this.b.Value.x - this.a.Value.x);
		
			return angle;
		}
	
    }