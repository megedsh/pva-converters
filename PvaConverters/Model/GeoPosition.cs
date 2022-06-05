using PvaConverters.Model.Scalars;

namespace PvaConverters.Model
{
    public readonly struct GeoPosition
    {
        public Angle Latitude { get; }
        public Angle Longitude { get; }
        public Distance Altitude { get; }

        public static GeoPosition FromDeg(double latDeg, double lonDeg, double altMeter)
        {
            var lat = Angle.FromDegrees(latDeg);
            var lon = Angle.FromDegrees(lonDeg);
            return new GeoPosition(lat, lon, Distance.FromMeters(altMeter));
        }
    

        public GeoPosition(double latRad, double lonRad, double altMeters) : this(Angle.FromRadians(latRad),
            Angle.FromRadians(lonRad), Distance.FromMeters(altMeters))
        {
        }

        public GeoPosition(Angle lat, Angle lon, Distance alt)
        {
            Latitude = lat;
            Longitude = lon;
            Altitude = alt;
        }

        public bool Equals(GeoPosition other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude) && Altitude.Equals(other.Altitude);
        }

        public override bool Equals(object obj)
        {
            return obj is GeoPosition other && Equals(other);
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