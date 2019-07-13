using System;
using System.Collections.Generic;
using System.Drawing;

namespace TetrisControlProject
{
    /// <summary>
    /// Square in tetris grid represented by its left top corner point and size of its side.
    /// </summary>
    [Serializable]
    class TetrisSquare
    {
        /// <summary>
        /// Gets the left top corner of this square. 
        /// </summary>
        public Point LeftTopCorner {get;}

        /// <summary>
        /// Gets the size of this square.
        /// </summary>
        public int SideSize { get; }

        /// <summary>
        /// Gets all points contained by this square.
        /// </summary>
        public List<Point> SquarePoints { get; }

        /// <summary>
        /// Indicates whether this square is empty (contains no piece color).
        /// </summary>
        public bool IsEmpty { get; set; } = false;

        /// <summary>
        /// A constuctor.
        /// Computes square points.
        /// </summary>
        /// <param name="leftRightCorner"></param>
        /// <param name="sideSize"></param>
        public TetrisSquare(Point leftRightCorner, int sideSize)
        {
            this.LeftTopCorner = leftRightCorner;
            this.SideSize = sideSize;
            this.SquarePoints = getSquarePoints();
        }

        public TetrisSquare() { }
        
        /// <summary>
        /// Computes all points contained by this square.
        /// </summary>
        /// <returns></returns>
        private List<Point> getSquarePoints()
        {
            List<Point> points = new List<Point>();
            Point start = LeftTopCorner;

            for (int col = 1 + start.Y; col <= SideSize - 1 + start.Y; col++)
            {
                points.Add(new Point(start.X, col));
            }
            start = new Point(start.X, start.Y + SideSize - 1);
            for (int row = 1 + start.X; row <= SideSize - 1 + start.X; row++)
            {
                points.Add(new Point(row, start.Y));
            }
            start = new Point(SideSize - 1 + start.X, start.Y);
            for (int col = start.Y - 1; col >= start.Y - SideSize + 1; col--)
            {
                points.Add(new Point(start.X, col));
            }
            start = new Point(start.X, start.Y - SideSize + 1);
            for (int row = start.X - 1; row >= start.X - SideSize + 1; row--)
            {
                points.Add(new Point(row, start.Y));
            }

            return points;
        }
    }
}
