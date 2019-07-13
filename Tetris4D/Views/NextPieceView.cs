
using System.Collections.Generic;
using System.Drawing;
using TetrisControlProject.Enums;
using TetrisControlProject.Interfaces;
using TetrisControlProject._EventArgs;

namespace Tetris4D.Views
{
    /// <summary>
    /// Shows next piece and next direction.
    /// </summary>
    class NextPieceView
    {
        private Bitmap bitmapToDisplay;

        /// <summary>
        /// Font used in this view.
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// Gets bitmap with given proportions representing next piece and next direction.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Bitmap GetBitmap(int height, int width, NextPieceChangedEventArgs args)
        {
            bitmapToDisplay = new Bitmap(width, height);

            Bitmap nextSign = createNextSign(width, (5 * width) / 24, Color.Gray);
            Color pieceColor = Color.Gray;
            foreach (var item in args.NextPiece.Shape)
            {
                if (item != null)
                {
                    pieceColor = (Color)item;
                    break;
                }
            }
            Bitmap arrow = createArrowImage((5 * width) / 24, (5 * width) / 24, pieceColor, args.NextDirection);
            Bitmap piece = createPieceImage(args.NextPiece, (arrow.Height / 5)*3, (arrow.Height / 5) * 3);

            int sideOffset = width / 10;
            int upOffset = nextSign.Height;
            int middle = width / 2;

            using (Graphics grfx = Graphics.FromImage(bitmapToDisplay))
            {
                grfx.DrawImage(nextSign, 0, 0);
                grfx.DrawImage(piece, sideOffset + (middle - piece.Width - sideOffset)/2 , nextSign.Height + upOffset);
                grfx.DrawImage(arrow, middle + (middle - sideOffset - arrow.Width) / 2, nextSign.Height + upOffset + (piece.Height - arrow.Height) / 2);
            }

            return bitmapToDisplay;
        }

        /// <summary>
        /// Creates picture of given piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="sqWidth">Width of one square of piece</param>
        /// <param name="sqHeight">Height of one square of piece</param>
        /// <returns></returns>
        private Bitmap createPieceImage(IPiece piece, int sqWidth, int sqHeight)
        {
            Bitmap bitmap = new Bitmap(sqWidth*piece.Width, sqHeight*piece.Height);
            //int sqWidth = sqWidth / piece.Width;
            //int sqHeight = sqHeight / piece.Height;

            using (Graphics grfx = Graphics.FromImage(bitmap))
            {
                for (int row = 0; row < piece.Width; row++)
                {
                    for (int col = 0; col < piece.Height; col++)
                    {
                        if (piece.Shape[col, row] != null)
                            grfx.DrawImage(TetrisSquareImage.GetTetrisSquareImage((Color)(piece.Shape[col, row]), sqHeight, sqWidth), row * sqWidth, col * sqHeight, sqWidth, sqHeight);
                    }
                }
            }
            return bitmap;
        }

        /// <summary>
        /// Creates image of arrow for given direction.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private Bitmap createArrowImage(int width, int height, Color color, DirectionEnum direction)
        {
            Bitmap bitmap = new Bitmap(width, height);
            int sqWidth = width / 5;
            int sqHeight = height / 5;

            Bitmap square = TetrisSquareImage.GetTetrisSquareImage(color, sqHeight, sqWidth);

            List<Point> toDraw = new List<Point> {
                new Point(0 , 2),
                new Point(1 ,3 ),
                new Point(2 ,0 ),
                new Point(2 ,1 ),
                new Point(2 ,2 ),
                new Point(2 ,3 ),
                new Point(2 ,4 ),
                new Point(3 ,3 ),
                new Point(4 ,2 ),
            };

            using (Graphics grfx = Graphics.FromImage(bitmap))
            {
                foreach (var item in toDraw)
                {
                    grfx.DrawImage(square, item.Y * sqWidth, item.X * sqHeight, sqWidth, sqHeight);
                }
            }

            switch (direction)
            {
                case DirectionEnum.Up:
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case DirectionEnum.Down:
                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case DirectionEnum.Right:
                    break;
                case DirectionEnum.Left:
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                default:
                    break;
            }

            return bitmap;
        }

        /// <summary>
        /// Creates the Next sign image.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Bitmap createNextSign(int width, int height, Color color)
        {
            Bitmap bitmap = new Bitmap(width, height);
            int sqWidth = width / 24;
            int sqHeight = height / 5;

            Bitmap square = TetrisSquareImage.GetTetrisSquareImage(color, sqHeight, sqWidth);

            List<Point> toDraw = new List<Point> {
                new Point(0 , 1),
                new Point(0 , 5),
                new Point(0 , 10),
                new Point(0 , 7),
                new Point(0 , 8),
                new Point(0 , 9),
                new Point(0 ,12),
                new Point(0 ,16),
                new Point(0 ,22),
                new Point(0 ,18),
                new Point(0 ,19),
                new Point(0 ,20),
                new Point(0 ,21),
                new Point(1 ,2),
                new Point(1 ,1),
                new Point(1 ,5),
                new Point(1 ,7),
                new Point(1 ,13),
                new Point(1 ,15),
                new Point(1 ,20),
                new Point(2 ,1),
                new Point(2 ,3),
                new Point(2 ,5),
                new Point(2 ,9),
                new Point(2 ,7),
                new Point(2 ,8),
                new Point(2 ,14),
                new Point(2 ,20),
                new Point(3 ,1),
                new Point(3, 5),
                new Point(3 ,4),
                new Point(3 ,7),
                new Point(3 ,13),
                new Point(3 ,15),
                new Point(3 ,20),
                new Point(4 ,1),
                new Point(4 ,5),
                new Point(4 ,10),
                new Point(4 ,7),
                new Point(4 ,8),
                new Point(4 ,9),
                new Point(4 ,12),
                new Point(4 ,16),
                new Point(4, 20),
               // new Point(1, 23),
               // new Point(3,23)
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
