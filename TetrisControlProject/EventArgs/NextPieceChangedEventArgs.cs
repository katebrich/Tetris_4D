using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisControlProject.Interfaces;
using TetrisControlProject.Enums;

namespace TetrisControlProject._EventArgs
{
    /// <summary>
    /// Event args for changed next piece event.
    /// Provides info about next piece and next direction.
    /// </summary>
    public class NextPieceChangedEventArgs : EventArgs
    {
        public IPiece NextPiece { get; }
        public DirectionEnum NextDirection { get; }

        public NextPieceChangedEventArgs(IPiece nextPiece, DirectionEnum nextDirection)
        {
            this.NextDirection = nextDirection;
            this.NextPiece = nextPiece;
        }
    }
}
