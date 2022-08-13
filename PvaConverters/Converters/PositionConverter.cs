using System;
using PvaConverters.Interfaces;
using PvaConverters.Model;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;
using PvaConverters.Mtrx;

namespace PvaConverters.Converters
{
    public class PositionConverter : IPositionConverter
    {
        #region Ltp

        public GeoPosition LtpToGeo(LtpPosition ltpPosition, GeoPosition origin, Datum datum = null) => ltpToGeo(ltpPosition, origin, datum);
        public EcefPosition LtpToEcef(LtpPosition ltpPosition, GeoPosition origin, Datum datum = null) => ltpToEcef(ltpPosition, origin, datum);
        public AzimuthElevationRange LtpToAer(LtpPosition ltpPosition)
        {
            fromCartesian3DToPolar3D(ltpPosition.North.Meters, ltpPosition.East.Meters, ltpPosition.Up.Meters, out double alphaRadians, out double betaRadians, out double r);

            return new AzimuthElevationRange(Angle.FromRadians(alphaRadians),
                Angle.FromRadians(betaRadians), Distance.FromMeters(r));
        }

        #endregion

        #region Geo

        public EcefPosition GeoToEcef(GeoPosition geo, Datum datum = null)
        {
            double lat = geo.Latitude.Radians;
            double lon = geo.Longitude.Radians;
            double alt = geo.Altitude.Meters;
            return GeoToEcef(lat, lon, alt);
        }

        public EcefPosition GeoToEcef(double latRad, double lonRad, double altMeters, Datum datum = null)
        {
            if (datum == null)
            {
                datum = Datum.WGS84;
            }

            double f = datum.Flattening;
            double majorAxis = datum.SemiMajorAxis;

            double cosLat = Math.Cos(latRad);
            double sinLat = Math.Sin(latRad);

            double cosLong = Math.Cos(lonRad);
            double sinLong = Math.Sin(lonRad);

            double c = 1 / Math.Sqrt(cosLat * cosLat + (1 - f) * (1 - f) * sinLat * sinLat);
            double s = (1 - f) * (1 - f) * c;

            double x = (majorAxis * c + altMeters) * cosLat * cosLong;
            double y = (majorAxis * c + altMeters) * cosLat * sinLong;
            double z = (majorAxis * s + altMeters) * sinLat;
            return new EcefPosition(x, y, z);
        }

        public LtpPosition GeoToLtp(GeoPosition target, GeoPosition origin, Datum datum = null)
        {
            EcefPosition ecef = GeoToEcef(target);
            return ecefToLtp(ecef, origin, datum);
        }

        public AzimuthElevationRange GeoToAer(GeoPosition origin, GeoPosition target, Datum datum = null)
        {
            LtpPosition ltp = GeoToLtp(target, origin, datum);
            fromCartesian3DToPolar3D(ltp.North.Meters, ltp.East.Meters, ltp.Up.Meters, out double alphaRadians, out double betaRadians, out double r);

            return new AzimuthElevationRange(Angle.FromRadians(alphaRadians),
                Angle.FromRadians(betaRadians), Distance.FromMeters(r));
        }

        #endregion

        #region Ecef

        public LtpPosition EcefToLtp(EcefPosition ecefPosition, GeoPosition origin, Datum datum = null) => ecefToLtp(ecefPosition, origin, datum);


        public GeoPosition EcefToGeo(EcefPosition ecef, Datum datum = null)
        {
            return EcefToGeo(ecef.X, ecef.Y, ecef.Z);
        }

        public GeoPosition EcefToGeo(double xMeters, double yMeters, double zMeters, Datum datum = null)
        {
            if (datum == null)
            {
                datum = Datum.WGS84;
            }

            double majorAxis = datum.SemiMajorAxis;
            double polarRadius = majorAxis - majorAxis * datum.Flattening; // Polar radius

            double ea = Math.Sqrt((majorAxis * majorAxis - polarRadius * polarRadius) / (majorAxis * majorAxis));
            double eb = Math.Sqrt((majorAxis * majorAxis - polarRadius * polarRadius) / (polarRadius * polarRadius));
            double p = Math.Sqrt(xMeters * xMeters + yMeters * yMeters);

            double theta = Math.Atan2(zMeters * majorAxis, p * polarRadius);
            double lon = Math.Atan2(yMeters, xMeters);
            double lat = Math.Atan2(zMeters + eb * eb * polarRadius * Math.Pow(Math.Sin(theta), 3),
                p - ea * ea * majorAxis * Math.Pow(Math.Cos(theta), 3));
            double n = majorAxis / Math.Sqrt(1 - ea * ea * Math.Sin(lat) * Math.Sin(lat));
            double alt = p / Math.Cos(lat) - n;

            return new GeoPosition(Angle.FromRadians(lat), Angle.FromRadians(lon), Distance.FromMeters(alt));
        }

        public AzimuthElevationRange EcefToAer(EcefPosition target, GeoPosition origin, Datum datum = null)
        {
            LtpPosition ltp = EcefToLtp(target, origin, datum);

            fromCartesian3DToPolar3D(ltp.North.Meters, ltp.East.Meters, ltp.Up.Meters, out double alphaRadians, out double betaRadians, out double r);

            return new AzimuthElevationRange(Angle.FromRadians(alphaRadians),
                Angle.FromRadians(betaRadians), Distance.FromMeters(r));
        }

        #endregion

        #region AzimuthElevationRange

        public EcefPosition AerToEcef(GeoPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null)
        {
            GeoPosition geo = AerToGeo(origin, azimuthElevationRange, datum);
            return GeoToEcef(geo);
        }


        public GeoPosition AerToGeo(GeoPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null)
        {
            fromPolar3DToCartesian3D(getNotNaN(azimuthElevationRange.Azimuth.Radians),
                getNotNaN(azimuthElevationRange.Elevation.Radians),
                getNotNaN(azimuthElevationRange.Range.Meters, 1),
                out double x, out double y, out double z);

            return ltpToGeo(new LtpPosition(x, y, -z), origin);
        }

        public LtpPosition AerToLtp(GeoPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null)
        {
            fromPolar3DToCartesian3D(getNotNaN(azimuthElevationRange.Azimuth.Radians),
                getNotNaN(azimuthElevationRange.Elevation.Radians),
                getNotNaN(azimuthElevationRange.Range.Meters, 1),
                out double x, out double y, out double z);

            return new LtpPosition(x, y, -z);
        }

        #endregion

        #region private

        private EcefPosition ltpToEcef(LtpPosition ltpPosition, GeoPosition origin, Datum datum = null)
        {
            double x = ltpPosition.East.Meters;
            double y = ltpPosition.North.Meters;
            double z = ltpPosition.Up.Meters;

            getEcefVectorFromEnu(origin, x, y, z, out double ex, out double ey, out double ez, datum);
            return new EcefPosition(ex, ey, ez);
        }

        private GeoPosition ltpToGeo(LtpPosition ltpPosition, GeoPosition origin, Datum datum = null)
        {
            double x = ltpPosition.East.Meters;
            double y = ltpPosition.North.Meters;
            double z = ltpPosition.Up.Meters;

            getEcefVectorFromEnu(origin, x, y, z, out double ex, out double ey, out double ez, datum);
            return EcefToGeo(ex, ey, ez, datum);
        }

        private void getEcefVectorFromEnu(GeoPosition origin, double eastMeters, double northMeters, double upMeters,
            out double x, out double y, out double z, Datum datum = null)
        {
            double[,] T = getEnuToEcefTransformMatrix(origin);
            x = T[0, 0] * eastMeters + T[0, 1] * northMeters + T[0, 2] * upMeters + T[0, 3];
            y = T[1, 0] * eastMeters + T[1, 1] * northMeters + T[1, 2] * upMeters + T[1, 3];
            z = T[2, 0] * eastMeters + T[2, 1] * northMeters + T[2, 2] * upMeters + T[2, 3];
        }

        private LtpPosition ecefToLtp(EcefPosition eceFfromLla, GeoPosition origin, Datum datum = null)
        {
            ecefToEnuVector(eceFfromLla, origin, datum, out double eastMeters, out double northMeters, out double upMeters);
            return new LtpPosition(northMeters, eastMeters, -upMeters);
        }

        private void ecefToEnuVector(EcefPosition eceFfromLla, GeoPosition origin, Datum datum, out double tx, out double ty, out double tz)
        {
            double[,] T = MatrixAlgo.Invert(getEnuToEcefTransformMatrix(origin, datum));
            double[] p = { eceFfromLla.X, eceFfromLla.Y, eceFfromLla.Z };

            tx = T[0, 0] * p[0] + T[0, 1] * p[1] + T[0, 2] * p[2] + T[0, 3];
            ty = T[1, 0] * p[0] + T[1, 1] * p[1] + T[1, 2] * p[2] + T[1, 3];
            tz = T[2, 0] * p[0] + T[2, 1] * p[1] + T[2, 2] * p[2] + T[2, 3];
        }

        private double[,] getEnuToEcefTransformMatrix(GeoPosition origin, Datum datum = null)
        {
            EcefPosition ecef = GeoToEcef(origin, datum);

            double lat = origin.Latitude.Radians;
            double lon = origin.Longitude.Radians;
            double sa = Math.Sin(lat);
            double ca = Math.Cos(lat);
            double so = Math.Sin(lon);
            double co = Math.Cos(lon);

            return new[,]
            {
                { -so, -sa * co, ca * co, ecef.X },
                { co, -sa * so, ca * so, ecef.Y },
                { 0, ca, sa, ecef.Z },
                { 0, 0, 0, 1 }
            };
        }

        private static void fromCartesian3DToPolar3D(double north, double east, double up, out double alphaRadians, out double betaRadians, out double r)
        {
            r = Math.Sqrt(Math.Pow(north, 2) + Math.Pow(east, 2) + Math.Pow(up, 2));

            if (north > 0 && east >= 0)
            {
                alphaRadians = Math.Atan(east / north);
            }
            else if (north > 0 && east < 0)
            {
                alphaRadians = Math.Atan(east / north) + 2 * Math.PI;
            }
            else if (north < 0)
            {
                alphaRadians = Math.Atan(east / north) + Math.PI;
            }
            else if (north == 0 && east > 0)
            {
                alphaRadians = Math.PI / 2;
            }
            else
            {
                alphaRadians = 3 * Math.PI / 2;
            }

            if (alphaRadians == 2 * Math.PI)
            {
                alphaRadians = 0;
            }

            double term = Math.Sqrt(Math.Pow(north, 2) + Math.Pow(east, 2));

            if (term == 0)
            {
                betaRadians = 0;
            }
            else
            {
                betaRadians = Math.Atan(up / term);
            }

            if (betaRadians == 2 * Math.PI)
            {
                betaRadians = 0;
            }
        }

        private static void fromPolar3DToCartesian3D(double alphaRadians, double betaRadians, double r, out double x, out double y, out double z)
        {
            x = r * Math.Cos(betaRadians) * Math.Cos(alphaRadians);
            y = r * Math.Cos(betaRadians) * Math.Sin(alphaRadians);
            z = r * Math.Sin(betaRadians);
        }

        private static double getNotNaN(double value, double defaultValue = 0) => double.IsNaN(value) ? defaultValue : value;

        #endregion
    }
}