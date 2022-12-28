using System;
using System.Collections.Generic;
using System.Linq;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    internal class AlgoDaten
    {
        public Graph Graph { get; }

        public AlgoDaten(Graph graph)
        {
            Graph = graph;

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="start"></param>
        /// <param name="ziel"></param>
        /// <returns></returns>
        public Kante[] GetKuerzesterPfadCO2(Knoten start, Knoten ziel)
        {
            Dictionary<Knoten, Kante> vorgenga = new Dictionary<Knoten, Kante>();
            Dictionary<Knoten, double> abstand = new Dictionary<Knoten, double>();

            foreach (var knoten in Graph.Knoten)
            {
                vorgenga.Add(knoten, null);
                abstand.Add(knoten, double.MaxValue);
            }
            abstand[start] = 0;
            List<Knoten> queue = new List<Knoten>();
            queue.AddRange(Graph.Knoten);

            while (queue.Count > 0)
            {
                Knoten ersteKnoten = queue.OrderBy(k => abstand[k]).First();
                queue.Remove(ersteKnoten);

                foreach (var kante in ersteKnoten.Kanten)
                {
                    Knoten nachbar = kante.Ziel;
                    if (queue.Contains(nachbar))
                    {
                        double neueAbstand = abstand[ersteKnoten] + kante.Verbindung.CO2;
                        if (neueAbstand < abstand[nachbar])
                        {
                            abstand[nachbar] = neueAbstand;
                            vorgenga[nachbar] = kante;
                        }
                    }
                }
            }
            List<Kante> pfad = new List<Kante>();
            while (ziel != null)
            {
                Kante kante = vorgenga[ziel];
                if (kante == null)
                {
                    ziel = null;
                }
                else
                {
                    pfad.Add(kante);
                    ziel = kante.Start;
                }

                if (ziel == start)
                {
                    break;
                }
            }
            if (ziel == null)
            {
                // es gibt kein weg von Start zum Ziel
                return null;
            }
            else
            {
                Kante[] result = pfad.ToArray();
                Array.Reverse(result);
                return result;
            }
        }

        public Kante[] GetKuersterPfadDauer(Knoten start, Knoten ziel)
        {
            Dictionary<Knoten, Kante> vorgenga = new Dictionary<Knoten, Kante>();
            Dictionary<Knoten, TimeSpan> abstand = new Dictionary<Knoten, TimeSpan>();

            foreach (var knoten in Graph.Knoten)
            {
                vorgenga.Add(knoten, null);
                abstand.Add(knoten, TimeSpan.MaxValue);
            }
            abstand[start] = new TimeSpan(0);
            List<Knoten> queue = new List<Knoten>();
            queue.AddRange(Graph.Knoten);

            while (queue.Count > 0)
            {
                Knoten ersteKnoten = queue.OrderBy(k => abstand[k]).First();
                queue.Remove(ersteKnoten);

                foreach (var kante in ersteKnoten.Kanten)
                {
                    Knoten nachbar = kante.Ziel;
                    if (queue.Contains(nachbar))
                    {
                        TimeSpan neueAbstand = abstand[ersteKnoten] + kante.Verbindung.Dauer;
                        if (neueAbstand < abstand[nachbar])
                        {
                            abstand[nachbar] = neueAbstand;
                            vorgenga[nachbar] = kante;
                        }
                    }
                }
            }
            List<Kante> pfad = new List<Kante>();
            while (ziel != null)
            {
                Kante kante = vorgenga[ziel];
                if (kante == null)
                {
                    ziel = null;
                }
                else
                {
                    pfad.Add(kante);
                    ziel = kante.Start;
                }

                if (ziel == start)
                {
                    break;
                }
            }
            if (ziel == null)
            {
                // es gibt kein weg von Start zum Ziel
                return null;
            }
            else
            {
                Kante[] result = pfad.ToArray();
                Array.Reverse(result);
                return result;
            }
        }
    }
}