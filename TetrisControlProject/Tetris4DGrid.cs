using System;
using System.Collections.Generic;
using System.Drawing;
using TetrisControlProject.Interfaces;
using TetrisControlProject.Enums;
using TetrisControlProject.Helper;

namespace TetrisControlProject
{
    /// <summary>
    /// Represents the grid with pieces.
    /// Computes the moves of pieces in all 4 directions.
    /// Can destroy filled pieces and move the rest of pieces correspondingly.
    /// Provides color array for views.
    /// Can save another piece into grid.
    /// Provides color array representing the state of pieces.
    /// </summary>
    [Serializable]
    class Tetris4DGrid : ITetrisGrid
    {
        //array of all squares sorted from smallest (inner) to biggest
        private TetrisSquare[] allSquaresInGrid;

        /// <summary>
        /// Size of the Grid array.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Size of outer borders where the pieces are displayed but the grid itself is not.
        /// </summary>
        public int BorderSize { get; set; }

        /// <summary>
        /// Height of one (of 4) parts of tetris grid.
        /// </summary>
        public int BlockHeight { get; set; }

        /// <summary>
        /// Width of middle square (and also width of parts of tetris grid).
        /// </summary>
        public int BlockWidth { get; set; }

        /// <summary>
        /// Array of colors representing pieces in grid.
        /// </summary>
        public Color[,] Grid { get; set; }

        /// <summary>
        /// Color representing background (place without any piece) in grid.
        /// </summary>
        public Color BackgroundColor { get; private set; }

        /// <summary>
        /// Color representing edges where piece cannot move or be placed (places out of game area).
        /// </summary>
        public Color EdgesColor { get; private set; }

        /// <summary>
        /// Color representing outer borders.
        /// Piece is placed there at the beginning and game is over if piece is stuck in the border.
        /// Borders should not be displayed in views.
        /// </summary>
        public Color BordersColor { get; private set; }

        /// <summary>
        /// A constuctor.
        /// Computes Size of the whole game area from height and width of block.
        /// Sets background, edges and borders color.
        /// Initializes colors in grid (borders, edges, background).
        /// Computes all squares in grid.
        /// </summary>
        /// <param name="blockHeight"></param>
        /// <param name="blockWidth"></param>
        public Tetris4DGrid(int blockHeight, int blockWidth)
        {
            this.BlockHeight = blockHeight;
            this.BlockWidth = blockWidth;
            this.Size = 2 * BlockHeight + BlockWidth;
            this.BorderSize = 2;
            Grid = new Color[Size, Size];
            this.BackgroundColor = Color.Transparent;
            this.EdgesColor = Color.Empty;
            this.BordersColor = Color.Black;
            InitShape();
            allSquaresInGrid = getAllSquaresInGrid();
        }

        public Tetris4DGrid() { }
        
        /// <summary>
        /// Finds filled squares, destroyes them and moves rest of pieces to the center.
        /// New filled squares can be created, so this repeats while there are some filled squares.
        /// </summary>
        /// <returns>Number of destroyed squares.</returns>
        public int DestroyFilledSquares()
        {
            bool proceed = true;
            int destroyedSquaresCount = 0;

            while(proceed)
            {
                markAllEmptySquares();
                List<TetrisSquare> filledSquares = findFilledSquares();

                if (filledSquares.Count == 0)
                {
                    proceed = false;
                    break;
                }

                destroySquares(filledSquares);
                destroyedSquaresCount += filledSquares.Count;

                int begin = 0;
                bool proceed2 = true;

                while (proceed2)
                {
                    //find first empty
                    while (!allSquaresInGrid[begin].IsEmpty)
                    {
                        begin++;
                        if (begin >= allSquaresInGrid.Length)
                        {
                            proceed2 = false;
                            break;
                        }
                    }
                    if (proceed2 == false)
                        break;
                    int emptySquareIndex = begin;
                    int filledSquareIndex = begin + 1;
                    //find first filled
                    while (allSquaresInGrid[filledSquareIndex].IsEmpty)
                    {
                        filledSquareIndex++;
                        if (filledSquareIndex == allSquaresInGrid.Length - 1)
                        {
                            proceed2 = false;
                            break;
                        }
                    }
                    if (proceed2 == false)
                        break;

                    //swap
                    swapSquares(allSquaresInGrid[filledSquareIndex], allSquaresInGrid[emptySquareIndex]);

                    begin = emptySquareIndex + 1;
                    if (begin >= allSquaresInGrid.Length)
                        proceed2 = false;
                }
            }

            return destroyedSquaresCount;
        }

        /// <summary>
        /// Returns array of colors representing current state of grid with extra piece placed in given location.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public Color[,] GetGridWithPiece(IPiece piece, Point location)
        {
            if (piece == null)
                return Grid;


            Color[,] copy = new Color[Size, Size];
            Array.Copy(Grid, copy, Grid.Length);
            for (int i = 0; i < piece.Height; i++)
            {
                for (int j = 0; j < piece.Width; j++)
                {
                    if (piece.Shape[i, j] != null)
                        copy[i + location.X, j + location.Y] = (Color)piece.Shape[i, j];
                }
            }


            return copy;
        }

        /// <summary>
        /// Places given piece in the middle of the border (with regard to the given direction).
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Point GetStartingLocation(IPiece piece, DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.Down:
                    return new Point(0, BlockHeight + (BlockWidth - piece.Width) / 2);
                case DirectionEnum.Up:
                    piece.Turn(2);
                    return new Point(Size - piece.Height, BlockHeight + (BlockWidth - piece.Width) / 2);
                case DirectionEnum.Left:
                    piece.Turn(3);
                    return new Point(BlockHeight + (BlockWidth - piece.Width) / 2, Size - piece.Width);
                case DirectionEnum.Right:
                    piece.Turn(1);
                    return new Point(BlockHeight + (BlockWidth - piece.Width) / 2, 0);
                default:
                    throw new NotImplementedException();
            }

        }

        /// <summary>
        /// Filles the color array with background, borders and edges colors.
        /// </summary>
        public void InitShape()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (
                        (row < BlockHeight || row > Size - BlockHeight - 1) &&
                        (col < BlockHeight || col > Size - BlockHeight - 1)
                        )
                    {
                        Grid[row, col] = EdgesColor;
                    }
                    else
                        Grid[row, col] = BackgroundColor;
                }
            }

            createMiddleSquare();
            CreateBorders();
        }

        /// <summary>
        /// Saves given piece into grid.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        public void AddPieceToGrid(IPiece piece, Point location)
        {
            for (int i = 0; i < piece.Shape.GetLength(0); i++)
            {
                for (int j = 0; j < piece.Shape.GetLength(1); j++)
                {
                    if (piece.Shape[i, j] != null)
                        Grid[i + location.X, j + location.Y] = (Color)piece.Shape[i, j];
                }
            }
        }

        /// <summary>
        /// Tells whether piece is in the border opposite to the starting location.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool WentThrough(IPiece piece, Point location, DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.Up:
                    return location.X < BorderSize;
                    break;
                case DirectionEnum.Down:
                    return location.X + piece.Height > Size - BorderSize - 1;
                    break;
                case DirectionEnum.Right:
                    return location.Y + piece.Width > Size - 1 - BorderSize;
                    break;
                case DirectionEnum.Left:
                    return location.Y < BorderSize;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Tells whether piece is in the starting border and blocked by another piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsStuckAtStart(IPiece piece, Point location, DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.Down:
                    return location.X < BorderSize;
                    break;
                case DirectionEnum.Up:
                    return location.X + piece.Height - 1 > Size - BorderSize - 1;
                    break;
                case DirectionEnum.Left:
                    return location.Y + piece.Width - 1 > Size - BorderSize - 1;
                    break;
                case DirectionEnum.Right:
                    return location.Y < BorderSize;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Tells whether given piece in given location can fit into rid (if there is no other piece or edge in conflict)
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <param name="currentDirection"></param>
        /// <returns></returns>
        public bool CanBePlaced(IPiece piece, Point location, DirectionEnum currentDirection)
        {
            int maxRow;
            int maxCol;
            int minRow;
            int minCol;
            getEdges(currentDirection, out maxRow, out maxCol, out minRow, out minCol);

            for (int row = 0; row < piece.Height; row++)
            {
                for (int col = 0; col < piece.Width; col++)
                {
                    if (piece.Shape[row, col] != null && (
                    location.X + row < minRow ||
                    location.X + row > maxRow ||
                    location.Y + col < minCol ||
                    location.Y + col > maxCol ||
                    Grid[location.X + row, location.Y + col] != BackgroundColor
                    ))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the piece can be moved and if so, it moves the piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <param name="currentDirection"></param>
        /// <returns>Returns if piece was shifted or not.</returns>
        public bool ShiftPiece(IPiece piece, ref Point location, DirectionEnum direction, DirectionEnum currentDirection)
        {
            if (isCollision(piece, location, direction, currentDirection))
                return false;

            switch (direction)
            {
                case DirectionEnum.Up:
                    location.X--;
                    break;
                case DirectionEnum.Down:
                    location.X++;
                    break;
                case DirectionEnum.Right:
                    location.Y++;
                    break;
                case DirectionEnum.Left:
                    location.Y--;
                    break;
                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// Creates the middle square where pieces cannot move through.
        /// </summary>
        private void createMiddleSquare()
        {
            if (Size % 2 == 0) // even, middle composed of 4 squares
            {
                int x = BlockHeight + BlockWidth / 2 - 1;
                int y = x;
                Grid[x, y] = BordersColor;
                Grid[x + 1, y] = BordersColor;
                Grid[x, y + 1] = BordersColor;
                Grid[x + 1, y + 1] = BordersColor;
            }
            else //odd, middle only 1 square
            {
                Grid[BlockHeight + BlockWidth / 2, BlockHeight + BlockWidth / 2] = BordersColor;
            }
        }

        /// <summary>
        /// Fills borders into the color array.
        /// </summary>
        private void CreateBorders()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (
                        ((col == BlockHeight - 1 || col == Size - BlockHeight) && ((row >= BorderSize && row <= BlockHeight - 1) || (row >= Size - BlockHeight && row < Size - BorderSize))) ||
                        ((row == BlockHeight - 1 || row == Size - BlockHeight) && ((col >= BorderSize && col <= BlockHeight - 1) || (col >= Size - BlockHeight && col < Size - BorderSize)))
                      
                        )
                    {
                        Grid[row, col] = BordersColor;
                    }
                }
            }
        }
      
        /// <summary>
        /// Checks whether there would be collision when moving the piece in the direction. 
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <param name="currentDirection"></param>
        /// <returns></returns>
        private bool isCollision(IPiece piece, Point location, DirectionEnum direction, DirectionEnum currentDirection)
        {
            List<Point> conflictPoints = getConflictPoints(getOuterPoints(piece, direction), location, direction);

            int maxRow;
            int maxCol;
            int minRow;
            int minCol;
            getEdges(currentDirection, out maxRow, out maxCol, out minRow, out minCol);

            foreach (var point in conflictPoints)
            {
                if (
                    point.X < minRow ||
                    point.X > maxRow ||
                    point.Y < minCol ||
                    point.Y > maxCol ||
                    Grid[point.X, point.Y] != BackgroundColor
                    )
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Computes the area where a piece can move when going in given direction.
        /// </summary>
        /// <param name="currentDirection"></param>
        /// <param name="maxRow"></param>
        /// <param name="maxCol"></param>
        /// <param name="minRow"></param>
        /// <param name="minCol"></param>
        private void getEdges(DirectionEnum currentDirection, out int maxRow, out int maxCol, out int minRow, out int minCol)
        {
            switch (currentDirection)
            {
                case DirectionEnum.Up:
                    maxRow = Size - 1;
                    minCol = BlockHeight;
                    maxCol = BlockHeight + BlockWidth - 1;
                    minRow = 0;
                    break;
                case DirectionEnum.Down:
                    minRow = 0;
                    minCol = BlockHeight;
                    maxCol = BlockHeight + BlockWidth - 1;
                    maxRow = Size - 1;
                    break;
                case DirectionEnum.Right:
                    minRow = BlockHeight;
                    maxRow = BlockHeight + BlockWidth - 1;
                    minCol = 0;
                    maxCol = Size - 1;
                    break;
                case DirectionEnum.Left:
                    minRow = BlockHeight;
                    maxRow = BlockHeight + BlockWidth - 1;
                    maxCol = Size - 1;
                    minCol = 0;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// According to the direction of move, gets points of the piece which are on the outside.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private List<Point> getOuterPoints(IPiece piece, DirectionEnum direction)
        {
            List<Point> pointsToReturn = new List<Point>();
            switch (direction)
            {
                case DirectionEnum.Up:
                    for (int i = 0; i < piece.Width; i++)
                    {
                        for (int j = 0; j < piece.Height; j++)
                        {
                            if (piece.Shape[j, i] != null)
                            {
                                pointsToReturn.Add(new Point(j, i));
                                break;
                            }
                        }
                    }
                    break;
                case DirectionEnum.Down:
                    for (int i = 0; i < piece.Width; i++)
                    {
                        for (int j = piece.Height - 1; j >= 0; j--)
                        {
                            if (piece.Shape[j, i] != null)
                            {
                                pointsToReturn.Add(new Point(j, i));
                                break;
                            }
                        }
                    }
                    break;
                case DirectionEnum.Right:
                    for (int i = 0; i < piece.Height; i++)
                    {
                        for (int j = piece.Width - 1; j >= 0; j--)
                        {
                            if (piece.Shape[i, j] != null)
                            {
                                pointsToReturn.Add(new Point(i, j));
                                break;
                            }
                        }
                    }
                    break;
                case DirectionEnum.Left:
                    for (int i = 0; i < piece.Height; i++)
                    {
                        for (int j = 0; j < piece.Width; j++)
                        {
                            if (piece.Shape[i, j] != null)
                            {
                                pointsToReturn.Add(new Point(i, j));
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return pointsToReturn;
        }

        /// <summary>
        /// Gets potential points where a conflict can happen when moving the piece.
        /// </summary>
        /// <param name="outerPoints"></param>
        /// <param name="location"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private List<Point> getConflictPoints(List<Point> outerPoints, Point location, DirectionEnum direction)
        {
            List<Point> conflictPoints = new List<Point>();


            switch (direction)
            {
                case DirectionEnum.Up:
                    foreach (var point in outerPoints)
                    {
                        conflictPoints.Add(new Point(point.X + location.X - 1, point.Y + location.Y));
                    }
                    break;
                case DirectionEnum.Down:
                    foreach (var point in outerPoints)
                    {
                        conflictPoints.Add(new Point(point.X + 1 + location.X, point.Y + location.Y));
                    }
                    break;
                case DirectionEnum.Right:
                    foreach (var point in outerPoints)
                    {
                        conflictPoints.Add(new Point(point.X + location.X, point.Y + 1 + location.Y));
                    }
                    break;
                case DirectionEnum.Left:
                    foreach (var point in outerPoints)
                    {
                        conflictPoints.Add(new Point(point.X + location.X, point.Y - 1 + location.Y));
                    }
                    break;
                default:
                    break;
            }

            return conflictPoints;
        }       

        /// <summary>
        /// Replaces pieces colors in this square by background color.
        /// </summary>
        /// <param name="squares"></param>
        private void destroySquares(List<TetrisSquare> squares)
        {
            foreach (var square in squares)
            {
                foreach (var point in square.SquarePoints)
                {
                    Grid[point.X, point.Y] = BackgroundColor;
                }
                square.IsEmpty = true;
            }
        }

        /// <summary>
        /// Decides whether this square is empty (contains only background, edges and borders).
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        private bool isEmpty(TetrisSquare square)
        {
            foreach (var point in square.SquarePoints)
            {
                if (Grid[point.X, point.Y] != BackgroundColor && Grid[point.X, point.Y] != EdgesColor && Grid[point.X, point.Y] != BordersColor)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Gets all squares that are fully filled with piece colors.
        /// </summary>
        /// <returns></returns>
        private List<TetrisSquare> findFilledSquares()
        {
            List<TetrisSquare> filledSquares = new List<TetrisSquare>();

            foreach (var square in allSquaresInGrid)
            {
                bool squareFilled = true;
                foreach (var item in square.SquarePoints)
                {
                    if (Grid[item.X, item.Y] == BackgroundColor ||
                        Grid[item.X, item.Y] == EdgesColor ||
                        Grid[item.X, item.Y] == BordersColor)
                    {
                        squareFilled = false;
                        break;
                    }
                }
                if (squareFilled)
                    filledSquares.Add(square);
            }

            return filledSquares;
        }

        /// <summary>
        /// Computes all squares in grid sorted from the smallest.
        /// </summary>
        /// <returns></returns>
        private TetrisSquare[] getAllSquaresInGrid()
        {
            int squaresCount;
            if (Size % 2 == 0)
                squaresCount = BlockHeight + BlockWidth / 2 - 1;
            else
                squaresCount = BlockHeight + BlockWidth / 2;

            TetrisSquare[] allSquares = new TetrisSquare[squaresCount];

            int x = 0;
            int y = 0;

            for (int i = squaresCount - 1; i >= 0; i--)
            {
                allSquares[i] = getSquare(new Point(x, y));
                x++;
                y++;
            }

            return allSquares;
        }

        /// <summary>
        /// Gets the square in grid containing given point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private TetrisSquare getSquare(Point point)
        {
            if (this.Size % 2 != 0) //liche, 1 kosticka stred
            {
                Point middle = new Point(BlockHeight + BlockWidth / 2, BlockHeight + BlockWidth / 2);
                int squareNumber = Math.Max(Math.Abs(middle.X - point.X), Math.Abs(middle.Y - point.Y));
                Point corner = new Point(middle.X - squareNumber, middle.Y - squareNumber);
                int sideSize = 1 + 2 * squareNumber;
                return new TetrisSquare(corner, sideSize);
            }

            else //sude, 4 kosticky stred, kostickam za diagonalou (vcetne) musime odecist 1
            {
                Point middle = new Point(BlockHeight + BlockWidth / 2 - 1, BlockHeight + BlockWidth / 2 - 1);
                int squareNumber = Math.Max(Math.Abs(middle.X - point.X), Math.Abs(middle.Y - point.Y));

                if (point.Y >= Size - 1 - point.X) //bod lezi za nebo na diagonale z praveho horniho rohu
                {
                    squareNumber--;
                }
                Point corner = new Point(middle.X - squareNumber, middle.Y - squareNumber);
                int sideSize = 2 + 2 * squareNumber;
                return new TetrisSquare(corner, sideSize);
            }
        }

        /// <summary>
        /// Makes outer square empty (replaces it with background color).
        /// Copies colors from outer square to the inner square.
        /// Treats corners differently - blends all colors from the corner of the outer square into one color 
        ///    and uses this color for corresponding corner of the inner square.
        /// Marks the outer square as empty and inner square not empty.
        /// </summary>
        /// <param name="outerSquare"></param>
        /// <param name="innerSquare"></param>
        private void swapSquares(TetrisSquare outerSquare, TetrisSquare innerSquare)
        {
            int outerCornerSize = (outerSquare.SideSize - innerSquare.SideSize) / 2;

            //copy up
            int k = 1;
            for (int col = outerCornerSize + 1; col < outerSquare.SideSize - outerCornerSize - 1; col++)
            {
                if (Grid[outerSquare.LeftTopCorner.X, outerSquare.LeftTopCorner.Y + col] != BackgroundColor &&
                    Grid[outerSquare.LeftTopCorner.X, outerSquare.LeftTopCorner.Y + col] != BordersColor &&
                    Grid[outerSquare.LeftTopCorner.X, outerSquare.LeftTopCorner.Y + col] != EdgesColor)
                {
                    Grid[innerSquare.LeftTopCorner.X, innerSquare.LeftTopCorner.Y + k] = Grid[outerSquare.LeftTopCorner.X, outerSquare.LeftTopCorner.Y + col];
                    Grid[outerSquare.LeftTopCorner.X, outerSquare.LeftTopCorner.Y + col] = BackgroundColor;
                }
                k++;
            }
            //copy down
            k = 1;
            for (int col = outerCornerSize + 1; col < outerSquare.SideSize - outerCornerSize - 1; col++)
            {
                if (Grid[outerSquare.LeftTopCorner.X + outerSquare.SideSize - 1, outerSquare.LeftTopCorner.Y + col] != BackgroundColor &&
                    Grid[outerSquare.LeftTopCorner.X + outerSquare.SideSize - 1, outerSquare.LeftTopCorner.Y + col] != BordersColor &&
                    Grid[outerSquare.LeftTopCorner.X + outerSquare.SideSize - 1, outerSquare.LeftTopCorner.Y + col] != EdgesColor)
                {
                    Grid[innerSquare.LeftTopCorner.X + innerSquare.SideSize - 1, innerSquare.LeftTopCorner.Y + k] = Grid[outerSquare.LeftTopCorner.X + outerSquare.SideSize - 1, outerSquare.LeftTopCorner.Y + col];
                    Grid[outerSquare.LeftTopCorner.X + outerSquare.SideSize - 1, outerSquare.LeftTopCorner.Y + col] = BackgroundColor;
                }
                k++;
            }

            //copy left
            k = 1;
            for (int row = outerCornerSize + 1; row < outerSquare.SideSize - outerCornerSize - 1; row++)
            {
                if (Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y] != BackgroundColor &&
                    Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y] != BordersColor &&
                    Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y] != EdgesColor)
                {
                    Grid[innerSquare.LeftTopCorner.X + k, innerSquare.LeftTopCorner.Y] = Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y];
                    Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y] = BackgroundColor;
                }
                k++;
            }

            //copy right
            k = 1;
            for (int row = outerCornerSize + 1; row < outerSquare.SideSize - outerCornerSize - 1; row++)
            {
                if (Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y + outerSquare.SideSize - 1] != BordersColor &&
                    Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y + outerSquare.SideSize - 1] != BackgroundColor &&
                    Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y + outerSquare.SideSize - 1] != EdgesColor)
                {
                    Grid[innerSquare.LeftTopCorner.X + k, innerSquare.LeftTopCorner.Y + innerSquare.SideSize - 1] = Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y + outerSquare.SideSize - 1];
                    Grid[outerSquare.LeftTopCorner.X + row, outerSquare.LeftTopCorner.Y + outerSquare.SideSize - 1] = BackgroundColor;
                }
                k++;
            }

            //merge corners

            //left top corner
            List<Point> cornerPoints = new List<Point>();
            for (int i = 0; i < outerCornerSize + 1; i++)
            {
                cornerPoints.Add(new Point(outerSquare.LeftTopCorner.X, outerSquare.LeftTopCorner.Y + i));
            }
            for (int i = 1; i < outerCornerSize + 1; i++)
            {
                cornerPoints.Add(new Point(outerSquare.LeftTopCorner.X + i, outerSquare.LeftTopCorner.Y));
            }
            List<Color> colorsToBlend = new List<Color>();
            foreach (var item in cornerPoints)
            {
                if (Grid[item.X, item.Y] != BackgroundColor &&
                    Grid[item.X, item.Y] != EdgesColor &&
                    Grid[item.X, item.Y] != BordersColor)
                    colorsToBlend.Add(Grid[item.X, item.Y]);
            }

            if (colorsToBlend.Count > 0)
            {
                Color newColor = ColorMethods.BlendHSB(colorsToBlend);
                Grid[innerSquare.LeftTopCorner.X, innerSquare.LeftTopCorner.Y] = newColor;
            }

            foreach (var item in cornerPoints)
            {
                if (Grid[item.X, item.Y] != BackgroundColor && Grid[item.X, item.Y] != EdgesColor && Grid[item.X, item.Y] != BordersColor)
                {
                    Grid[item.X, item.Y] = BackgroundColor;
                }
            }

            //right top corner
            cornerPoints = new List<Point>();
            for (int i = outerSquare.SideSize - outerCornerSize - 1; i < outerSquare.SideSize; i++)
            {
                cornerPoints.Add(new Point(outerSquare.LeftTopCorner.X, outerSquare.LeftTopCorner.Y + i));
            }
            for (int i = 1; i < outerCornerSize + 1; i++)
            {
                cornerPoints.Add(new Point(outerSquare.LeftTopCorner.X + i, outerSquare.LeftTopCorner.Y + outerSquare.SideSize - 1));
            }

            colorsToBlend = new List<Color>();
            foreach (var item in cornerPoints)
            {
                if (Grid[item.X, item.Y] != BackgroundColor &&
                    Grid[item.X, item.Y] != EdgesColor &&
                    Grid[item.X, item.Y] != BordersColor)
                    colorsToBlend.Add(Grid[item.X, item.Y]);
            }

            if (colorsToBlend.Count > 0)
            {
                Color newColor = ColorMethods.BlendHSB(colorsToBlend);
                Grid[innerSquare.LeftTopCorner.X, innerSquare.LeftTopCorner.Y + innerSquare.SideSize - 1] = newColor;
            }

            foreach (var item in cornerPoints)
            {
                if (Grid[item.X, item.Y] != BackgroundColor && Grid[item.X, item.Y] != EdgesColor && Grid[item.X, item.Y] != BordersColor)
                {
                    Grid[item.X, item.Y] = BackgroundColor;
                }
            }

            //left bottom corner
            cornerPoints = new List<Point>();
            for (int i = 0; i < outerCornerSize + 1; i++)
            {
                cornerPoints.Add(new Point(outerSquare.LeftTopCorner.X + outerSquare.SideSize - 1, outerSquare.LeftTopCorner.Y + i));
            }
            for (int i = outerSquare.SideSize - outerCornerSize - 1; i < outerSquare.SideSize - 1; i++)
            {
                cornerPoints.Add(new Point(outerSquare.LeftTopCorner.X + i, outerSquare.LeftTopCorner.Y));
            }

            colorsToBlend = new List<Color>();
            foreach (var item in cornerPoints)
            {
                if (Grid[item.X, item.Y] != BackgroundColor &&
                    Grid[item.X, item.Y] != EdgesColor &&
                    Grid[item.X, item.Y] != BordersColor)
                    colorsToBlend.Add(Grid[item.X, item.Y]);
            }

            if (colorsToBlend.Count > 0)
            {
                Color newColor = ColorMethods.BlendHSB(colorsToBlend);
                Grid[innerSquare.LeftTopCorner.X + innerSquare.SideSize - 1, innerSquare.LeftTopCorner.Y] = newColor;
            }

            foreach (var item in cornerPoints)
            {
                if (Grid[item.X, item.Y] != BackgroundColor && Grid[item.X, item.Y] != EdgesColor && Grid[item.X, item.Y] != BordersColor)
                {
                    Grid[item.X, item.Y] = BackgroundColor;
                }
            }

            //right bottom corner
            cornerPoints = new List<Point>();
            for (int i = outerSquare.SideSize - outerCornerSize - 1; i < outerSquare.SideSize; i++)
            {
                cornerPoints.Add(new Point(outerSquare.LeftTopCorner.X + outerSquare.SideSize - 1, outerSquare.LeftTopCorner.Y + i));
            }
            for (int i = outerSquare.SideSize - outerCornerSize - 1; i < outerSquare.SideSize - 1; i++)

            //for (int i = outerSquare.SideSize - outerCornerSize - 1; i < outerCornerSize - 1; i++)
            {
                cornerPoints.Add(new Point(outerSquare.LeftTopCorner.X + i, outerSquare.LeftTopCorner.Y + outerSquare.SideSize - 1));
            }

            colorsToBlend = new List<Color>();
            foreach (var item in cornerPoints)
            {
                if (Grid[item.X, item.Y] != BackgroundColor &&
                    Grid[item.X, item.Y] != EdgesColor &&
                    Grid[item.X, item.Y] != BordersColor)
                    colorsToBlend.Add(Grid[item.X, item.Y]);
            }

            if (colorsToBlend.Count > 0)
            {
                Color newColor = ColorMethods.BlendHSB(colorsToBlend);
                Grid[innerSquare.LeftTopCorner.X + innerSquare.SideSize - 1, innerSquare.LeftTopCorner.Y + innerSquare.SideSize - 1] = newColor;
            }

            foreach (var item in cornerPoints)
            {
                if (Grid[item.X, item.Y] != BackgroundColor && Grid[item.X, item.Y] != EdgesColor && Grid[item.X, item.Y] != BordersColor)
                {
                    Grid[item.X, item.Y] = BackgroundColor;
                }
            }


            outerSquare.IsEmpty = true;
            innerSquare.IsEmpty = false;
        }

        /// <summary>
        /// For each square in grid, markes it as empty if the are no pieces colors and as not empty otherwise.
        /// </summary>
        private void markAllEmptySquares()
        {
            for (int i = 0; i < allSquaresInGrid.Length; i++)
            {
                allSquaresInGrid[i].IsEmpty = isEmpty(allSquaresInGrid[i]);
            }
        }
    }
}
