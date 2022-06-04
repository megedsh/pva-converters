using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public abstract class LtpPosition : Vector3d, ILocalTangentPlane<Distance, NedPosition, EnuPosition>
    {
        protected LtpPosition(double xMeters, double yMeters, double zMeters) : base(xMeters, yMeters, zMeters)
        {
        }

        public Distance East { get; protected set; }
        public Distance West { get; protected set; }
        public Distance North { get; protected set; }
        public Distance South { get; protected set; }
        public Distance Up { get; protected set; }
        public Distance Down { get; protected set; }

        public NedPosition AsNed()
        {
            if (this is NedPosition ned)
            {
                return ned;
            }

            return new NedPosition(North, East, Down);
        }


        public EnuPosition AsEnu()
        {
            if (this is EnuPosition enu)
            {
                return enu;
            }

            return new EnuPosition(East, North, Up);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(East)}: {East}, {nameof(West)}: {West}, {nameof(North)}: {North}, {nameof(South)}: {South}, {nameof(Up)}: {Up}, {nameof(Down)}: {Down}";
        }
    }
}