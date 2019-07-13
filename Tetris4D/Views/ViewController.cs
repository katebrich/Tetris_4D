using System;
using TetrisControlProject.Interfaces;
using System.Windows.Forms;
using System.Drawing;
using TetrisControlProject.Helper;

namespace Tetris4D.Views
{
    /// <summary>
    /// Listens to Controller events and changes views apropriatelly.
    /// Sets views of given components.
    /// Sets sizes and properties of these components.
    /// </summary>
    public class ViewController
    {
        //components
        private Button btMenu;
        private PictureBox pbTetrisGrid;
        private PictureBox pbScore;
        private PictureBox pbNextPiece;
        private Label lblNewScore;

        private int highScore;

        //views
        private GridView tetrisGridView;
        private NextPieceView nextPieceView;
        private ScoreView scoreView;

        //timer used for animation of newly achieved score.
        private System.Timers.Timer scoreTextAnimationTimer;
        private int steps; //how many times the label should get bigger = how many times the timer will tick
        private int fontSizeChange; //how much bigger the label with new score should be with each timer tick
        private Font lblNewScoreFont; //initial font used for label with new score

        private Font viewsFont; //font used for all views       

        /// <summary>
        /// Gets or sets the high score.
        /// </summary>
        public int HighScore {
            get { return highScore; }
            set
            {
                scoreView.SetHighScore(value);
                highScore = value;
            }
        }
        
        public event EventHandler ViewChanged;

        /// <summary>
        /// A cnstructor. 
        /// Sets the components.
        /// Creates views.
        /// Assignes the controller events.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="pbTetrisGrid"></param>
        /// <param name="pbNextPiece"></param>
        /// <param name="pbScore"></param>
        /// <param name="lblNewScore"></param>
        /// <param name="btMenu"></param>
        public ViewController(ITetrisController controller, PictureBox pbTetrisGrid, PictureBox pbNextPiece, PictureBox pbScore, Label lblNewScore, Button btMenu)
        {
            this.pbTetrisGrid = pbTetrisGrid;
            this.pbNextPiece = pbNextPiece;
            this.pbScore = pbScore;
            this.lblNewScore = lblNewScore;
            this.btMenu = btMenu;

            this.tetrisGridView = new GridView();
            this.nextPieceView = new NextPieceView();
            this.scoreView = new ScoreView();

            controller.GridChangedEvent += controller_GridChangedEvent;
            controller.NextPieceChangedEvent += controller_NextPieceChanged;
            controller.ScoreChangedEvent += controller_ScoreChangedEvent;
            controller.GameOverEvent += controller_GameOver;           

            initSettings(); //sets the components
        }

        /// <summary>
        /// Recomputes sizes of all views according to the given width and height of window.
        /// </summary>
        /// <param name="windowHeight"></param>
        /// <param name="windowWidth"></param>
        public void RecomputeComponentsProportions(int windowHeight, int windowWidth)
        {
            int upDownOffset = 10;
            int gridSize = Math.Min(windowHeight, windowWidth) - upDownOffset * 2;
            int columnWidth = gridSize / 3;
            int middleSpaceSize = gridSize / 12;
            int leftRightOffset = (windowWidth - gridSize - middleSpaceSize - columnWidth) / 2;

            pbTetrisGrid.Height = gridSize;
            pbTetrisGrid.Width = gridSize;
            pbTetrisGrid.Location = new Point(leftRightOffset, upDownOffset);

            lblNewScore.Width = pbTetrisGrid.Width;
            lblNewScore.Height = pbTetrisGrid.Height;

            int columnXLocation = leftRightOffset + gridSize + middleSpaceSize;
            int columnInnerOffset = windowHeight / 30;
            int columnUpDownOffset = windowHeight / 20;

            pbNextPiece.Location = new Point(columnXLocation, columnUpDownOffset);
            pbNextPiece.Width = columnWidth;
            pbNextPiece.Height = columnWidth - columnInnerOffset;

            btMenu.Width = (int)(columnWidth * (2f / 3f));
            btMenu.Height = btMenu.Width / 4;
            btMenu.Location = new Point(columnXLocation + (columnWidth - btMenu.Width) / 2, windowHeight - 2 * columnUpDownOffset - btMenu.Height);
            btMenu.Font = new Font(btMenu.Font.FontFamily, btMenu.Height / 2);

            pbScore.Location = new Point(columnXLocation, columnUpDownOffset + pbNextPiece.Height + columnInnerOffset);
            pbScore.Width = columnWidth;
            pbScore.Height = windowHeight - pbNextPiece.Height - btMenu.Height - 2 * columnUpDownOffset - 3 * columnInnerOffset;


            ViewChanged?.Invoke(this, null);

        }

        /// <summary>
        /// Sets view for pause.
        /// </summary>
        public void setPausedView()
        {
            pbTetrisGrid.Image = tetrisGridView.GetPausedBitmap();
            ViewChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Sets view when pause is canceled.
        /// </summary>
        public void setUnpausedView()
        {
            pbTetrisGrid.Image = tetrisGridView.GetUnpausedBitmap();
            ViewChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Sets needed properties of all components.
        /// </summary>
        private void initSettings()
        {
            FontFamily fontFamily = new FontFamily("Comic Sans MS");
            viewsFont = new Font(fontFamily, 10, FontStyle.Regular);
            nextPieceView.Font = viewsFont;
            scoreView.Font = viewsFont;
            tetrisGridView.Font = viewsFont;

            btMenu.Text = Messages.ViewController_MenuButtonText;
            btMenu.Font = new Font(this.viewsFont.FontFamily, btMenu.Height / 2);
            btMenu.ForeColor = Color.Gainsboro;

            fontSizeChange = 2;

            lblNewScoreFont = new Font(this.viewsFont.FontFamily, 5);
            lblNewScore.Location = new Point(0, 0);
            lblNewScore.ForeColor = Color.White;
            lblNewScore.BackColor = Color.Transparent;
            lblNewScore.Parent = pbTetrisGrid; //kvuli pruhlednosti labelu
            lblNewScore.BringToFront();
            lblNewScore.AutoSize = false;
            lblNewScore.TextAlign = ContentAlignment.MiddleCenter;

            pbTetrisGrid.SizeMode = PictureBoxSizeMode.StretchImage;
            pbNextPiece.SizeMode = PictureBoxSizeMode.StretchImage;
            pbScore.SizeMode = PictureBoxSizeMode.StretchImage;

            btMenu.BackColor = Color.Black;
            btMenu.ForeColor = Color.White;     
        }

        /// <summary>
        /// Controls controllers GameOver event. Gets new view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controller_GameOver(object sender, TetrisControlProject._EventArgs.GameOverEventArgs e)
        {
            pbTetrisGrid.Image = tetrisGridView.GetGameOverBitmap();
            ViewChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Controls controllers ScoreChangedEvent. Gets new view. 
        /// Animates new score - starts timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controller_ScoreChangedEvent(object sender, TetrisControlProject._EventArgs.ScoreChangedEventArgs e)
        {
            if (e.NewScore != 0)
            {
                lblNewScore.Invoke((MethodInvoker)delegate
                {
                    lblNewScore.Text = e.NewScore.ToString();
                });

                scoreTextAnimationTimer = new System.Timers.Timer();
                lblNewScore.Font = lblNewScoreFont;
                scoreTextAnimationTimer.AutoReset = false;
                steps = 40;
                scoreTextAnimationTimer.Interval = 10;
                scoreTextAnimationTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerEvent);
                scoreTextAnimationTimer.Enabled = true;
            }

            pbScore.Image = scoreView.GetBitmap(pbScore.Height, pbScore.Width, e);
            ViewChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Controls controllers NestPieceChanged event. Gets new view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controller_NextPieceChanged(object sender, TetrisControlProject._EventArgs.NextPieceChangedEventArgs e)
        {
            pbNextPiece.Image = nextPieceView.GetBitmap(pbNextPiece.Height, pbNextPiece.Width, e);
            ViewChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Controls controller GridChangedEvent. Gets new view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controller_GridChangedEvent(object sender, TetrisControlProject._EventArgs.GridChangedEventArgs e)
        {
            pbTetrisGrid.Image = tetrisGridView.GetBitmap(pbTetrisGrid.Width, pbTetrisGrid.Height, e);
            ViewChanged?.Invoke(this, null);
        }

        /// <summary>
        /// With every tick of timer, the label font size gets bigger.
        /// Checks number of steps and if needed, ends the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerEvent(object sender, EventArgs e)
        {
            try
            {
                steps--;
                lblNewScore.SafeInvoke(() =>
                {
                    lblNewScore.Font = new Font("Arial", lblNewScore.Font.Size + fontSizeChange);
                }, false);
            }
            finally
            {
                if (steps > 0)
                    scoreTextAnimationTimer.Start();
                else
                {
                    lblNewScore.SafeInvoke(() =>
                    {
                        lblNewScore.Text = "";
                        lblNewScore.Font = lblNewScoreFont;
                    }, false);
                }
            }
        }
    }
}
