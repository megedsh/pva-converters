namespace NavigationToolkit.Model.Ecef
{
    public class EcefAcceleration : IEcef
    {
        public static EcefAcceleration Empty = new EcefAcceleration(double.NaN, double.NaN, double.NaN);
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public EcefAcceleration(double xMetersPerSquareSec, double yMetersPerSquareSec, double zMetersPerSquareSec)
        {
            X = xMetersPerSquareSec;
            Y = yMetersPerSquareSec;
            Z = zMetersPerSquareSec;
        }

        protected bool Equals(EcefAcceleration other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EcefAcceleration)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}";
        }
    }
}