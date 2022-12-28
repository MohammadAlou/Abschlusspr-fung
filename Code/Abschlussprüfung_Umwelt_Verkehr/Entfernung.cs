using static System.Math;

namespace Abschlussprüfung_Umwelt_Verkehr
{
    static class Entfernung
    {
        public static double Berechnen(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            latitude1 = latitude1 / 180 * PI;
            longitude1 = longitude1 / 180 * PI;

            latitude2 = latitude2 / 180 * PI;
            longitude2 = longitude2 / 180 * PI;

            double erdeRadius = 6378.388;
            return erdeRadius * Acos(Sin(latitude1) * Sin(latitude2) + Cos(latitude1) * Cos(latitude2) * Cos(longitude2 - longitude1));
        }
    }
}
