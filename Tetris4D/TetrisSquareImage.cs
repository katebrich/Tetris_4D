using System;
using System.Drawing;
using TetrisControlProject.Helper;

namespace Tetris4D
{
    static class TetrisSquareImage
    {
        /// <summary>
        /// Returns bitmap with given proportions with tetris square drawn in given color. 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="squareHeight"></param>
        /// <param name="squareWidth"></param>
        /// <returns></returns>
        public static Bitmap GetTetrisSquareImage(Color color, int squareHeight, int squareWidth)
        {
            float[,] coefficients = colorRect(1, squareWidth, squareHeight);
            Bitmap bitmap = new Bitmap(squareWidth, squareHeight);

            for (int x = 0; x < coefficients.GetLength(0); x++)
            {
                for (int y = 0; y < coefficients.GetLength(1); y++)
                {
                    Color c2 = Color.FromArgb(color.A, (int)(Math.Min(255, color.R * coefficients[x, y])), (int)(Math.Min(255, color.G * coefficients[x, y])), (int)(Math.Min(255, color.B * coefficients[x, y])));

                    bitmap.SetPixel(x, y, c2);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Computes coefficients for multiplying the RGB values to make color lighter or darker on corresponding pixels.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static float[,] colorRect(int color, int width, int height)
        {
            //coefficients, can be adjusted
            float colorTop = 2.1f * color;
            float colorLeft = 0.8f * color;
            float colorRight = 1.5f * color;
            float colorDown = 0.6f * color;
            float innerRectSizeRatio = 0.55f;

            float[,] coefficients = new float[width, height];

            float offsetX = ((1 - innerRectSizeRatio) / 2);
            float offsetY = ((1 - innerRectSizeRatio) / 2);

            //inner rectangle with the same color
            for (int x = (int)(width * offsetX); x < width * (1 - offsetX); x++)
            {
                for (int y = (int)(height * offsetY); y < height * (1 - offsetY); y++)
                {
                    coefficients[x, y] = color;
                }
            }

            //top and down parts
            for (int y = 0; y < height * offsetY; y++)
            {
                for (int x = y; x < width - y; x++)
                {
                    coefficients[x, y] = colorDown;
                    coefficients[x, height - y - 1] = colorTop;
                }
            }

            //left and right parts
            for (int x = 0; x < width * offsetX; x++)
            {
                for (int y = x; y < height - x; y++)
                {
                    coefficients[x, y] = colorLeft;
                    coefficients[width - x - 1, y] = colorRight;
                }
            }
            //ugly fix, nechtelo se mi prepocitavat ty indexy...snad to dodelam na konci...
            coefficients = coefficients.RotateMatrixCounterClockwise();
            coefficients = coefficients.RotateMatrixCounterClockwise();

            return coefficients;

        }
    }
}
