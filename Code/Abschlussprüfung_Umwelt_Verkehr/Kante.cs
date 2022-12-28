using System.Collections.Generic;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    internal class Kante
    {
        public Knoten Start { get; }
        public Knoten Ziel { get; }

        public IVerbindung Verbindung { get; }

        public Kante(Knoten start, Knoten ziel, IVerbindung verbindung)
        {
            Start = start;
            Ziel = ziel;
            Verbindung = verbindung;
        }
    }
}