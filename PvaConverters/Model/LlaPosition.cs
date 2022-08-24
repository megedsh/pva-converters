namespace PvaConverters.Model
{
    public readonly struct LlaPosition
    {
        public static LlaPosition Empty = new LlaPosition(double.NaN, double.NaN, double.NaN);
        public double Latitude { get; }
        public double Longitude { get; }
        public double Altitude { get; }


        public LlaPosition(double latDeg, double lonDeg, double altMeters)
        {
            Latitude = latDeg;
            Longitude = lonDeg;
            Altitude = altMeters;
        }

        public bool Equals(LlaPosition other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude) && Altitude.Equals(other.Altitude);
        }

        public override bool Equals(object obj)
        {
            return obj is LlaPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Altitude.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"Lat: {Latitude}, Lon: {Longitude}, Alt: {Altitude}";
        }
    }
}