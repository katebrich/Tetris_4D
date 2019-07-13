using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisControlProject._EventArgs
{
    /// <summary>
    /// Event args for game over.
    /// </summary>
    public class GameOverEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the total score achieved in this game.
        /// </summary>
        public int TotalScore { get; }

        /// <summary>
        /// Tells whether the player won.
        /// </summary>
        public bool IsWin { get; }

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="totalScore"></param>
        /// <param name="win"></param>
        public GameOverEventArgs(int totalScore, bool win)
        {
            this.TotalScore = totalScore;
            this.IsWin = win;
        }
    }
}
