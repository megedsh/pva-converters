using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.Aeronautical
{
    public class AeronauticalVelocity : AeronauticalVector<Velocity>
    {
        public AeronauticalVelocity(Angle course, Velocity rateOfClimb, Velocity groundSpeed) : base(course, rateOfClimb, groundSpeed)
        {
        }

        public Velocity RateOfClimb => Vertic;
        public Velocity GroundSpeed => Horiz;
    }
}