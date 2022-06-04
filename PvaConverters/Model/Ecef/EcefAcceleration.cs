using System;
using System.Collections.Generic;
using System.Text;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.Ecef
{
    public class EcefAcceleration : EcefBase<Acceleration>
    {

        public EcefAcceleration(double xMetersPerSquareSec, double yMetersPerSquareSec, double zMetersPerSquareSec) : this(Acceleration.FromMetersPerSquareSecond(xMetersPerSquareSec), Acceleration.FromMetersPerSquareSecond(yMetersPerSquareSec), Acceleration.FromMetersPerSquareSecond(zMetersPerSquareSec))
        {
        }

        public EcefAcceleration(Acceleration x, Acceleration y, Acceleration z) : base(x.MetersPerSquareSecond, y.MetersPerSquareSecond, z.MetersPerSquareSecond)
        {
            XAxis = x;
            YAxis = y;
            ZAxis = z;
        }
    }
}
