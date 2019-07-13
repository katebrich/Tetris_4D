using System;
using TetrisControlProject.Interfaces;

namespace TetrisControlProject
{
    /// <summary>
    /// Provides score for different situations.
    /// The bigger the multiplier, the bigger the score.
    /// </summary>
    [Serializable]
    class BasicScoreProvider : IScoreProvider
    {
        //Score values, can be adjusted.
        private int multiplier = 1;
        private int pieceScore = 20;
        private int squareScore = 50;
        private int multipleSquaresMultiplier = 2; //more squares at once means higher score
        private int PieceThroughPenalty = -100;

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="multiplier">Score values are multiplied by this value.</param>
        public BasicScoreProvider(int multiplier = 1)
        {
            this.multiplier = multiplier;
        }

        public BasicScoreProvider() { }

        /// <summary>
        /// Gets score for placing this piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public int GetScore(IPiece piece)
        {
            return (multiplier * pieceScore);
        }

        /// <summary>
        /// Gets score for this number of destroyed squares (at once)
        /// </summary>
        /// <param name="squaresDestroyed"></param>
        /// <returns></returns>
        public int GetScore(int squaresDestroyed)
        {
            return (multiplier * squareScore * (int)Math.Pow(multipleSquaresMultiplier, squaresDestroyed - 1)); //1 square...base*1; 2 square....base*2; 3 square....base*4 etc.
        }

        /// <summary>
        /// Gets minus score for letting the piece go through the game area.
        /// </summary>
        /// <returns></returns>
        public int GetScorePieceThrough()
        {
            return (multiplier * PieceThroughPenalty);
        }
    }
}
