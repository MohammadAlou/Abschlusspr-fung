namespace Abschlussprüfung_Umwelt_Verkehr
{
    internal class Flug
    {
        public Location Location1 { get; }
        public Location Location2 { get; }
        public int Stops { get; }
        public bool IstInlandsflug { get; }

        public Flug(Location location1, Location location2, int stops, bool istInlandsflug)
        {
            Location1 = location1;
            Location2 = location2;
            Stops = stops;
            IstInlandsflug = istInlandsflug;
        }
    }
}
