namespace PvaConverters.Model.LocalTangentPlane
{
    public interface ILocalTangentPlane<T,NedT,EnuT>: IVector3d
    {
        T East { get; }
        T West { get; }
        T North { get; }
        T South { get; }
        T Up { get; }
        T Down { get; }

        NedT AsNed();
        EnuT AsEnu();
    }
}