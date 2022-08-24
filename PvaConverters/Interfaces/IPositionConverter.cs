using PvaConverters.Model;
using PvaConverters.Model.AzimuthElevation;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;

namespace PvaConverters.Interfaces
{
    public interface IPositionConverter
    {
        LlaPosition LtpToLla(LtpPosition ltpPosition, LlaPosition origin, Datum datum = null);
        EcefPosition LtpToEcef(LtpPosition ltpPosition, LlaPosition origin, Datum datum = null);
        AzimuthElevationRange LtpToAer(LtpPosition ltpPosition);
        EcefPosition LlaToEcef(LlaPosition lla, Datum datum = null);
        EcefPosition LlaToEcef(double latRad, double lonRad, double altMeters, Datum datum = null);
        LtpPosition LlaToLtp(LlaPosition target, LlaPosition origin, Datum datum = null);
        AzimuthElevationRange LlaToAer(LlaPosition origin, LlaPosition target, Datum datum = null);
        LtpPosition EcefToLtp(EcefPosition ecefPosition, LlaPosition origin, Datum datum = null);
        LlaPosition EcefToLla(EcefPosition ecef, Datum datum = null);
        LlaPosition EcefToLla(double xMeters, double yMeters, double zMeters, Datum datum = null);
        AzimuthElevationRange EcefToAer(EcefPosition target, LlaPosition origin, Datum datum = null);
        EcefPosition AerToEcef(LlaPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null);
        LlaPosition AerToLla(LlaPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null);
        LtpPosition AerToLtp(LlaPosition origin, AzimuthElevationRange azimuthElevationRange, Datum datum = null);
    }
}