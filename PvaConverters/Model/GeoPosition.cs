using PvaConverters.Model.Scalars;

namespace PvaConverters.Model
{
    public readonly struct GeoPosition
    {
        public Angle Latitude { get; }
        public Angle Longitude { get; }
        public Distance Altitude { get; }

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

        public override string ToString()
        {
            return $"Lat: {Latitude}, Lon: {Longitude}, Alt: {Altitude}";
        }
    }
}