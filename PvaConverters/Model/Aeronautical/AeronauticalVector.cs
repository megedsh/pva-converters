﻿namespace PvaConverters.Model.Aeronautical
{
    public interface IAeronauticalVector
    {
        double Course { get; }
        double GetVertical();
        double GetHorizontal();
    }
}