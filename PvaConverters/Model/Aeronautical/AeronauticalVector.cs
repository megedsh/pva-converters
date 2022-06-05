using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.Aeronautical
{
    public abstract class AeronauticalVector<T>
    {
        public AeronauticalVector(Angle course, T vertical, T horizontal)
        {
            Course = course;
            Vertical = vertical;
            Horizontal = horizontal;
        }

        public Angle Course { get; }
        public T Vertical { get; }
        public T Horizontal { get; }
    }
}