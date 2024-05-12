using NavigationToolkit.Interfaces;
using NavigationToolkit.Model.Aeronautical;
using NavigationToolkit.Model.Ecef;
using NavigationToolkit.Model.LocalTangentPlane;

namespace NavigationToolkit.Converters
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