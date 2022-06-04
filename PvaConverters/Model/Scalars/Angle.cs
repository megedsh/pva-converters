using System;

namespace PvaConverters.Model.Scalars
{
    public readonly struct Angle: IScalar
    {
        public static readonly Angle Zero = new Angle(0.0);
        public static readonly Angle Angle90 = new Angle(90.0);
        public static readonly Angle Angle180 = new Angle(180.0);
        public static readonly Angle Angle270 = new Angle(270.0);
        public static readonly Angle Angle360 = new Angle(360.0);
        private readonly double m_degrees;

        private Angle(double degrees) => m_degrees = degrees;

        public static Angle FromDegrees(double degrees) => new Angle(degrees);

        public static Angle FromRadians(double radians) => new Angle(radians / (Math.PI / 180.0));


        public double Degrees => m_degrees;

        public double Radians => m_degrees * (Math.PI / 180.0);


        public Angle Normalized()
        {
            double degrees = m_degrees % 360.0;
            if (degrees < 0.0)
            {
                double num = degrees + 360.0;
                degrees = num.Equals(360.0) ? 0.0 : num;
            }

            Angle angle = new Angle(degrees);
            return angle;
        }

        public double AsDouble()
        {
            return Radians;
        }
    }
}