namespace Abschlussprüfung_Umwelt_Verkehr
{
    internal static class Parameter
    {
        public const double CO2Fussgaenger = 0;
        public const double CO2StadtVerkehr = 0.189;
        public const double CO2Autobahn = 0.189;
        public const double CO2Nahverkehr = 0.055;
        public const double CO2Fernverkehr = 0.055;
        public const double CO2Flug = 0.2113;

        public const double KMHFussgaenger = 4;
        public const double KMHStadtVerkehr = 30;
        public const double KMHAutobahn = 100;
        public const double KMHNahverkehr = 30;
        public const double KMHFernverkehr = 100;
        public const double KMHFlug = 900;

        public const double EntfernungFaktorIndividual = 1.20; // +20%
        public const double EntfernungFaktorOeffentlich = 1.10; // +10%
        public const double EntfernungFaktorFlug = 1.02; // +2%

        public const double WartezeitInland = 2.0;
        public const double WartezeitAusland = 3.0;

        public const double MaximalEntfernungFussgaenger = 1;
        public const double MaximalEntfernungStadtVerkehr = 10;
        public const double MaximalEntfernungAutobahn = 2000;
        public const double MaximalEntfernungIndividual = MaximalEntfernungFussgaenger + MaximalEntfernungStadtVerkehr + MaximalEntfernungAutobahn;

        public const double MaximalEntfernungNahverkehr = 25;
    }
}