using System;
using System.Collections.Generic;

namespace Tetris4D
{
    /// <summary>
    /// Score comparer comparing score items first by score from highest to lowest, then by date and then by name.
    /// </summary>
    [Serializable]
    class ScoreComparer : IComparer<ScoreItem>
    {
        public int Compare(ScoreItem x, ScoreItem y)
        {
            if (x.Score != y.Score)
                return 0 - x.Score.CompareTo(y.Score); //descending order
            else if (x.Date != y.Date)
                return 0 - x.Date.CompareTo(y.Date);
            else if (x.Name != y.Name)
                return 0 - x.Name.CompareTo(y.Name);
            else return 0;
        }
    }
}
