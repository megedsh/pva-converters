namespace PvaConverters.Model.Aeronautical
{
    public struct AeronauticalAcceleration : IAeronauticalVector
    {
        public static AeronauticalAcceleration Empty = new AeronauticalAcceleration(double.NaN, double.NaN, double.NaN);
        public AeronauticalAcceleration(double courseDeg, double climbMetersPerSquareSec, double groundMetersPerSquareSec)
        {
            Course = courseDeg;
            ClimbAcceleration = climbMetersPerSquareSec;
            GroundAcceleration = groundMetersPerSquareSec;
        }

        public double Course { get; }
        public double ClimbAcceleration { get; }
        public double GroundAcceleration { get; }

        public double GetVertical() => ClimbAcceleration;
        public double GetHorizontal() => GroundAcceleration;


        public bool Equals(AeronauticalAcceleration other)
        {
            return Course.Equals(other.Course) && ClimbAcceleration.Equals(other.ClimbAcceleration) && GroundAcceleration.Equals(other.GroundAcceleration);
        }

        public override bool Equals(object obj)
        {
            return obj is AeronauticalAcceleration other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Course.GetHashCode();
                hashCode = (hashCode * 397) ^ ClimbAcceleration.GetHashCode();
                hashCode = (hashCode * 397) ^ GroundAcceleration.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Course)}: {Course}, {nameof(GroundAcceleration)}: {GroundAcceleration}, {nameof(ClimbAcceleration)}: {ClimbAcceleration}";
        }
    }
}