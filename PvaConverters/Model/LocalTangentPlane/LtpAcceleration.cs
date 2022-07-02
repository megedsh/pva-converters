using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class LtpAcceleration : ILocalTangentPlane<Acceleration>
    {

        public LtpAcceleration(double northMetersPerSec, double eastMetersPerSec, double downMetersPerSec):this(Acceleration.FromMetersPerSquareSecond(northMetersPerSec),
            Acceleration.FromMetersPerSquareSecond(eastMetersPerSec),
            Acceleration.FromMetersPerSquareSecond(downMetersPerSec)
            )
        {
        }
        public LtpAcceleration(Acceleration north, Acceleration east, Acceleration down)
        {
            North = north;
            East = east;
            West = -east;
            South = -north;
            Up = -down;
            Down = down;
        }


        public Acceleration East { get; protected set; }
        public Acceleration West { get; protected set; }
        public Acceleration North { get; protected set; }
        public Acceleration South { get; protected set; }
        public Acceleration Up { get; protected set; }
        public Acceleration Down { get; protected set; }
    }
}