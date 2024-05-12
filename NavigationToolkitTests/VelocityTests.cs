using NavigationToolkit.Converters;
using NavigationToolkit.Model;
using NavigationToolkit.Model.Aeronautical;
using NavigationToolkit.Model.Ecef;
using NavigationToolkit.Model.LocalTangentPlane;

namespace NavigationToolkitTests
{
    public class VelocityTests
    {
        public static readonly LlaPosition s_origin = new LlaPosition(32.086D, 34.789D, 0);

        readonly VelocityConverter subject = new VelocityConverter();

        [Test]
        public void AeronauticalToLtp()
        {
            double azimuth = 63.55;
            var aeronauticalVelocity = new AeronauticalVelocity(azimuth, 3.22, 1.22);
            LtpVelocity ltp = subject.AeronauticalToLtp(aeronauticalVelocity);
            assertLtp(ltp, 0.543408332214331, 1.0922945502381851, -3.22);
        }

        [Test]
        public void EcefToLtp()
        {
            EcefVelocity ecef = new EcefVelocity(1.2, 2.1, 3.2);
            LtpVelocity ltp = subject.EcefToLtp(ecef, s_origin);
            assertLtp(ltp, 1.5512542622059828, 1.0399763131879816, -3.5499379546684624);
        }

        [Test]
        public void LtpToEcef()
        {
            LtpVelocity ltp = new LtpVelocity(1.5564436374509982, 1.036777782662639, -3.5793120893288206);
            EcefVelocity ecef = subject.LtpToEcef(ltp, s_origin);
            assertEcef(ecef, 1.2199999999999995, 2.1100000000000003, 3.2199999999999998);
        }


        private void assertEcef(EcefVelocity ecef, double x, double y, double z)
        {
            Assert.AreEqual(x, ecef.X);
            Assert.AreEqual(y, ecef.Y);
            Assert.AreEqual(z, ecef.Z);
        }

        private void assertLtp(LtpVelocity ltp, double north, double east, double down)
        {
            Assert.AreEqual(north, ltp.North);
            Assert.AreEqual(east, ltp.East);
            Assert.AreEqual(down, ltp.Down);
        }
    }
}