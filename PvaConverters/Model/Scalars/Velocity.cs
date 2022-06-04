using System.Collections.Generic;

namespace PvaConverters.Model.Scalars
{
    public class Velocity : IScalar
    {
        public static readonly Velocity Zero = FromMetersPerSecond(0);
        private Velocity(double meterToSecond) => MetersPerSecond = meterToSecond;

        public static Velocity FromCentimetersPerSecond(double centimetersPerSec) => new Velocity(centimetersPerSec / 100);

        public static Velocity FromMetersPerSecond(double metersPerSec) => new Velocity(metersPerSec);

        public static Velocity FromMilesPerHour(double milesPerHour) => new Velocity(milesPerHour / 2.236936);

        public static Velocity FromKilometersPerSecond(double kilometersPerSec) => new Velocity(kilometersPerSec * 1000);

        public static Velocity FromKilometersPerHour(double kilometersPerHour) => new Velocity(kilometersPerHour / 3.6);

        public static Velocity FromKnots(double knots) => new Velocity(knots / 1.943844);

        public double CentimetersPerSecond => MetersPerSecond * 100;
        public double MetersPerSecond { get; }

        public double KilometersPerSecond => MetersPerSecond / 1000;
        public double FeetPerSecond => MetersPerSecond * 3.28084;
        public double Knots => MetersPerSecond * 1.943844;
        public double KilometersPerHour => MetersPerSecond * 3.6;
        public double MilesPerHour => MetersPerSecond * 2.236936;

        private sealed class MetersPerSecondEqualityComparer : IEqualityComparer<Velocity>
        {
            public bool Equals(Velocity x, Velocity y)
            {
                return x.MetersPerSecond.Equals(y.MetersPerSecond);
            }

            public int GetHashCode(Velocity obj)
            {
                return obj.MetersPerSecond.GetHashCode();
            }
        }

        public static IEqualityComparer<Velocity> MetersPerSecondComparer { get; } = new MetersPerSecondEqualityComparer();

        public override string ToString()
        {
            return $"{MetersPerSecond} mps";
        }

        public double AsDouble()
        {
            return MetersPerSecond;
        }
    }
}