using PvaConverters.Converters;
using PvaConverters.Model;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;

namespace PvaConvertersTests
{
    public class PositionTests
    {
        private readonly PositionConverter m_subject = new PositionConverter();

        private readonly TestVector[] m_testVectors =
        {
            new TestVector(0, 0, 0, Datum.WGS84.SemiMajorAxis, 0, 0),
            new TestVector(0, 90, 0, 3.905482530786651E-10, Datum.WGS84.SemiMajorAxis, 0),
            new TestVector(0, -90, 0, 3.905482530786651E-10, -Datum.WGS84.SemiMajorAxis, 0),
            new TestVector(0, 180, 0, -Datum.WGS84.SemiMajorAxis, 7.810965061573302E-10, 0),
            new TestVector(-43.907787, -70.013676, 1000, 1573391.6094067297, -4326070.505316898, -4401409.0969682215),
            new TestVector(35.203683802455934, -111.6660578308822, -100, -1926226.5550630623, -4848752.7459563175, 3656296.708297751),
            new TestVector(32.1882286, 34.8963593, 0, 4431451.158948314, 3091004.546873152, 3378114.0153715312),
            new TestVector(-36.8584105, 174.7635925, 500.00, -5088508.684251692, 466350.9313900066, -3805132.3841278534),
        };

        [Test]
        public void Test1()
        {
            var pc = new PositionConverter();
            LlaPosition origin = new LlaPosition(32.18, 34.89, 1000);
            EcefPosition ecefPosition = new EcefPosition(4431798.0, 3091247.0, 3378380.0);
            LtpPosition ltp = m_subject.EcefToLtp(ecefPosition, origin);
            Console.WriteLine(ltp.ToStringNed());
        }


        [Test]
        public void LlaToEcef()
        {
            foreach (var testVector in m_testVectors)
            {
                var lat = testVector.Latitude;
                var lon = testVector.Longitude;
                LlaPosition lla = new LlaPosition(lat, lon, testVector.Altitude);

                EcefPosition ecef = m_subject.LlaToEcef(lla);
                Assert.That(ecef, Is.EqualTo(new EcefPosition(testVector.ExpectedX, testVector.ExpectedY, testVector.ExpectedZ)));

                var geo2 = m_subject.EcefToLla(ecef);
                assertEquals(lla, geo2);
            }
        }

        [Test]
        public void LlaToLtp()
        {
            LlaPosition contextLlaPosition = new LlaPosition(-51.736538, -59.430458, 0);
            LlaPosition orgLlaPosition = new LlaPosition(-51.687572, -60.158750, 3000);

            LtpPosition ltp = m_subject.LlaToLtp(orgLlaPosition, contextLlaPosition);

            assertLtp(ltp, 5199.1660, -50387.3839, -2799.35089);

            LlaPosition llaPosition = m_subject.LtpToLla(ltp, contextLlaPosition);
            assertEquals(orgLlaPosition, llaPosition);
        }

        [Test]
        public void LlaToAed()
        {
            LlaPosition origin = new LlaPosition(-51.687572, -60.158750, 3000);
            AzimuthElevationDistance[] testVector =
            {
                new AzimuthElevationDistance(10, 10, 1000),
                new AzimuthElevationDistance(10, 0, 1000),
                new AzimuthElevationDistance(10, 10, 1),
            };

            foreach (AzimuthElevationDistance aer in testVector)
            {
                LlaPosition llaPos = m_subject.AedToLla(origin, aer);
                var actual = m_subject.LlaToAed(origin, llaPos);


                double delta = 1e-3;
                Assert.AreEqual(aer.Azimuth, actual.Azimuth, delta);
                Assert.AreEqual(aer.Elevation, actual.Elevation, delta);
                Assert.AreEqual(aer.Distance, actual.Distance, delta);
            }
        }


        [Test]
        public void LtpToEcef()
        {
            LlaPosition origin = new LlaPosition(-51.736538, -59.430458, 0);
            LlaPosition target = new LlaPosition(-51.687572, -60.158750, 3000);

            LtpPosition ned = m_subject.LlaToLtp(target, origin);

            EcefPosition ecef1 = m_subject.LtpToEcef(ned, origin);
            EcefPosition ecef2 = m_subject.LlaToEcef(target);

            assertEquals(ecef1, ecef2);
        }


        [Test]
        public void LtpToAed()
        {
            LlaPosition origin = new LlaPosition(-51.736538, -59.430458, 0);
            LlaPosition target = new LlaPosition(-51.687572, -60.158750, 3000);

            LtpPosition ned = m_subject.LlaToLtp(target, origin);

            AzimuthElevationDistance aer1 = m_subject.LtpToAed(ned);
            EcefPosition ecef = m_subject.LtpToEcef(ned, origin);
            AzimuthElevationDistance aer2 = m_subject.EcefToAed(ecef, origin);
            assertEquals(aer1, aer2);
        }

        [Test]
        public void EcefToLtp()
        {
            LlaPosition origin = new LlaPosition(-51.736538, -59.430458, 0);
            LlaPosition target = new LlaPosition(-51.687572, -60.158750, 3000);

            var targetEcef = m_subject.LlaToEcef(target);

            var ltp1 = m_subject.EcefToLtp(targetEcef, origin);
            var ltp2 = m_subject.LlaToLtp(target, origin);


            assertEquals(ltp1, ltp2);
        }


        private void assertLtp(ILocalTangentPlane ned, double north, double east, double down)
        {
            Assert.That(ned.North, Is.EqualTo(north).Within(0.0001));
            Assert.That(ned.East, Is.EqualTo(east).Within(0.0001));
            Assert.That(ned.Down, Is.EqualTo(down).Within(0.0001));
        }

        private void assertEquals(LlaPosition orgLlaPosition, LlaPosition llaPosition)
        {
            Assert.IsTrue(Math.Abs(orgLlaPosition.Altitude - llaPosition.Altitude) < 0.0001);
            Assert.IsTrue(Math.Abs(orgLlaPosition.Longitude - llaPosition.Longitude) < 0.00001);
            Assert.IsTrue(Math.Abs(orgLlaPosition.Latitude - llaPosition.Latitude) < 0.00001);
        }

        private void assertEquals(EcefPosition ecef1, EcefPosition ecef2)
        {
            Assert.IsTrue(Math.Abs(ecef1.X - ecef2.X) < 0.0001);
            Assert.IsTrue(Math.Abs(ecef1.Y - ecef2.Y) < 0.00001);
            Assert.IsTrue(Math.Abs(ecef1.Z - ecef2.Z) < 0.00001);
        }

        private void assertEquals(IAzimuthElevation aer1, IAzimuthElevation aer2)
        {
            Assert.IsTrue(Math.Abs(aer1.Azimuth - aer2.Azimuth) < 0.0001);
            Assert.IsTrue(Math.Abs(aer1.Elevation - aer2.Elevation) < 0.00001);
            Assert.IsTrue(Math.Abs(aer1.GetScalar() - aer2.GetScalar()) < 0.00001);
        }

        private void assertEquals(ILocalTangentPlane ltp1, ILocalTangentPlane ltp2)
        {
            Assert.IsTrue(Math.Abs(ltp1.North - ltp2.North) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.South - ltp2.South) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.East - ltp2.East) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.West - ltp2.West) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.Up - ltp2.Up) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.Down - ltp2.Down) < 0.0001);
        }
    }

    internal class TestVector
    {
        public TestVector(double latitude, double longitude, double altitude, double expectedX, double expectedY, double expectedZ)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            ExpectedX = expectedX;
            ExpectedY = expectedY;
            ExpectedZ = expectedZ;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }

        public double ExpectedX { get; set; }
        public double ExpectedY { get; set; }
        public double ExpectedZ { get; set; }
    }
}