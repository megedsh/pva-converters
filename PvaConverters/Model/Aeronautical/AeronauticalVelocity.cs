using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.Aeronautical
{
    public class AeronauticalVelocity : AeronauticalVector<Velocity>
    {
        public AeronauticalVelocity(Angle course, Velocity vertical, Velocity horizontal) : base(course, vertical, horizontal)
        {
        }

        public Velocity RateOfClimb => Vertical;
        public Velocity GroundVelocity => Horizontal;
    }
}