using System;
using System.Windows.Forms;
using TetrisControlProject.Interfaces;
using Tetris4D.Views;
using TetrisControlProject.Enums;

namespace Tetris4D.Forms
{
    /// <summary>
    /// The grid, next piece and score are displayed in this form.
    /// Communicates with the game controller.
    /// Reacts on keyboard inputs.
    /// Can start, end, pause and unpause the game.
    /// </summary>
    public partial class GameForm : Form
    {
        private ViewController viewController; //handles views of this form
        private Keys keyHold; //which key is being held
        private int holdTime; //for how many miliseconds the key is being held

        /// <summary>
        /// The game controller.
        /// </summary>
        public ITetrisController Controller { get; }

        /// <summary>
        /// Tells if the game ended.
        /// </summary>
        public bool IsGameOver
        {
            get { return Controller.GameOver; }
        }
        
        /// <summary>
        /// Tells if the current game progress is saved.
        /// </summary>
        public bool IsSaved { get; set; }

        /// <summary>
        /// Keyboard settings.
        /// </summary>
        public IKeysSettings KeysSettings { get; set; }

        /// <summary>
        /// A constructor.
        /// Creates viewController.
        /// Sets the game controller.
        /// </summary>
        /// <param name="controller"></param>
        public GameForm(ITetrisController controller)
        {
            InitializeComponent();
            initMessages();

            this.Controller = controller;
            this.viewController = new ViewController(controller, pbTetrisGrid, pbNextPiece, pbScore, lblNewScore, btMenu);

            controller.GameOverEvent += controller_GameOver;
            controller.TimerIntervalChanged += controller_TimerIntervalChanged;

            viewController.RecomputeComponentsProportions(this.ClientSize.Height, this.ClientSize.Width);

            this.Visible = true;
            this.CenterToParent();
        }
        
        /// <summary>
        /// Tells controller to start the game.
        /// Starts the keyListener timer and pieceShiftTimer.
        /// </summary>
        public void StartGame()
        {
            Controller.StartGame();
            keyListener.Start();
            pieceShiftTimer.Start();
        }

        /// <summary>
        /// Stops the timers.
        /// </summary>
        public void EndGame()
        {
            keyListener.Stop();
            pieceShiftTimer.Stop();
        }

        /// <summary>
        /// Pause the controller.
        /// Stops the timers.
        /// </summary>
        public void Pause()
        {
            Controller.Pause();
            viewController.setPausedView();
            this.pieceShiftTimer.Stop();
            this.keyListener.Stop();
        }

        /// <summary>
        /// Unpauses the controller.
        /// Starts the timers.
        /// Shows the form.
        /// </summary>
        public void Unpause()
        {
            this.viewController.setUnpausedView();
            this.keyListener.Start();
            this.pieceShiftTimer.Start();
            this.Show();
            Controller.Unpause();
        }

        /// <summary>
        /// Sets high score to the viewController.
        /// </summary>
        /// <param name="score"></param>
        public void SetHighScore(int score)
        {
            this.viewController.HighScore = score;
        }

        /// <summary>
        /// Returns total score achieved in this game so far from the controller.
        /// </summary>
        /// <returns></returns>
        public int GetTotalScore()
        {
            return this.Controller.TotalScore;
        }

        /// <summary>
        /// Initializes texts for this form.
        /// </summary>
        private void initMessages()
        {
            this.Text = Messages.GameForm_GameFormText;
        }

        /// <summary>
        /// When interval for the timer changes in controller, it tells it to the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controller_TimerIntervalChanged(object sender, TetrisControlProject._EventArgs.TimerIntervalChangedEventArgs e)
        {
            this.pieceShiftTimer.Interval = e.NewInterval;
        }

        /// <summary>
        /// If controller tells the game is over, it ends the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controller_GameOver(object sender, TetrisControlProject._EventArgs.GameOverEventArgs e)
        {
            EndGame();
        }

        /// <summary>
        /// Invalidate the form after view change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewController_ViewChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        /// <summary>
        /// When keyListener timer ticks, checks if any key is held and tells the controller to process this keypress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyListener_Tick(object sender, EventArgs e)
        {
            if (keyHold != default(Keys) && (holdTime == 0 || holdTime > 1000))
            {
                if (keyHold == KeysSettings.DownKey)
                    Controller.ProcessControl(ControlsEnum.Down);
                else if (keyHold == KeysSettings.UpKey)
                    Controller.ProcessControl(ControlsEnum.Up);
                else if (keyHold == KeysSettings.RightKey)
                    Controller.ProcessControl(ControlsEnum.Right);
                else if (keyHold == KeysSettings.LeftKey)
                    Controller.ProcessControl(ControlsEnum.Left);
                else if (keyHold == KeysSettings.DropKey)
                    Controller.ProcessControl(ControlsEnum.Drop);
                else throw new NotImplementedException();

                holdTime += keyListener.Interval;
            }
        }

        /// <summary>
        /// If the pieceShiftTimer ticks, it tells it to the controller.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pieceShiftTimer_Tick(object sender, EventArgs e)
        {
            Controller.ProcessTimerTick();
        }

        /// <summary>
        /// Processes key press.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == KeysSettings.PauseKey)
            {
                if (!Controller.GameOver)
                {
                    if (Controller.GamePaused)
                        Unpause();
                    else
                        Pause();
                }
            }
            else if (keyData == KeysSettings.UpKey ||
                keyData == KeysSettings.DownKey ||
                keyData == KeysSettings.RightKey ||
                keyData == KeysSettings.LeftKey ||
                keyData == KeysSettings.DropKey)
            {
                keyHold = keyData;
                holdTime = 0;
            }

            return true;
        }

        /// <summary>
        /// processes keyUp event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            Keys key = (Keys)e.KeyData;
            if (key == keyHold)
            {
                keyHold = default(Keys);
            }
        }

        /// <summary>
        /// When size is changed, it tells the viewController to resize its components.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_ClientSizeChanged(object sender, EventArgs e)
        {
            if (this.ClientSize.Height != 0 && this.ClientSize.Width != 0)
                viewController.RecomputeComponentsProportions(this.ClientSize.Height, this.ClientSize.Width);
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// If the form is minimized, the game is paused.
        /// If the form is resized, tells the viewController to resize its components.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Pause();
            }
            else
            {
                viewController.RecomputeComponentsProportions(this.ClientSize.Height, this.ClientSize.Width);
            }
        }

    }
}
