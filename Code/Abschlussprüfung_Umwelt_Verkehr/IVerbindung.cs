using System;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    interface IVerbindung
    {
        double Distanz { get; }
        double KMH { get; }
        TimeSpan Dauer { get; }
        double CO2 { get; }
    }
}
