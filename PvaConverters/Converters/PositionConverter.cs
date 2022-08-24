using System;
using PvaConverters.Interfaces;
using PvaConverters.Model;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Mtrx;

namespace PvaConverters.Converters
{
    public class PositionConverter : IPositionConverter
    {
        private static readonly double s_piTo180 = (Math.PI / 180.0);

        #region Ltp

        public LlaPosition LtpToLla(LtpPosition ltpPosition, LlaPosition origin, Datum datum = null) => ltpToLla(ltpPosition, origin, datum);
        public EcefPosition LtpToEcef(LtpPosition ltpPosition, LlaPosition origin, Datum datum = null) => ltpToEcef(ltpPosition, origin, datum);

        public AzimuthElevationDistance LtpToAed(LtpPosition ltpPosition)
        {
            fromCartesian3DToPolar3D(ltpPosition.North, ltpPosition.East, ltpPosition.Up, out double alphaRadians, out double betaRadians, out double r);

            return new AzimuthElevationDistance(alphaRadians / s_piTo180, betaRadians / s_piTo180, r);
        }

        #endregion

        #region Lla

        public EcefPosition LlaToEcef(LlaPosition lla, Datum datum = null)
        {
            double lat = lla.Latitude * s_piTo180;
            double lon = lla.Longitude * s_piTo180;
            double alt = lla.Altitude;
            return LlaToEcef(lat, lon, alt);
        }

        public EcefPosition LlaToEcef(double latRad, double lonRad, double altMeters, Datum datum = null)
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

        public LtpPosition LlaToLtp(LlaPosition target, LlaPosition origin, Datum datum = null)
        {
            EcefPosition ecef = LlaToEcef(target);
            return ecefToLtp(ecef, origin, datum);
        }

        public AzimuthElevationDistance LlaToAed(LlaPosition origin, LlaPosition target, Datum datum = null)
        {
            LtpPosition ltp = LlaToLtp(target, origin, datum);
            fromCartesian3DToPolar3D(ltp.North, ltp.East, ltp.Up, out double alphaRadians, out double betaRadians, out double r);

            return new AzimuthElevationDistance(alphaRadians / s_piTo180, betaRadians / s_piTo180, r);
        }

        #endregion

        #region Ecef

        public LtpPosition EcefToLtp(EcefPosition ecefPosition, LlaPosition origin, Datum datum = null) => ecefToLtp(ecefPosition, origin, datum);


        public LlaPosition EcefToLla(EcefPosition ecef, Datum datum = null)
        {
            return EcefToLla(ecef.X, ecef.Y, ecef.Z);
        }

        public LlaPosition EcefToLla(double xMeters, double yMeters, double zMeters, Datum datum = null)
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

            return new LlaPosition(lat / s_piTo180, lon / s_piTo180, alt);
        }

        public AzimuthElevationDistance EcefToAed(EcefPosition target, LlaPosition origin, Datum datum = null)
        {
            LtpPosition ltp = EcefToLtp(target, origin, datum);

            fromCartesian3DToPolar3D(ltp.North, ltp.East, ltp.Up, out double alphaRadians, out double betaRadians, out double r);

            return new AzimuthElevationDistance(alphaRadians / s_piTo180, betaRadians / s_piTo180, r);
        }

        #endregion

        #region AzimuthElevationDistance

        public EcefPosition AedToEcef(LlaPosition origin, AzimuthElevationDistance azimuthElevationDistance, Datum datum = null)
        {
            LlaPosition lla = AedToLla(origin, azimuthElevationDistance, datum);
            return LlaToEcef(lla);
        }


        public LlaPosition AedToLla(LlaPosition origin, AzimuthElevationDistance azimuthElevationDistance, Datum datum = null)
        {
            fromPolar3DToCartesian3D(getNotNaN(azimuthElevationDistance.Azimuth * s_piTo180),
                getNotNaN(azimuthElevationDistance.Elevation * s_piTo180),
                getNotNaN(azimuthElevationDistance.Distance, 1),
                out double x, out double y, out double z);

            return ltpToLla(new LtpPosition(x, y, -z), origin);
        }

        public LtpPosition AedToLtp(LlaPosition origin, AzimuthElevationDistance azimuthElevationDistance, Datum datum = null)
        {
            fromPolar3DToCartesian3D(getNotNaN(azimuthElevationDistance.Azimuth * s_piTo180),
                getNotNaN(azimuthElevationDistance.Elevation * s_piTo180),
                getNotNaN(azimuthElevationDistance.Distance, 1),
                out double x, out double y, out double z);

            return new LtpPosition(x, y, -z);
        }

        #endregion

        #region private

        private EcefPosition ltpToEcef(LtpPosition ltpPosition, LlaPosition origin, Datum datum = null)
        {
            double x = ltpPosition.East;
            double y = ltpPosition.North;
            double z = ltpPosition.Up;

            getEcefVectorFromEnu(origin, x, y, z, out double ex, out double ey, out double ez, datum);
            return new EcefPosition(ex, ey, ez);
        }

        private LlaPosition ltpToLla(LtpPosition ltpPosition, LlaPosition origin, Datum datum = null)
        {
            double x = ltpPosition.East;
            double y = ltpPosition.North;
            double z = ltpPosition.Up;

            getEcefVectorFromEnu(origin, x, y, z, out double ex, out double ey, out double ez, datum);
            return EcefToLla(ex, ey, ez, datum);
        }

        private void getEcefVectorFromEnu(LlaPosition origin, double eastMeters, double northMeters, double upMeters,
            out double x, out double y, out double z, Datum datum = null)
        {
            double[,] T = getEnuToEcefTransformMatrix(origin);
            x = T[0, 0] * eastMeters + T[0, 1] * northMeters + T[0, 2] * upMeters + T[0, 3];
            y = T[1, 0] * eastMeters + T[1, 1] * northMeters + T[1, 2] * upMeters + T[1, 3];
            z = T[2, 0] * eastMeters + T[2, 1] * northMeters + T[2, 2] * upMeters + T[2, 3];
        }

        private LtpPosition ecefToLtp(EcefPosition eceFfromLla, LlaPosition origin, Datum datum = null)
        {
            ecefToEnuVector(eceFfromLla, origin, datum, out double eastMeters, out double northMeters, out double upMeters);
            return new LtpPosition(northMeters, eastMeters, -upMeters);
        }

        private void ecefToEnuVector(EcefPosition eceFfromLla, LlaPosition origin, Datum datum, out double tx, out double ty, out double tz)
        {
            double[,] T = MatrixAlgo.Invert(getEnuToEcefTransformMatrix(origin, datum));
            double[] p = { eceFfromLla.X, eceFfromLla.Y, eceFfromLla.Z };

            tx = T[0, 0] * p[0] + T[0, 1] * p[1] + T[0, 2] * p[2] + T[0, 3];
            ty = T[1, 0] * p[0] + T[1, 1] * p[1] + T[1, 2] * p[2] + T[1, 3];
            tz = T[2, 0] * p[0] + T[2, 1] * p[1] + T[2, 2] * p[2] + T[2, 3];
        }

        private double[,] getEnuToEcefTransformMatrix(LlaPosition origin, Datum datum = null)
        {
            EcefPosition ecef = LlaToEcef(origin, datum);

            double lat = origin.Latitude * s_piTo180;
            double lon = origin.Longitude * s_piTo180;
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