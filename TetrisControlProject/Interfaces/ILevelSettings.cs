

namespace TetrisControlProject.Interfaces
{
    /// <summary>
    /// Settings for one level.
    /// </summary>
    internal interface ILevelSettings
    {
        /// <summary>
        /// Provides score for this level.
        /// </summary>
        IScoreProvider ScoreProvider { get; set; }
        
        /// <summary>
        /// Time for a piece to make a step down.
        /// </summary>
        int PieceShiftTime { get; set; }

        /// <summary>
        /// How long this level lasts before it is changed for next one.
        /// </summary>
        int Duration { get; set; }
    }
}
