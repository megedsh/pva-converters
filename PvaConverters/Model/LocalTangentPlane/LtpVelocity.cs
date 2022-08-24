namespace PvaConverters.Model.LocalTangentPlane
{
    public struct LtpVelocity : ILocalTangentPlane
    {
        public static LtpVelocity Empty = new LtpVelocity(double.NaN, double.NaN, double.NaN);
        public double East { get; }
        public double West { get; }
        public double North { get; }
        public double South { get; }
        public double Up { get; }
        public double Down { get; }

        public LtpVelocity(double northMetersPerSec, double eastMetersPerSec, double downMetersPerSec)
        {
            North = northMetersPerSec;
            East = eastMetersPerSec;
            West = -eastMetersPerSec;
            South = -northMetersPerSec;
            Up = -downMetersPerSec;
            Down = downMetersPerSec;
        }

        public bool Equals(LtpVelocity other)
        {
            return East.Equals(other.East) && North.Equals(other.North) && Down.Equals(other.Down);
        }

        public override bool Equals(object obj)
        {
            return obj is LtpVelocity other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = East.GetHashCode();
                hashCode = (hashCode * 397) ^ North.GetHashCode();
                hashCode = (hashCode * 397) ^ Down.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() => ToStringNed();

        public string ToStringNed()
        {
            return $"{nameof(North)}: {North}, {nameof(East)}: {East}, {nameof(Down)}: {Down}";
        }

        public string ToStringEnu()
        {
            return $"{nameof(East)}: {East}, {nameof(North)}: {North}, {nameof(Up)}: {Up}";
        }
    }
}