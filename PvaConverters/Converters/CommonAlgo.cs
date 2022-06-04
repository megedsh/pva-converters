using System;
using PvaConverters.Model;
using PvaConverters.Mtrx;

namespace PvaConverters.Converters
{
    public class CommonAlgo
    {
        public static Vector3d TransformNedToEcef(double north, double east, double down, GeoPosition origin)
        {
            double x = north;
            double y = east;
            double z = down;

            Matrix T = LllnToEcefMatrix(origin);

            double ex = T[0, 0] * x + T[0, 1] * y + T[0, 2] * z;
            double ey = T[1, 0] * x + T[1, 1] * y + T[1, 2] * z;
            double ez = T[2, 0] * x + T[2, 1] * y + T[2, 2] * z;
            return new Vector3d(ex, ey, ez);
        }

        public static Matrix LllnToEcefMatrix(GeoPosition origin)
        {
            double[,] matrix = new double[3, 3];

            double lat = origin.Latitude.Radians;
            double lon = origin.Longitude.Radians;

            double cosLat = Math.Cos(lat);
            double sinLat = Math.Sin(lat);
            double cosLon = Math.Cos(lon);
            double sinLon = Math.Sin(lon);

            matrix[0, 0] = -cosLon * sinLat;
            matrix[0, 1] = -sinLon;
            matrix[0, 2] = -cosLon * cosLat;
            matrix[1, 0] = -sinLon * sinLat;
            matrix[1, 1] = cosLon;
            matrix[1, 2] = -sinLon * cosLat;
            matrix[2, 0] = cosLat;
            matrix[2, 1] = 0.0;
            matrix[2, 2] = -sinLat;
            return new Matrix(matrix);
        }
    }
}