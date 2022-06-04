using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Converters
{
    public class AccelerationConverter : CartesianBase<LtpAcceleration, AzimuthElevationAcceleration, EcefAcceleration, Acceleration, EnuAcceleration, NedAcceleration, AeronauticalAcceleration>
    {
        public AccelerationConverter() : base(s_polarFactory, s_ecefFactory, s_ltpFactory, s_scalarFactory)
        {
        }

        private static Acceleration s_scalarFactory(double arg)
        {
            return Acceleration.FromMetersPerSquareSecond(arg);
        }

        private static LtpAcceleration s_ltpFactory(double x, double y, double z)
        {
            return new NedAcceleration(x, y, z);
        }

        private static EcefAcceleration s_ecefFactory(double x, double y, double z)
        {
            return new EcefAcceleration(x, y, z);
        }

        private static AzimuthElevationAcceleration s_polarFactory(Angle az, Angle el, double sc)
        {
            return new AzimuthElevationAcceleration(az, el, Acceleration.FromMetersPerSquareSecond(sc));
        }
    }
}