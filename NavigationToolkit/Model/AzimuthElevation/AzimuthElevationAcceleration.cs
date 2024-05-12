namespace NavigationToolkit.Model.AzimuthElevation
{
    public struct AzimuthElevationAcceleration : IAzimuthElevation
    {
        public static AzimuthElevationAcceleration Empty = new AzimuthElevationAcceleration(double.NaN, double.NaN, double.NaN);
        public double Acceleration { get; }
        public double Azimuth { get; }
        public double Elevation { get; }
        public double GetScalar() => Acceleration;

        public AzimuthElevationAcceleration(double azimuthDeg, double elevationDeg, double metersPerSquareSec)
        {
            Azimuth = azimuthDeg;
            Elevation = elevationDeg;
            Acceleration = metersPerSquareSec;
        }

        public bool Equals(AzimuthElevationAcceleration other)
        {
            return Azimuth.Equals(other.Azimuth) && Elevation.Equals(other.Elevation) && Acceleration.Equals(other.Acceleration);
        }

        public override bool Equals(object obj)
        {
            return obj is AzimuthElevationAcceleration other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Azimuth.GetHashCode();
                hashCode = (hashCode * 397) ^ Elevation.GetHashCode();
                hashCode = (hashCode * 397) ^ Acceleration.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Azimuth)}: {Azimuth}, {nameof(Elevation)}: {Elevation}, {nameof(Acceleration)}: {Acceleration}";
        }
    }
}