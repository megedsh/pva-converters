using PvaConverters.Model;
using PvaConverters.Model.Aeronautical;
using PvaConverters.Model.Ecef;
using PvaConverters.Model.LocalTangentPlane;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Interfaces
{
    public interface ICartesianConverter<TLtp, TEcef, TScalar, TAeronautical>
        where TScalar : IScalar
        where TLtp : ILocalTangentPlane<TScalar>
        where TEcef : EcefBase<TScalar>
        where TAeronautical : AeronauticalVector<TScalar>
    {
        TEcef LtpToEcef(TLtp ltp, GeoPosition originGeoPosition);
        TEcef LtpToEcef(double north, double east, double down, GeoPosition originGeoPosition);
        TLtp EcefToLtp(TEcef ecef, GeoPosition originGeoPosition);
        TLtp EcefToLtp(double x, double y, double z, GeoPosition originGeoPosition);

        TAeronautical EcefToAeronautical(TEcef ecef, GeoPosition originGeoPosition);

        TEcef AeronauticalToEcef(TAeronautical an, GeoPosition originGeoPosition);

        TEcef AeronauticalToEcef(double courseRad, double vertical,
            double horizontal, GeoPosition originGeoPosition);

        TLtp AeronauticalToLtp(TAeronautical an);
    }

    public interface IAccelerationConverter : ICartesianConverter<LtpAcceleration, EcefAcceleration, Acceleration, AeronauticalAcceleration>
    {
    }


    public interface IVelocityConverter : ICartesianConverter<LtpVelocity, EcefVelocity, Velocity, AeronauticalVelocity>
    {
    }
}