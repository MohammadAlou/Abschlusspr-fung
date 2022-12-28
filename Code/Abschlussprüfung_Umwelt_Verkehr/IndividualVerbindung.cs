using System;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    class IndividualVerbindung : IVerbindung
    {
        private readonly Location _location1;
        private readonly Location _location2;
        public double Distanz => Parameter.EntfernungFaktorIndividual * Entfernung.Berechnen(_location1.Latitude, _location1.Longitude, _location2.Latitude, _location2.Longitude);

        public double KMH
        {
            get
            {
                double kmh = 0;

                double distanz = Distanz;
                double d = distanz;
                if (distanz > 0) // gehen
                {
                    double distanzGehen = Math.Min(Parameter.MaximalEntfernungFussgaenger, distanz); // maximal 1km gehen
                    kmh += Parameter.KMHFussgaenger * distanzGehen;

                    distanz -= distanzGehen;
                }

                if (distanz > 0) // Stadtverkehr
                {
                    double distanzStadt = Math.Min(Parameter.MaximalEntfernungStadtVerkehr, distanz); // maximal 10km in stadt
                    kmh += Parameter.KMHStadtVerkehr * distanzStadt;

                    distanz -= distanzStadt;
                }

                // rest mit Autobahn
                kmh += Parameter.KMHAutobahn * distanz;
                return kmh / d;
            }
        }
        public TimeSpan Dauer
        {
            get
            {
                double dauer = 0;
                double distanz = Distanz;
                // gehen = 0co2
                if (distanz > 0)
                {
                    double distanzGehen = Math.Min(Parameter.MaximalEntfernungFussgaenger, distanz); // maximal 1km gehen
                    dauer += distanzGehen / Parameter.KMHFussgaenger;
                    distanz -= distanzGehen;
                }

                if (distanz > 0) // Stadtverkehr
                {
                    double distanzStadt = Math.Min(Parameter.MaximalEntfernungStadtVerkehr, distanz); // maximal 10km in stadt
                    dauer += distanzStadt / Parameter.KMHStadtVerkehr;

                    distanz -= distanzStadt;
                }
                // rest mit Autobahn
                dauer += distanz / Parameter.KMHAutobahn;

                return TimeSpan.FromHours(dauer);
            }
        }
        public double CO2
        {
            get
            {
                double co2 = 0;
                double distanz = Distanz;
                // gehen = 0co2
                if (distanz > 0)
                {
                    double distanzGehen = Math.Min(Parameter.MaximalEntfernungFussgaenger, distanz); // maximal 1km gehen
                    co2 += Parameter.CO2Fussgaenger * distanzGehen;
                    distanz -= distanzGehen;
                }

                if (distanz > 0) // Stadtverkehr
                {
                    double distanzStadt = Math.Min(Parameter.MaximalEntfernungStadtVerkehr, distanz); // maximal 10km in stadt
                    co2 += Parameter.CO2StadtVerkehr * distanzStadt;

                    distanz -= distanzStadt;
                }
                // rest mit Autobahn
                co2 += Parameter.CO2Autobahn * distanz;

                return co2;
            }
        }
        public IndividualVerbindung(Location location1, Location location2)
        {
            _location1 = location1;
            _location2 = location2;
        }
    }
}
