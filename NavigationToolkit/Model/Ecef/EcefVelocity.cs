namespace NavigationToolkit.Model.Ecef
{
    public struct EcefVelocity : IEcef
    {
        public static EcefVelocity Empty = new EcefVelocity(double.NaN, double.NaN, double.NaN);
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public EcefVelocity(double xMetersPerSec, double yMetersPerSec, double zMetersPerSec)
        {
            X = xMetersPerSec;
            Y = yMetersPerSec;
            Z = zMetersPerSec;
        }

        public bool Equals(EcefVelocity other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            return obj is EcefVelocity other && Equals(other);
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