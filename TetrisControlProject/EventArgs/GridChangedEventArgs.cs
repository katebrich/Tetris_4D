using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisControlProject._EventArgs
{
    /// <summary>
    /// Event args for changed grid event.
    /// Provides color array to draw and colors representing edges, background and borders.
    /// </summary>
    public class GridChangedEventArgs : EventArgs
    {
        public Color[,] Grid { get; }
        public int BorderSize { get; }
        public Color BackgroundColor { get; }
        public Color EdgesColor { get; }
        public Color BordersColor { get; }

        public GridChangedEventArgs(Color[,] grid, int borderSize, Color backgroundColor, Color edgesColor, Color bordersColor)
        {
            this.Grid = grid;
            this.BordersColor = bordersColor;
            this.BorderSize = borderSize;
            this.BackgroundColor = backgroundColor;
            this.EdgesColor = edgesColor;
        }

    }
}
