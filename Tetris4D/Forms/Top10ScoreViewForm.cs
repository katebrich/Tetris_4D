using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris4D.Forms
{
    /// <summary>
    /// Form to display top 10 score items from given score set.
    /// </summary>
    public partial class Top10ScoreViewForm : Form
    {
        SortedSet<ScoreItem> scoreSet; //list of scores
        Bitmap formViewBitmap; //bitmap to display on the background
        int squareSide = 20;
        int heightSquaresCount = 39;
        int widthSquaresCount = 31;
        Bitmap squareImage;
        Font font;
        Brush brush;

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="scoreSet"></param>
        public Top10ScoreViewForm(SortedSet<ScoreItem> scoreSet)
        {
            this.scoreSet = scoreSet;
            InitializeComponent();

            FontFamily fontFamily = new FontFamily("Comic Sans MS");
            font = new Font(fontFamily, squareSide, FontStyle.Regular);
            brush = new SolidBrush(Color.White);

            createView();
            this.pbView.Image = formViewBitmap;

            this.ClientSize = new Size(formViewBitmap.Width, formViewBitmap.Height);
            this.CenterToParent();
        }

        /// <summary>
        /// Creates bitmap to display.
        /// </summary>
        private void createView()
        {
            pbView.Height = squareSide * heightSquaresCount;
            pbView.Width = squareSide * widthSquaresCount;
            pbView.Location = new Point(0, 0);
            formViewBitmap = new Bitmap(squareSide * widthSquaresCount, squareSide * heightSquaresCount);

            squareImage = TetrisSquareImage.GetTetrisSquareImage(Color.Gray, squareSide, squareSide);

            drawBackground(Color.Black);
            drawGrid();
            drawScoreSign();
            drawScores();

        }

        /// <summary>
        /// Draws one-color background on the bitmap.
        /// </summary>
        /// <param name="color"></param>
        private void drawBackground( Color color)
        {
            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                grfx.FillRectangle(new SolidBrush(color), 0, 0, formViewBitmap.Width, formViewBitmap.Height);
            }
        }

        /// <summary>
        /// Draws score table using tetris square image.
        /// </summary>
        private void drawGrid()
        {
            List<Point> toDraw = new List<Point>();

            //sloupce
            for (int i = 0; i < heightSquaresCount; i++)
            {
                toDraw.Add(new Point(i, 0));
                toDraw.Add(new Point(i, widthSquaresCount - 1));
            }
            //radky
            List<int> rows = new List<int> { 0, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35, 38 };
            foreach (var row in rows)
            {
                for (int i = 1; i < widthSquaresCount - 1; i++)
                {
                    toDraw.Add(new Point(row, i));
                }
            }

            //prostredni sloupce
            for (int i = 9; i < heightSquaresCount - 1; i++)
            {
                toDraw.Add(new Point(i, 10));
                toDraw.Add(new Point(i, 20));
            }          

            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                foreach (var item in toDraw)
                {
                    grfx.DrawImage(squareImage, item.Y * squareSide, item.X * squareSide, squareSide, squareSide);
                }
            }
        }

        /// <summary>
        /// Draws the Score heading.
        /// </summary>
        private void drawScoreSign()
        {
            List<Point> toDraw = new List<Point> {
                new Point(2, 4),
                new Point(2, 5),
                new Point(2, 6),
                new Point(2, 7),
                new Point(2, 8),
                new Point(2, 10),
                new Point(2, 11),
                new Point(2, 12),
                new Point(2, 13),
                new Point(2, 15),
                new Point(2, 16),
                new Point(2, 17),
                new Point(2, 21),
                new Point(2, 23),
                new Point(2, 24),
                new Point(2, 25),
                new Point(2, 26),
                new Point(3, 6),
                new Point(3, 10),
                new Point(3, 13),
                new Point(3, 15),
                new Point(3, 18),
                new Point(3, 21),
                new Point(3, 23),
                new Point(3, 26),
                new Point(4, 6),
                new Point(4, 10),
                new Point(4, 13),
                new Point(4, 15),
                new Point(4, 16),
                new Point(4, 17),
                new Point(4, 21),
                new Point(4, 23),
                new Point(4, 26),
                new Point(5, 6),
                new Point(5, 10),
                new Point(5, 13),
                new Point(5, 15),
                new Point(5, 21),
                new Point(5, 23),
                new Point(5, 26),
                new Point(6, 6),
                new Point(6, 10),
                new Point(6, 11),
                new Point(6, 12),
                new Point(6, 13),
                new Point(6, 15),
                new Point(6, 21),
                new Point(6, 23),
                new Point(6, 24),
                new Point(6, 25),
                new Point(6, 26)
            };

            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                /*Bitmap greenImage = TetrisSquare.GetTetrisSquareImage(Color.LimeGreen, squareSide, squareSide);
                Bitmap yellowImage = TetrisSquare.GetTetrisSquareImage(Color.Yellow, squareSide, squareSide);
                Bitmap redImage = TetrisSquare.GetTetrisSquareImage(Color.Red, squareSide, squareSide);
                Bitmap purpleImage = TetrisSquare.GetTetrisSquareImage(Color.Purple, squareSide, squareSide);
                Bitmap blueImage = TetrisSquare.GetTetrisSquareImage(Color.Blue, squareSide, squareSide);
                */

                foreach (var item in toDraw)
                {
                    grfx.DrawImage(squareImage, item.Y * squareSide, item.X * squareSide, squareSide, squareSide);
                }
            }
        }

        /// <summary>
        /// Draws top 10 scores into table
        /// </summary>
        private void drawScores()
        {
            IEnumerator<ScoreItem> enumerator = scoreSet.GetEnumerator();
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                int i = 0;
                while (enumerator.MoveNext() && i < 10)
                {
                    ScoreItem item = enumerator.Current;
                    grfx.DrawString(item.Score.ToString(), font, brush, new Rectangle(1 * squareSide, 9 * squareSide + i * 3 * squareSide, 9 * squareSide, 2 * squareSide), format);
                    grfx.DrawString(item.Name.ToString(), font, brush, new Rectangle(11 * squareSide, 9 * squareSide + i * 3 * squareSide, 9 * squareSide, 2 * squareSide), format);
                    grfx.DrawString(item.Date.ToShortDateString(), font, brush, new Rectangle(21 * squareSide, 9 * squareSide + i * 3 * squareSide, 9 * squareSide, 2 * squareSide), format);

                    i++;
                }
            }
        }

    }
}
