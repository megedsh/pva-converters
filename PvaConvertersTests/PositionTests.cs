using PvaConverters.Converters;
using PvaConverters.Model;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.Scalars;

namespace PvaConvertersTests
{
    public class PositionTests
    {
        private PositionConverter subject = new PositionConverter();

        private TestVector[] testVectors = new[]
        {
            new TestVector(0, 0, 0, Datum.WGS84.SemiMajorAxis, 0, 0),
            new TestVector(0, 90, 0, 3.905482530786651E-10 , Datum.WGS84.SemiMajorAxis, 0),
            new TestVector(0, -90, 0, 3.905482530786651E-10 , -Datum.WGS84.SemiMajorAxis, 0),
            new TestVector(0, 180, 0, -Datum.WGS84.SemiMajorAxis , 7.810965061573302E-10, 0),
            new TestVector(-43.907787, -70.013676, 1000, 1573391.6094067297, -4326070.505316898, -4401409.0969682215),
            new TestVector(35.203683802455934, -111.6660578308822, -100, -1926226.5550630623, -4848752.7459563175, 3656296.708297751),
            new TestVector(32.1882286, 34.8963593, 0, 4431451.158948314, 3091004.546873152, 3378114.0153715312),
            new TestVector(-36.8584105, 174.7635925, 500.00, -5088508.684251692, 466350.9313900066, -3805132.3841278534),
        };

        [Test]
        public void test1()
        {
            foreach (var testVector in testVectors)
            {
                var lat = Angle.FromDegrees(testVector.Latitude);
                var lon = Angle.FromDegrees(testVector.Longitude);
                GeoPosition geo = new GeoPosition(lat, lon, Distance.FromMeters(testVector.Altitude));

                EcefPosition ecef = subject.GeoToEcef(geo);
                Assert.AreEqual(new EcefPosition(testVector.ExpectedX,testVector.ExpectedY,testVector.ExpectedZ), ecef);    
            }
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

        public double ExpectedX{ get; set; }
        public double ExpectedY { get; set; }
        public double ExpectedZ{ get; set; }
    }
                        
  }
