namespace NavigationToolkit.Model.Ecef
{
    public class EcefPosition : IEcef
    {
        public static EcefPosition Empty = new EcefPosition(double.NaN, double.NaN, double.NaN);
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public EcefPosition(double xMeters, double yMeters, double zMeters)
        {
            X = xMeters;
            Y = yMeters;
            Z = zMeters;
        }

        protected bool Equals(EcefPosition other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EcefPosition)obj);
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