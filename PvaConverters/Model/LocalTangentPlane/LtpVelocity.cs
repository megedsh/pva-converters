using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public abstract class LtpVelocity : Vector3d, ILocalTangentPlane<Velocity, NedVelocity, EnuVelocity>
    {
        protected LtpVelocity(double xMetersPerSec, double yMetersPerSec, double zMetersPerSec) : base(xMetersPerSec, yMetersPerSec, zMetersPerSec)
        {
        }

        public Velocity East { get; protected set; }
        public Velocity West { get; protected set; }
        public Velocity North { get; protected set; }
        public Velocity South { get; protected set; }
        public Velocity Up { get; protected set; }
        public Velocity Down { get; protected set; }

        public NedVelocity AsNed()
        {
            if (this is NedVelocity ned)
            {
                return ned;
            }

            return new NedVelocity(North, East, Down);
        }


        public EnuVelocity AsEnu()
        {
            if (this is EnuVelocity enu)
            {
                return enu;
            }

            return new EnuVelocity(East, North, Up);
        }
    }
}