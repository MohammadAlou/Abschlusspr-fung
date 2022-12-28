using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    internal class Datei
    {
        private enum EinleseTyp
        {
            Locations, FlightSchedule, FindBestConnections
        }
        public string Pfad { get; }


        public Datei(string pfad)
        {
            Pfad = pfad;
        }

        public Graph Einlesen(out Knoten start, out Knoten ziel)
        {
            start = null;
            ziel = null;
            Location startLocation = null;
            Location zielLocation = null;

            List<Location> locations = new List<Location>();
            List<Flug> fluge = new List<Flug>();
            string[] text = File.ReadAllLines(Pfad);
            EinleseTyp einleseType = EinleseTyp.Locations;

            foreach (var line in text)
            {
                if (line.StartsWith("#") || line.Length == 0)
                {
                    continue;
                }
                if (line == "Locations:")
                {
                    einleseType = EinleseTyp.Locations;
                    continue;
                    // Locations lesen
                }
                else if (line == "FlightSchedule:")
                {
                    einleseType = EinleseTyp.FlightSchedule;
                    continue;

                    // FlightSchedule: Lesen
                }
                else if (line == "FindBestConnections:")
                {
                    einleseType = EinleseTyp.FindBestConnections;
                    continue;
                    // FindBestConnections: lesen
                }

                if (einleseType == EinleseTyp.Locations)
                {
                    string[] lineSplit = line.Split(';');
                    string id = lineSplit[0];
                    string kindOfLocation = lineSplit[1].Trim();
                    double latitzde = double.Parse(lineSplit[2].Replace('.', ','));
                    double longitude = double.Parse(lineSplit[3].Replace('.', ','));
                    int continent = int.Parse(lineSplit[4]);
                    string name = lineSplit[5].Trim();

                    LocationType art = LocationType.Location;
                    if (kindOfLocation == "PublicTransportStop")
                    {
                        art = LocationType.PublicTransportStop;
                    }
                    else if (kindOfLocation == "Airport")
                    {
                        art = LocationType.Airport;
                    }

                    else if (kindOfLocation == "Location")
                    {
                        art = LocationType.Location;
                    }

                    Location location = new Location(id, art, latitzde, longitude, continent, name);
                    locations.Add(location);

                }
                else if (einleseType == EinleseTyp.FlightSchedule)
                {
                    string[] lineSplit = line.Split(';');
                    string id1 = lineSplit[0].Trim();
                    string id2 = lineSplit[1].Trim();
                    int stops = 0;
                    bool domesticFlifht = false;

                    if (lineSplit.Length > 2 && !string.IsNullOrWhiteSpace(lineSplit[2]))
                    {
                        stops = int.Parse(lineSplit[2]);
                    }
                    if (lineSplit.Length > 3 && !string.IsNullOrWhiteSpace(lineSplit[3]))
                    {
                        domesticFlifht = bool.Parse(lineSplit[3]);
                    }



                    Location location1 = locations.First(l => l.Id == id1);
                    Location location2 = locations.First(l => l.Id == id2);
                    Flug flug = new Flug(location1, location2, stops, domesticFlifht);
                    fluge.Add(flug);

                }
                else if (einleseType == EinleseTyp.FindBestConnections)
                {
                    string[] lineSplit = line.Split(';');
                    string id1 = lineSplit[0].Trim();
                    string id2 = lineSplit[1].Trim();
                    startLocation = locations.First(l => l.Id == id1);
                    zielLocation = locations.First(l => l.Id == id2);
                }
            }
            Dictionary<Location, Knoten> knoten = new Dictionary<Location, Knoten>();
            foreach (var loction in locations)
            {
                knoten.Add(loction, new Knoten(loction));
            }
            start = knoten[startLocation];
            ziel = knoten[zielLocation];
            foreach (var flug in fluge)
            {
                Knoten knoten1 = knoten[flug.Location1];
                Knoten knoten2 = knoten[flug.Location2];
                FlugVerbindung flugVerbindung = new FlugVerbindung(flug);
                Kante k1 = new Kante(knoten1, knoten2, flugVerbindung);
                Kante k2 = new Kante(knoten2, knoten1, flugVerbindung);
                knoten1.Kanten.Add(k1);
                knoten2.Kanten.Add(k2);
            }
            foreach (var knoten1 in knoten.Values)
            {
                foreach (var knoten2 in knoten.Values)
                {
                    if (knoten1 != knoten2)
                    {
                        IndividualVerbindung individualVerbindung = new IndividualVerbindung(knoten1.Location, knoten2.Location);
                        if (individualVerbindung.Distanz <= Parameter.MaximalEntfernungIndividual
                            && knoten1.Location.Kontinent == knoten2.Location.Kontinent)
                        {
                            Kante k1 = new Kante(knoten1, knoten2, individualVerbindung);
                            knoten1.Kanten.Add(k1);
                        }
                        OeffentlichenVerbindung oeffentlichenVerbindung = new OeffentlichenVerbindung(knoten1.Location, knoten2.Location);
                        if (knoten1.Location.Art != LocationType.Location && knoten2.Location.Art != LocationType.Location
                            && knoten1.Location.Kontinent == knoten2.Location.Kontinent)
                        {
                            Kante k1 = new Kante(knoten1, knoten2, oeffentlichenVerbindung);
                            knoten1.Kanten.Add(k1);
                        }
                    }
                }
            }
            return new Graph(knoten.Values.ToArray());
        }
    }
}