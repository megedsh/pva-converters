namespace PvaConverters.Model.AzimuthElevation
{
    public struct AzimuthElevationVelocity : IAzimuthElevation
    {
        public double Velocity { get; }
        public double Azimuth { get; }
        public double Elevation { get; }
        public double GetScalar() => Velocity;

        public AzimuthElevationVelocity(double azimuthDeg, double elevationDeg, double metersPerSec)
        {
            Azimuth = azimuthDeg;
            Elevation = elevationDeg;
            Velocity = metersPerSec;
        }

        public bool Equals(AzimuthElevationVelocity other)
        {
            return Azimuth.Equals(other.Azimuth) && Elevation.Equals(other.Elevation) && Velocity.Equals(other.Velocity);
        }

        public override bool Equals(object obj)
        {
            return obj is AzimuthElevationVelocity other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Azimuth.GetHashCode();
                hashCode = (hashCode * 397) ^ Elevation.GetHashCode();
                hashCode = (hashCode * 397) ^ Velocity.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Azimuth)}: {Azimuth}, {nameof(Elevation)}: {Elevation}, {nameof(Velocity)}: {Velocity}";
        }
    }
}