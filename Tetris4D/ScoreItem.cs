using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4D
{
    /// <summary>
    /// Represents one score achieved in a game.
    /// </summary>
    [Serializable]
    public struct ScoreItem
    {
        /// <summary>
        /// Value of score.
        /// </summary>
        public int Score { get; }

        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Date of achieving this score.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="name"></param>
        /// <param name="date"></param>
        public ScoreItem(int score, string name, DateTime date)
        {
            this.Score = score;
            this.Name = name;
            this.Date = date;
        }
    }
}
