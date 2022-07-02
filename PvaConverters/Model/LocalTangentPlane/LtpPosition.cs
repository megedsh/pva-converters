using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public class LtpPosition :  ILocalTangentPlane<Distance>
    {
        public LtpPosition(Distance north, Distance east, Distance down)
        {
            North = north;
            East = east;
            West = -east;
            South = -north;
            Up = -down;
            Down = down;
        }
        public LtpPosition(double northMeters, double eastMeters, double downMeters):this(Distance.FromMeters(northMeters), Distance.FromMeters(eastMeters), Distance.FromMeters(downMeters))
        {
        }

        public Distance East { get; protected set; }
        public Distance West { get; protected set; }
        public Distance North { get; protected set; }
        public Distance South { get; protected set; }
        public Distance Up { get; protected set; }
        public Distance Down { get; protected set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(East)}: {East}, {nameof(West)}: {West}, {nameof(North)}: {North}, {nameof(South)}: {South}, {nameof(Up)}: {Up}, {nameof(Down)}: {Down}";
        }
    }
}