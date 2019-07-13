using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisControlProject.Helper
{
    public static class _2DArrayExtension
    { 
        /// <summary>
        /// Extension. Rotates given 2D array counter clockwise by 90 degrees.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldMatrix"></param>
        /// <returns></returns>
        public static T[,] RotateMatrixCounterClockwise<T>(this T[,] oldMatrix)
        {
            T[,] newMatrix = new T[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }
    }
}
