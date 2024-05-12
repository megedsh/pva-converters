using System;

namespace NavigationToolkit.Model
{
    [Serializable]
    public class Datum
    {
        private string m_name;
        private string m_description;
        private double m_semiMajorAxis;
        private double m_flattening;
        private double m_translationX;
        private double m_translationY;
        private double m_translationZ;
        private double m_rotationX;
        private double m_rotationY;
        private double m_rotationZ;
        private double m_scaling;

        public static readonly Datum WGS84 = new Datum(nameof(WGS84), "World Geodetic System 1984", 6378137.0, 0.00335281066474748, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
        public static readonly Datum WGS72 = new Datum(nameof(WGS72), "World Geodetic System 1972", 6378135.0, 0.0033527794541675, 0.0, 0.0, 4.5, 0.0, 0.0, 0.554, 2.263E-07);
        public static readonly Datum SPHERE = new Datum(nameof(SPHERE), "Spherical Earth", 6371009.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);

        public Datum()
        {
            m_name = WGS84.Name;
            m_description = WGS84.Description;
            m_semiMajorAxis = WGS84.SemiMajorAxis;
            m_flattening = WGS84.Flattening;
            m_translationX = WGS84.TranslationX;
            m_translationY = WGS84.TranslationY;
            m_translationZ = WGS84.TranslationZ;
            m_rotationX = WGS84.RotationX;
            m_rotationY = WGS84.RotationY;
            m_rotationZ = WGS84.RotationZ;
            m_scaling = WGS84.Scaling;
        }

        public Datum(
            string name,
            string description,
            double semiMajorAxis,
            double flattening,
            double translationX,
            double translationY,
            double translationZ)
        {
            m_name = name;
            m_description = description;
            m_semiMajorAxis = semiMajorAxis;
            m_flattening = flattening;
            m_translationX = translationX;
            m_translationY = translationY;
            m_translationZ = translationZ;
        }

        public Datum(
            string name,
            string description,
            double semiMajorAxis,
            double flattening,
            double translationX,
            double translationY,
            double translationZ,
            double rotationX,
            double rotationY,
            double rotationZ,
            double scaling)
        {
            m_name = name;
            m_description = description;
            m_semiMajorAxis = semiMajorAxis;
            m_flattening = flattening;
            m_translationX = translationX;
            m_translationY = translationY;
            m_translationZ = translationZ;
            m_rotationX = rotationX;
            m_rotationY = rotationY;
            m_rotationZ = rotationZ;
            m_scaling = scaling;
        }

        public string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public string Description
        {
            get => m_description;
            set => m_description = value;
        }

        public double SemiMajorAxis
        {
            get => m_semiMajorAxis;
            set => m_semiMajorAxis = value;
        }

        public double SemiMinorAxis => m_semiMajorAxis * (1.0 - m_flattening);

        public double Flattening
        {
            get => m_flattening;
            set => m_flattening = value;
        }

        public double FlatteningInverse
        {
            get => 1.0 / m_flattening;
            set => m_flattening = 1.0 / value;
        }

        public double TranslationX
        {
            get => m_translationX;
            set => m_translationX = value;
        }

        public double TranslationY
        {
            get => m_translationY;
            set => m_translationY = value;
        }

        public double TranslationZ
        {
            get => m_translationZ;
            set => m_translationZ = value;
        }

        public double RotationX
        {
            get => m_rotationX;
            set => m_rotationX = value;
        }

        public double RotationY
        {
            get => m_rotationY;
            set => m_rotationY = value;
        }

        public double RotationZ
        {
            get => m_rotationZ;
            set => m_rotationZ = value;
        }

        public double Scaling
        {
            get => m_scaling;
            set => m_scaling = value;
        }

        public double EccentricitySquared => m_flattening * (2.0 - m_flattening);
    }
}