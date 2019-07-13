using System;
using TetrisControlProject.Enums;
using TetrisControlProject.Interfaces;
using TetrisControlProject._EventArgs;
using System.Runtime.Serialization;

namespace TetrisControlProject
{
    /// <summary>
    /// Game is controled bz this class.
    /// Connects piece generator, tetris box, score provider and level provider and provides communication between these classes.
    /// Can start new game, be paused, unpaused.
    /// Informs about changed views and game over event.
    /// Can process inputs from user.
    /// </summary>
    [Serializable]
    public class Tetris4DController : ITetrisController
    {
        //[NonSerialized]//
        private IScoreProvider scoreProvider;
        //[NonSerialized]//
        private ITetrisBox TetrisBox;
        //[NonSerialized]//
        private ILevelProvider levelProvider;
        //[NonSerialized]//
        private IPieceGenerator pieceGenerator;
        [NonSerialized]
        private System.Diagnostics.Stopwatch levelStopwatch; //measures time from beginning of new level
        private int currentLevelDuration; //how much time the current level should last
        
        private IPiece nextPiece;
        //[NonSerialized]//
        private DirectionEnum nextDirection;
        private int squaresDestroyedCount = 0; //total number of destroyed squares during game

        /// <summary>
        /// Tells total score.
        /// </summary>
        public int TotalScore { get; private set; }

        /// <summary>
        /// Tells whether the game is paused.
        /// </summary>
        public bool GamePaused { get; private set; }

        /// <summary>
        /// Tells whether the game ended.
        /// </summary>
        public bool GameOver { get; set; }

        /// <summary>
        /// Informs that view of the tetris grid changed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<GridChangedEventArgs> GridChangedEvent;

        /// <summary>
        /// Informs that total score changed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ScoreChangedEventArgs> ScoreChangedEvent;

        /// <summary>
        /// Informs that next piece changed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<NextPieceChangedEventArgs> NextPieceChangedEvent;

        /// <summary>
        /// Informs that game ended.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<GameOverEventArgs> GameOverEvent;

        /// <summary>
        /// Informs that the interval when the piece should move down changed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<TimerIntervalChangedEventArgs> TimerIntervalChanged;
        
        /// <summary>
        /// A constructor.
        /// </summary>
        public Tetris4DController()
        {
            ITetrisGrid grid = new Tetris4DGrid(10, 15);
            this.TetrisBox = new Tetris4DBox(grid);
            this.pieceGenerator = new RandomPieceGenerator();
            this.levelProvider = new SpeedingUpLevelProvider();

            this.levelStopwatch = new System.Diagnostics.Stopwatch();
        }

        /// <summary>
        /// Sets first level settings, starts measuring level time.
        /// Adds a piece to the tetris box.
        /// Initializes views.
        /// </summary>
        public void StartGame()
        {
            setSettings(levelProvider.GetNextLevelSettings());
            levelStopwatch.Restart();
            TetrisBox.ViewChanged += tetrisBox_ViewChanged;
            TetrisBox.PiecePlaced += tetrisBox_PiecePlaced;
            TetrisBox.AddNewPiece(pieceGenerator.GetNextPiece(), pieceGenerator.GetNextDirection()); nextPiece = pieceGenerator.GetNextPiece();
            nextDirection = pieceGenerator.GetNextDirection();
            NextPieceChangedEvent?.Invoke(this, new NextPieceChangedEventArgs(nextPiece, nextDirection));
            TotalScore = 0;
            ScoreChangedEvent?.Invoke(this, new ScoreChangedEventArgs(TotalScore, 0, squaresDestroyedCount));
        }
       
        /// <summary>
        /// Restarts the stopwatch.
        /// Rises events to restore views.
        /// </summary>
        public void Unpause()
        {
            this.GamePaused = false;
            levelStopwatch.Start();
            NextPieceChangedEvent?.Invoke(this, new NextPieceChangedEventArgs(nextPiece, nextDirection));
            ScoreChangedEvent?.Invoke(this, new ScoreChangedEventArgs(TotalScore, 0, squaresDestroyedCount));
            GridChangedEvent?.Invoke(this, TetrisBox.GetGridChangedEventArgs());
        }

        /// <summary>
        /// Pauses game.
        /// </summary>
        public void Pause()
        {
            this.GamePaused = true;
            levelStopwatch.Stop();
        }
        
        /// <summary>
        /// According to given control, tells the tetris box what to do.
        /// </summary>
        /// <param name="control"></param>
        public void ProcessControl(ControlsEnum control)
        {
            switch (control)
            {
                case ControlsEnum.Up:
                    TetrisBox.MoveInDirection(DirectionEnum.Up);
                    break;
                case ControlsEnum.Down:
                    TetrisBox.MoveInDirection(DirectionEnum.Down);
                    break;
                case ControlsEnum.Right:
                    TetrisBox.MoveInDirection(DirectionEnum.Right);
                    break;
                case ControlsEnum.Left:
                    TetrisBox.MoveInDirection(DirectionEnum.Left);
                    break;
                case ControlsEnum.Drop:
                    TetrisBox.DropPiece();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Tells tetris box to move piece in current direction.
        /// </summary>
        public void ProcessTimerTick()
        {
            TetrisBox.Move();
        }

        /// <summary>
        /// Restores state after deserialization.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        public void SetAfterDeserialization(StreamingContext context)
        {
            this.levelStopwatch = new System.Diagnostics.Stopwatch();
            TetrisBox.ViewChanged += tetrisBox_ViewChanged;
            TetrisBox.PiecePlaced += tetrisBox_PiecePlaced;
        }

        /// <summary>
        /// Saves the stopwatch state before serialization.
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing]
        public void SetBeforeSerialization(StreamingContext context)
        {
            currentLevelDuration -= (int)levelStopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Occures when current piece was placed in tetris box.
        /// Checks if game is over.
        /// Computes score.
        /// Restores views.
        /// Checks if level is over - can set settings for another level.
        /// Adds next piece to tetris box and gets next piece and next direction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tetrisBox_PiecePlaced(object sender, PiecePlacedEventArgs e)
        {
            if (e.GameOver)
            {
                GameOver = true;
                levelStopwatch.Stop();
                GameOverEvent(this, new GameOverEventArgs(TotalScore, false));
                return;
            }
            else if (e.PieceWentThrough)
            {
                int newScore = scoreProvider.GetScorePieceThrough();
                TotalScore += newScore;
                ScoreChangedEvent?.Invoke(this, new ScoreChangedEventArgs(TotalScore, newScore, squaresDestroyedCount));
            }
            else
            {
                int newScore = scoreProvider.GetScore(e.SquaresDestroyed) + scoreProvider.GetScore(e.Piece);
                TotalScore += newScore;
                squaresDestroyedCount += e.SquaresDestroyed;
                ScoreChangedEvent?.Invoke(this, new ScoreChangedEventArgs(TotalScore, newScore, squaresDestroyedCount));
            }

            //next level check
            if (levelStopwatch.ElapsedMilliseconds > currentLevelDuration)
            {
                if (!levelProvider.HasNextLevel())
                {
                    if (levelProvider.ContinueAfterLastLevel)
                    {
                        levelStopwatch.Restart();
                        levelStopwatch.Stop(); //stops on small time that will be smaller than levelduration
                    }
                    else
                    {
                        GameOver = true;
                        levelStopwatch.Stop();
                        GameOverEvent(this, new GameOverEventArgs(TotalScore, true));
                        return;
                    }
                }
                else
                {
                    setSettings(levelProvider.GetNextLevelSettings());
                    levelStopwatch.Restart();
                }
            }

            TetrisBox.AddNewPiece(nextPiece, nextDirection);
            nextPiece = pieceGenerator.GetNextPiece();
            nextDirection = pieceGenerator.GetNextDirection();
            NextPieceChangedEvent(this, new NextPieceChangedEventArgs(nextPiece, nextDirection));
        }

        /// <summary>
        /// Sets the next level.
        /// </summary>
        /// <param name="settings"></param>
        private void setSettings(ILevelSettings settings)
        {
            this.scoreProvider = settings.ScoreProvider;
            this.currentLevelDuration = settings.Duration;
            TimerIntervalChanged(this, new TimerIntervalChangedEventArgs(settings.PieceShiftTime));
        }

        /// <summary>
        /// If view of tetris box changes, invokes own event to report changed view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tetrisBox_ViewChanged(object sender, GridChangedEventArgs e)
        {
            GridChangedEvent?.Invoke(sender, e);
        }
    }
}
