namespace NavigationToolkit.Model.AzimuthElevation
{
    public interface IAzimuthElevation
    {
        double Azimuth { get; }
        double Elevation { get; }
        double GetScalar();
    }
}