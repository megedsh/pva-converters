using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Converters
{
    public class AccelerationConverter : CartesianBase<LtpAcceleration,  EcefAcceleration, Acceleration,   AeronauticalAcceleration>
    {
        
        protected override Acceleration createScalar(double arg)
        {
            return Acceleration.FromMetersPerSquareSecond(arg);
        }

        protected override LtpAcceleration createLtp(double north, double east, double down)
        {
            return new LtpAcceleration(north, east, down);
        }

        protected override EcefAcceleration createEcef(double x, double y, double z)
        {
            return new EcefAcceleration(x, y, z);
        }

    }
}