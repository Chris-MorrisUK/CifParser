using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Windows.Media;
using VDS.RDF;
namespace ontovis
{
    public static class Util
    {
        public static bool AllUpper(this string toCheck)
        {
            foreach (char c in toCheck)
            {
                if (!char.IsUpper(c))
                    return false;
            }
            return true;
        }

        public static void AddNamesSpaces(IGraph targetGraph)
        {
            targetGraph.NamespaceMap.AddNamespace("prov", UriFactory.Create(ScheduleVis.Properties.Settings.Default.ProvNS));
            targetGraph.NamespaceMap.AddNamespace("u", UriFactory.Create(ScheduleVis.Properties.Settings.Default.racoonUpper));
            targetGraph.NamespaceMap.AddNamespace("is", UriFactory.Create(ScheduleVis.Properties.Settings.Default.racoonIS));
            targetGraph.NamespaceMap.AddNamespace("rdfs", UriFactory.Create(ScheduleVis.Properties.Settings.Default.RDFS));
            targetGraph.NamespaceMap.AddNamespace("wgspos", UriFactory.Create(ScheduleVis.Properties.Settings.Default.Geo));
            targetGraph.NamespaceMap.AddNamespace("spatialrelations", UriFactory.Create(ScheduleVis.Properties.Settings.Default.Geo));
            targetGraph.NamespaceMap.AddNamespace("tt", UriFactory.Create(ScheduleVis.Properties.Settings.Default.TimeTableNameSpace));
        }

        /// <summary>
        /// Creates a Color from alpha, hue, saturation and brightness.
        /// </summary>
        /// <param name="alpha">The alpha channel value.</param>
        /// <param name="hue">The hue value.</param>
        /// <param name="saturation">The saturation value.</param>
        /// <param name="brightness">The brightness value.</param>
        /// <returns>A Color with the given values.</returns>
        public static Color ColourFromAhsb(byte alpha, float hue, float saturation, float brightness)
        {
            if (0 > alpha
                || 255 < alpha)
            {
                throw new ArgumentOutOfRangeException(
                    "alpha",
                    alpha,
                    "Value must be within a range of 0 - 255.");
            }

            if (0f > hue
                || 360f < hue)
            {
                throw new ArgumentOutOfRangeException(
                    "hue",
                    hue,
                    "Value must be within a range of 0 - 360.");
            }

            if (0f > saturation
                || 1f < saturation)
            {
                throw new ArgumentOutOfRangeException(
                    "saturation",
                    saturation,
                    "Value must be within a range of 0 - 1.");
            }

            if (0f > brightness
                || 1f < brightness)
            {
                throw new ArgumentOutOfRangeException(
                    "brightness",
                    brightness,
                    "Value must be within a range of 0 - 1.");
            }

            if (0 == saturation)
            {
                return Color.FromArgb(
                                    alpha,
                                    Convert.ToByte(brightness * 255),
                                    Convert.ToByte(brightness * 255),
                                    Convert.ToByte(brightness * 255));
            }

            float fMax, fMid, fMin;
            int iSextant;
            byte bMax, bMid, bMin;

            if (0.5 < brightness)
            {
                fMax = brightness - (brightness * saturation) + saturation;
                fMin = brightness + (brightness * saturation) - saturation;
            }
            else
            {
                fMax = brightness + (brightness * saturation);
                fMin = brightness - (brightness * saturation);
            }

            iSextant = (int)Math.Floor(hue / 60f);
            if (300f <= hue)
            {
                hue -= 360f;
            }

            hue /= 60f;
            hue -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
            if (0 == iSextant % 2)
            {
                fMid = (hue * (fMax - fMin)) + fMin;
            }
            else
            {
                fMid = fMin - (hue * (fMax - fMin));
            }

            bMax = Convert.ToByte(fMax * 255);
            bMid = Convert.ToByte(fMid * 255);
            bMin = Convert.ToByte(fMin * 255);

            switch (iSextant)
            {
                case 1:
                    return Color.FromArgb(alpha, bMid, bMax, bMin);
                case 2:
                    return Color.FromArgb(alpha, bMin, bMax, bMid);
                case 3:
                    return Color.FromArgb(alpha, bMin, bMid, bMax);
                case 4:
                    return Color.FromArgb(alpha, bMid, bMin, bMax);
                case 5:
                    return Color.FromArgb(alpha, bMax, bMin, bMid);
                default:
                    return Color.FromArgb(alpha, bMax, bMid, bMin);
            }
        }
    }
}
