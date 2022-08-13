using PvaConverters.Model;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;

namespace PvaConverters.Converters
{
    public interface IPositionConverter
    {
        GeoPosition LtpToGeo(LtpPosition ltpPosition, GeoPosition origin, Datum datum = null);
        EcefPosition LtpToEcef(LtpPosition ltpPosition, GeoPosition origin, Datum datum = null);
        AzimuthElevationRange LtpToAer(LtpPosition ltpPosition);
        EcefPosition GeoToEcef(GeoPosition geo, Datum datum = null);
        EcefPosition GeoToEcef(double latRad, double lonRad, double altMeters, Datum datum = null);
        LtpPosition GeoToLtp(GeoPosition target, GeoPosition origin, Datum datum = null);
        AzimuthElevationRange GeoToAer(GeoPosition origin, GeoPosition target, Datum datum = null);
        LtpPosition EcefToLtp(EcefPosition ecefPosition, GeoPosition origin, Datum datum = null);
        GeoPosition EcefToGeo(EcefPosition ecef, Datum datum = null);
        GeoPosition EcefToGeo(double xMeters, double yMeters, double zMeters, Datum datum = null);
        AzimuthElevationRange EcefToAer(EcefPosition target, GeoPosition origin, Datum datum = null);
        EcefPosition AerToEcef(GeoPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null);
        GeoPosition AerToGeo(GeoPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null);
        LtpPosition AerToLtp(GeoPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null);
    }
}