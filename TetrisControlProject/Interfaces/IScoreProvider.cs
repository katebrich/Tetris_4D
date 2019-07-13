

namespace TetrisControlProject.Interfaces
{
    /// <summary>
    /// Gets scores for different situations.
    /// </summary>
    internal interface IScoreProvider
    { 
        /// <summary>
        /// Gets score for placing the piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        int GetScore(IPiece piece);

        /// <summary>
        /// Gets score for destroyed squares.
        /// </summary>
        /// <param name="squaresDestroyed">How many squares destroyed.</param>
        /// <returns></returns>
        int GetScore(int squaresDestroyed);

        /// <summary>
        /// Gets score (it should be negative) for not placing the piece and letting it go through.
        /// </summary>
        /// <returns></returns>
        int GetScorePieceThrough();
    }
}
