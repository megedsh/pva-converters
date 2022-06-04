using System;
using System.Collections.Generic;
using System.Text;

namespace PvaConverters.Model.Ecef
{
    public class EcefBase<T> : Vector3d
    {
        public T XAxis { get; protected set;}
        public T YAxis { get; protected set;}
        public T ZAxis { get; protected set; }

        public EcefBase(double x, double y, double z) : base(x, y, z)
        {
        }
    }
}
