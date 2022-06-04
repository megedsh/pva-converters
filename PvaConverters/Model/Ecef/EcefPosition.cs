using System;
using System.Collections.Generic;
using System.Text;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.Ecef
{
    public class EcefPosition : EcefBase<Distance>
    {
        public EcefPosition(double xMeters, double yMeters, double zMeters) : this(Distance.FromMeters(xMeters), Distance.FromMeters(yMeters), Distance.FromMeters(zMeters))
        {
        }

        public EcefPosition(Distance x, Distance y, Distance z) : base(x.Meters, y.Meters, z.Meters)
        {
            XAxis = x;
            YAxis = y;
            ZAxis = z;
        }
    }
}
