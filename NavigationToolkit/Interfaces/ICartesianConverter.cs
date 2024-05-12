using NavigationToolkit.Model;
using NavigationToolkit.Model.Aeronautical;
using NavigationToolkit.Model.Ecef;
using NavigationToolkit.Model.LocalTangentPlane;

namespace NavigationToolkit.Interfaces
{
    public interface ICartesianConverter<TLtp, TEcef, TAeronautical>
        where TLtp : ILocalTangentPlane
        where TEcef : IEcef
        where TAeronautical : IAeronauticalVector
    {
        TEcef LtpToEcef(TLtp ltp, LlaPosition originLlaPosition);
        TEcef LtpToEcef(double north, double east, double down, LlaPosition originLlaPosition);
        TLtp EcefToLtp(TEcef ecef, LlaPosition originLlaPosition);
        TLtp EcefToLtp(double x, double y, double z, LlaPosition originLlaPosition);

        TAeronautical EcefToAeronautical(TEcef ecef, LlaPosition originLlaPosition);

        TEcef AeronauticalToEcef(TAeronautical an, LlaPosition originLlaPosition);

        TEcef AeronauticalToEcef(double courseRad, double vertical,
            double horizontal, LlaPosition originLlaPosition);

        TLtp AeronauticalToLtp(TAeronautical an);
    }

    public interface IAccelerationConverter : ICartesianConverter<LtpAcceleration, EcefAcceleration, AeronauticalAcceleration>
    {
    }


    public interface IVelocityConverter : ICartesianConverter<LtpVelocity, EcefVelocity, AeronauticalVelocity>
    {
    }
}