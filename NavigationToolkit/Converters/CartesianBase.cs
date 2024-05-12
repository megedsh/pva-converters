using System;

using NavigationToolkit.Model;
using NavigationToolkit.Model.Aeronautical;
using NavigationToolkit.Model.AzimuthElevation;
using NavigationToolkit.Model.Ecef;
using NavigationToolkit.Model.LocalTangentPlane;
using NavigationToolkit.Mtrx;

namespace NavigationToolkit.Converters
{
    public abstract class CartesianBase<TLtp, TEcef, TAeronautical>
        where TLtp : ILocalTangentPlane
        where TEcef : IEcef
        where TAeronautical : IAeronauticalVector
    {
        private static readonly double s_piTo180 = (Math.PI / 180.0);
        protected abstract TEcef createEcef(double x, double y, double z);
        protected abstract TLtp createLtp(double north, double east, double down);
        protected abstract TAeronautical createAeronauticalVector(double courseDeg, double vertical, double horizontal);


        #region Ltp

        public TEcef LtpToEcef(TLtp ltp, LlaPosition originLlaPosition) => LtpToEcef(ltp.North, ltp.East, ltp.Down, originLlaPosition);

        public TEcef LtpToEcef(double north, double east, double down, LlaPosition originLlaPosition)
        {
            CommonAlgo.TransformNedToEcef(north, east, down, originLlaPosition, out double x, out double y, out double z);
            return createEcef(x, y, z);
        }

        #endregion

        #region Ecef

        public TLtp EcefToLtp(TEcef ecef, LlaPosition originLlaPosition)
        {
            return EcefToLtp(ecef.X, ecef.Y, ecef.Z, originLlaPosition);
        }

        public TLtp EcefToLtp(double x, double y, double z, LlaPosition originLlaPosition)
        {
            IEcef v = getNedVectorFromEcef(x, y, z, originLlaPosition);

            return createLtp(v.X, v.Y, v.Z);
        }

        public TAeronautical EcefToAeronautical(TEcef ecef, LlaPosition originLlaPosition)
        {
            var ned = EcefToLtp(ecef.X, ecef.Y, ecef.Z, originLlaPosition);
            nedToAeronautical(out double courseDeg, out double vertical, out double horiz, ned);
            return createAeronauticalVector(courseDeg, vertical, horiz);
        }

        #endregion

        #region Aeronautical

        public TEcef AeronauticalToEcef(TAeronautical an, LlaPosition originLlaPosition) => AeronauticalToEcef(an.Course, an.GetVertical(), an.GetHorizontal(), originLlaPosition);

        public TEcef AeronauticalToEcef(double courseRad, double vertical,
            double horizontal, LlaPosition originLlaPosition)
        {
            getNedVector(courseRad, vertical, horizontal,
                out double north,
                out double east,
                out double down);

            return LtpToEcef(north, east, down, originLlaPosition);
        }

        public TLtp AeronauticalToLtp(TAeronautical an)
        {
            getNedVector(an.Course, an.GetVertical(), an.GetHorizontal(),
                out double north,
                out double east,
                out double down);
            return createLtp(north, east, down);
        }

        #endregion

        #region private

        private IEcef getNedVectorFromEcef(double x,
            double y,
            double z, LlaPosition originLlaPosition)
        {
            Matrix m = CommonAlgo.GeoPositionToEcefMatrix(originLlaPosition);
            Matrix T = m.Transpose();

            double ex = T[0, 0] * x + T[0, 1] * y + T[0, 2] * z;
            double ey = T[1, 0] * x + T[1, 1] * y + T[1, 2] * z;
            double ez = T[2, 0] * x + T[2, 1] * y + T[2, 2] * z;

            return createEcef(ex, ey, ez);
        }

        private void nedToAeronautical(out double courseDeg, out double vertical, out double horiz, ILocalTangentPlane ned)
        {
            double azRad = 0;
            double x = ned.North;
            double y = ned.East;
            double z = ned.Down;

            double magnitude = Math.Sqrt(x * x + y * y + z * z);

            if (magnitude > 0.0)
            {
                double normX = x / magnitude;
                double normY = y / magnitude;
                int num = Math.Abs(x) >= 1E-10 ? 0 : Math.Abs(y) < 1E-10 ? 1 : 0;
                azRad = num == 0 ? Math.Atan2(normY, normX) : 0.0;
            }

            courseDeg = azRad / s_piTo180;
            vertical = ned.Up;
            horiz = Math.Sqrt(x * x + y * y);
        }

        private static void getNedVector(double courseDeg,
            double vertical,
            double horizontal, out double north, out double east, out double down)
        {
            double courseRad = courseDeg * s_piTo180;
            north = horizontal * Math.Cos(courseRad);
            east = horizontal * Math.Sin(courseRad);
            down = -vertical;
        }

        private static Matrix calcRzC(AzimuthElevationDistance raePos)
        {
            double c = raePos.Azimuth * s_piTo180;
            double[,] v =
            {
                { Math.Cos(c), Math.Sin(c), 0.0, 0.0 },
                { -1 * Math.Sin(c), Math.Cos(c), 0.0, 0.0 },
                { 0.0, 0.0, 1.0, 0.0 },
                { 0.0, 0.0, 0.0, 1.0 }
            };
            return new Matrix(v);
        }

        private static Matrix calcRyB(AzimuthElevationDistance raePos)
        {
            double b = raePos.Elevation * s_piTo180;
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
                { ltp.North },
                { ltp.East },
                { ltp.Down },
                { val4Th }
            });

        #endregion
    }
}