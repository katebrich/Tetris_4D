using System;
using TetrisControlProject._EventArgs;

namespace TetrisControlProject.Interfaces
{
    /// <summary>
    /// Controls progress of game.
    /// Can be started, paused, unpaused.
    /// Informs about changed views and game over event.
    /// Can process inputs from user.
    /// </summary>
    public interface ITetrisController
    {
        /// <summary>
        /// Tells whether the game is paused.
        /// </summary>
        bool GamePaused { get; }

        /// <summary>
        /// Tells whether the game ended.
        /// </summary>
        bool GameOver { get; set; }

        /// <summary>
        /// Tells total score.
        /// </summary>
        int TotalScore { get; }

        /// <summary>
        /// Informs that view of the tetris grid changed.
        /// </summary>
        event EventHandler<GridChangedEventArgs> GridChangedEvent;

        /// <summary>
        /// Informs that total score changed.
        /// </summary>
        event EventHandler<ScoreChangedEventArgs> ScoreChangedEvent;

        /// <summary>
        /// Informs that next piece changed.
        /// </summary>
        event EventHandler<NextPieceChangedEventArgs> NextPieceChangedEvent;

        /// <summary>
        /// Informs that game ended.
        /// </summary>
        event EventHandler<GameOverEventArgs> GameOverEvent;

        /// <summary>
        /// Informs that the interval when the piece should move down changed.
        /// </summary>
        event EventHandler<TimerIntervalChangedEventArgs> TimerIntervalChanged;

        /// <summary>
        /// Starts the game.
        /// </summary>
        void StartGame();

        /// <summary>
        /// Restarts paused game.
        /// </summary>
        void Unpause();

        /// <summary>
        /// Stops the game until it is unpaused.
        /// </summary>
        void Pause();

        /// <summary>
        /// Processes user input.
        /// </summary>
        /// <param name="control"></param>
        void ProcessControl(Enums.ControlsEnum control);

        /// <summary>
        /// Processes tick of the piece shift timer (information that the piece should move in direction)
        /// </summary>
        void ProcessTimerTick();
    }
}
