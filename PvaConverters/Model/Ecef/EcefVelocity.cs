using System;
using System.Collections.Generic;
using System.Text;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.Ecef
{
    public class EcefVelocity : EcefBase<Velocity>
    {

        public EcefVelocity(double xMetersPerSec, double yMetersPerSec, double zMetersPerSec) : this(Velocity.FromMetersPerSecond(xMetersPerSec), Velocity.FromMetersPerSecond(yMetersPerSec), Velocity.FromMetersPerSecond(zMetersPerSec))
        {
        }

        public EcefVelocity(Velocity x, Velocity y, Velocity z) : base(x.MetersPerSecond, y.MetersPerSecond, z.MetersPerSecond)
        {
            XAxis = x;
            YAxis = y;
            ZAxis = z;
        }
    }
}
