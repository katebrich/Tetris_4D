using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisControlProject.Helper
{
    public static class ColorMethods
    {
        /// <summary>
        /// Blend given colors by creating color with average HSB values.
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        public static Color BlendHSB(List<Color> colors)
        {
            if (colors.Count == 0)
                return default(Color);

            //count average hsb
            float sumH = 0;
            float sumS = 0;
            float sumB = 0;
            int sumA = 0;
            foreach (var color in colors)
            {
                sumH += color.GetHue();
                sumS += color.GetSaturation();
                sumB += color.GetBrightness();
                sumA += color.A;
            }

            float h = sumH / colors.Count;
            float s = sumS / colors.Count;
            float b = sumB / colors.Count;
            int a = (int)(sumA / colors.Count);
            //TODO
            return FromAhsb(a, h, s, b);
        }

        /// <summary>
        /// Blend given colors by creating color with average RGB values.
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        public static Color Blend(List<Color> colors)
        {
            //count average rgb
            int sumR = 0;
            int sumG = 0;
            int sumB = 0;
            foreach (var color in colors)
            {
                sumR += color.R;
                sumG += color.G;
                sumB += color.B;
            }

            byte r = (byte)(sumR / colors.Count);
            byte g = (byte)(sumG / colors.Count);
            byte b = (byte)(sumB / colors.Count);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Creates a Color from alpha, hue, saturation and brightness.
        /// </summary>
        /// <param name="alpha">The alpha channel value.</param>
        /// <param name="hue">The hue value.</param>
        /// <param name="saturation">The saturation value.</param>
        /// <param name="brightness">The brightness value.</param>
        /// <returns>A Color with the given values.</returns>
        public static Color FromAhsb(int alpha, float hue, float saturation, float brightness)
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
                                    Convert.ToInt32(brightness * 255),
                                    Convert.ToInt32(brightness * 255),
                                    Convert.ToInt32(brightness * 255));
            }

            float fMax, fMid, fMin;
            int iSextant, iMax, iMid, iMin;

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

            iMax = Convert.ToInt32(fMax * 255);
            iMid = Convert.ToInt32(fMid * 255);
            iMin = Convert.ToInt32(fMin * 255);

            switch (iSextant)
            {
                case 1:
                    return Color.FromArgb(alpha, iMid, iMax, iMin);
                case 2:
                    return Color.FromArgb(alpha, iMin, iMax, iMid);
                case 3:
                    return Color.FromArgb(alpha, iMin, iMid, iMax);
                case 4:
                    return Color.FromArgb(alpha, iMid, iMin, iMax);
                case 5:
                    return Color.FromArgb(alpha, iMax, iMin, iMid);
                default:
                    return Color.FromArgb(alpha, iMax, iMid, iMin);
            }
        }
    }
}
