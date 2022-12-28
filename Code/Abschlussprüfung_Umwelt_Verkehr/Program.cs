using System.IO;
using System;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentPath = Directory.GetCurrentDirectory();
            currentPath = Environment.CurrentDirectory;
            string testFolderPath = currentPath + @"\Daten\";
            string testAusgabePath = currentPath + @"\Ausgabe\";
            Directory.CreateDirectory(testAusgabePath);
            //string projectDirectory = args[0];

            string[] filePaths = Directory.GetFiles(testFolderPath, "*.txt",
                                         SearchOption.AllDirectories);


            for (int i = 0; i < filePaths.Length; i++)
            {
                try
                {

                    string testflie = filePaths[i];
                    Datei datei = new Datei(testflie);

                    Graph graph = datei.Einlesen(out Knoten start, out Knoten ziel);
                    AlgoDaten algodaten = new AlgoDaten(graph);


                    var dauerPfad = algodaten.GetKuersterPfadDauer(start, ziel);
                    var dauer = new TimeSpan(0);

                    var co2Pfad = algodaten.GetKuerzesterPfadCO2(start, ziel);
                    double co2 = 0;


                    string fileName = testAusgabePath + Path.GetFileName(filePaths[i]);
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.WriteLine(dauerPfad[0].Start.Location.Name + "-->" + dauerPfad[dauerPfad.Length - 1].Ziel.Location.Name);
                        sw.WriteLine("------------");
                        sw.WriteLine("Strecke Luftline: " + Entfernung.Berechnen(start.Location.Latitude, start.Location.Longitude,
                            ziel.Location.Latitude, ziel.Location.Longitude));
                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.WriteLine("Schnellsete Verbindungen");
                        foreach (Kante kante in dauerPfad)
                        {
                            dauer += kante.Verbindung.Dauer;
                            co2 += kante.Verbindung.CO2;

                            sw.WriteLine(kante.Start.Location.Name + "-- " + kante.Verbindung.GetType().Name + "-- > " + kante.Ziel.Location.Name);
                        }
                        sw.WriteLine("Dauer: " + dauer + ", CO2-Emission: " + co2);

                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.WriteLine("Verbindungen mit der gringsten CO2-Emission:");
                        dauer = new TimeSpan(0);
                        co2 = 0;
                        foreach (Kante kante in co2Pfad)
                        {
                            dauer += kante.Verbindung.Dauer;
                            co2 += kante.Verbindung.CO2;
                            sw.WriteLine(kante.Start.Location.Name + "-- " + kante.Verbindung.GetType().Name + "-- > " + kante.Ziel.Location.Name);
                        }
                        sw.WriteLine("Dauer: " + dauer + ", CO2-Emission: " + co2);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(filePaths[i] + " Hat ein Problem");
                }
            }
        }
    }
}