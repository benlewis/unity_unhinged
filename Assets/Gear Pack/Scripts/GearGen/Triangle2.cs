
using System.Collections;

    public struct Triangle2
    {
        public TVector2 a;
        public TVector2 b;
        public TVector2 c;
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
                    default:
                        throw new System.IndexOutOfRangeException("Invalid Triangle2 index!");
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
                        throw new System.IndexOutOfRangeException("Invalid Triangle2 index!");
                }
            }
        }


    }