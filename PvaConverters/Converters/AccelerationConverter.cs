using PvaConverters.Interfaces;
using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;

namespace PvaConverters.Converters
{
    public class AccelerationConverter : CartesianBase<LtpAcceleration, EcefAcceleration, AeronauticalAcceleration>, IAccelerationConverter
    {
        protected override AeronauticalAcceleration createAeronauticalVector(double courseDeg, double vertical, double horizontal)
        {
            return new AeronauticalAcceleration(courseDeg, vertical, horizontal);
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