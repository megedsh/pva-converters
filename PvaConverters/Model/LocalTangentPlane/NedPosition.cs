using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class NedPosition : LtpPosition
    {
        public NedPosition(Distance north, Distance east, Distance down) : this(north.Meters, east.Meters, down.Meters)
        {
        }
        public NedPosition(double northMeters, double eastMeters, double downMeters) : base(northMeters, eastMeters, downMeters)
        {
            North = Distance.FromMeters(X);
            East = Distance.FromMeters(Y);
            West = Distance.FromMeters(-Y);
            South = Distance.FromMeters(-X);
            Up = Distance.FromMeters(-Z);
            Down = Distance.FromMeters(Z);
        }

        public override string ToString()
        {
            return $"{nameof(North)}: {North}, {nameof(East)}: {East}, {nameof(Down)}: {Down}";
        }
        
    }
}