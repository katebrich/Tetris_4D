using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisControlProject.Enums;
using System.Drawing;

namespace TetrisControlProject.Interfaces
{
    /// <summary>
    /// Manipulates with given pieces in TetrisGrid.
    /// Can add new pieces.
    /// Informs about changed views and placed pieces.
    /// </summary>
    internal interface ITetrisBox
    {
        /// <summary>
        /// The grid where the box moves and adds the pieces.
        /// </summary>
        ITetrisGrid TetrisGrid { get; }

        /// <summary>
        /// Informs about changed view.
        /// </summary>
        event EventHandler<_EventArgs.GridChangedEventArgs> ViewChanged;
        
        /// <summary>
        /// Informs that current piece was placed.
        /// </summary>
        event EventHandler<_EventArgs.PiecePlacedEventArgs> PiecePlaced;

        /// <summary>
        /// Moves the current piece one step in given direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        bool MoveInDirection(DirectionEnum direction);
        
        /// <summary>
        /// Moves the current piece one step in current direction.
        /// </summary>
        /// <returns></returns>
        bool Move();

        /// <summary>
        /// Moves the current piece in current direction as many times as possible (before it is placed).
        /// </summary>
        void DropPiece();

        /// <summary>
        /// Sets another piece and direction to the box.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="direction"></param>
        void AddNewPiece(IPiece piece, DirectionEnum direction);

        /// <summary>
        /// Gets info about tetris grid.
        /// </summary>
        /// <returns></returns>
        _EventArgs.GridChangedEventArgs GetGridChangedEventArgs();
    }
}
