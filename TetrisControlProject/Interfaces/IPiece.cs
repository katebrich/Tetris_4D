
using System.Drawing;

namespace TetrisControlProject.Interfaces
{
    /// <summary>
    /// Represents one Tetris piece.
    /// </summary>
    public interface IPiece
    {
        /// <summary>
        /// Width of Shape array.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Height of Shape array.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Represents shape of the piece by colors.
        /// If color is null, there is no color, free square.
        /// </summary>
        Color?[,] Shape { get; set; }

        /// <summary>
        /// Turns Shape array 90degrees counter clockwise.
        /// </summary>
        /// <param name="times">How many times to turn.</param>
        void Turn(int times);
    }
}
