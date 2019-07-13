using System;
using System.Collections.Generic;
using TetrisControlProject.Tetris4Directions.Pieces;
using TetrisControlProject.Interfaces;
using System.Drawing;

namespace TetrisControlProject
{
    /// <summary>
    /// Generates pieces from its library randomly.
    /// Uses standard Tetris pieces.
    /// </summary>
    [Serializable]
    class RandomPieceGenerator : IPieceGenerator
    {
        [Serializable]
        private class PieceInfo
        {
            public string PieceName;
            public Color PieceColor;

            public PieceInfo(string pieceName, Color pieceColor)
            {
                this.PieceName = pieceName;
                this.PieceColor = pieceColor;
            }
        }

        private PieceInfo[] pieceLibrary;
        private Random rnd = new Random();

        /// <summary>
        /// A constructor. Creates the library.
        /// </summary>
        public RandomPieceGenerator()
        {
            pieceLibrary = new PieceInfo[7];
            initLibrary();
        }

        /// <summary>
        /// Generates next piece from the library.
        /// </summary>
        /// <returns></returns>
        public IPiece GetNextPiece()
        {
            int index = rnd.Next(0, pieceLibrary.Length);
            string name = pieceLibrary[index].PieceName;
            Color color = pieceLibrary[index].PieceColor;

            if (name == "LeftZPiece")
                return new LeftZPiece(color);
            else if (name == "RightZPiece")
                return new RightZPiece(color);
            else if (name == "SquarePiece")
                return new SquarePiece(color);
            else if (name == "LinePiece")
                return new LinePiece(color);
            else if (name == "LeftLPiece")
                return new LeftLPiece(color);
            else if (name == "RightLPiece")
                return new RightLPiece(color);
            else if (name == "TPiece")
                return new TPiece(color);
            else
                throw new Exception("IPiece generator library is not defined correctly.");
        }

        /// <summary>
        /// Generates the next direction from DirectionEnum.
        /// </summary>
        /// <returns></returns>
        public Enums.DirectionEnum GetNextDirection()
        {
            int upperBound = Enum.GetNames(typeof(Enums.DirectionEnum)).Length;
            int index = rnd.Next(0, upperBound);
            return (Enums.DirectionEnum)(index);
        }

        /// <summary>
        /// Creates the library of pieces - pairs of name and color.
        /// </summary>
        private void initLibrary()
        {
            pieceLibrary[0] = new PieceInfo("LeftZPiece", Color.Red);
            pieceLibrary[1] = new PieceInfo("RightZPiece", Color.LimeGreen);
            pieceLibrary[2] = new PieceInfo("SquarePiece", Color.Yellow);
            pieceLibrary[3] = new PieceInfo("LinePiece", Color.MediumTurquoise);
            pieceLibrary[4] = new PieceInfo("LeftLPiece", Color.OrangeRed);
            pieceLibrary[5] = new PieceInfo("RightLPiece", Color.Blue);
            pieceLibrary[6] = new PieceInfo("TPiece", Color.Purple);
        }
    }
}
