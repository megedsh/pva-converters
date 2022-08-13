using PvaConverters.Interfaces;
using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Converters
{
    public class VelocityConverter : CartesianBase<LtpVelocity, EcefVelocity, Velocity,AeronauticalVelocity>, IVelocityConverter
    {
        protected override Velocity createScalar(double arg)
        {
            return Velocity.FromMetersPerSecond(arg);
        }
        protected override LtpVelocity createLtp(double x, double y, double z)
        {
            return new LtpVelocity(x, y, z);
        }
        protected override EcefVelocity createEcef(double x, double y, double z)
        {
            return new EcefVelocity(x, y, z);
        }
        protected override AeronauticalVelocity createAeronauticalVector(Angle course, Velocity vertical, Velocity horizontal)
        {
            return new AeronauticalVelocity(course, vertical, horizontal);
        }

    }
}