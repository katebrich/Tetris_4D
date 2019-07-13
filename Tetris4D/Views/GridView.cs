using System;
using System.Collections.Generic;
using System.Drawing;
using TetrisControlProject._EventArgs;

namespace Tetris4D.Views
{
    /// <summary>
    /// Shows the tetris grid - draws the color array.
    /// </summary>
    public class GridView
    {
        private Bitmap bitmapToDisplay;
        private int squareHeight; //height, in pixels, of one square of the grid
        private int squareWidth;
        private Bitmap freeSquare; //image for background square
        private Dictionary<Color, Bitmap> bitmaps = new Dictionary<Color, Bitmap>(); //images for colors that were already computed are saved for another usage

        /// <summary>
        /// A font used for this view.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// A constructor.
        /// </summary>
        public GridView()
        {
            bitmapToDisplay = new Bitmap(1, 1);
        }

        /// <summary>
        /// Gets image of given proportions representing this view.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Bitmap GetBitmap(int width, int height, GridChangedEventArgs args)
        {
            bitmapToDisplay = new Bitmap(width, height);
            squareHeight = bitmapToDisplay.Height / args.Grid.GetLength(0);
            squareWidth = bitmapToDisplay.Width / args.Grid.GetLength(1);

            freeSquare = createFreeSquare(squareWidth, squareHeight);

            drawGrid(args.Grid, args.BordersColor, args.EdgesColor, args.BackgroundColor, args.BorderSize);

            return bitmapToDisplay;
        }

        /// <summary>
        /// Gets paused view - covers all game area with semitransparent label.
        /// </summary>
        /// <returns></returns>
        public Bitmap GetPausedBitmap()
        {
            Bitmap bitmap = new Bitmap(bitmapToDisplay);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                graphics.FillRectangle(new SolidBrush(Color.FromArgb(140, Color.Black)), new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                graphics.DrawString(Messages.GridView_GamePaused, new Font(FontFamily.GenericSansSerif, 40f), new SolidBrush(Color.White), new RectangleF(0, 0, bitmap.Width, bitmap.Height), format);
            }

            return bitmap;
        }

        /// <summary>
        /// Gets bitmap without paused view.
        /// </summary>
        /// <returns></returns>
        public Bitmap GetUnpausedBitmap()
        {
            return bitmapToDisplay;
        }

        /// <summary>
        /// Gets game over view - covers all game area with semitransparent label.
        /// </summary>
        /// <returns></returns>
        public Bitmap GetGameOverBitmap()
        {
            Bitmap bitmap = new Bitmap(bitmapToDisplay);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                graphics.FillRectangle(new SolidBrush(Color.FromArgb(140, Color.Black)), new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                graphics.DrawString(Messages.GridView_GameOver, new Font(FontFamily.GenericSansSerif, 40f), new SolidBrush(Color.White), new RectangleF(0, 0, bitmap.Width, bitmap.Height), format);
            }

            return bitmap;
        }

        /// <summary>
        /// Gets image for background square.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Bitmap createFreeSquare(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            int borderSize = Math.Min(width / 15, height / 15);

            for (int x = 0; x < squareWidth; x++)
            {
                for (int y = 0; y < squareHeight; y++)
                {
                    if (x < borderSize || x > height - borderSize || y < borderSize || y > width - borderSize)
                    {
                        bitmap.SetPixel(x, y, Color.DarkSlateGray);
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Draws whole colors array. 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="bordersColor"></param>
        /// <param name="edgesColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="borderSize"></param>
        private void drawGrid(Color[,] grid, Color bordersColor, Color edgesColor, Color backgroundColor, int borderSize)
        {
            using (Graphics grfx = Graphics.FromImage(bitmapToDisplay))
            {
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        if (col < borderSize ||
                            col > grid.GetLength(1) - borderSize - 1 ||
                            row < borderSize ||
                            row > grid.GetLength(0) - borderSize - 1
                            )
                        {
                            grfx.FillRectangle(new SolidBrush(Color.Transparent), row * squareWidth, col * squareHeight, squareWidth, squareHeight);
                        }
                        else if (grid[col, row] == edgesColor)
                        {
                            grfx.FillRectangle(new SolidBrush(Color.Transparent), row * squareWidth, col * squareHeight, squareWidth, squareHeight);
                        }
                        else if (grid[col, row] == backgroundColor)
                        {
                            grfx.DrawImage(freeSquare, row * squareWidth, col * squareHeight);
                            //grfx.FillRectangle(new SolidBrush(Color.Transparent), row * squareWidth, col * squareHeight, squareWidth, squareHeight);
                        }

                        else if (grid[col, row] == bordersColor)
                        {
                            grid[col, row] = Color.Gray;
                        }

                        if (grid[col, row] != edgesColor && grid[col, row] != backgroundColor) //&& grid[col, row] != bordersColor)
                        {
                            Color color = grid[col, row];

                            bitmaps.TryGetValue(color, out Bitmap bitmap);

                            //TODO !!!! kdyz se resizuje okno, bude potreba vse vypo9citat znovu


                            if (bitmap == default(Bitmap)) //jeste nebyl vytvoren pro tuto barvu
                            {
                                bitmap = TetrisSquareImage.GetTetrisSquareImage(color, squareHeight, squareWidth);
                                bitmaps.Add(color, bitmap);
                            }
                            grfx.DrawImage(bitmap, row * squareWidth, col * squareHeight, squareWidth, squareHeight);
                        }
                    }
                }
            }
        }

        
    }
}
