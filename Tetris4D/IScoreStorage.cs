using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisControlProject.Helper;

namespace Tetris4D
{
    /// <summary>
    /// Stores all saved scores.
    /// </summary>
    interface IScoreStorage
    {
        /// <summary>
        /// Gets all scores sorted.
        /// </summary>
        SortedSet<ScoreItem> sortedScoreSet { get; }

        /// <summary>
        /// Adds another score item.
        /// </summary>
        /// <param name="score"></param>
        void AddScore(ScoreItem score);

        /// <summary>
        /// Gets the highest score saved in the storage.
        /// </summary>
        /// <returns></returns>
        ScoreItem GetHighScore();
    }
}
