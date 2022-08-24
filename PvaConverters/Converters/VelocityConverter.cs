using PvaConverters.Interfaces;
using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;

namespace PvaConverters.Converters
{
    public class VelocityConverter : CartesianBase<LtpVelocity, EcefVelocity, AeronauticalVelocity>, IVelocityConverter
    {
        protected override LtpVelocity createLtp(double x, double y, double z)
        {
            return new LtpVelocity(x, y, z);
        }

        protected override EcefVelocity createEcef(double x, double y, double z)
        {
            return new EcefVelocity(x, y, z);
        }

        protected override AeronauticalVelocity createAeronauticalVector(double courseDeg, double vertical, double horizontal)
        {
            return new AeronauticalVelocity(courseDeg, vertical, horizontal);
        }
    }
}