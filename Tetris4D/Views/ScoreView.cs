using System.Collections.Generic;
using System.Drawing;
using TetrisControlProject._EventArgs;

namespace Tetris4D.Views
{
    /// <summary>
    /// Shows total score, high score and number of destroyed squares.
    /// </summary>
    class ScoreView
    {
        private Bitmap bitmapToDisplay;
        private int highScore;

        /// <summary>
        /// A font used for this view.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// Gets bitmap of given proportions representing the view.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="args">Info about score changes</param>
        /// <returns></returns>
        public Bitmap GetBitmap(int height, int width, ScoreChangedEventArgs args)
        {
            bitmapToDisplay = new Bitmap(width, height);

            Bitmap scoreSign = createScoreSign(width, (width * 5) / 26, Color.Gray);
            int offset = scoreSign.Height / 3;
            Font font = new Font(this.Font.FontFamily, scoreSign.Height / 2);
            Font scoreFont = new Font(this.Font.FontFamily, scoreSign.Height / 1.5f);
            Brush brush = Brushes.Gainsboro;

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            using (Graphics grfx = Graphics.FromImage(bitmapToDisplay))
            {
                grfx.DrawImage(scoreSign, 0, 0);

                grfx.DrawString(args.TotalScore.ToString(), scoreFont, brush, new RectangleF(0, scoreSign.Height + offset, width, scoreFont.Size+offset), format);

                grfx.DrawString(Messages.ScoreView_HighScoreHeading, font, brush, new RectangleF(0, scoreSign.Height + 2*offset + scoreFont.Size, width, font.Size + 2* offset), format);

                grfx.DrawString(highScore.ToString(), font, brush, new RectangleF(0, scoreSign.Height + 3 * offset + scoreFont.Size + font.Size, width, font.Size + 3 * offset), format);

                grfx.DrawString(Messages.ScoreView_SquaresHeading, font, brush, new RectangleF(0, scoreSign.Height + 4 * offset + scoreFont.Size + 2* font.Size, width, font.Size + 4 * offset), format);

                grfx.DrawString(args.TotalSquaresDestroyed.ToString(), font, brush, new RectangleF(0, scoreSign.Height + 5 * offset + scoreFont.Size + 3* font.Size, width, font.Size + 5 * offset), format);

            }

            return bitmapToDisplay;
        }

        /// <summary>
        /// Sets the high score.
        /// </summary>
        /// <param name="score"></param>
        public void SetHighScore(int score)
        {
            this.highScore = score;
        }

        /// <summary>
        /// Creates image of the Score sign with given proportions.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Bitmap createScoreSign(int width, int height, Color color)
        {
            Bitmap bitmap = new Bitmap(width, height);
            int sqWidth = width / 26;
            int sqHeight = height / 5;

            Bitmap square = TetrisSquareImage.GetTetrisSquareImage(color, sqHeight, sqWidth);

            List<Point> toDraw = new List<Point> {
               new Point(0 ,4 ),
               new Point(0 ,2 ),
               new Point(0 ,3 ),
               new Point(0 ,9 ),
               new Point(0 ,7 ),
               new Point(0 ,8 ),
               new Point(0 ,13 ),
               new Point(0 ,12 ),
               new Point(0 ,18 ),
               new Point(0 ,16 ),
               new Point(0 ,17 ),
               new Point(0 ,24 ),
               new Point(0 ,21 ),
               new Point(0 ,22 ),
               new Point(0 ,23 ),
               new Point(1, 1),
               new Point(1, 6),
               new Point(1, 11),
               new Point(1, 14),
               new Point(1, 16),
               new Point(1, 19),
               new Point(1, 21),
               //new Point(1, 25),
               new Point(2, 3),
               new Point(2, 2),
               new Point(2, 6),
               new Point(2, 11),
               new Point(2, 14),
               new Point(2, 18),
               new Point(2, 16),
               new Point(2, 17),
               new Point(2, 23),
               new Point(2, 21),
               new Point(2, 22),
               new Point(3, 4),
               new Point(3, 6),
               new Point(3, 11),
               new Point(3, 14),
               new Point(3, 16),
               new Point(3, 19),
               new Point(3, 21),
               //new Point(3, 25),
               new Point(4, 3),
               new Point(4, 1),
               new Point(4, 2),
               new Point(4, 9),
               new Point(4, 7),
               new Point(4, 8),
               new Point(4, 13),
               new Point(4, 12),
               new Point(4, 16),
               new Point(4, 19),
               new Point(4, 24),
               new Point(4, 21),
               new Point(4, 22),
               new Point(4, 23),
            };

            using (Graphics grfx = Graphics.FromImage(bitmap))
            {
                foreach (var item in toDraw)
                {
                    grfx.DrawImage(square, item.Y * sqWidth, item.X * sqHeight, sqWidth, sqHeight);
                }
            }

            return bitmap;
        }
    }
}
