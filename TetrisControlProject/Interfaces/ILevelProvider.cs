

namespace TetrisControlProject.Interfaces
{
    /// <summary>
    /// Provides levelSettings for each level.
    /// </summary>
    internal interface ILevelProvider
    {
        /// <summary>
        /// Gets next level.
        /// </summary>
        /// <returns></returns>
        ILevelSettings GetNextLevelSettings();

        /// <summary>
        /// Tells whether there is another level.
        /// </summary>
        /// <returns></returns>
        bool HasNextLevel();

        /// <summary>
        /// Tells whether the game should continue after this level or if the player won.
        /// </summary>
        bool ContinueAfterLastLevel { get; set; }
    }
}
