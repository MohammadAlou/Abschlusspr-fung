namespace Abschlussprüfung_Umwelt_Verkehr
{
    internal class Location
    {
        public string Id { get; }
        public LocationType Art { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public int Kontinent { get; }
        public string Name { get; }

        public Location(string id, LocationType art, double latitude, double longitude, int kontinent, string name)
        {
            Id = id;
            Art = art;
            Latitude = latitude;
            Longitude = longitude;
            Kontinent = kontinent;
            Name = name;
        }
    }
}
