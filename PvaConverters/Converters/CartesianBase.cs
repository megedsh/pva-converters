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
    public abstract class CartesianBase<TLtp, TAe, TEcef, TScalar, TEnu, TNed, TAeronautical>
        where TScalar : IScalar
        where TLtp : ILocalTangentPlane<TScalar, TNed, TEnu>
        where TNed : ILocalTangentPlane<TScalar, TNed, TEnu>
        where TEcef : EcefBase<TScalar>
        where TAeronautical : AeronauticalVector<TScalar>
        where TAe : AzimuthElevationBase<TScalar>
    {
        private readonly Func<Angle, Angle, double, TAe> m_polarFactory;
        private readonly Func<double, double, double, TEcef> m_ecefFactory;
        private readonly Func<double, double, double, TLtp> m_ltpFactory;
        private readonly Func<double, TScalar> m_scalarFactory;

        protected CartesianBase(Func<Angle, Angle, double, TAe> polarFactory,
            Func<double, double, double, TEcef> ecefFactory,
            Func<double, double, double, TLtp> ltpFactory,
            Func<double, TScalar> scalarFactory)
        {
            m_polarFactory = polarFactory;
            m_ecefFactory = ecefFactory;
            m_ltpFactory = ltpFactory;
            m_scalarFactory = scalarFactory;
        }

        #region Ltp

        public TAe LtpToAev(TLtp ltp, AzimuthElevationRange raePosition)
        {
            Matrix r1 = calcRzC(raePosition);
            Matrix r2 = calcRyB(raePosition);
            Matrix r3 = calcRxA();

            Matrix rotationMatrix = calcRotationMatrix(r1, r2, r3);
            Matrix matrix4D = ltpToMatrix4D(ltp, 1);

            Matrix multiply = MatrixAlgo.Multiply(rotationMatrix, matrix4D);

            double scalar = multiply[0, 0];
            Angle a = Angle.FromRadians(Math.Atan(multiply[2, 0] / raePosition.Range.Meters));
            Angle e = Angle.FromRadians(Math.Atan(multiply[1, 0] / raePosition.Range.Meters));

            return m_polarFactory(a, e, scalar);
        }

        public TEcef LtpToEcef(TLtp ltp, GeoPosition originGeoPosition) => LtpToEcef(ltp.North.AsDouble(), ltp.East.AsDouble(), ltp.Down.AsDouble(), originGeoPosition);

        public TEcef LtpToEcef(double north, double east, double down, GeoPosition originGeoPosition)
        {
            Vector3d ecef = CommonAlgo.TransformNedToEcef(north, east, down, originGeoPosition);
            return m_ecefFactory(ecef.X, ecef.Y, ecef.Z);
        }

        #endregion

        #region Ecef

        public TNed EcefToNed(TEcef ecef, GeoPosition originGeoPosition)
        {
            return EcefToNed(ecef.X, ecef.Y, ecef.Z, originGeoPosition);
        }

        public TNed EcefToNed(double x, double y, double z, GeoPosition originGeoPosition)
        {
            Vector3d v = getNedVectorFromEcef(x, y, z, originGeoPosition);

            return m_ltpFactory(v.X, v.Y, v.Z).AsNed();
        }


        public TEnu EcefToEnu(TEcef ecef, GeoPosition originGeoPosition)
        {
            return EcefToEnu(ecef.X, ecef.Y, ecef.Z, originGeoPosition);
        }

        public TEnu EcefToEnu(double x, double y, double z, GeoPosition originGeoPosition)
        {
            Vector3d v = getNedVectorFromEcef(x, y, z, originGeoPosition);
            return m_ltpFactory(v.X, v.Y, v.Z).AsEnu();
        }

        public void EcefToAeronautical(out Angle course,
            out TScalar rateOfClimb,
            out TScalar groundSpeed,
            TEcef ecef, GeoPosition originGeoPosition)
        {
            TNed ned = EcefToNed(ecef.X, ecef.Y, ecef.Z, originGeoPosition);
            nedToAeronautical(out course, out rateOfClimb, out groundSpeed, ned);
        }

        #endregion

        #region Aeronautical

        public TEcef AeronauticalToEcef(TAeronautical an, GeoPosition originGeoPosition) => AeronauticalToEcef(an.Course.Radians, an.Vertic.AsDouble(), an.Horiz.AsDouble(), originGeoPosition);

        public TEcef AeronauticalToEcef(double courseRad, double vertical,
            double horizontal, GeoPosition originGeoPosition)
        {
            Vector3d nedVector = getNedVector(courseRad, vertical, horizontal);

            return LtpToEcef(nedVector.X, nedVector.Y, nedVector.Z, originGeoPosition);
        }

        public TNed AeronauticalToNed(TAeronautical an)
        {
            var nedVector = getNedVector(an.Course.Radians, an.Vertic.AsDouble(), an.Horiz.AsDouble());
            return m_ltpFactory(nedVector.X, nedVector.Y, nedVector.Z).AsNed();
        }

        public TEnu AeronauticalToEnu(TAeronautical an)
        {
            var nedVector = getNedVector(an.Course.Radians, an.Vertic.AsDouble(), an.Horiz.AsDouble());
            return m_ltpFactory(nedVector.X, nedVector.Y, nedVector.Z).AsEnu();
        }

        #endregion

        #region AzimuthElevation

        public TNed AevToNed(AzimuthElevationRange aer, TAe aev)
        {
            Vector3d nedVector = getNedVector(aer, aev);
            return m_ltpFactory(nedVector.X, nedVector.Y, nedVector.Z).AsNed();
        }

        public TEnu AevToEnu(AzimuthElevationRange aer, TAe aev)
        {
            Vector3d nedVector = getNedVector(aer, aev);
            return m_ltpFactory(nedVector.X, nedVector.Y, nedVector.Z).AsEnu();
        }

        public TEcef AevToEcef(AzimuthElevationRange aer, TAe aev, GeoPosition origin)
        {
            Vector3d nedVector = getNedVector(aer, aev);
            TLtp ltp = m_ltpFactory(nedVector.X, nedVector.Y, nedVector.Z);
            return LtpToEcef(ltp, origin);
        }

        #endregion

        #region private

        private static Vector3d getNedVector(AzimuthElevationRange aer, TAe aev)
        {
            Matrix r1 = calcRzC(aer);
            Matrix r2 = calcRyB(aer);
            Matrix r3 = calcRxA();

            Matrix rotationMatrix = calcRotationMatrix(r1, r2, r3).Transpose();

            Matrix raeMatrix = new Matrix(new[,]
            {
                { aev.Scalar.AsDouble() },
                { aer.Range.Meters * Math.Tan(aev.Elevation.Radians) },
                { aer.Range.Meters * Math.Tan(aev.Azimuth.Radians) },
                { 1.0 }
            });

            Matrix multiply = MatrixAlgo.Multiply(rotationMatrix, raeMatrix);

            return new Vector3d(multiply[0, 0],
                multiply[1, 0],
                multiply[2, 0]);
        }

        private static Vector3d getNedVectorFromEcef(double x,
            double y,
            double z, GeoPosition originGeoPosition)
        {
            Matrix m = CommonAlgo.LllnToEcefMatrix(originGeoPosition);
            Matrix T = m.Transpose();

            double ex = T[0, 0] * x + T[0, 1] * y + T[0, 2] * z;
            double ey = T[1, 0] * x + T[1, 1] * y + T[1, 2] * z;
            double ez = T[2, 0] * x + T[2, 1] * y + T[2, 2] * z;

            return new Vector3d(ex, ey, ez);
        }

        private void nedToAeronautical(out Angle course, out TScalar rateOfClimb, out TScalar groundSpeed, TNed ned)
        {
            double azRad = 0;
            double magnitude = Math.Sqrt(ned.X * ned.X + ned.Y * ned.Y + ned.Z * ned.Z);

            if (magnitude > 0.0)
            {
                double normX = ned.X / magnitude;
                double normY = ned.Y / magnitude;
                int num = Math.Abs(ned.X) >= 1E-10 ? 0 : Math.Abs(ned.Y) < 1E-10 ? 1 : 0;
                azRad = num == 0 ? Math.Atan2(normY, normX) : 0.0;
            }

            course = Angle.FromRadians(azRad);
            rateOfClimb = ned.Up;
            groundSpeed = m_scalarFactory(Math.Sqrt(ned.X * ned.X + ned.Y * ned.Y));
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