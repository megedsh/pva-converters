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
        private readonly PositionConverter subject = new PositionConverter();

        private readonly TestVector[] testVectors =
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
            foreach (var testVector in testVectors)
            {
                var lat = Angle.FromDegrees(testVector.Latitude);
                var lon = Angle.FromDegrees(testVector.Longitude);
                GeoPosition geo = new GeoPosition(lat, lon, Distance.FromMeters(testVector.Altitude));

                EcefPosition ecef = subject.GeoToEcef(geo);

                Assert.That(ecef, Is.EqualTo(new EcefPosition(testVector.ExpectedX, testVector.ExpectedY, testVector.ExpectedZ)));
            }
        }

        [Test]
        public void GeoToNedAndEnu()
        {
            GeoPosition contextGeoPosition = GeoPosition.FromDeg(-51.736538, -59.430458, 0);
            GeoPosition orgGeoPosition = GeoPosition.FromDeg(-51.687572, -60.158750, 3000);

            NedPosition ned = subject.GeoToNed(orgGeoPosition, contextGeoPosition);
            EnuPosition enu = subject.GeoToEnu(orgGeoPosition, contextGeoPosition);

            NedPosition ned2 = enu.AsNed();

            assertNed(ned, 5199.1660, -50387.3839, -2799.35089);
            assertNed(ned2, 5199.1660, -50387.3839, -2799.35089);
            GeoPosition geoPosition = subject.LtpToGeo(ned, contextGeoPosition);
            asserEquals(orgGeoPosition, geoPosition);
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
                GeoPosition geoPos = subject.AerToGeo(origin, aer);
                var actual = subject.GeoToAer(origin, geoPos);


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
            GeoPosition orgGeoPosition = GeoPosition.FromDeg(-51.687572, -60.158750, 3000);

            NedPosition ned = subject.GeoToNed(orgGeoPosition, origin);
            EnuPosition enu = subject.GeoToEnu(orgGeoPosition, origin);

            EcefPosition ecef1 = subject.LtpToEcef(ned, origin);
            EcefPosition ecef2 = subject.LtpToEcef(enu, origin);
            EcefPosition ecef3 = subject.GeoToEcef(orgGeoPosition);

            asserEquals(ecef1, ecef2);
            asserEquals(ecef2, ecef3);
        }


        private void assertNed(NedPosition ned, double north, double east, double down)
        {
            Assert.That(ned.North.Meters, Is.EqualTo(north).Within(0.0001));
            Assert.That(ned.East.Meters, Is.EqualTo(east).Within(0.0001));
            Assert.That(ned.Down.Meters, Is.EqualTo(down).Within(0.0001));
        }

        private void asserEquals(GeoPosition orgGeoPosition, GeoPosition geoPosition)
        {
            Assert.IsTrue(Math.Abs(orgGeoPosition.Altitude.Meters - geoPosition.Altitude.Meters) < 0.0001);
            Assert.IsTrue(Math.Abs(orgGeoPosition.Longitude.Degrees - geoPosition.Longitude.Degrees) < 0.00001);
            Assert.IsTrue(Math.Abs(orgGeoPosition.Latitude.Degrees - geoPosition.Latitude.Degrees) < 0.00001);
        }

        private void asserEquals(EcefPosition ecef1, EcefPosition ecef2)
        {
            Assert.IsTrue(Math.Abs(ecef1.X - ecef2.X) < 0.0001);
            Assert.IsTrue(Math.Abs(ecef1.Y - ecef2.Y) < 0.00001);
            Assert.IsTrue(Math.Abs(ecef1.Z - ecef2.Z) < 0.00001);
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


//using System;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using mPrest.CUAS.Entities.Tracks;
//using mPrest.GBAD.UnitTests.Common;
//using mPrest.Infra.Algo.Geometry;
//using mPrest.Infra.BasicDataTypes.Scalars;
//using mPrest.Infra.BasicDataTypes.SpatialEntities.BAT;
//using mPrest.Infra.BasicDataTypes.SpatialEntities.ECF;
//using mPrest.Infra.BasicDataTypes.SpatialEntities.Generic;
//using mPrest.Infra.BasicDataTypes.SpatialEntities.Geo;
//using mPrest.Infra.CoordinatesConverter;

//namespace mPrest.Infra.UnitTests.CoordinatesConverter
//{
//    [TestClass]
//    public class CoordinatesConverterTests : BaseCoordinatesConverterTest
//    {
//        IPositionConverter s_positionConverter = new PositionConverter();
//        IVelocityConverter s_velocityConverter = new VelocityConverter();

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void InternalPositionConverter_GeoToNed()
//        {
//            GeoPosition orgGeoPosition = GeoPosition.FromLatLonAndAltitude(-51.687572, -60.158750, 3000);
//            //lat = -51.687572°; Long=-60.158750°; Alt=3000.000 m bat position:X=5199.21, Y=-50387.52, Z=-2799.87
//            //Context: Lat=-51.736538°; Long=-59.430458°; Alt=0.000 m

//            GeoPosition contextGeoPosition = GeoPosition.FromLatLonAndAltitude(-51.736538, -59.430458, 0);

//            BATPosition batPosition = s_positionConverter.GeoToNed(orgGeoPosition, contextGeoPosition);
//            GeoPosition geoPosition = s_positionConverter.NedToGeo(batPosition, contextGeoPosition);

//            Assert.IsTrue(geoPosition.Equals(orgGeoPosition));
//        }


//        [TestMethod]
//        [Ignore]
//        public void Debug_CalculateDistanceBetweenGeoPoints()
//        {
//            GeoPosition pos1 = GeoPosition.FromLatLonAndAltitude(-51.877809, -59.741085, 2500.000);
//            GeoPosition pos2 = GeoPosition.FromLatLonAndAltitude(-51.876901, -59.741121, 2500.000);

//            GeoPosition pos3 = GeoPosition.FromLatLonAndAltitude(-51.602983616256722, -59.4798652233078, 100.26342632913244);

//            DistanceScalar dist1_2 = GeometricCalculations.Distance(pos1, pos2);
//            DistanceScalar dist2_3 = GeometricCalculations.Distance(pos2, pos3);
//            DistanceScalar dist3_1 = GeometricCalculations.Distance(pos3, pos1);
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void InternalPositionConverter_PolarToGeo()
//        {
//            GeoPosition contextGeoPos = GeoPosition.FromLatLonAndAltitude(-51.687572, -60.158750, 3000);
//            PolarPosition[] testPolarPositions =
//            {
//                //RAE
//                new PolarPosition(DistanceScalar.FromMeters(1000), Angle.FromDegrees(10), Angle.FromDegrees(10)),
//                //RA
//                new PolarPosition(DistanceScalar.FromMeters(1000), Angle.FromDegrees(10), Angle.Zero),
//                new PolarPosition(DistanceScalar.FromMeters(1000), Angle.FromDegrees(10), Angle.Empty),
//                //AE
//                new PolarPosition(DistanceScalar.FromMeters(1), Angle.FromDegrees(10), Angle.FromDegrees(10)),
//                new PolarPosition(DistanceScalar.Empty,         Angle.FromDegrees(10), Angle.FromDegrees(10))
//            };

//            foreach (PolarPosition originalPolarPos in testPolarPositions)
//            {
//                GeoPosition geoPos = s_positionConverter.PolarToGeo(originalPolarPos, contextGeoPos);
//                PolarPosition actualPolarPos = s_positionConverter.GeoToPolar(geoPos, contextGeoPos);

//                string msg = $"\nExpected: {originalPolarPos}" +
//                             $"\nActual:   {actualPolarPos}" +
//                             $"\nGeoPos:   {geoPos}" +
//                             $"\nContext:  {contextGeoPos}";
//                double delta = 1e-3;
//                Assert.AreEqual(getNotNaN(originalPolarPos.Azimuth.Degrees), actualPolarPos.Azimuth.Degrees, delta, "Wrong Azimuth" + msg);
//                Assert.AreEqual(getNotNaN(originalPolarPos.Elevation.Degrees), actualPolarPos.Elevation.Degrees, delta, "Wrong Elevation" + msg);
//                Assert.AreEqual(getNotNaN(originalPolarPos.Range.Kilometers), actualPolarPos.Range.Kilometers, delta, "Wrong Range" + msg);
//            }
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void CoordinatesConversion_VelocityBat_ToECEF_ToBat()
//        {
//            runTest(() =>
//            {
//                Velocity velocity = Velocity.CreateBAT(VelocityScalar.FromMetersPerSecond(35.07),
//                                                       VelocityScalar.FromMetersPerSecond(15.73),
//                                                       VelocityScalar.FromMetersPerSecond(-27.74), 1);

//                ECEFVelocity ecefVelocity = conversionContext.ToECEF(velocity);
//                Velocity ecefVelocityWrapper = Velocity.CreateECEF(ecefVelocity.X,
//                                                                   ecefVelocity.Y,
//                                                                   ecefVelocity.Z);

//                BATVelocity batVelocity = conversionContext.ToBAT(ecefVelocityWrapper);
//                Velocity batVelocityWrapper = Velocity.CreateBAT(batVelocity.X,
//                                                                 batVelocity.Y,
//                                                                 batVelocity.Z, 1);
//                // Assert
//                double ACCURACY = 0.0001;
//                Assert.AreEqual(velocity.X.MetersPerSecond, batVelocityWrapper.X.MetersPerSecond, ACCURACY, $"Wrong velocity.X \nActual:   {batVelocityWrapper}\nExpected: {velocity}");
//                Assert.AreEqual(velocity.Y.MetersPerSecond, batVelocityWrapper.Y.MetersPerSecond, ACCURACY, $"Wrong velocity.Y \nActual:   {batVelocityWrapper}\nExpected: {velocity}");
//                Assert.AreEqual(velocity.Z.MetersPerSecond, batVelocityWrapper.Z.MetersPerSecond, ACCURACY, $"Wrong velocity.Z \nActual:   {batVelocityWrapper}\nExpected: {velocity}");
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void CoordinatesConversion_VelocityECEF_ToAeronautic_ToECEF()
//        {
//            Tuple<AeronauticVelocity, ECEFPosition, Velocity>[] points =
//            {
//                new Tuple<AeronauticVelocity, ECEFPosition, Velocity>(
//                    AeronauticVelocity.Create(Angle.Zero, 100, 0),
//                    new ECEFPosition(6378137, 0, 0),
//                    Velocity.CreateECEF(
//                        VelocityScalar.FromMetersPerSecond(100),
//                        VelocityScalar.FromMetersPerSecond(0),
//                        VelocityScalar.FromMetersPerSecond(0))),
//                new Tuple<AeronauticVelocity, ECEFPosition, Velocity>(
//                        AeronauticVelocity.Create(Angle.Zero, 100, 0),
//                        new ECEFPosition(0,6378137, 0),
//                        Velocity.CreateECEF(
//                            VelocityScalar.FromMetersPerSecond(0),
//                            VelocityScalar.FromMetersPerSecond(100),
//                            VelocityScalar.FromMetersPerSecond(0))),
//                new Tuple<AeronauticVelocity, ECEFPosition, Velocity>(
//                            AeronauticVelocity.Create(Angle.Zero, 100, 0),
//                            new ECEFPosition(-6378137, 0, 0),
//                            Velocity.CreateECEF(
//                                VelocityScalar.FromMetersPerSecond(-100),
//                                VelocityScalar.FromMetersPerSecond(0),
//                                VelocityScalar.FromMetersPerSecond(0))),
//                new Tuple<AeronauticVelocity, ECEFPosition, Velocity>(
//                            AeronauticVelocity.Create(Angle.Angle90, 0, 100),
//                            new ECEFPosition(6378137, 0, 0),
//                            Velocity.CreateECEF(
//                                VelocityScalar.FromMetersPerSecond(0),
//                                VelocityScalar.FromMetersPerSecond(100),
//                                VelocityScalar.FromMetersPerSecond(0))),
//                new Tuple<AeronauticVelocity, ECEFPosition, Velocity>(
//                            AeronauticVelocity.Create(Angle.Angle270, 0, 100),
//                            new ECEFPosition(0, 6378137, 0),
//                            Velocity.CreateECEF(
//                                VelocityScalar.FromMetersPerSecond(100),
//                                VelocityScalar.FromMetersPerSecond(0),
//                                VelocityScalar.FromMetersPerSecond(0))),
//                //new Tuple<AeronauticVelocity, ECEFPosition, Velocity>(
//                //            AeronauticVelocity.Create(Angle.Zero, 0, 100),
//                //            new ECEFPosition(-6378137, 0, 0),
//                //            Velocity.CreateECEF(
//                //                VelocityScalar.FromMetersPerSecond(0),
//                //                VelocityScalar.FromMetersPerSecond(0),
//                //                VelocityScalar.FromMetersPerSecond(100)))
//            };
//            double delta = 1e-6;

//            foreach (Tuple<AeronauticVelocity, ECEFPosition, Velocity> pointData in points)
//            {
//                BATVelocity batVelocity = conversionContext.ToBAT(pointData.Item3);
//                Velocity velocityBat = conversionContext.CreateFromBAT(batVelocity);

//                ECEFVelocity velocity = conversionContext.ToECEF(velocityBat);
//                GeoPosition origin = s_positionConverter.EcefToGeo(pointData.Item2);
//                s_velocityConverter.EcefToAeronauticalVelocity(
//                    out Angle course,
//                    out VelocityScalar rateOfClimb,
//                    out VelocityScalar groundSpeed,
//                    velocity,
//                    origin);
//                double newCourse = Math.Abs(course.Degrees) < delta ? 0 : course.Degrees < 0 ? 360 + course.Degrees : course.Degrees;
//                Assert.AreEqual(getNotNaN(pointData.Item1.Course.Degrees), newCourse, delta, "Wrong course");
//                Assert.AreEqual(getNotNaN(pointData.Item1.RateOfClimb.MetersPerSecond), rateOfClimb.MetersPerSecond, delta, "Wrong RateOfClimb");
//                Assert.AreEqual(getNotNaN(pointData.Item1.GroundSpeed.MetersPerSecond), groundSpeed.MetersPerSecond, delta, "Wrong GroundSpeed");

//                ECEFVelocity newECEF = s_velocityConverter.AeronauticalToEcefVelocity(course, rateOfClimb, groundSpeed, origin);
//                Assert.AreEqual(getNotNaN(pointData.Item3.X.MetersPerSecond), newECEF.X.MetersPerSecond, delta, "Wrong X");
//                Assert.AreEqual(getNotNaN(pointData.Item3.Y.MetersPerSecond), newECEF.Y.MetersPerSecond, delta, "Wrong Y");
//                Assert.AreEqual(getNotNaN(pointData.Item3.Z.MetersPerSecond), newECEF.Z.MetersPerSecond, delta, "Wrong Z");
//            }
//        }

//      

//        private static double getNotNaN(double value) => double.IsNaN(value) ? 0 : value;
//    }
//}