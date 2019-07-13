using System;
using System.Windows.Forms;
using TetrisControlProject.Interfaces;

namespace Tetris4D
{
    /// <summary>
    /// Represents keyboard settings used in game.
    /// </summary>
    [Serializable]
    public class UserKeysSettings : IKeysSettings
    {
        public Keys DownKey { get; set; }
        public Keys UpKey { get; set; }
        public Keys RightKey { get; set; }
        public Keys LeftKey { get; set; }
        public Keys DropKey { get; set; }
        public Keys PauseKey { get; set; }

        /// <summary>
        /// A constructor, creates settings from given keys.
        /// </summary>
        /// <param name="downKey"></param>
        /// <param name="upKey"></param>
        /// <param name="rightKey"></param>
        /// <param name="leftKey"></param>
        /// <param name="dropKey"></param>
        /// <param name="pauseKey"></param>
        public UserKeysSettings(Keys downKey, Keys upKey, Keys rightKey, Keys leftKey, Keys dropKey, Keys pauseKey)
        {
            DownKey = downKey;
            UpKey = upKey;
            RightKey = rightKey;
            LeftKey = leftKey;
            DropKey = dropKey;
            PauseKey = pauseKey;
        }

        /// <summary>
        /// A constructor, creates default settings.
        /// </summary>
        public UserKeysSettings()
        {
            DownKey = Keys.Down;
            UpKey = Keys.Up;
            RightKey = Keys.Right;
            LeftKey = Keys.Left;
            DropKey = Keys.Space;
            PauseKey = Keys.P;
        }
    }
}
