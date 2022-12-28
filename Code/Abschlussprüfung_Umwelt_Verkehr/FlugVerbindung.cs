using System;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    class FlugVerbindung : IVerbindung
    {
        private readonly Flug _flug;

        public double Distanz => Parameter.EntfernungFaktorFlug * Entfernung.Berechnen(_flug.Location1.Latitude, _flug.Location1.Longitude, _flug.Location2.Latitude, _flug.Location2.Longitude);

        public double KMH => Parameter.KMHFlug;

        public TimeSpan Dauer => TimeSpan.FromHours(Distanz / KMH + Wartezeit * (1 + _flug.Stops));
        private double Wartezeit => _flug.IstInlandsflug ? Parameter.WartezeitInland : Parameter.WartezeitAusland;

        public double CO2 => Parameter.CO2Flug * Distanz;


        public FlugVerbindung(Flug flug)
        {
            _flug = flug;
        }
    }
}
