using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.AzimuthElevation
{
    public class AzimuthElevationVelocity : AzimuthElevationBase<Velocity>
    {
        public Velocity Velocity => Scalar;

        public AzimuthElevationVelocity(double azimuthRad, double elevationRad, double velocityMetersPerSec) : this(Angle.FromRadians(azimuthRad), Angle.FromRadians(elevationRad), Velocity.FromMetersPerSecond(velocityMetersPerSec))
        {
        }

        public AzimuthElevationVelocity(Angle azimuth, Angle elevation, Velocity velocity) : base(azimuth, elevation, velocity)
        {

        }
    }
}