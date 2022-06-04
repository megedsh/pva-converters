using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public abstract class LtpAcceleration : Vector3d, ILocalTangentPlane<Acceleration,NedAcceleration,EnuAcceleration>
    {
        protected LtpAcceleration(double xMetersPerSqrSec, double yMetersPerSqrSec, double zMetersPerSqrSec) : base(xMetersPerSqrSec, yMetersPerSqrSec, zMetersPerSqrSec)
        {
        }

        public  Acceleration East { get; protected set; }
        public  Acceleration West { get; protected set; }
        public  Acceleration North { get; protected set; }
        public  Acceleration South { get; protected set; }
        public  Acceleration Up { get; protected set; }
        public  Acceleration Down { get; protected set; }

        public NedAcceleration AsNed()
        {
            if (this is NedAcceleration ned)
            {
                return ned;
            }

            return new NedAcceleration(North, East, Down);
        }


        public EnuAcceleration AsEnu()
        {
            if (this is EnuAcceleration enu)
            {
                return enu;
            }

            return new EnuAcceleration(East, North, Up);
        }
    }
}