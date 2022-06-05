namespace PvaConverters.Model.Scalars
{
    public readonly struct Distance : IScalar
    {
        public static readonly Distance Zero = new Distance(0);

        private const double KilometersToMeters = 1000;
        private const double CentimetersToMeters = 0.01;
        private const double FeetToMeters = 0.3048;
        private const double NauticalMilesToMeters = 1853.184;
        private const double MilesToMeters = 1609.344;
        private const double YardsToMetes = 0.9144;
        public static Distance FromMeters(double meters) => new Distance(meters);

        public static Distance FromKilometers(double kilometers) => new Distance(kilometers * KilometersToMeters);

        public static Distance FromCentimeter(double centimeters) => new Distance(centimeters * CentimetersToMeters);

        public static Distance FromFeet(double feet) => new Distance(feet * FeetToMeters);

        public static Distance FromYard(double yard) => new Distance(yard * YardsToMetes);

        public static Distance FromNauticalMiles(double nauticalMiles) => new Distance(nauticalMiles * NauticalMilesToMeters);

        public static Distance FromMiles(double miles) => new Distance(miles * MilesToMeters);

        private Distance(double meters) => Meters = meters;

        public double Meters { get; }

        public double Kilometers => Meters / KilometersToMeters;

        public double Centimeters => Meters / CentimetersToMeters;

        public double Inches => Meters / 0.0254;

        public double Feet => Meters / FeetToMeters;

        public double Yards => Meters / YardsToMetes;

        public double Miles => Meters / 1609.344;

        public double NauticalMiles => Meters / NauticalMilesToMeters;

        public double AsDouble()
        {
            return Meters;
        }

        public bool Equals(Distance other)
        {
            return Meters.Equals(other.Meters);
        }

        public override bool Equals(object obj)
        {
            return obj is Distance other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Meters.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Meters}";
        }
    }
}

