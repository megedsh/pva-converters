//namespace PvaConverters.Model
//{
//    public struct Vector3d : IVector3d
//    {
//        public Vector3d(double x, double y, double z)
//        {
//            X = x;
//            Y = y;
//            Z = z;
//        }

//        public double X { get; }
//        public double Y { get; }
//        public double Z { get; }

//        public bool Equals(Vector3d other)
//        {
//            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
//        }

//        public override bool Equals(object obj)
//        {
//            return obj is Vector3d other && Equals(other);
//        }

//        public override int GetHashCode()
//        {
//            unchecked
//            {
//                var hashCode = X.GetHashCode();
//                hashCode = (hashCode * 397) ^ Y.GetHashCode();
//                hashCode = (hashCode * 397) ^ Z.GetHashCode();
//                return hashCode;
//            }
//        }

//        public override string ToString()
//        {
//            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}";
//        }
//    }
//}

