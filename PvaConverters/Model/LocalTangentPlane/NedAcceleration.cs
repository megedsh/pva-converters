using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class NedAcceleration : LtpAcceleration
    {
        public NedAcceleration(Acceleration north, Acceleration east, Acceleration down) : this(north.MetersPerSquareSecond, east.MetersPerSquareSecond, down.MetersPerSquareSecond)
        {
        }
        public NedAcceleration(double northMetersPerSec, double eastMetersPerSec, double downMetersPerSec) : base(northMetersPerSec, eastMetersPerSec, downMetersPerSec)
        {
            North = Acceleration.FromMetersPerSquareSecond(X);
            East = Acceleration.FromMetersPerSquareSecond(Y);
            West = Acceleration.FromMetersPerSquareSecond(-Y);
            South = Acceleration.FromMetersPerSquareSecond(-X);
            Up = Acceleration.FromMetersPerSquareSecond(-Z);
            Down = Acceleration.FromMetersPerSquareSecond(Z);
        }


        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(North)}: {North}, {nameof(East)}: {East}, {nameof(Down)}: {Down}";
        }

    }
}