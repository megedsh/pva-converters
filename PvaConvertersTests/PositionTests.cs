using PvaConverters.Converters;
using PvaConverters.Model;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;

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
        public void GeoToEcef()
        {
            foreach (var testVector in m_testVectors)
            {
                var lat = Angle.FromDegrees(testVector.Latitude);
                var lon = Angle.FromDegrees(testVector.Longitude);
                GeoPosition geo = new GeoPosition(lat, lon, Distance.FromMeters(testVector.Altitude));

                EcefPosition ecef = m_subject.GeoToEcef(geo);
                Assert.That(ecef, Is.EqualTo(new EcefPosition(testVector.ExpectedX, testVector.ExpectedY, testVector.ExpectedZ)));

                var geo2 = m_subject.EcefToGeo(ecef);
                assertEquals(geo, geo2);
            }
        }

        [Test]
        public void GeoToLtp()
        {
            GeoPosition contextGeoPosition = GeoPosition.FromDeg(-51.736538, -59.430458, 0);
            GeoPosition orgGeoPosition = GeoPosition.FromDeg(-51.687572, -60.158750, 3000);

            LtpPosition ltp = m_subject.GeoToLtp(orgGeoPosition, contextGeoPosition);

            assertLtp(ltp, 5199.1660, -50387.3839, -2799.35089);

            GeoPosition geoPosition = m_subject.LtpToGeo(ltp, contextGeoPosition);
            assertEquals(orgGeoPosition, geoPosition);
        }

        [Test]
        public void GeoToAer()
        {
            GeoPosition origin = GeoPosition.FromDeg(-51.687572, -60.158750, 3000);
            AzimuthElevationRange[] testVector =
            {
                new AzimuthElevationRange(Angle.FromDegrees(10), Angle.FromDegrees(10), Distance.FromMeters(1000)),
                new AzimuthElevationRange(Angle.FromDegrees(10), Angle.Zero, Distance.FromMeters(1000)),
                new AzimuthElevationRange(Angle.FromDegrees(10), Angle.FromDegrees(10), Distance.FromMeters(1)),
            };

            foreach (AzimuthElevationRange aer in testVector)
            {
                GeoPosition geoPos = m_subject.AerToGeo(origin, aer);
                var actual = m_subject.GeoToAer(origin, geoPos);


                double delta = 1e-3;
                Assert.AreEqual(aer.Azimuth, actual.Azimuth, delta);
                Assert.AreEqual(aer.Elevation, actual.Elevation, delta);
                Assert.AreEqual(aer.Range.Meters, actual.Range.Meters, delta);
            }
        }


        [Test]
        public void LtpToEcef()
        {
            GeoPosition origin = GeoPosition.FromDeg(-51.736538, -59.430458, 0);
            GeoPosition target = GeoPosition.FromDeg(-51.687572, -60.158750, 3000);

            LtpPosition ned = m_subject.GeoToLtp(target, origin);

            EcefPosition ecef1 = m_subject.LtpToEcef(ned, origin);
            EcefPosition ecef2 = m_subject.GeoToEcef(target);

            assertEquals(ecef1, ecef2);
        }


        [Test]
        public void LtpToAer()
        {
            GeoPosition origin = GeoPosition.FromDeg(-51.736538, -59.430458, 0);
            GeoPosition target = GeoPosition.FromDeg(-51.687572, -60.158750, 3000);

            LtpPosition ned = m_subject.GeoToLtp(target, origin);

            AzimuthElevationRange aer1 = m_subject.LtpToAer(ned);
            EcefPosition ecef = m_subject.LtpToEcef(ned, origin);
            AzimuthElevationRange aer2 = m_subject.EcefToAer(ecef, origin);
            assertEquals(aer1, aer2);
        }

        [Test]
        public void EcefToLtp()
        {
            GeoPosition origin = GeoPosition.FromDeg(-51.736538, -59.430458, 0);
            GeoPosition target = GeoPosition.FromDeg(-51.687572, -60.158750, 3000);

            var targetEcef = m_subject.GeoToEcef(target);

            var ltp1 = m_subject.EcefToLtp(targetEcef, origin);
            var ltp2 = m_subject.GeoToLtp(target, origin);


            assertEquals(ltp1, ltp2);
        }


        private void assertLtp(ILocalTangentPlane<Distance> ned, double north, double east, double down)
        {
            Assert.That(ned.North.Meters, Is.EqualTo(north).Within(0.0001));
            Assert.That(ned.East.Meters, Is.EqualTo(east).Within(0.0001));
            Assert.That(ned.Down.Meters, Is.EqualTo(down).Within(0.0001));
        }

        private void assertEquals(GeoPosition orgGeoPosition, GeoPosition geoPosition)
        {
            Assert.IsTrue(Math.Abs(orgGeoPosition.Altitude.Meters - geoPosition.Altitude.Meters) < 0.0001);
            Assert.IsTrue(Math.Abs(orgGeoPosition.Longitude.Degrees - geoPosition.Longitude.Degrees) < 0.00001);
            Assert.IsTrue(Math.Abs(orgGeoPosition.Latitude.Degrees - geoPosition.Latitude.Degrees) < 0.00001);
        }

        private void assertEquals(EcefPosition ecef1, EcefPosition ecef2)
        {
            Assert.IsTrue(Math.Abs(ecef1.X - ecef2.X) < 0.0001);
            Assert.IsTrue(Math.Abs(ecef1.Y - ecef2.Y) < 0.00001);
            Assert.IsTrue(Math.Abs(ecef1.Z - ecef2.Z) < 0.00001);
        }

        private void assertEquals<T>(AzimuthElevationBase<T> aer1, AzimuthElevationBase<T> aer2)
            where T : IScalar
        {
            Assert.IsTrue(Math.Abs(aer1.Azimuth - aer2.Azimuth) < 0.0001);
            Assert.IsTrue(Math.Abs(aer1.Elevation - aer2.Elevation) < 0.00001);
            Assert.IsTrue(Math.Abs(aer1.Scalar.AsDouble() - aer2.Scalar.AsDouble()) < 0.00001);
        }

        private void assertEquals<T>(ILocalTangentPlane<T> ltp1, ILocalTangentPlane<T> ltp2)
            where T : IScalar
        {
            Assert.IsTrue(Math.Abs(ltp1.North.AsDouble() - ltp2.North.AsDouble()) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.South.AsDouble() - ltp2.South.AsDouble()) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.East.AsDouble() - ltp2.East.AsDouble()) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.West.AsDouble() - ltp2.West.AsDouble()) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.Up.AsDouble() - ltp2.Up.AsDouble()) < 0.0001);
            Assert.IsTrue(Math.Abs(ltp1.Down.AsDouble() - ltp2.Down.AsDouble()) < 0.0001);
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