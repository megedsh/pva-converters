using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.Aeronautical
{
    public abstract class AeronauticalVector<T>
    {
        public AeronauticalVector(Angle course, T vertic, T horiz)
        {
            Course = course;
            Vertic = vertic;
            Horiz = horiz;
        }

        public Angle Course { get; }
        public T Vertic { get; }
        public T Horiz { get; }
    }
}