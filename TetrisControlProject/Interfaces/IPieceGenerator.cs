
namespace TetrisControlProject.Interfaces
{
    /// <summary>
    /// Generates next pieces and directions (from where it comes).
    /// </summary>
    internal interface IPieceGenerator
    {
        /// <summary>
        /// Generates next piece.
        /// </summary>
        /// <returns></returns>
        IPiece GetNextPiece();

        /// <summary>
        /// Generates next direction.
        /// </summary>
        /// <returns></returns>
        Enums.DirectionEnum GetNextDirection();
    }
}
