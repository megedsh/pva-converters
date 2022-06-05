using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.Aeronautical
{
    public class AeronauticalAcceleration : AeronauticalVector<Acceleration>
    {
        public AeronauticalAcceleration(Angle course, Acceleration climbAccel, Acceleration groundAccel) : base(course, climbAccel, groundAccel)
        {
        }

        public Acceleration ClimbAcceleration => Vertical;
        public Acceleration GroundAcceleration => Horizontal;
    }
}