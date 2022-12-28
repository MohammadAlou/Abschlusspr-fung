using System.Collections.Generic;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    internal class Knoten
    {
        public Location Location { get; }
        public List<Kante> Kanten { get; } = new List<Kante>();

        public Knoten(Location location)
        {
            Location = location;
        }
    }
}