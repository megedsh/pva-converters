using System;
using PvaConverters.Model;
using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;
using PvaConverters.Mtrx;

namespace PvaConverters.Converters
{
    public abstract class CartesianBase<TLtp, TEcef, TScalar, TAeronautical>
        where TScalar : IScalar
        where TLtp : ILocalTangentPlane<TScalar>
        where TEcef : EcefBase<TScalar>
        where TAeronautical : AeronauticalVector<TScalar>
    {
        protected abstract TEcef createEcef(double x, double y, double z);
        protected abstract TLtp createLtp(double north, double east, double down);
        protected abstract TScalar createScalar(double scalar);


        #region Ltp

        public TEcef LtpToEcef(TLtp ltp, GeoPosition originGeoPosition) => LtpToEcef(ltp.North.AsDouble(), ltp.East.AsDouble(), ltp.Down.AsDouble(), originGeoPosition);

        public TEcef LtpToEcef(double north, double east, double down, GeoPosition originGeoPosition)
        {
            Vector3d ecef = CommonAlgo.TransformNedToEcef(north, east, down, originGeoPosition);
            return createEcef(ecef.X, ecef.Y, ecef.Z);
        }

        #endregion

        #region Ecef

        public TLtp EcefToLtp(TEcef ecef, GeoPosition originGeoPosition)
        {
            return EcefToLtp(ecef.X, ecef.Y, ecef.Z, originGeoPosition);
        }

        public TLtp EcefToLtp(double x, double y, double z, GeoPosition originGeoPosition)
        {
            Vector3d v = getNedVectorFromEcef(x, y, z, originGeoPosition);

            return createLtp(v.X, v.Y, v.Z);
        }

        public void EcefToAeronautical(out Angle course,
            out TScalar vertical,
            out TScalar horiz,
            TEcef ecef, GeoPosition originGeoPosition)
        {
            var ned = EcefToLtp(ecef.X, ecef.Y, ecef.Z, originGeoPosition);
            nedToAeronautical(out course, out vertical, out horiz, ned);
        }

        #endregion

        #region Aeronautical

        public TEcef AeronauticalToEcef(TAeronautical an, GeoPosition originGeoPosition) => AeronauticalToEcef(an.Course.Radians, an.Vertical.AsDouble(), an.Horizontal.AsDouble(), originGeoPosition);

        public TEcef AeronauticalToEcef(double courseRad, double vertical,
            double horizontal, GeoPosition originGeoPosition)
        {
            Vector3d nedVector = getNedVector(courseRad, vertical, horizontal);

            return LtpToEcef(nedVector.X, nedVector.Y, nedVector.Z, originGeoPosition);
        }

        public TLtp AeronauticalToLtp(TAeronautical an)
        {
            var nedVector = getNedVector(an.Course.Radians, an.Vertical.AsDouble(), an.Horizontal.AsDouble());
            return createLtp(nedVector.X, nedVector.Y, nedVector.Z);
        }

        #endregion

        #region private

        private static Vector3d getNedVectorFromEcef(double x,
            double y,
            double z, GeoPosition originGeoPosition)
        {
            Matrix m = CommonAlgo.GeoPositionToEcefMatrix(originGeoPosition);
            Matrix T = m.Transpose();

            double ex = T[0, 0] * x + T[0, 1] * y + T[0, 2] * z;
            double ey = T[1, 0] * x + T[1, 1] * y + T[1, 2] * z;
            double ez = T[2, 0] * x + T[2, 1] * y + T[2, 2] * z;

            return new Vector3d(ex, ey, ez);
        }

        private void nedToAeronautical(out Angle course, out TScalar vertical, out TScalar horiz, ILocalTangentPlane<TScalar> ned)
        {
            double azRad = 0;
            double x = ned.North.AsDouble();
            double y = ned.East.AsDouble();
            double z = ned.Down.AsDouble();

            double magnitude = Math.Sqrt(x * x + y * y + z * z);

            if (magnitude > 0.0)
            {
                double normX = x / magnitude;
                double normY = y / magnitude;
                int num = Math.Abs(x) >= 1E-10 ? 0 : Math.Abs(y) < 1E-10 ? 1 : 0;
                azRad = num == 0 ? Math.Atan2(normY, normX) : 0.0;
            }

            course = Angle.FromRadians(azRad);
            vertical = ned.Up;
            horiz = createScalar(Math.Sqrt(x * x + y * y));
        }

        private static Vector3d getNedVector(double courseRad, double vertical,
            double horizontal)
        {
            double north = horizontal * Math.Cos(courseRad);
            double east = horizontal * Math.Sin(courseRad);
            double down = -vertical;
            return new Vector3d(north, east, down);
        }

        private static Matrix calcRzC(AzimuthElevationRange raePos)
        {
            double c = raePos.Azimuth.Radians;
            double[,] v =
            {
                { Math.Cos(c), Math.Sin(c), 0.0, 0.0 },
                { -1 * Math.Sin(c), Math.Cos(c), 0.0, 0.0 },
                { 0.0, 0.0, 1.0, 0.0 },
                { 0.0, 0.0, 0.0, 1.0 }
            };
            return new Matrix(v);
        }

        private static Matrix calcRyB(AzimuthElevationRange raePos)
        {
            double b = raePos.Elevation.Radians;
            double[,] v =
            {
                { Math.Cos(b), 0.0, -1 * Math.Sin(b), 0.0 },
                { 0.0, 1.0, 0.0, 0.0 },
                { Math.Sin(b), 0.0, Math.Cos(b), 0.0 },
                { 0.0, 0.0, 0.0, 1.0 }
            };
            return new Matrix(v);
        }

        private static Matrix calcRxA()
        {
            double a = 3 * Math.PI / 2;
            double[,] v =
            {
                { 1, 0.0, 0.0, 0.0 },
                { 0.0, Math.Cos(a), Math.Sin(a), 0.0 },
                { 0.0, -1 * Math.Sin(a), Math.Cos(a), 0.0 },
                { 0.0, 0.0, 0.0, 1.0 }
            };
            return new Matrix(v);
        }

        private static Matrix calcRotationMatrix(Matrix r1, Matrix r2, Matrix r3)
        {
            Matrix m1 = MatrixAlgo.Multiply(r2, r1);
            Matrix m2 = MatrixAlgo.Multiply(r3, m1);
            return m2;
        }

        private static Matrix ltpToMatrix4D(TLtp ltp, double val4Th) =>
            new Matrix(new[,]
            {
                { ltp.North.AsDouble() },
                { ltp.East.AsDouble() },
                { ltp.Down.AsDouble() },
                { val4Th }
            });

        #endregion
    }
}