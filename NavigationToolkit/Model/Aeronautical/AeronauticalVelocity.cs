namespace NavigationToolkit.Model.Aeronautical
{
    public struct AeronauticalVelocity : IAeronauticalVector
    {
        public static AeronauticalVelocity Empty = new AeronauticalVelocity(double.NaN, double.NaN, double.NaN);
        public AeronauticalVelocity(double courseDeg, double climbMetersPerSec, double groundMetersPerSec)
        {
            Course = courseDeg;
            ClimbVelocity = climbMetersPerSec;
            GroundVelocity = groundMetersPerSec;
        }

        public double Course { get; }
        public double ClimbVelocity { get; }
        public double GroundVelocity { get; }

        public double GetVertical() => ClimbVelocity;
        public double GetHorizontal() => GroundVelocity;


        public bool Equals(AeronauticalVelocity other)
        {
            return Course.Equals(other.Course) && ClimbVelocity.Equals(other.ClimbVelocity) && GroundVelocity.Equals(other.GroundVelocity);
        }

        public override bool Equals(object obj)
        {
            return obj is AeronauticalVelocity other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Course.GetHashCode();
                hashCode = (hashCode * 397) ^ ClimbVelocity.GetHashCode();
                hashCode = (hashCode * 397) ^ GroundVelocity.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Course)}: {Course}, {nameof(GroundVelocity)}: {GroundVelocity}, {nameof(ClimbVelocity)}: {ClimbVelocity}";
        }
    }
}