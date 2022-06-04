using System;
using System.Collections.Generic;
using System.Text;
using PvaConverters.Model.Scalars;

namespace PvaConverters.Model.AzimuthElevation
{
    public abstract class AzimuthElevationBase<T> 
    {
        protected AzimuthElevationBase(Angle azimuth, Angle elevation, T scalar)
        {
            Azimuth = azimuth;
            Elevation = elevation;
            Scalar = scalar;
        }

        public Angle Azimuth { get; }
        public Angle Elevation { get; }
        public T Scalar { get; }
    }
}
