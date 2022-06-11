using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class EnuPosition : LtpPosition
    {
        public EnuPosition(Distance east, Distance north, Distance up) : this(east.Meters, north.Meters, up.Meters)
        {
        }

        public EnuPosition(double eastMeters, double northMeters, double upMeters) : base(eastMeters, northMeters, upMeters)
        {
            North = Distance.FromMeters(Y);
            East = Distance.FromMeters(X);
            West = Distance.FromMeters(-X);
            South = Distance.FromMeters(-Y);
            Up = Distance.FromMeters(Z);
            Down = Distance.FromMeters(-Z);
        }

        public override string ToString()
        {
            return $"{nameof(East)}: {East}, {nameof(North)}: {North}, {nameof(Up)}: {Up}";
        }
    }
}