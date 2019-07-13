using System;
using TetrisControlProject.Interfaces;
using System.Drawing;
using TetrisControlProject.Enums;
using TetrisControlProject._EventArgs;

namespace TetrisControlProject
{
    /// <summary>
    /// Manipulates with given pieces in TetrisGrid.
    /// Can add new pieces.
    /// Informs about changed views and placed pieces.
    /// </summary>
    [Serializable]
    class Tetris4DBox : ITetrisBox
    {
        //A piece currently being processed by the box.
        private IPiece currentPiece;
        private DirectionEnum currentDirection;
        private Point currentPieceLocation;

        /// <summary>
        /// The grid where the box moves and adds the pieces.
        /// </summary>
        public ITetrisGrid TetrisGrid { get; }

        /// <summary>
        /// Informs about changed view.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<GridChangedEventArgs> ViewChanged;

        /// <summary>
        /// Informs that current piece was placed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<PiecePlacedEventArgs> PiecePlaced;

        /// <summary>
        /// A constructor. 
        /// </summary>
        public Tetris4DBox(ITetrisGrid tetrisGrid)
        {
            TetrisGrid = tetrisGrid;
        }

        public Tetris4DBox()
        {

        }

        /// <summary>
        /// Moves the current piece one step in given direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool MoveInDirection(DirectionEnum direction)
        {
            bool placed = false;

            if (isOppositeDirection(direction, currentDirection))
            {               
                currentPiece.Turn(1);
                if (!TetrisGrid.CanBePlaced(currentPiece, currentPieceLocation, currentDirection))
                {
                    currentPiece.Turn(3);
                    return false;
                }
            }
            else
            {
                Point point = currentPieceLocation;
                bool shifted = TetrisGrid.ShiftPiece(currentPiece, ref point, direction, currentDirection);
                currentPieceLocation = point;

                if (!shifted)
                {
                    if (TetrisGrid.WentThrough(currentPiece, currentPieceLocation, currentDirection))
                    {
                        //dont add piece to grid
                        PiecePlaced(this, new PiecePlacedEventArgs(currentPiece, false, true, 0));
                        placed = true;
                    }
                    else if (direction == currentDirection && 
                        TetrisGrid.IsStuckAtStart(currentPiece, currentPieceLocation, currentDirection))
                    {
                        //game over
                        PiecePlaced(this, new PiecePlacedEventArgs(currentPiece, true, false, 0));
                        return true;
                    }
                    else if (direction == currentDirection) //normalne umistena kosticka
                    {
                        TetrisGrid.AddPieceToGrid(currentPiece, currentPieceLocation);
                        int rowsDestroyed = TetrisGrid.DestroyFilledSquares();
                        PiecePlaced(this, new PiecePlacedEventArgs(currentPiece, false, false, rowsDestroyed));
                        placed = true;
                    }
                }
               
            }

            ViewChanged?.Invoke(this, GetGridChangedEventArgs());

            return placed;
        }

        /// <summary>
        /// Moves the current piece one step in current direction.
        /// </summary>
        /// <returns></returns>
        public bool Move()
        {
            return MoveInDirection(currentDirection);
        }

        /// <summary>
        /// Moves the current piece in current direction as many times as possible (before it is placed).
        /// </summary>
        public void DropPiece()
        {
            bool placed = false;
            while (!placed)
            {
                placed = Move();
            }
        }

        /// <summary>
        /// Adds another piece and direction to the box.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="direction"></param>
        public void AddNewPiece(IPiece piece, DirectionEnum direction)
        {
            currentPiece = piece;
            currentDirection = direction;
            currentPieceLocation = TetrisGrid.GetStartingLocation(piece, direction);
            ViewChanged?.Invoke(this, new GridChangedEventArgs(
                    GetGridToDraw(), TetrisGrid.BorderSize, TetrisGrid.BackgroundColor,
                    TetrisGrid.EdgesColor, TetrisGrid.BordersColor)
                    );
        }

        /// <summary>
        /// Gets info about tetris grid.
        /// </summary>
        /// <returns></returns>
        public GridChangedEventArgs GetGridChangedEventArgs()
        {
            return new GridChangedEventArgs(
                GetGridToDraw(), TetrisGrid.BorderSize, TetrisGrid.BackgroundColor,
                TetrisGrid.EdgesColor, TetrisGrid.BordersColor);
        }

        /// <summary>
        /// Decides whether two given directions are the opposite ones.
        /// </summary>
        /// <param name="direction1"></param>
        /// <param name="direction2"></param>
        /// <returns></returns>
        private bool isOppositeDirection(DirectionEnum direction1, DirectionEnum direction2)
        {
            return (
                (direction1 == DirectionEnum.Down && direction2 == DirectionEnum.Up) ||
                (direction2 == DirectionEnum.Down && direction1 == DirectionEnum.Up) ||
                (direction1 == DirectionEnum.Right && direction2 == DirectionEnum.Left) ||
                (direction2 == DirectionEnum.Right && direction1 == DirectionEnum.Left)
                );

        }

        /// <summary>
        /// Gets grid with current piece in it.
        /// </summary>
        /// <returns></returns>
        private Color[,] GetGridToDraw()
        {
            return TetrisGrid.GetGridWithPiece(currentPiece, currentPieceLocation);
        }
    }
}
