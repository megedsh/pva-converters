using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Converters
{
    public class AccelerationConverter : CartesianBase<LtpAcceleration, AzimuthElevationAcceleration, EcefAcceleration, Acceleration, EnuAcceleration, NedAcceleration, AeronauticalAcceleration>
    {
        
        protected override Acceleration createScalar(double arg)
        {
            return Acceleration.FromMetersPerSquareSecond(arg);
        }

        protected override LtpAcceleration createNed(double x, double y, double z)
        {
            return new NedAcceleration(x, y, z);
        }

        protected override EcefAcceleration createEcef(double x, double y, double z)
        {
            return new EcefAcceleration(x, y, z);
        }


        protected override AzimuthElevationAcceleration createPolar(Angle az, Angle el, double sc)
        {
            return new AzimuthElevationAcceleration(az, el, Acceleration.FromMetersPerSquareSecond(sc));
        }
    }
}