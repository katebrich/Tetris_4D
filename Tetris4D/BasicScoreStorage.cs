using System;
using System.Collections.Generic;

namespace Tetris4D
{
    /// <summary>
    /// Stores saved scores.
    /// </summary>
    [Serializable]
    class BasicScoreStorage : IScoreStorage
    {
        /// <summary>
        /// Gets all scores sorted.
        /// </summary>
        public SortedSet<ScoreItem> sortedScoreSet { get; private set; }

        /// <summary>
        /// A constructor.
        /// </summary>
        public BasicScoreStorage()
        {
            sortedScoreSet = new SortedSet<ScoreItem>(new ScoreComparer());
        }

        /// <summary>
        /// And another score item.
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(ScoreItem score)
        {
            sortedScoreSet.Add(score);
        }

        /// <summary>
        /// Gets the highest score saved in the storage.
        /// </summary>
        /// <returns></returns>
        public ScoreItem GetHighScore()
        {
            //if no item, returns 0
            return sortedScoreSet.Min;
        }
    }
}
