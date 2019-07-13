using System.Windows.Forms;

namespace Tetris4D
{
    /// <summary>
    /// Represents keyboard settings used in game.
    /// </summary>
    public interface IKeysSettings
    {
        Keys DownKey { get; set; }
        Keys UpKey { get; set; }
        Keys RightKey { get; set; }
        Keys LeftKey { get; set; }
        Keys DropKey { get; set; }
        Keys PauseKey { get; set; }
    }
}
