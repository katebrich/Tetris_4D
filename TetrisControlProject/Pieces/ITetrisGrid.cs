using System.Drawing;

namespace TetrisControlProject.Interfaces
{
    /// <summary>
    /// Represents the grid with pieces.
    /// Computes the moves of pieces in all directions.
    /// Can save another piece into grid.
    /// Provides color array representing the state of pieces.
    /// </summary>
    internal interface ITetrisGrid
    {
        /// <summary>
        /// Size of the Grid array.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Size of outer borders where the pieces are displayed but the grid itself is not.
        /// </summary>
        int BorderSize { get; }

        /// <summary>
        /// Height of one (of 4) parts of tetris grid.
        /// </summary>
        int BlockHeight { get; }

        /// <summary>
        /// Width of middle square (and also width of parts of tetris grid).
        /// </summary>
        int BlockWidth { get; }

        /// <summary>
        /// Array of colors representing pieces in grid.
        /// </summary>
        Color[,] Grid { get; }

        /// <summary>
        /// Color representing background (place without any piece) in grid.
        /// </summary>
        Color BackgroundColor { get; }

        /// <summary>
        /// Color representing outer borders.
        /// </summary>
        Color BordersColor { get; }

        /// <summary>
        /// Color representing edges where piece cannot move or be placed.
        /// </summary>
        Color EdgesColor { get; }

        /// <summary>
        /// If there are any filled squares, makes them empty and moves the rest of pieces appropriately.
        /// </summary>
        /// <returns>Number of destroyed squares</returns>
        int DestroyFilledSquares();

        /// <summary>
        /// Returns the color array with given piece placed in given location. 
        /// Not add the piece into own grid.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        Color[,] GetGridWithPiece(IPiece piece, Point location);

        /// <summary>
        /// Adds the given piece into own grid (saves it)
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        void AddPieceToGrid(IPiece piece, Point location);

        /// <summary>
        /// Computes starting location in grid for piece coming in given direction.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        Point GetStartingLocation(IPiece piece, Enums.DirectionEnum direction);

        /// <summary>
        /// Initializes the color array. Fills background, edges and border colors.
        /// </summary>
        void InitShape();

        /// <summary>
        /// Computes if the piece can be places into given location in grid.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        bool CanBePlaced(IPiece piece, Point location, Enums.DirectionEnum direction);

        /// <summary>
        /// Moves the piece in grid in given direction. 
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <param name="currentDirection"></param>
        /// <returns></returns>
        bool ShiftPiece(IPiece piece, ref Point location, Enums.DirectionEnum direction, Enums.DirectionEnum currentDirection);

        /// <summary>
        /// Computes if given piece coming from given direction is out of borders.
        /// </summary>
        /// <param name="currentPiece"></param>
        /// <param name="currentPieceLocation"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        bool WentThrough(IPiece currentPiece, Point currentPieceLocation, Enums.DirectionEnum direction);

        /// <summary>
        /// Computes if the piece is in starting border and cannot move in direction (because there are other piece i.e.).
        /// </summary>
        /// <param name="currentPiece"></param>
        /// <param name="currentPieceLocation"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        bool IsStuckAtStart(IPiece currentPiece, Point currentPieceLocation, Enums.DirectionEnum direction);
    }
}
