namespace PvaConverters.Model.AzimuthElevation
{
    public struct AzimuthElevationDistance : IAzimuthElevation
    {
        public static AzimuthElevationDistance Empty = new AzimuthElevationDistance(double.NaN, double.NaN, double.NaN);
        public double Distance { get; }
        public double Azimuth { get; }
        public double Elevation { get; }
        public double GetScalar() => Distance;

        public AzimuthElevationDistance(double azimuthDeg, double elevationDeg, double distanceMeters)
        {
            Azimuth = azimuthDeg;
            Elevation = elevationDeg;
            Distance = distanceMeters;
        }

        public bool Equals(AzimuthElevationDistance other)
        {
            return Azimuth.Equals(other.Azimuth) && Elevation.Equals(other.Elevation) && Distance.Equals(other.Distance);
        }

        public override bool Equals(object obj)
        {
            return obj is AzimuthElevationDistance other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Azimuth.GetHashCode();
                hashCode = (hashCode * 397) ^ Elevation.GetHashCode();
                hashCode = (hashCode * 397) ^ Distance.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Azimuth)}: {Azimuth}, {nameof(Elevation)}: {Elevation}, {nameof(Distance)}: {Distance}";
        }
    }
}