using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class EnuVelocity : LtpVelocity
    {
        public EnuVelocity(Velocity east, Velocity north, Velocity up) : this(east.MetersPerSecond, north.MetersPerSecond,  up.MetersPerSecond)
        {
        }
        public EnuVelocity( double eastMetersPerSec, double northMetersPerSec, double upMetersPerSec) : base( eastMetersPerSec, northMetersPerSec, upMetersPerSec)
        {
            North = Velocity.FromMetersPerSecond(Y);
            South = Velocity.FromMetersPerSecond(-Y);
            East =  Velocity.FromMetersPerSecond(X);
            West =  Velocity.FromMetersPerSecond(-X);
            Up =    Velocity.FromMetersPerSecond(Z);
            Down = Velocity.FromMetersPerSecond(-Z);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(East)}: {East}, {nameof(North)}: {North}, {nameof(Up)}: {Up}";
        }
        
    }
}