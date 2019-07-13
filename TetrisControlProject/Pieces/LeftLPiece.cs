using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TetrisControlProject.Interfaces;
using TetrisControlProject.Helper;

namespace TetrisControlProject.Tetris4Directions.Pieces
{
    [Serializable]
    class LeftLPiece : IPiece
    {
        private Color?[,] shape;
        public Color Color { get; set; }
        public Color?[,] Shape
        {
            get
            {
                return shape;
            }
            set
            {
                shape = value;
                Width = shape.GetLength(1);
                Height = shape.GetLength(0);
            }
        }
        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        /// A constructor for this class.
        /// </summary>
        /// <param name="color">Creates piece with given color.</param>
        public LeftLPiece(Color color)
        {
            this.Color = color;
            this.Shape = new Color?[2, 3] { { null, null, color }, { color, color, color } };
        }

        public LeftLPiece() { }

        public void Turn(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Shape = Shape.RotateMatrixCounterClockwise();
            }
        }
    }
}

