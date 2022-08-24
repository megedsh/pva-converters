using System;
using PvaConverters.Model;
using PvaConverters.Mtrx;

namespace PvaConverters.Converters
{
    public class CommonAlgo
    {
        private static readonly double s_piTo180 = (Math.PI / 180.0);

        public static void TransformNedToEcef(double north, double east, double down, LlaPosition origin,
            out double x, out double y, out double z)
        {
            Matrix T = GeoPositionToEcefMatrix(origin);

            x = T[0, 0] * north + T[0, 1] * east + T[0, 2] * down;
            y = T[1, 0] * north + T[1, 1] * east + T[1, 2] * down;
            z = T[2, 0] * north + T[2, 1] * east + T[2, 2] * down;
        }

        public static Matrix GeoPositionToEcefMatrix(LlaPosition origin)
        {
            double[,] matrix = new double[3, 3];

            double lat = origin.Latitude * s_piTo180;
            double lon = origin.Longitude * s_piTo180;

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