
//namespace UnitTests.CoordinatesConverter
//{
//    [TestClass]
//    [Ignore]
//    public class BatAxisContextTest : BaseCoordinatesConverterTest
//    {
        //public static readonly GeoPosition BatGeoPosition = GeoPosition.FromLatLonAndAltitude(32.086, 34.789, DistanceScalar.Zero);


//        #region Velocity

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*Velocity*/ Velocity_CreateTest( /*VelocityScalar north, VelocityScalar east, VelocityScalar up*/)
//        {
//            runTest(() =>
//            {
//                VelocityScalar north = VelocityScalar.FromMetersPerSecond(1.22);
//                VelocityScalar east = VelocityScalar.FromMetersPerSecond(2.11);
//                VelocityScalar up = VelocityScalar.FromMetersPerSecond(3.22);
//                Velocity expected = conversionContext.CreateFromBAT(
//                                                                    VelocityScalar.FromMetersPerSecond(1.22),
//                                                                    VelocityScalar.FromMetersPerSecond(2.11),
//                                                                    VelocityScalar.FromMetersPerSecond(-3.22));

//                Velocity actual = conversionContext.Create(north, east, up);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*Velocity*/ Velocity_CreateTest2( /*VelocityScalar groundVelocity, Angle azimuth, VelocityScalar Z_Velocity*/)
//        {
//            runTest(() =>
//            {
//                VelocityScalar groundVelocity = VelocityScalar.FromMetersPerSecond(1.22);
//                Angle azimuth = Angle.FromDegrees(63.55);
//                VelocityScalar zVelocity = VelocityScalar.FromMetersPerSecond(3.22);
//                Velocity expected = conversionContext.CreateFromBAT(
//                                                                    VelocityScalar.FromMetersPerSecond(0.543408332214331),
//                                                                    VelocityScalar.FromMetersPerSecond(1.0922945502381851),
//                                                                    VelocityScalar.FromMetersPerSecond(-3.22));

//                Velocity actual = conversionContext.Create(groundVelocity, azimuth, zVelocity);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*Velocity*/ Velocity_CreateFromECEFTest( /*VelocityScalar ecefX, VelocityScalar ecefY, VelocityScalar ecefZ*/)
//        {
//            runTest(() =>
//            {
//                VelocityScalar ecefX = VelocityScalar.FromMetersPerSecond(1.2);
//                VelocityScalar ecefY = VelocityScalar.FromMetersPerSecond(2.1);
//                VelocityScalar ecefZ = VelocityScalar.FromMetersPerSecond(3.2);

//                ECEFVelocity ecefVel = new ECEFVelocity(ecefX, ecefY, ecefZ);
//                Velocity expected = conversionContext.CreateFromBAT(
//                                                                    VelocityScalar.FromMetersPerSecond(1.5512542622059824),
//                                                                    VelocityScalar.FromMetersPerSecond(1.039976313187982),
//                                                                    //TODO: Can be z-Velocity negtive?
//                                                                    VelocityScalar.FromMetersPerSecond(-3.5499379546684624));

//                Velocity actual = conversionContext.CreateFromECEF(ecefVel.X, ecefVel.Y, ecefVel.Z);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*BATVelocity*/ Velocity_ToBATTest( /*Velocity vel*/)
//        {
//            runTest(() =>
//            {
//                Velocity vel = Velocity.CreateECEF(
//                                                   VelocityScalar.FromMetersPerSecond(1.22),
//                                                   VelocityScalar.FromMetersPerSecond(2.11),
//                                                   VelocityScalar.FromMetersPerSecond(3.22));
//                BATVelocity expected = new BATVelocity(
//                                                       VelocityScalar.FromMetersPerSecond(1.5564436374509982),
//                                                       VelocityScalar.FromMetersPerSecond(1.036777782662639),
//                                                       //TODO: Can be z-Velocity negtive?
//                                                       VelocityScalar.FromMetersPerSecond(-3.5793120893288206));

//                BATVelocity actual = conversionContext.ToBAT(vel);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*ECEFVelocity*/ Velocity_ToECEFTest( /*Velocity vel*/)
//        {
//            runTest(() =>
//            {
//                Velocity vel = conversionContext.CreateFromBAT(
//                                                               VelocityScalar.FromMetersPerSecond(1.5564436374509982),
//                                                               VelocityScalar.FromMetersPerSecond(1.036777782662639),
//                                                               //TODO: Can be z-Velocity negtive?
//                                                               VelocityScalar.FromMetersPerSecond(-3.5793120893288206));

//                ECEFVelocity expected = new ECEFVelocity(
//                                                         //TODO: should be 1.22 as in preveous test
//                                                         VelocityScalar.FromMetersPerSecond(1.2199999999999993),
//                                                         VelocityScalar.FromMetersPerSecond(2.11),
//                                                         VelocityScalar.FromMetersPerSecond(3.22));

//                ECEFVelocity actual = conversionContext.ToECEF(vel);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        #endregion

//        #region Acceleration

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*Acceleration*/ Acceleration_CreateTest( /*AccelerationScalar north, AccelerationScalar east, AccelerationScalar up*/)
//        {
//            runTest(() =>
//            {
//                AccelerationScalar north = AccelerationScalar.FromMetersPerSquareSecond(1.22);
//                AccelerationScalar east = AccelerationScalar.FromMetersPerSquareSecond(2.11);
//                AccelerationScalar up = AccelerationScalar.FromMetersPerSquareSecond(3.22);
//                Acceleration expected = conversionContext.CreateFromBAT(
//                                                                        AccelerationScalar.FromMetersPerSquareSecond(1.22),
//                                                                        AccelerationScalar.FromMetersPerSquareSecond(2.11),
//                                                                        AccelerationScalar.FromMetersPerSquareSecond(-3.22));

//                Acceleration actual = conversionContext.Create(north, east, up);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*Acceleration*/ Acceleration_CreateFromECEFTest( /*ECEFAcceleration ecefAcc*/)
//        {
//            runTest(() =>
//            {
//                ECEFAcceleration ecefAcc = new ECEFAcceleration(
//                                                                AccelerationScalar.FromMetersPerSquareSecond(1.22),
//                                                                AccelerationScalar.FromMetersPerSquareSecond(2.11),
//                                                                AccelerationScalar.FromMetersPerSquareSecond(3.22));
//                Acceleration expected = conversionContext.CreateFromBAT(
//                                                                        AccelerationScalar.FromMetersPerSquareSecond(1.5564436374509982),
//                                                                        AccelerationScalar.FromMetersPerSquareSecond(1.036777782662639),
//                                                                        AccelerationScalar.FromMetersPerSquareSecond(-3.5793120893288206));

//                Acceleration actual = conversionContext.CreateFromECEF(ecefAcc);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*Acceleration*/ Acceleration_CreateFromBATTest( /*BATAcceleration batAcc*/)
//        {
//            runTest(() =>
//            {
//                BATAcceleration batAcc = new BATAcceleration(
//                                                             AccelerationScalar.FromMetersPerSquareSecond(1.5564436374509982),
//                                                             AccelerationScalar.FromMetersPerSquareSecond(1.036777782662639),
//                                                             AccelerationScalar.FromMetersPerSquareSecond(-3.5793120893288206));
//                Acceleration expected = conversionContext.CreateFromECEF(
//                                                                         AccelerationScalar.FromMetersPerSquareSecond(1.22),
//                                                                         AccelerationScalar.FromMetersPerSquareSecond(2.11),
//                                                                         AccelerationScalar.FromMetersPerSquareSecond(3.22));

//                Acceleration actual = conversionContext.CreateFromBAT(batAcc);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*ECEFAcceleration*/ Acceleration_ToECEFTest( /*Acceleration acc*/)
//        {
//            runTest(() =>
//            {
//                Acceleration acc = conversionContext.CreateFromBAT(
//                                                                   AccelerationScalar.FromMetersPerSquareSecond(1.22),
//                                                                   AccelerationScalar.FromMetersPerSquareSecond(2.11),
//                                                                   AccelerationScalar.FromMetersPerSquareSecond(3.22));

//                ECEFAcceleration expected = new ECEFAcceleration(
//                                                                 AccelerationScalar.FromMetersPerSquareSecond(-3.9766103540653317),
//                                                                 AccelerationScalar.FromMetersPerSquareSecond(-0.19345733379962704),
//                                                                 AccelerationScalar.FromMetersPerSquareSecond(-0.67678973983027957));
//                ECEFAcceleration actual = conversionContext.ToECEF(acc);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        public void /*BATAcceleration*/ Acceleration_ToBATTest( /*Acceleration acc*/)
//        {
//            runTest(() =>
//            {
//                Acceleration acc = Acceleration.CreateECEF(
//                                                           AccelerationScalar.FromMetersPerSquareSecond(-3.9766103540653317),
//                                                           AccelerationScalar.FromMetersPerSquareSecond(-0.19345733379962704),
//                                                           AccelerationScalar.FromMetersPerSquareSecond(-0.67678973983027957));

//                BATAcceleration expected = new BATAcceleration(
//                                                               //TODO: should be 1.22 as in preveous test
//                                                               AccelerationScalar.FromMetersPerSquareSecond(1.2200000000000002),
//                                                               AccelerationScalar.FromMetersPerSquareSecond(2.11),
//                                                               AccelerationScalar.FromMetersPerSquareSecond(3.22));
//                BATAcceleration actual = conversionContext.ToBAT(acc);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        #endregion

//        #region Acceleration Cov

//        [TestMethod]
//        [TestCategory(TestCategories.CoordinatesConversion)]
//        [Ignore]
//        public void /*AccelerationCov*/ AccelerationCov_CreateAccelerationCovFromBATTest( /*CovMatrix3<double> batCov*/)
//        {
//            runTest(() =>
//            {
//                //CovMatrix3<double> batCov = new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11);
//                //AccelerationCov expected = AccelerationCov.CreateBAT(batCov, conversionContext.Id);
//                //AccelerationCov actual = conversionContext.CreateAccelerationCovFromBAT(batCov);
//                //Assert.AreEqual(actual, expected);
//            });
//        }

//#if false
//        [TestMethod]
//        public void /*AccelerationCov*/ AccelerationCov_CreateAccelerationCovFromECEFTest( /*CovMatrix3<double> ecefCov*/)
//        {
//            runTest(() =>
//            {
//                AccelerationCov actual;
//                AccelerationCov expected;
//                CovMatrix3<double> ecefCov = new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11);

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    expected = AccelerationCov.CreateBAT(new CovMatrix3<double>(
//                            0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                           -0.32814088601975488, 1.3402066779573691, -2.3832419865659293,
//                           -2.4123492409786809,-2.3832419865659293, 8.6107368844070535),
//                            convCntx.Id);
                        
//                    actual = convCntx.CreateAccelerationCovFromECEF(ecefCov);
//                }
//               Assert.AreEqual(actual, expected);
//            });
//        }
//#endif
//#if false
//        [TestMethod]
//        public void /*CovMatrix3<double>*/ AccelerationCov_ToBATTest( /*AccelerationCov cov*/)
//        {
//            runTest(() =>
//            {
//                CovMatrix3<double> actual;
//                CovMatrix3<double> expected;
//                AccelerationCov cov = AccelerationCov.CreateECEF(new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11));

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    expected = new CovMatrix3<double>(
//                        0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                        -0.32814088601975488, 1.3402066779573691, -2.3832419865659293,
//                        -2.4123492409786809, -2.3832419865659293, 8.6107368844070535);

//                    actual = convCntx.ToBAT(cov);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif
//#if false
//        [TestMethod]
//        public void /*CovMatrix3<double>*/ AccelerationCov_ToECEFTest( /*AccelerationCov cov*/)
//        {
//            runTest(() =>
//            {
//                CovMatrix3<double> actual;
//                CovMatrix3<double> expected;
                
//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    AccelerationCov cov = AccelerationCov.CreateBAT(new CovMatrix3<double>(
//                        0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                        1.3402066779573691, -2.3832419865659293,
//                        8.6107368844070535), 
//                        convCntx.Id);

//                    expected = new CovMatrix3<double>(
//                        1.22, 2.11, 3.2200000000000006,
//                        2.11, 4.3299999999999992, 3.4399999999999995,
//                        3.2200000000000006, 3.4399999999999995, 5.1100000000000012);

//                    actual = convCntx.ToECEF(cov);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif

//        #endregion

//        #region Position Cov

//        [TestMethod]
//        [Ignore]
//        public void /*PositionCov*/ PositionCov_CreatePositionCovFromBATTest( /*CovMatrix3<double> batCov*/)
//        {
//            runTest(() =>
//            {
//                //CovMatrix3<double> batCov = new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11);
//                //PositionCov expected = PositionCov.CreateBAT(batCov, conversionContext.Id);
//                //PositionCov actual = conversionContext.CreatePositionCovFromBAT(batCov);
//                //Assert.AreEqual(actual, expected);
//            });
//        }

//#if false
//        [TestMethod]
//        public void /*PositionCov*/ PositionCov_CreatePositionCovFromECEFTest( /*CovMatrix3<double> ecefCov*/)
//        {
//            runTest(() =>
//            {
//                PositionCov actual;
//                PositionCov expected;
//                CovMatrix3<double> ecefCov = new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11);

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    expected = PositionCov.CreateBAT(new CovMatrix3<double>(
//                            0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                            -0.32814088601975488, 1.3402066779573691, -2.3832419865659293,
//                            -2.4123492409786809, -2.3832419865659293, 8.6107368844070535),
//                        convCntx.Id);

//                    actual = convCntx.CreatePositionCovFromECEF(ecefCov);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif
//#if false
//        [TestMethod]
//        public void /*CovMatrix3<double>*/ PositionCov_ToBATTest( /*PositionCov cov*/)
//        {
//            runTest(() =>
//            {
//                CovMatrix3<double> actual;
//                CovMatrix3<double> expected;
//                PositionCov cov = PositionCov.CreateECEF(new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11));

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    expected = new CovMatrix3<double>(
//                        0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                       -0.32814088601975488, 1.3402066779573691, -2.3832419865659293,
//                       -2.4123492409786809, -2.3832419865659293, 8.6107368844070535);

//                    actual = convCntx.ToBAT(cov);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif
//#if false
//        [TestMethod]
//        public void /*CovMatrix3<double>*/ PositionCov_ToECEFTest( /*PositionCov cov*/)
//        {
//            runTest(() =>
//            {
//                CovMatrix3<double> actual;
//                CovMatrix3<double> expected;
              
//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    PositionCov cov = PositionCov.CreateBAT(new CovMatrix3<double>(
//                            0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                            1.3402066779573691, -2.3832419865659293,
//                            8.6107368844070535),
//                        convCntx.Id);

//                    expected = new CovMatrix3<double>(
//                        1.22, 2.11, 3.2200000000000006,
//                        2.11, 4.3299999999999992, 3.4399999999999995,
//                        3.2200000000000006, 3.4399999999999995, 5.1100000000000012);

//                    actual = convCntx.ToECEF(cov);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif

//        #endregion

//        #region Velocity Cov

//        [TestMethod]
//        [Ignore]
//        public void /*VelocityCov*/ VelocityCov_CreateVelocityCovFromBATTest( /*CovMatrix3<double> batCov*/)
//        {
//            runTest(() =>
//            {
//                //CovMatrix3<double> batCov = new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11);
//                //VelocityCov expected = VelocityCov.CreateBAT(batCov, conversionContext.Id);
//                //VelocityCov actual = conversionContext.CreateVelocityCovFromBAT(batCov);
//                //Assert.AreEqual(actual, expected);
//            });
//        }

//#if false
//        [TestMethod]
//        public void /*VelocityCov*/ VelocityCov_CreateVelocityCovFromECEFTest( /*CovMatrix3<double> ecefCov*/)
//        {
//            runTest(() =>
//            {
//                VelocityCov actual;
//                VelocityCov expected;

//                CovMatrix3<double> ecefCov = new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11);

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    expected = VelocityCov.CreateBAT(new CovMatrix3<double>(
//                            0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                            -0.32814088601975488, 1.3402066779573691, -2.3832419865659293,
//                            -2.4123492409786809, -2.3832419865659293, 8.6107368844070535),
//                        convCntx.Id);

//                    actual = convCntx.CreateVelocityCovFromECEF(ecefCov);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif
//#if false
//        [TestMethod]
//        public void /*CovMatrix3<double>*/ VelocityCov_ToBATTest( /*VelocityCov cov*/)
//        {
//            runTest(() =>
//            {
//                CovMatrix3<double> actual;
//                CovMatrix3<double> expected;

//                VelocityCov cov = VelocityCov.CreateECEF(new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11));

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    expected = new CovMatrix3<double>(
//                        0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                        -0.32814088601975488, 1.3402066779573691, -2.3832419865659293,
//                        -2.4123492409786809, -2.3832419865659293, 8.6107368844070535);

//                    actual = convCntx.ToBAT(cov);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif
//#if false
//        [TestMethod]
//        public void /*CovMatrix3<double>*/ VelocityCov_ToECEFTest( /*VelocityCov cov*/)
//        {
//            runTest(() =>
//            {
//                CovMatrix3<double> actual;
//                CovMatrix3<double> expected;

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    VelocityCov cov = VelocityCov.CreateBAT(new CovMatrix3<double>(
//                            0.70905643763557791, -0.32814088601975488, -2.4123492409786809,
//                            1.3402066779573691, -2.3832419865659293,
//                            8.6107368844070535),
//                        convCntx.Id);

//                    expected = new CovMatrix3<double>(
//                        1.22, 2.11, 3.2200000000000006,
//                        2.11, 4.3299999999999992, 3.4399999999999995,
//                        3.2200000000000006, 3.4399999999999995, 5.1100000000000012);

//                    actual = convCntx.ToECEF(cov);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif

//        #endregion

//        #region Rotation

//        [TestMethod]
//        [Ignore]
//        public void /*DirectionalCosineMatrix*/ Rotation_ToECEFTest( /*DirectionalCosineMatrix batDCM*/)
//        {
//            runTest(() =>
//            {
//                DirectionalCosineMatrix bat = new DirectionalCosineMatrix();

//                for (int i = 0; i < 3; i++)
//                {
//                    for (int j = 0; j < 3; j++)
//                    {
//                        bat[i, j] = 3.8256 * i + j;
//                    }
//                }

//                DirectionalCosineMatrix expected = new DirectionalCosineMatrix
//                {
//                    [0, 0] = 5.3230507537465384,
//                    [0, 1] = 5.4309822709922884,
//                    [0, 2] = 5.5389137882380393,
//                    [1, 0] = 3.1418075242647214,
//                    [1, 1] = 3.3925103795670903,
//                    [1, 2] = 3.6432132348694606,
//                    [2, 0] = -5.91356516439212,
//                    [2, 1] = -7.6239741406104589,
//                    [2, 2] = -9.3343831168288
//                };

//                DirectionalCosineMatrix actual = new DirectionalCosineMatrix(); // conversionContext.ToECEF(bat);
//                Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [Ignore]
//        public void /*RotationAngles*/ Rotation_ToECEFTest2( /*RotationAngles batRotationAngles*/)
//        {
//            runTest(() =>
//            {
//                //RotationAngles batRotationAngles = new RotationAngles(
//                //    Angle.FromDegrees(25.33),
//                //    Angle.FromDegrees(25.33),
//                //    Angle.FromDegrees(25.33));
//                //RotationAngles expected = new RotationAngles(
//                //    Angle.FromDegrees(-75.534675035239289),
//                //    Angle.FromDegrees(-49.6065274121446),
//                //    Angle.FromDegrees(119.91708254321522));

//                //RotationAngles actual = conversionContext.ToECEF(batRotationAngles);
//                //Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [Ignore]
//        public void /*DirectionalCosineMatrix*/ Rotation_ToBATTest( /*DirectionalCosineMatrix ecefDCM*/)
//        {
//            runTest(() =>
//            {
//                //DirectionalCosineMatrix ecef = new DirectionalCosineMatrix();

//                //for (int i = 0; i < 3; i++)
//                //{
//                //    for (int j = 0; j < 3; j++)
//                //    {
//                //        ecef[i, j] = 3.8256 * i + j;
//                //    }
//                //}

//                //DirectionalCosineMatrix expected = new DirectionalCosineMatrix
//                //{
//                //    [0, 0] = -7.5065224722945514,
//                //    [0, 1] = -9.20913703123012,
//                //    [0, 2] = -10.911751590165689,
//                //    [1, 0] = -0.5568168820854118,
//                //    [1, 1] = -0.522037095357287,
//                //    [1, 2] = -0.48725730862916183,
//                //    [2, 0] = -4.0642529612170533,
//                //    [2, 1] = -3.7481927926798266,
//                //    [2, 2] = -3.4321326241426
//                //};

//                //DirectionalCosineMatrix actual = conversionContext.ToBAT(ecef);
//                //Assert.AreEqual(actual, expected);
//                Assert.AreEqual(true, true);
//            });
//        }

//        [TestMethod]
//        [Ignore]
//        public void /*RotationAngles*/ Rotation_ToBATTest2( /*RotationAngles ecefRotationAngles*/)
//        {
//            runTest(() =>
//            {
//                //RotationAngles ecefRotationAngles = new RotationAngles(
//                //    Angle.FromDegrees(25.33),
//                //    Angle.FromDegrees(25.33),
//                //    Angle.FromDegrees(25.33));
//                //RotationAngles expected = new RotationAngles(
//                //    Angle.FromDegrees(-138.81864948199467),
//                //    Angle.FromDegrees(37.045243757008244),
//                //    Angle.FromDegrees(176.24136243350557));

//                //RotationAngles actual = conversionContext.ToBAT(ecefRotationAngles);
//                //Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [Ignore]
//        public void /*DirectionalCosineMatrix*/ Rotation_ToDCMTest( /*RotationAngles rotationAngles*/)
//        {
//            runTest(() =>
//            {
//                //RotationAngles rotationAngles = new RotationAngles(
//                //    Angle.FromDegrees(25.33),
//                //    Angle.FromDegrees(25.33),
//                //    Angle.FromDegrees(25.33));
//                //DirectionalCosineMatrix expected = new DirectionalCosineMatrix
//                //{
//                //    [0, 0] = 0.81696048030717816,
//                //    [0, 1] = 0.38669891895820319,
//                //    [0, 2] = -0.42783118130031372,
//                //    [1, 0] = -0.22125706365276082,
//                //    [1, 1] = 0.89527049424200023,
//                //    [1, 2] = 0.38669891895820319,
//                //    [2, 0] = 0.53256068705831361,
//                //    [2, 1] = -0.22125706365276082,
//                //    [2, 2] = 0.81696048030717816
//                //};
//                //DirectionalCosineMatrix actual = conversionContext.ToDCM(rotationAngles);
//                //Assert.AreEqual(actual, expected);
//            });
//        }

//        [TestMethod]
//        [Ignore]
//        public void /*RotationAngles*/ Rotation_ToRotationAnglesTest( /*DirectionalCosineMatrix dcm*/)
//        {
//            runTest(() =>
//            {
//                //DirectionalCosineMatrix dcm = new DirectionalCosineMatrix
//                //{
//                //    [0, 0] = 0.81696048030717816,
//                //    [0, 1] = 0.38669891895820319,
//                //    [0, 2] = -0.42783118130031372,
//                //    [1, 0] = -0.22125706365276082,
//                //    [1, 1] = 0.89527049424200023,
//                //    [1, 2] = 0.38669891895820319,
//                //    [2, 0] = 0.53256068705831361,
//                //    [2, 1] = -0.22125706365276082,
//                //    [2, 2] = 0.81696048030717816
//                //};
//                //RotationAngles expected = new RotationAngles(
//                //    Angle.FromDegrees(25.33),
//                //    Angle.FromDegrees(25.33),
//                //    Angle.FromDegrees(25.33));
//                //RotationAngles actual = conversionContext.ToRotationAngles(dcm);
//                //Assert.AreEqual(actual, expected);
//            });
//        }

//#if false
//        [TestMethod]
//        public void /*CovMatrix3<double>*/ Rotation_ToECEFRotationTest( /*CovMatrix3<double> rotatedCovMatrix*/)
//        {
//            runTest(() =>
//            {
//                CovMatrix3<double> actual;
//                CovMatrix3<double> expected;
//                CovMatrix3<double> rotatedCovMatrix = new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11);

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    expected = new CovMatrix3<double>(
//                         2.6279295979593753, 0,0,
//                         0,1.7186913593042554,0,
//                         0,0,- 6.0406527825742442);
//                    actual = convCntx.ToECEFRotation(rotatedCovMatrix);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif
//#if false
//        [TestMethod]
//        public void /*CovMatrix3<double>*/ Rotation_ToBATRotationTest( /*CovMatrix3<double> covMatrix*/)
//        {
//            runTest(() =>
//            {
//                CovMatrix3<double> actual;
//                CovMatrix3<double> expected;
//                CovMatrix3<double> covMatrix = new CovMatrix3<double>(1.22, 2.11, 3.22, 4.33, 3.44, 5.11);

//                using (IConversionContext convCntx = BatAxisContext.Create(BatGeoPosition, GetType()))
//                {
//                    expected = new CovMatrix3<double>(
//                        2.2423109504710883, 0, 0,
//                        0, -6.3647240281044368, 0,
//                        0, 0, 0.900960402829931);
//                    actual = convCntx.ToBATRotation(covMatrix);
//                }
//                Assert.AreEqual(actual, expected);
//            });
//        }
//#endif

//        #endregion
//    }
//}