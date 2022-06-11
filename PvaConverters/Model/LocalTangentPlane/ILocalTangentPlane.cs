using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.LocalTangentPlane
{
    public interface ILocalTangentPlane<T>
    where T: IScalar
    {
        T East { get; }
        T West { get; }
        T North { get; }
        T South { get; }
        T Up { get; }
        T Down { get; }
    }


    public interface ILocalTangentPlane<T,NedT,EnuT>: ILocalTangentPlane<T>, IVector3d
    where T: IScalar
    {
        NedT AsNed();
        EnuT AsEnu();
    }
}