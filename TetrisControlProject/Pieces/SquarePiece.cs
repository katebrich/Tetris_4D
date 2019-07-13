using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisControlProject.Interfaces;

namespace TetrisControlProject.Tetris4Directions.Pieces
{
    [Serializable]
    class SquarePiece : IPiece
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
        public SquarePiece(Color color)
        {
            this.Color = color;
            this.Shape = new Color?[2, 2] { { color, color }, { color, color } };
        }

        public SquarePiece() { }

        public void Turn(int times)
        {
            //no need to turn
        }
    }
}
