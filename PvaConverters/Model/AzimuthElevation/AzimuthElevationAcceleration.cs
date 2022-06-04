using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.AzimuthElevation
{
    public class AzimuthElevationAcceleration : AzimuthElevationBase<Acceleration>
    {
        public Acceleration Acceleration => Scalar;

        public AzimuthElevationAcceleration(double azimuthRad, double elevationRad, double accelerationMetersPerSqrSec) : this(Angle.FromRadians(azimuthRad), Angle.FromRadians(elevationRad), Acceleration.FromMetersPerSquareSecond(accelerationMetersPerSqrSec))
        {
        }

        public AzimuthElevationAcceleration(Angle azimuth, Angle elevation, Acceleration acceleration) : base(azimuth, elevation, acceleration)
        {

        }
    }
}