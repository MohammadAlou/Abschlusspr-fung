using System;
using System.Collections.Generic;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    class OeffentlichenVerbindung : IVerbindung
    {
        private Location _location1;
        private Location _location2;

        public double Distanz => Parameter.EntfernungFaktorOeffentlich * Entfernung.Berechnen(_location1.Latitude, _location1.Longitude, _location2.Latitude, _location2.Longitude);

        public double KMH
        {
            get
            {
                double kmh = 0;

                double distanz = Distanz;
                double d = distanz;
                if (distanz > 0) // Nah Verkher
                {
                    double distanzGehen = Math.Min(Parameter.MaximalEntfernungNahverkehr, distanz); // maximal 25km gehen
                    kmh += Parameter.KMHNahverkehr * distanzGehen;

                    distanz -= distanzGehen;
                }
                // Fern Verkehr
                kmh += Parameter.KMHFernverkehr * distanz;
                return kmh / d;
            }
        }
        public TimeSpan Dauer
        {
            get
            {
                double dauer = 0;

                double distanz = Distanz;
                if (distanz > 0) // Nah Verkher
                {
                    double distanzGehen = Math.Min(Parameter.MaximalEntfernungNahverkehr, distanz); // maximal 25km gehen
                    dauer += distanzGehen / Parameter.KMHNahverkehr;

                    distanz -= distanzGehen;
                }
                // Fern Verkehr
                dauer += distanz / Parameter.KMHFernverkehr;
                return TimeSpan.FromHours(dauer);
            }
        }

        public double CO2
        {
            get
            {
                double co2 = 0;

                double distanz = Distanz;
                if (distanz > 0) // Nah Verkher
                {
                    double distanzGehen = Math.Min(Parameter.MaximalEntfernungNahverkehr, distanz); // maximal 25km gehen
                    co2 += Parameter.CO2Nahverkehr * distanzGehen;

                    distanz -= distanzGehen;
                }
                // Fern Verkehr
                co2 += Parameter.CO2Fernverkehr * distanz;
                return co2;
            }
        }

        public OeffentlichenVerbindung(Location location1, Location locationl2)
        {
            _location1 = location1;
            _location2 = locationl2;
        }
    }
}
