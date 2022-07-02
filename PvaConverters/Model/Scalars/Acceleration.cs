namespace PvaConverters.Model.Scalars
{
    public readonly struct Acceleration : IScalar
    {
        public static readonly Acceleration Zero = new Acceleration(0);
        private Acceleration(double meterToSquareSecond) => MetersPerSquareSecond = meterToSquareSecond;

        public static Acceleration FromCentimetersPerSquareSecond(double centimeters) => new Acceleration(centimeters / 100);

        public static Acceleration FromMetersPerSquareSecond(double meters) => new Acceleration(meters);

        public static Acceleration FromKilometersPerSquareSecond(double kilometers) => new Acceleration(kilometers * 1000);

        public static Acceleration FromKnotPerSecond(double knotPerSc) => new Acceleration(knotPerSc * 0.51444); // TODO: Verify!!

        public static Acceleration FromFootPerSquareSecond(double footPerSqSc) => new Acceleration(footPerSqSc * 0.3048);

        public static Acceleration FromMilesPerHourSecond(double milesPerSqSc) => new Acceleration(milesPerSqSc * 0.44704);

        public double CentimetersPerSquareSecond => MetersPerSquareSecond * 100;
        public double MetersPerSquareSecond { get; }

        public double KilometersPerSquareSecond => MetersPerSquareSecond / 1000;
        public double KnotPerSecond => MetersPerSquareSecond / 0.514444; // TODO: Verify!!
        public double FootPerSquareSecond => MetersPerSquareSecond / 0.3048;
        public double MilesPerHourSecond => MetersPerSquareSecond / 0.44704;


        public bool Equals(Acceleration other)
        {
            return MetersPerSquareSecond.Equals(other.MetersPerSquareSecond);
        }

        public override bool Equals(object obj)
        {
            return obj is Acceleration other && Equals(other);
        }

        public override int GetHashCode()
        {
            return MetersPerSquareSecond.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(MetersPerSquareSecond)}: {MetersPerSquareSecond}";
        }

        public double AsDouble()
        {
            return MetersPerSquareSecond;
        }

        public static Acceleration operator -(Acceleration f, Acceleration s) => FromMetersPerSquareSecond(f.MetersPerSquareSecond - s.MetersPerSquareSecond);
        public static implicit operator Acceleration(double mpss) => new Acceleration(mpss);
        public static implicit operator double(Acceleration distance) => distance.MetersPerSquareSecond;
    }
}