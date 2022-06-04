using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class EnuAcceleration : LtpAcceleration
    {
        public EnuAcceleration(Acceleration east, Acceleration north, Acceleration up) : this(east.MetersPerSquareSecond, north.MetersPerSquareSecond, up.MetersPerSquareSecond)
        {
        }
        public EnuAcceleration( double eastMetersPerSec, double northMetersPerSec, double upMetersPerSec) : base(eastMetersPerSec, northMetersPerSec, upMetersPerSec)
        {
            North = Acceleration.FromMetersPerSquareSecond(Y);
            East = Acceleration.FromMetersPerSquareSecond(X);
            West = Acceleration.FromMetersPerSquareSecond(-X);
            South = Acceleration.FromMetersPerSquareSecond(-Y);
            Up = Acceleration.FromMetersPerSquareSecond(Z);
            Down = Acceleration.FromMetersPerSquareSecond(-Z);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(East)}: {East}, {nameof(North)}: {North}, {nameof(Up)}: {Up}";
        }
    }
}