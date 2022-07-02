using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class LtpVelocity : ILocalTangentPlane<Velocity>
    {
        public LtpVelocity(Velocity north, Velocity east, Velocity down) : this(north.MetersPerSecond, east.MetersPerSecond, down.MetersPerSecond)
        {
        }
        public LtpVelocity(double northMetersPerSec, double eastMetersPerSec, double downMetersPerSec) 
        {
            North = Velocity.FromMetersPerSecond(northMetersPerSec);
            East = Velocity.FromMetersPerSecond(eastMetersPerSec);
            West = Velocity.FromMetersPerSecond(-eastMetersPerSec);
            South = Velocity.FromMetersPerSecond(-northMetersPerSec);
            Up = Velocity.FromMetersPerSecond(-downMetersPerSec);
            Down = Velocity.FromMetersPerSecond(downMetersPerSec);
        }

        public Velocity East { get; protected set; }
        public Velocity West { get; protected set; }
        public Velocity North { get; protected set; }
        public Velocity South { get; protected set; }
        public Velocity Up { get; protected set; }
        public Velocity Down { get; protected set; }
    }
}