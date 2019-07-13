using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisControlProject._EventArgs
{
    /// <summary>
    /// Info about placed piece in tetris box.
    /// </summary>
    public class PiecePlacedEventArgs : EventArgs
    {
        /// <summary>
        /// Tells whether the game ended by placing this piece.
        /// </summary>
        public bool GameOver { get; }

        /// <summary>
        /// Gets number of squares destroyed by this move.
        /// </summary>
        public int SquaresDestroyed { get; }

        /// <summary>
        /// Tells whether the piece went through the opposite side of the game area.
        /// </summary>
        public bool PieceWentThrough { get; }

        /// <summary>
        /// Gets the placed piece.
        /// </summary>
        public Interfaces.IPiece Piece { get; } 

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="gameOver"></param>
        /// <param name="pieceWentThrough"></param>
        /// <param name="rowsDestroyed"></param>
        public PiecePlacedEventArgs(Interfaces.IPiece piece, bool gameOver, bool pieceWentThrough, int rowsDestroyed)
        {
            this.GameOver = gameOver;
            this.SquaresDestroyed = rowsDestroyed;
            this.PieceWentThrough = pieceWentThrough;
            this.Piece = piece;
        }
    }
}
