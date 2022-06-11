using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.AzimuthElevation
{
    public class AzimuthElevationRange:AzimuthElevationBase<Distance>
    {
        public Distance Range => Scalar;

        public AzimuthElevationRange(double azimuthRad, double elevationRad, double rangeMeters) : this(Angle.FromRadians(azimuthRad), Angle.FromRadians(elevationRad), Distance.FromMeters(rangeMeters))
        {
        }

        public AzimuthElevationRange(Angle azimuth, Angle elevation, Distance range) : base(azimuth,elevation,range)
        {
            
        }

        public override string ToString()
        {
            return $"{base.ToString()}";
        }

        //public override string ToString()
        //{
        //    return $"{nameof(Azimuth)}:{Azimuth}, {nameof(Elevation)}:{Elevation},{nameof(Range)}:{Range} ";
        //}
    }
}