using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class NedVelocity : LtpVelocity
    {
        public NedVelocity(Velocity north, Velocity east, Velocity down) : this(north.MetersPerSecond, east.MetersPerSecond, down.MetersPerSecond)
        {
        }
        public NedVelocity(double northMetersPerSec, double eastMetersPerSec, double downMetersPerSec) : base(northMetersPerSec, eastMetersPerSec, downMetersPerSec)
        {
            North = Velocity.FromMetersPerSecond(X);
            East =  Velocity.FromMetersPerSecond(Y);
            West =  Velocity.FromMetersPerSecond(-Y);
            South = Velocity.FromMetersPerSecond(-X);
            Up =    Velocity.FromMetersPerSecond(-Z);
            Down = Velocity.FromMetersPerSecond(Z);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(North)}: {North}, {nameof(East)}: {East}, {nameof(Down)}: {Down}";
        }
        
    }
}