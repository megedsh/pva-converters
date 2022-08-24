namespace PvaConverters.Model.LocalTangentPlane
{
    public interface ILocalTangentPlane
    {
        double East { get; }
        double West { get; }
        double North { get; }
        double South { get; }
        double Up { get; }
        double Down { get; }
    }
}