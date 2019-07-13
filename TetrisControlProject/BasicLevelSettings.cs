using System;
using TetrisControlProject.Interfaces;

namespace TetrisControlProject
{
    [Serializable]
    class BasicLevelSettings : ILevelSettings
    {
        /// <summary>
        /// A constructor, sets the score provider, time for piece shift and duration of this level.
        /// </summary>
        /// <param name="scoreProvider"></param>
        /// <param name="pieceShiftTime"></param>
        /// <param name="duration"></param>
        public BasicLevelSettings(IScoreProvider scoreProvider, int pieceShiftTime, int duration)
        {
            this.ScoreProvider = scoreProvider;
            this.PieceShiftTime = pieceShiftTime;
            this.Duration = duration;
        }
        public IScoreProvider ScoreProvider { get; set; }
        public int PieceShiftTime { get; set; }
        public int Duration { get; set; }
    }
}
