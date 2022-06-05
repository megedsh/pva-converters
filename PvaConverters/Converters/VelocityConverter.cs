using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Converters
{
    public class VelocityConverter : CartesianBase<LtpVelocity, AzimuthElevationVelocity, EcefVelocity, Velocity, EnuVelocity, NedVelocity, AeronauticalVelocity>
    {
        protected override Velocity createScalar(double arg)
        {
            return Velocity.FromMetersPerSecond(arg);
        }

        protected override LtpVelocity createNed(double x, double y, double z)
        {
            return new NedVelocity(x, y, z);
        }

        protected override EcefVelocity createEcef(double x, double y, double z)
        {
            return new EcefVelocity(x, y, z);
        }


        protected override AzimuthElevationVelocity createPolar(Angle az, Angle el, double sc)
        {
            return new AzimuthElevationVelocity(az, el, Velocity.FromMetersPerSecond(sc));
        }
    }
}

//public static class VelocityConverter
//{
//    #region Ltp

//    public static AzimuthElevationVelocity LtpToAev(LtpVelocity ltpVelocity, AzimuthElevationRange raePosition)
//    {
//        Matrix r1 = calcRzC(raePosition);
//        Matrix r2 = calcRyB(raePosition);
//        Matrix r3 = calcRxA();

//        Matrix rotationMatrix = calcRotationMatrix(r1, r2, r3);
//        Matrix velocityToMatrix4D = ltpVelocityToMatrix4D(ltpVelocity, 1);

//        Matrix vel = MatrixAlgo.Multiply(rotationMatrix, velocityToMatrix4D);

//        Velocity r = Velocity.FromMetersPerSecond(vel[0, 0]);
//        Angle a = Angle.FromRadians(Math.Atan(vel[2, 0] / raePosition.Range.Meters));
//        Angle e = Angle.FromRadians(Math.Atan(vel[1, 0] / raePosition.Range.Meters));

//        AzimuthElevationVelocity result = new AzimuthElevationVelocity(a, e, r);
//        return result;
//    }

//    public static EcefVelocity LtpToEcef(LtpVelocity nedVelocity, GeoPosition originGeoPosition) => LtpToEcef(nedVelocity.North.MetersPerSecond, nedVelocity.East.MetersPerSecond, nedVelocity.Down.MetersPerSecond, originGeoPosition);

//    public static EcefVelocity LtpToEcef(double northMetersPerSecond, double eastMetersPerSecond, double downMetersPerSecond, GeoPosition originGeoPosition)
//    {
//        Vector3d ecef = CommonAlgo.TransformNedToEcef(northMetersPerSecond, eastMetersPerSecond, downMetersPerSecond, originGeoPosition);
//        return new EcefVelocity(Velocity.FromMetersPerSecond(ecef.X),
//            Velocity.FromMetersPerSecond(ecef.Y),
//            Velocity.FromMetersPerSecond(ecef.Z));
//    }

//    #endregion

//    #region Ecef

//    public static NedVelocity EcefToNed(EcefVelocity ecefVelocity, GeoPosition originGeoPosition)
//    {
//        return EcefToNed(ecefVelocity.X, ecefVelocity.Y, ecefVelocity.Z, originGeoPosition);
//    }

//    public static NedVelocity EcefToNed(double xMetersPerSecond, double yMetersPerSecond, double zMetersPerSecond, GeoPosition originGeoPosition)
//    {
//        Vector3d v = getNedVectorFromEcef(xMetersPerSecond, yMetersPerSecond, zMetersPerSecond, originGeoPosition);
//        return new NedVelocity(Velocity.FromMetersPerSecond(v.X),
//            Velocity.FromMetersPerSecond(v.Y),
//            Velocity.FromMetersPerSecond(v.Z));
//    }


//    public static EnuVelocity EcefToEnu(EcefVelocity ecefVelocity, GeoPosition originGeoPosition)
//    {
//        return EcefToEnu(ecefVelocity.X, ecefVelocity.Y, ecefVelocity.Z, originGeoPosition);
//    }

//    public static EnuVelocity EcefToEnu(double xMetersPerSecond, double yMetersPerSecond, double zMetersPerSecond, GeoPosition originGeoPosition)
//    {
//        Vector3d v = getNedVectorFromEcef(xMetersPerSecond, yMetersPerSecond, zMetersPerSecond, originGeoPosition);
//        return new EnuVelocity(Velocity.FromMetersPerSecond(v.Y), Velocity.FromMetersPerSecond(v.X), Velocity.FromMetersPerSecond(-v.Z));
//    }

//    public static void EcefToAeronauticalVelocity(out Angle course,
//        out Velocity rateOfClimb,
//        out Velocity groundSpeed,
//        EcefVelocity ecefVelocity, GeoPosition originGeoPosition)
//    {
//        NedVelocity nedVelocity = EcefToNed(ecefVelocity.X, ecefVelocity.Y, ecefVelocity.Z, originGeoPosition);
//        nedToAeronautical(out course, out rateOfClimb, out groundSpeed, nedVelocity);
//    }

//    #endregion

//    #region Aeronautical

//    public static EcefVelocity AeronauticalToEcefVelocity(AeronauticalVelocity anVelocity, GeoPosition originGeoPosition) => AeronauticalToEcefVelocity(anVelocity.Course.Radians, anVelocity.RateOfClimb.MetersPerSecond, anVelocity.GroundSpeed.MetersPerSecond, originGeoPosition);

//    public static EcefVelocity AeronauticalToEcefVelocity(double courseRad, double rateOfClimbMetersPerSec,
//        double groundSpeedMetersPerSec, GeoPosition originGeoPosition)
//    {
//        Vector3d nedVector = getNedVector(courseRad, rateOfClimbMetersPerSec, groundSpeedMetersPerSec);

//        return LtpToEcef(nedVector.X, nedVector.Y, nedVector.Z, originGeoPosition);
//    }

//    public static NedVelocity AeronauticalToNed(AeronauticalVelocity anVelocity)
//    {
//        var nedVector = getNedVector(anVelocity.Course.Radians, anVelocity.RateOfClimb.MetersPerSecond, anVelocity.GroundSpeed.MetersPerSecond);
//        return new NedVelocity(nedVector.X, nedVector.Y, nedVector.Z);
//    }

//    public static EnuVelocity AeronauticalToEnu(AeronauticalVelocity anVelocity)
//    {
//        var nedVector = getNedVector(anVelocity.Course.Radians, anVelocity.RateOfClimb.MetersPerSecond, anVelocity.GroundSpeed.MetersPerSecond);
//        return new EnuVelocity(nedVector.Y, nedVector.X, -nedVector.Z);
//    }

//    #endregion

//    #region AzimuthElevationVelocity

//    public static NedVelocity AevToNed(AzimuthElevationRange aer, AzimuthElevationVelocity aev)
//    {
//        Vector3d nedVector = getNedVector(aer, aev);

//        return new NedVelocity(Velocity.FromMetersPerSecond(nedVector.X),
//            Velocity.FromMetersPerSecond(nedVector.Y),
//            Velocity.FromMetersPerSecond(nedVector.Z));
//    }

//    public static EnuVelocity AevToEnu(AzimuthElevationRange aer, AzimuthElevationVelocity aev)
//    {
//        Vector3d nedVector = getNedVector(aer, aev);

//        return new EnuVelocity(Velocity.FromMetersPerSecond(nedVector.Y),
//            Velocity.FromMetersPerSecond(nedVector.X),
//            Velocity.FromMetersPerSecond(-nedVector.Z));
//    }

//    public static EcefVelocity AevToEcef(AzimuthElevationRange aer, AzimuthElevationVelocity aev, GeoPosition origin)
//    {
//        Vector3d nedVector = getNedVector(aer, aev);

//        var nedVelocity = new NedVelocity(Velocity.FromMetersPerSecond(nedVector.X),
//            Velocity.FromMetersPerSecond(nedVector.Y),
//            Velocity.FromMetersPerSecond(nedVector.Z));
//        return LtpToEcef(nedVelocity, origin);
//    }

//    private static Vector3d getNedVector(AzimuthElevationRange aer, AzimuthElevationVelocity aev)
//    {
//        Matrix r1 = calcRzC(aer);
//        Matrix r2 = calcRyB(aer);
//        Matrix r3 = calcRxA();

//        Matrix rotationMatrix = calcRotationMatrix(r1, r2, r3).Transpose();

//        Matrix raeMatrix = new Matrix(new[,]
//        {
//            { aev.Velocity.MetersPerSecond },
//            { aer.Range.Meters * Math.Tan(aev.Elevation.Radians) },
//            { aer.Range.Meters * Math.Tan(aev.Azimuth.Radians) },
//            { 1.0 }
//        });

//        Matrix vel = MatrixAlgo.Multiply(rotationMatrix, raeMatrix);

//        return new Vector3d(vel[0, 0],
//            vel[1, 0],
//            vel[2, 0]);
//    }

//    #endregion

//    #region private

//    private static Vector3d getNedVectorFromEcef(double xMetersPerSecond,
//        double yMetersPerSecond,
//        double zMetersPerSecond, GeoPosition originGeoPosition)
//    {
//        Matrix m = CommonAlgo.LllnToEcefMatrix(originGeoPosition);
//        Matrix T = m.Transpose();

//        double ex = T[0, 0] * xMetersPerSecond + T[0, 1] * yMetersPerSecond + T[0, 2] * zMetersPerSecond;
//        double ey = T[1, 0] * xMetersPerSecond + T[1, 1] * yMetersPerSecond + T[1, 2] * zMetersPerSecond;
//        double ez = T[2, 0] * xMetersPerSecond + T[2, 1] * yMetersPerSecond + T[2, 2] * zMetersPerSecond;

//        return new Vector3d(ex, ey, ez);
//    }

//    private static void nedToAeronautical(out Angle course, out Velocity rateOfClimb, out Velocity groundSpeed, NedVelocity nedVelocity)
//    {
//        double azRad = 0;
//        double magnitude = Math.Sqrt(nedVelocity.X * nedVelocity.X + nedVelocity.Y * nedVelocity.Y + nedVelocity.Z * nedVelocity.Z);

//        if (magnitude > 0.0)
//        {
//            double normX = nedVelocity.X / magnitude;
//            double normY = nedVelocity.Y / magnitude;
//            int num = Math.Abs(nedVelocity.X) >= 1E-10 ? 0 : Math.Abs(nedVelocity.Y) < 1E-10 ? 1 : 0;
//            azRad = num == 0 ? Math.Atan2(normY, normX) : 0.0;
//        }

//        course = Angle.FromRadians(azRad);
//        rateOfClimb = nedVelocity.Up;
//        groundSpeed = Velocity.FromMetersPerSecond(Math.Sqrt(nedVelocity.X * nedVelocity.X + nedVelocity.Y * nedVelocity.Y));
//    }

//    private static Vector3d getNedVector(double courseRad, double rateOfClimbMetersPerSec,
//        double groundSpeedMetersPerSec)
//    {
//        double north = groundSpeedMetersPerSec * Math.Cos(courseRad);
//        double east = groundSpeedMetersPerSec * Math.Sin(courseRad);
//        double down = -rateOfClimbMetersPerSec;
//        return new Vector3d(north, east, down);
//    }

//    private static Matrix calcRzC(AzimuthElevationRange raePos)
//    {
//        double c = raePos.Azimuth.Radians;
//        double[,] v =
//        {
//            { Math.Cos(c), Math.Sin(c), 0.0, 0.0 },
//            { -1 * Math.Sin(c), Math.Cos(c), 0.0, 0.0 },
//            { 0.0, 0.0, 1.0, 0.0 },
//            { 0.0, 0.0, 0.0, 1.0 }
//        };
//        return new Matrix(v);
//    }

//    private static Matrix calcRyB(AzimuthElevationRange raePos)
//    {
//        double b = raePos.Elevation.Radians;
//        double[,] v =
//        {
//            { Math.Cos(b), 0.0, -1 * Math.Sin(b), 0.0 },
//            { 0.0, 1.0, 0.0, 0.0 },
//            { Math.Sin(b), 0.0, Math.Cos(b), 0.0 },
//            { 0.0, 0.0, 0.0, 1.0 }
//        };
//        return new Matrix(v);
//    }

//    private static Matrix calcRxA()
//    {
//        double a = 3 * Math.PI / 2;
//        double[,] v =
//        {
//            { 1, 0.0, 0.0, 0.0 },
//            { 0.0, Math.Cos(a), Math.Sin(a), 0.0 },
//            { 0.0, -1 * Math.Sin(a), Math.Cos(a), 0.0 },
//            { 0.0, 0.0, 0.0, 1.0 }
//        };
//        return new Matrix(v);
//    }

//    private static Matrix calcRotationMatrix(Matrix r1, Matrix r2, Matrix r3)
//    {
//        Matrix m1 = MatrixAlgo.Multiply(r2, r1);
//        Matrix m2 = MatrixAlgo.Multiply(r3, m1);
//        return m2;
//    }

//    private static Matrix ltpVelocityToMatrix4D(LtpVelocity v, double val4Th) =>
//        new Matrix(new[,]
//        {
//            { v.North.MetersPerSecond },
//            { v.East.MetersPerSecond },
//            { v.Down.MetersPerSecond },
//            { val4Th }
//        });

//    #endregion
//}