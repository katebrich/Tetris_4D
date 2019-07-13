using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TetrisControlProject;
using TetrisControlProject.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tetris4D.Forms
{
    /// <summary>
    /// Controls the game window, sets its properties.
    /// Starts new game.
    /// Saves current game progress, loads saved games.
    /// Handles saving score and showing best score table.
    /// Handles keyboard settings.
    /// Is hidden when game form is visible and is active when game form is closed.
    /// </summary>
    public partial class MenuForm : Form
    {       
        private IKeysSettings keysSettings;
        private GameForm gameForm;
        private IScoreStorage scoreStorage;

        private string scorePath = "score.Tetris4D";
        private string keysSettingsPath = "keysSettings.Tetris4D";

        private bool isSaved; //tells if state of the current game is saved

        private int squareSide = 15;
        private int heightSquaresCount = 45;
        private int widthSquaresCount = 33;

        /// <summary>
        /// A constructor.
        /// Deserializes score and keyboard settings.
        /// Creates view of this form and adjusts the size.
        /// </summary>
        public MenuForm()
        {
            InitializeComponent();
            initMessages();
            scoreStorage = deserializeScoreStorage(scorePath);
            keysSettings = deserializeKeysSettings(keysSettingsPath);
            this.pbView.Image = createView();
            this.ClientSize = new Size(pbView.Image.Width, pbView.Image.Height);
            this.CenterToScreen();
            this.Icon = createIcon();
        }

        /// <summary>
        /// Sets texts for this form.
        /// </summary>
        private void initMessages()
        {
            this.Text = Messages.MenuForm_MenuFormText;
            btContinue.Text = Messages.MenuForm_BtContinueText;
            btNewGame.Text = Messages.MenuForm_BtNewGameText;
            btLoadGame.Text = Messages.MenuForm_BtLoadGameText;
            btSaveGame.Text = Messages.MenuForm_BtSaveGameText;
            btShowScore.Text = Messages.MenuForm_BtShowScoreText;
            btControls.Text = Messages.MenuForm_BtControlsText;
            btQuit.Text = Messages.MenuForm_BtQuitText;
        }

        private Icon createIcon()
        {
            Bitmap bmp = TetrisSquareImage.GetTetrisSquareImage(Color.OrangeRed, squareSide, squareSide);
            return Icon.FromHandle(bmp.GetHicon());
        }

        #region Creating view

        /// <summary>
        /// Creates view of this form.
        /// </summary>
        /// <returns></returns>
        private Bitmap createView()
        {
            Bitmap formViewBitmap;

            pbView.Height = squareSide * heightSquaresCount;
            pbView.Width = squareSide * widthSquaresCount;
            pbView.Location = new Point(0, 0);
            formViewBitmap = new Bitmap(squareSide * widthSquaresCount, squareSide * heightSquaresCount);

            drawBackground(Color.Black, formViewBitmap);
            drawTetrisSign(formViewBitmap);
            drawPieces(formViewBitmap);
            setButtons();
            drawButtonsEdges(formViewBitmap);

            return formViewBitmap;
        }

        /// <summary>
        /// A helper struct for creating view.
        /// </summary>
        private struct PointsColorPair
        {
            public List<Point> Points;
            public Bitmap ColorSquare;

            public PointsColorPair(List<Point> points, Bitmap colorSquare)
            {
                this.Points = points;
                this.ColorSquare = colorSquare;
            }
        }

        /// <summary>
        /// Draws pieces displayed on the background of the form.
        /// </summary>
        /// <param name="formViewBitmap"></param>
        private void drawPieces(Bitmap formViewBitmap)
        {
            List<PointsColorPair> toDraw = new List<PointsColorPair>();

            List<Point> points = new List<Point> {
                new Point(35,0),
                new Point(37,0),
                new Point(39,0),
                new Point(41,0),
                new Point(31,31),
                new Point(33,31),
                new Point(35,31),
                new Point(37,31),
                };
            toDraw.Add(new PointsColorPair(points,
                TetrisSquareImage.GetTetrisSquareImage(Color.MediumTurquoise, 2*squareSide, 2*squareSide)));

            points = new List<Point> {
                new Point(43,0),
                new Point(43,2),
                new Point(41,2),
                new Point(41,4)
                };
            toDraw.Add(new PointsColorPair(points,
                TetrisSquareImage.GetTetrisSquareImage(Color.Red, 2 * squareSide, 2 * squareSide)));

            points = new List<Point> {
                new Point(35,29),
                new Point(39,27),
                new Point(37,29),
                new Point(39,29)
                };
            toDraw.Add(new PointsColorPair(points,
                TetrisSquareImage.GetTetrisSquareImage(Color.Yellow, 2 * squareSide, 2 * squareSide)));

            points = new List<Point> {
                new Point(37,2),
                new Point(39,2),
                new Point(39, 4),
                new Point(39,6)
                };
            toDraw.Add(new PointsColorPair(points,
                TetrisSquareImage.GetTetrisSquareImage(Color.OrangeRed, 2 * squareSide, 2 * squareSide)));
                
            points = new List<Point> {
                new Point(41,25),
                new Point(43,25),
                new Point(43, 27),
                new Point(41,27)
                };
            toDraw.Add(new PointsColorPair(points,
                TetrisSquareImage.GetTetrisSquareImage(Color.Blue, 2 * squareSide, 2 * squareSide)));

            points = new List<Point> {
                new Point(41, 29),
                new Point(41,31),
                new Point(43,31),
                new Point(39,31)
                };
            toDraw.Add(new PointsColorPair(points,
                TetrisSquareImage.GetTetrisSquareImage(Color.Purple, 2 * squareSide, 2 * squareSide)));

            points = new List<Point> {
                new Point(31,0),
                new Point(33,0),
                new Point(31,2),
                new Point(29,2)
                };
            toDraw.Add(new PointsColorPair(points,
                TetrisSquareImage.GetTetrisSquareImage(Color.LimeGreen, 2 * squareSide, 2 * squareSide)));


            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                foreach (var pair in toDraw)
                {
                    foreach (var point in pair.Points)
                    {
                        grfx.DrawImage(pair.ColorSquare, point.Y * squareSide, point.X * squareSide, 2*squareSide, 2*squareSide);

                    }
                }
            }

        }

        /// <summary>
        /// Draws colourful squares on the sides of the buttons.
        /// </summary>
        /// <param name="formViewBitmap"></param>
        private void drawButtonsEdges(Bitmap formViewBitmap)
        {
            List<PointsColorPair> toDraw = new List<PointsColorPair>();

            List<Point> points = new List<Point> {
                new Point(16,10),
                new Point(17,10),
                new Point(18,10),
                new Point(16,22),
                new Point(17,22),
                new Point(18,22)};
            toDraw.Add(new PointsColorPair(points,
                TetrisSquareImage.GetTetrisSquareImage(Color.Yellow, squareSide, squareSide)));
            //btContinue.ForeColor = Color.OrangeRed;

            points = new List<Point>
            {
                new Point(20,10),
                new Point(21,10),
                new Point(22,10),
                new Point(20,22),
                new Point(21,22),
                new Point(22,22),
            };
            toDraw.Add(new PointsColorPair(points,
                            TetrisSquareImage.GetTetrisSquareImage(Color.OrangeRed, squareSide, squareSide)));
            //btNewGame.ForeColor = Color.LimeGreen;

            points = new List<Point>
            {
                new Point(24,10),
                new Point(25,10),
                new Point(26,10),
                new Point(24,22),
                new Point(25,22),
                new Point(26,22),
            };
            toDraw.Add(new PointsColorPair(points,
                            TetrisSquareImage.GetTetrisSquareImage(Color.Red, squareSide, squareSide)));
            //btLoadGame.ForeColor = Color.Yellow;

            points = new List<Point>
            {
                new Point(28,10),
                new Point(29,10),
                new Point(30,10),
                new Point(29,22),
                new Point(30,22),
                new Point(28,22),
            };
            toDraw.Add(new PointsColorPair(points,
                            TetrisSquareImage.GetTetrisSquareImage(Color.Purple, squareSide, squareSide)));
            //btSaveGame.ForeColor = Color.MediumTurquoise;

            points = new List<Point>
            {
                new Point(32,10),
                new Point(33,10),
                new Point(34,10),
                new Point(32,22),
                new Point(33,22),
                new Point(34,22),
            };
            toDraw.Add(new PointsColorPair(points,
                            TetrisSquareImage.GetTetrisSquareImage(Color.Blue, squareSide, squareSide)));
            //btShowScore.ForeColor = Color.Purple;

            points = new List<Point>
            {
                new Point(36,10),
                new Point(37,10),
                new Point(38,10),
                new Point(36,22),
                new Point(37,22),
                new Point(38,22)
            };
            toDraw.Add(new PointsColorPair(points,
                            TetrisSquareImage.GetTetrisSquareImage(Color.MediumTurquoise, squareSide, squareSide)));
            //btControls.ForeColor = Color.Blue;

            points = new List<Point>
            {
                new Point(40,10),
                new Point(41,10),
                new Point(42,10),
                new Point(40,22),
                new Point(41,22),
                new Point(42,22)
            };
            toDraw.Add(new PointsColorPair(points,
                            TetrisSquareImage.GetTetrisSquareImage(Color.LimeGreen, squareSide, squareSide)));
            //btQuit.ForeColor = Color.Red;

            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                foreach (var pair in toDraw)
                {
                    foreach (var point in pair.Points)
                    {
                        grfx.DrawImage(pair.ColorSquare, point.Y * squareSide, point.X * squareSide, squareSide, squareSide);

                    }
                }
            }
        }

        /// <summary>
        /// Sets buttons locations, sizes and fonts.
        /// </summary>
        private void setButtons()
        {
            btContinue.Location = new Point(11 * squareSide, 16 * squareSide);
            btNewGame.Location = new Point(11 * squareSide, 20 * squareSide);
            btLoadGame.Location = new Point(11 * squareSide, 24 * squareSide);
            btSaveGame.Location = new Point(11 * squareSide, 28 * squareSide);
            btShowScore.Location = new Point(11 * squareSide, 32 * squareSide);
            btControls.Location = new Point(11 * squareSide, 36 * squareSide);
            btQuit.Location = new Point(11 * squareSide, 40 * squareSide);

            Size size = new Size(11 * squareSide, 3 * squareSide);
            btContinue.Size = size;
            btNewGame.Size = size;
            btLoadGame.Size = size;
            btSaveGame.Size = size;
            btShowScore.Size = size;
            btControls.Size = size;
            btQuit.Size = size;

            System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection();
            fonts.AddFontFile("ShowcardGothicFont.ttf");
            Font font = new Font(fonts.Families[0], btContinue.Height / 2.8f);
            btContinue.Font = font;
            btNewGame.Font = font;
            btLoadGame.Font = font;
            btSaveGame.Font = font;
            btShowScore.Font = font;
            btControls.Font = font;
            btQuit.Font = font;
        }

        /// <summary>
        /// Fills the bitmap with one color.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="formViewBitmap"></param>
        private void drawBackground(Color color, Bitmap formViewBitmap)
        {
            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                grfx.FillRectangle(new SolidBrush(color), 0, 0, formViewBitmap.Width, formViewBitmap.Height);
            }
        }

        /// <summary>
        /// Draws the Tetris sign on the bitmap.
        /// </summary>
        /// <param name="formViewBitmap"></param>
        private void drawTetrisSign(Bitmap formViewBitmap)
        {
            List<Point> toDraw = new List<Point> {
                new Point(2,2),
                new Point(2,3),
                new Point(2,4),
                new Point(2,5),
                new Point(2,6),
                new Point(2,8),
                new Point(2,9),
                new Point(2,10),
                new Point(2,11),
                new Point(2,13),
                new Point(2,14),
                new Point(2,15),
                new Point(2,16),
                new Point(2,17),
                new Point(2,19),
                new Point(2,20),
                new Point(2,21),
                new Point(2,24),
                new Point(2,27),
                new Point(2,28),
                new Point(2,29),
                new Point(3,4),
                new Point(3,8),
                new Point(3,15),
                new Point(3,19),
                new Point(3,22),
                new Point(3,24),
                new Point(3,26),
                new Point(4,4),
                new Point(4,8),
                new Point(4,9),
                new Point(4,10),
                new Point(4,15),
                new Point(4,19),
                new Point(4,20),
                new Point(4,21),
                new Point(4,24),
                new Point(4,27),
                new Point(4,28),
                new Point(5,4),
                new Point(5,8),
                new Point(5,15),
                new Point(5,19),
                new Point(5,22),
                new Point(5,24),
                new Point(5,29),
                new Point(6,4),
                new Point(6,8),
                new Point(6,9),
                new Point(6,10),
                new Point(6,11),
                new Point(6,15),
                new Point(6,19),
                new Point(6,22),
                new Point(6,24),
                new Point(6,26),
                new Point(6,27),
                new Point(6,28),
            };

            Bitmap squareImage = TetrisSquareImage.GetTetrisSquareImage(Color.Gray, squareSide, squareSide);


            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                foreach (var item in toDraw)
                {
                    grfx.DrawImage(squareImage, item.Y * squareSide, item.X * squareSide, squareSide, squareSide);
                }

                //draw "irections" string
                System.Drawing.Text.PrivateFontCollection privateFonts = new System.Drawing.Text.PrivateFontCollection();
                privateFonts.AddFontFile("MistralFont.ttf");
                Font font = new Font(privateFonts.Families[0], squareSide * 2.5f);
                Brush brush = Brushes.Gainsboro;
                grfx.TranslateTransform(20 * squareSide, 11 * squareSide);
                grfx.RotateTransform(-25);
                SizeF textSize = grfx.MeasureString("irections", font);

                grfx.DrawString("irections", font, brush, new Point(0, 0));
            }

            //draw colourful 4D
            List<PointsColorPair> pairs = new List<PointsColorPair>();

            pairs.Add(new PointsColorPair(
                new List<Point>
                {
                    new Point(8, 12),
                    new Point(9, 12),
                    new Point(10, 12),
                    new Point(11, 12),
                    new Point(11, 13),
                },
                TetrisSquareImage.GetTetrisSquareImage(Color.Gray, squareSide, squareSide)//Color.MediumTurquoise, squareSide, squareSide)
            ));

            pairs.Add(new PointsColorPair(
                new List<Point>
                {
                new Point(10, 14),
                new Point(11, 14),
                 new Point(12, 14),
                 new Point(13, 14),
                  new Point(11, 15),
                },
                TetrisSquareImage.GetTetrisSquareImage(Color.Gray, squareSide, squareSide)//Color.LimeGreen, squareSide, squareSide)
            ));

            pairs.Add(new PointsColorPair(
                new List<Point>
                {
                    new Point(8, 17),
                     new Point(9, 17),
                      new Point(10, 17),                    
                new Point(11, 17),
                new Point(12, 17),
                 new Point(13, 17),
                new Point(13, 18),
                 new Point(8, 18),
                },
                TetrisSquareImage.GetTetrisSquareImage(Color.Gray, squareSide, squareSide)//Color.Yellow, squareSide, squareSide)
            ));

            pairs.Add(new PointsColorPair(
                new List<Point>
                {
                new Point(8, 19),
                new Point(9, 20),
                new Point(10, 20),
                new Point(11, 20),
                new Point(12, 20),
                new Point(13, 19)
                },
                TetrisSquareImage.GetTetrisSquareImage(Color.Gray, squareSide, squareSide)//Color.OrangeRed, squareSide, squareSide)
            ));

            using (Graphics grfx = Graphics.FromImage(formViewBitmap))
            {
                foreach (var pair in pairs)
                {
                    foreach (var point in pair.Points)
                    {
                        grfx.DrawImage(pair.ColorSquare, point.Y * squareSide, point.X * squareSide, squareSide, squareSide);
                    }
                }
            }
        }

        # endregion Creating view

        /// <summary>
        /// Checks if there is unsaved game and asks for saving it.
        /// Ends current game.
        /// Initializes and starts new game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btNewGame_Click(object sender, EventArgs e)
        {
            if (gameForm != null)
            {
                if (!isSaved && !gameForm.IsGameOver)
                {
                    if (new TetrisMessageBox(Messages.WarningGameNotSaved, Messages.GameNotSaved, TetrisMesBoxStyleEnum.Warning, TetrisMesBoxButtonsEnum.NoYes).ShowDialog()
                        != DialogResult.OK)
                    {
                        return;
                    }
                }
                gameForm.EndGame();
                gameForm.Close();
            }
            initGame(new Tetris4DController());
            gameForm.StartGame();
        }

        /// <summary>
        /// Creates new game form with given controller.
        /// Sets properties of the game form needed for starting the game.
        /// Shows the game form and hides itself.
        /// </summary>
        /// <param name="controller"></param>
        private void initGame(ITetrisController controller)
        {
            gameForm = new GameForm(controller);
            gameForm.Icon = this.Icon;
            gameForm.KeysSettings = keysSettings;
            gameForm.VisibleChanged += gameForm_VisibleChanged;
            gameForm.FormClosed += gameForm_FormClosed;
            gameForm.FormClosing += gameForm_FormClosing;
            gameForm.SetHighScore(scoreStorage.GetHighScore().Score);
            gameForm.Show();
            this.Hide();
            isSaved = false;
        }

        /// <summary>
        /// Warns if there is unsaved game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gameForm != null && !isSaved)
            {
                if (new TetrisMessageBox(Messages.WarningGameNotSaved, Messages.GameNotSaved, TetrisMesBoxStyleEnum.Warning, TetrisMesBoxButtonsEnum.NoYes).ShowDialog()
                        != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// If game is not over yet, instead of closing the game form only pauses and hides it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!gameForm.IsGameOver)
            {
                gameForm.Pause();
                gameForm.Hide();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Disables Continue button. Adds new score. Shows menu form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            btContinue.Enabled = false;
            if (gameForm.IsGameOver)
                addNewScore();
            this.Show();
            gameForm = null;
        }

        /// <summary>
        /// If game form hides, it shows menu form and enables Continue button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!gameForm.Visible)
            {
                this.Show();
                btContinue.Enabled = true;
            }
        }

        /// <summary>
        /// Continues the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btContinue_Click(object sender, EventArgs e)
        {
            gameForm.Unpause();
            this.Hide();
            isSaved = false;
        }

        /// <summary>
        /// Displays form that shows score table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btShowScore_Click(object sender, EventArgs e)
        {
            Form scoreForm = new Forms.Top10ScoreViewForm(scoreStorage.sortedScoreSet);
            scoreForm.Icon = this.Icon;
            scoreForm.ShowDialog();
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles saving current game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            if (gameForm == null)
                new TetrisMessageBox(Messages.WarningNoGameToSave, Messages.NoGameToSave, TetrisMesBoxStyleEnum.Info, TetrisMesBoxButtonsEnum.OK).ShowDialog();
            else
            {
                try
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.AddExtension = true;
                    dialog.DefaultExt = "tetris4D";
                    dialog.Filter = "Tetris4D Files |*.tetris4D";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        bool success = serializeController(gameForm.Controller, dialog.FileName);
                        if (!success)
                        {
                            new TetrisMessageBox(Messages.ErrorSavingGame, Messages.GameNotSaved, TetrisMesBoxStyleEnum.Error, TetrisMesBoxButtonsEnum.OK).ShowDialog();
                            return;
                        }
                        isSaved = true;
                        gameForm.IsSaved = true;
                        new TetrisMessageBox(Messages.InfoGameSaved, Messages.GameSaved, TetrisMesBoxStyleEnum.Info, TetrisMesBoxButtonsEnum.OK).ShowDialog();
                    }
                }
                catch
                {
                    new TetrisMessageBox(Messages.ErrorSavingGame, Messages.GameNotSaved, TetrisMesBoxStyleEnum.Error, TetrisMesBoxButtonsEnum.OK).ShowDialog();
                }
            }
        }

        /// <summary>
        /// Handles loading saved game.
        /// Warns if there is unsaved game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLoadGame_Click(object sender, EventArgs e)
        {
            if (gameForm != null)
            {
                if (!isSaved && !gameForm.IsGameOver)
                {
                    if (new TetrisMessageBox(Messages.WarningGameNotSaved, Messages.GameNotSaved, TetrisMesBoxStyleEnum.Warning, TetrisMesBoxButtonsEnum.NoYes).ShowDialog()
                        != DialogResult.OK)
                    {
                        return;
                    }

                }
                gameForm.EndGame();
                gameForm.Close();
            }

            ITetrisController controller;
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = "tetris4D";
                dialog.Filter = "Tetris4D Files |*.tetris4D";

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    bool success = deserializeController(dialog.FileName, out controller);
                    if (!success)
                    {
                        new TetrisMessageBox(Messages.ErrorLoadingGame, Messages.GameNotLoaded, TetrisMesBoxStyleEnum.Error, TetrisMesBoxButtonsEnum.OK).ShowDialog();
                        return;
                    }
                    initGame(controller);
                    gameForm.Unpause();
                    gameForm.Pause();
                }
            }
            catch
            {
                new TetrisMessageBox(Messages.ErrorLoadingGame, Messages.GameNotLoaded, TetrisMesBoxStyleEnum.Error, TetrisMesBoxButtonsEnum.OK).ShowDialog();
            }
        }

        /// <summary>
        /// Shows form to change keyboard settings. If settings are changed, saves the change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btControls_Click(object sender, EventArgs e)
        {
            IKeysSettings newSettings;
            ControlsForm form = new ControlsForm(keysSettings);
            if (form.ShowDialog() == DialogResult.OK)
            {
                newSettings = form.GetNewSettings();

                //any change
               if (newSettings.UpKey != keysSettings.UpKey ||
                   newSettings.DownKey != keysSettings.DownKey ||
                   newSettings.LeftKey != keysSettings.LeftKey ||
                   newSettings.RightKey != keysSettings.RightKey ||
                   newSettings.DropKey != keysSettings.DropKey ||
                   newSettings.PauseKey != keysSettings.PauseKey
                   )
                {
                    keysSettings = newSettings;
                    if (gameForm != null)
                        gameForm.KeysSettings = newSettings;
                    serializeKeysSettings(newSettings, keysSettingsPath);
                }
            }
        }

        /// <summary>
        /// Shows form that asks clients name.
        /// Saves score to the storage.
        /// Serializes the storage.
        /// </summary>
        private void addNewScore()
        {
            Forms.AskNameForm nameForm = new Forms.AskNameForm();
            nameForm.Icon = this.Icon;
            if (nameForm.ShowDialog() == DialogResult.OK)
            {
                scoreStorage.AddScore(new ScoreItem(gameForm.GetTotalScore(), nameForm._Name, DateTime.Now));
            }
            nameForm.Close();

            serializeScoreStorage(scoreStorage, scorePath);
        }

        #region Serialization

        /// <summary>
        /// Serializes given IScoreStorage instance to the given path using MyFormatter.
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="path"></param>
        private void serializeScoreStorage(IScoreStorage storage, string path)
        {
            bool succeeded = false;
            string tempPath = path + "temp";
            //FileStream fs = new FileStream(tempPath, FileMode.Create);
            TextWriter writer = new StreamWriter(tempPath);

            try
            {
                //BinaryFormatter formatter = new BinaryFormatter();
                MyFormatter formatter = new MyFormatter();
                formatter.Serialize(writer, storage);
                succeeded = true;
            }
            catch (Exception)
            {
                new TetrisMessageBox(Messages.ErrorSavingScores, Messages.Error, TetrisMesBoxStyleEnum.Error, TetrisMesBoxButtonsEnum.OK).Show();
            }
            finally
            {
                writer.Close();
                //fs.Close();
                if (succeeded)
                {
                    File.Delete(path);
                    File.Move(tempPath, path);
                }
                File.Delete(tempPath);
            }
        }

        /// <summary>
        /// Returns IScoreStorage deserialized from the given path. Uses MyFormatter.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IScoreStorage deserializeScoreStorage(string path)
        {
            IScoreStorage storage = null;

            if (!File.Exists(path))
                return new BasicScoreStorage();

            //FileStream fs = new FileStream(path, FileMode.Open);
            TextReader reader = new StreamReader(path);

            try
            {
                //BinaryFormatter formatter = new BinaryFormatter();
                MyFormatter formatter = new MyFormatter();
                storage = (IScoreStorage)formatter.Deserialize(reader);
            }
            catch
            {
                new TetrisMessageBox(Messages.ErrorLoadingScores, Messages.Error, TetrisMesBoxStyleEnum.Error, TetrisMesBoxButtonsEnum.OK).Show();
            }
            finally
            {
                reader.Close();
                //fs.Close();
            }
            return storage;
        }

        /// <summary>
        /// Serializes given IKeysSettings instance to the given path using MyFormatter.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="path"></param>
        private void serializeKeysSettings(IKeysSettings settings, string path)
        {
            bool succeeded = false;
            string tempPath = path + "temp";
            //FileStream fs = new FileStream(tempPath, FileMode.Create);
            TextWriter writer = new StreamWriter(tempPath);
            try
            {
                //BinaryFormatter formatter = new BinaryFormatter();
                MyFormatter formatter = new MyFormatter();
                formatter.Serialize(writer, settings);
                succeeded = true;
            }
            catch (Exception)
            {
            }
            finally
            {
                writer.Close();
                //fs.Close();
                if (succeeded)
                {
                    File.Delete(path);
                    File.Move(tempPath, path);
                }
                File.Delete(tempPath);
            }
        }

        /// <summary>
        /// Returns IKeysSettings deserialized from the given path. Uses MyFormatter.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IKeysSettings deserializeKeysSettings(string path)
        {
            IKeysSettings settings = null;

            if (!File.Exists(path))
                return new UserKeysSettings();

            //FileStream fs = new FileStream(path, FileMode.Open);
            TextReader reader = new StreamReader(path);

            try
            {
                //BinaryFormatter formatter = new BinaryFormatter();
                MyFormatter formatter = new MyFormatter();
                settings = (IKeysSettings)formatter.Deserialize(reader);
                if (settings == null)
                    return new UserKeysSettings();
            }
            catch
            {
                return new UserKeysSettings();
            }
            finally
            {
                reader.Close();
                //fs.Close();
            }
            return settings;
        }

        /// <summary>
        /// Serializes given ITetrisController instance to the given path using MyFormatter.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool serializeController(ITetrisController controller, string path)
        {
            bool succeeded = false;
            string tempPath = path + "temp";
            TextWriter writer = new StreamWriter(tempPath);
            try
            {
                MyFormatter formatter = new MyFormatter();
                formatter.Serialize(writer, controller);
                succeeded = true;
            }
            catch (Exception)
            {
                succeeded = false;
            }
            finally
            {
                writer.Close();
                if (succeeded)
                {
                    File.Delete(path);
                    File.Move(tempPath, path);
                }
                File.Delete(tempPath);
            }
            return succeeded;
        }

        /// <summary>
        /// Deserializes ITetrisController from the given path. Uses MyFormatter.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contr"></param>
        /// <returns></returns>
        private bool deserializeController(string path, out ITetrisController contr)
        {
            TextReader reader = new StreamReader(path);
            bool success = false;

            try
            {
                MyFormatter formatter = new MyFormatter();
                contr = (ITetrisController)formatter.Deserialize(reader);
                success = true;
            }
            catch
            {
                success = false;
                contr = null;
            }
            finally
            {
                reader.Close();
            }
            return success;
        }

        #endregion Serialization
    }
}
