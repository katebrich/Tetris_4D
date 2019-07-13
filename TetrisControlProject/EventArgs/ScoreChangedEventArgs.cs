using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisControlProject._EventArgs
{
    /// <summary>
    /// Gets info about total score, newly obtained score and number of destroyed squares.
    /// </summary>
    public class ScoreChangedEventArgs : EventArgs
    {
        public int TotalScore { get; }
        public int NewScore { get; }
        public int TotalSquaresDestroyed { get; }

        public ScoreChangedEventArgs(int totalScore, int newScore, int totalSquaresDestroyed)
        {
            this.TotalScore = totalScore;
            this.NewScore = newScore;
            this.TotalSquaresDestroyed = totalSquaresDestroyed;
        }

    }
}
