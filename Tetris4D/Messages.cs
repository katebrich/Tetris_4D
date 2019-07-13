using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4D
{
    /// <summary>
    /// Messages for UI.
    /// </summary>
    static class Messages
    {

        //Common
        public const string Common_btOK = "OK";
        public const string Common_btCancel = "Cancel";
        public const string Common_btYes = "Yes";
        public const string Common_btNo = "No";

        //MessageBox texts
        public const string Warning = "Warning";
        public const string OK = "OK";
        public const string Cancel = "Cancel";
        public const string Error = "Error";
        public const string NoGameToSave = "No game to save";
        public const string GameNotSaved = "Game not saved";
        public const string GameSaved = "Game saved";
        public const string GameNotLoaded = "Game not loaded";
        public const string WarningGameNotSaved = "You will lose your progress in game.\nDo you want to continue?";
        public const string WarningNoGameToSave = "There is no started game that can be saved.";
        public const string ErrorSavingScores = "An error occured when saving the score table.";
        public const string ErrorLoadingScores = "An error occured when loading the score table.";
        public const string InfoGameSaved = "Game was successfully saved.";
        public const string ErrorSavingGame = "An error occured when saving the game.";
        public const string ErrorLoadingGame = "An error occured when loading the game.";

        //ScoreView
        public const string ScoreView_HighScoreHeading = "HighScore";
        public const string ScoreView_SquaresHeading = "Squares";

        //GridView
        public const string GridView_GamePaused = "Press P to continue";
        public const string GridView_GameOver = "Game Over";
        public const string GridView_GameOverWin = "You've completed all the levels!";

        //ViewController
        public const string ViewController_MenuButtonText = "Menu";

        //MenuForm
        public const string MenuForm_BtContinueText = "Continue";
        public const string MenuForm_BtNewGameText = "New Game";
        public const string MenuForm_BtLoadGameText = "Load Game";
        public const string MenuForm_BtSaveGameText = "Save Game";
        public const string MenuForm_BtShowScoreText = "Top players";
        public const string MenuForm_BtControlsText = "Controls";
        public const string MenuForm_BtQuitText = "Quit";
        public const string MenuForm_MenuFormText = "Tetris 4 Directions";

        //GameForm
        public const string GameForm_GameFormText = "Tetris 4 Directions";

        //AskNameForm
        public const string AskNameForm_lblQuestion = "Would you like to save your score?";
        public const string AskNameForm_lblEnterName = "Enter your name:";

        //ControlsForm
        public const string ControlsForm_lblHeading = "Keyboard settings";
        public const string ControlsForm_lblUp = "Move Up";
        public const string ControlsForm_lblDown = "Move Down";
        public const string ControlsForm_lblLeft = "Move Left";
        public const string ControlsForm_lblRight = "Move Right";
        public const string ControlsForm_lblDrop = "Drop";
        public const string ControlsForm_lblPause = "Pause";
        public const string ControlsForm_btDefaults = "Reset Defaults";
        public const string ControlsForm_btConfirm = "Confirm";

    }
}
