using System;

namespace _1.Code.Defines
{
    [Flags]
    public enum HUDTypes
    { 
        None = 0,
        Co2 = 1, 
        Forest = 2, 
        IceProtection = 4, 
        OceanHealth = 8,
    }
}