using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TetrisControlProject.Interfaces;
using TetrisControlProject;

namespace Tetris4D.Forms
{
    /// <summary>
    /// Keyboard settings form.
    /// </summary>
    public partial class ControlsForm : Form
    {
        private int listening = 0; //tells whether some button is listening to the key press. 0...noone listening

        /// <summary>
        /// A constructor.
        /// Creates close button.
        /// Sets texts on buttons.
        /// </summary>
        /// <param name="settings"></param>
        public ControlsForm(IKeysSettings settings)
        {
            InitializeComponent();
            initMessages();
            CenterToParent();

            Bitmap cross = new Bitmap(btClose.Width, btClose.Height);
            using (Graphics g = Graphics.FromImage(cross))
            {
                g.FillRectangle(Brushes.Gainsboro, 0, 0, cross.Width, cross.Height);
                g.DrawLine(Pens.Black, new Point(0, 0), new Point(cross.Width, cross.Height));
                g.DrawLine(Pens.Black, new Point(cross.Width, 0), new Point(0, cross.Height));
            }
            btClose.Image = cross;

            setButtons(settings);
        }
       
        /// <summary>
        /// Returns new keysSettings obtained from texts on buttons.
        /// </summary>
        /// <returns></returns>
        public IKeysSettings GetNewSettings()
        {
            Keys keyUp = (Keys)Enum.Parse(typeof(Keys), btUp.Text, false);
            Keys keyDown = (Keys)Enum.Parse(typeof(Keys), btDown.Text, false);
            Keys keyLeft = (Keys)Enum.Parse(typeof(Keys), btLeft.Text, false);
            Keys keyRight = (Keys)Enum.Parse(typeof(Keys), btRight.Text, false);
            Keys keyDrop = (Keys)Enum.Parse(typeof(Keys), btDrop.Text, false);
            Keys keyPause = (Keys)Enum.Parse(typeof(Keys), btPause.Text, false);

            return new UserKeysSettings(keyDown, keyUp, keyRight, keyLeft, keyDrop, keyPause);           
        }

        /// <summary>
        /// Takes care of press key events
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (listening == 0)
                return true;

            //is this key already used?
            List<Button> buttons = new List<Button>
            {
                btUp,
                btDown,
                btLeft,
                btRight,
                btDrop,
                btPause
            };
            Button toChange = null;
            foreach (var button in buttons)
            {
                if (keyData.ToString() == button.Text)
                {
                    toChange = button;
                    break;
                }
            }


            if (listening == 1)
            {
                if (toChange != null)
                    toChange.Text = btUp.Text;
                btUp.Text = keyData.ToString();
                btUp.BackColor = Color.Gainsboro;
            }
            else if (listening == 2)
            {
                if (toChange != null)
                    toChange.Text = btDown.Text;
                btDown.Text = keyData.ToString();
                btDown.BackColor = Color.Gainsboro;
            }
            else if (listening == 3)
            {
                if (toChange != null)
                    toChange.Text = btLeft.Text;
                btLeft.Text = keyData.ToString();
                btLeft.BackColor = Color.Gainsboro;
            }
            else if (listening == 4)
            {
                if (toChange != null)
                    toChange.Text = btRight.Text;
                btRight.Text = keyData.ToString();
                btRight.BackColor = Color.Gainsboro;
            }
            else if (listening == 5)
            {
                if (toChange != null)
                    toChange.Text = btDrop.Text;
                btDrop.Text = keyData.ToString();
                btDrop.BackColor = Color.Gainsboro;
            }
            else if (listening == 6)
            {
                if (toChange != null)
                    toChange.Text = btPause.Text;
                btPause.Text = keyData.ToString();
                btPause.BackColor = Color.Gainsboro;
            }

            listening = 0;
            return true;
        }

        /// <summary>
        /// Initializes texts in form.
        /// </summary>
        private void initMessages()
        {
            lblHeading.Text = Messages.ControlsForm_lblHeading;
            btDefault.Text = Messages.ControlsForm_btDefaults;
            btConfirm.Text = Messages.ControlsForm_btConfirm;
            lblUp.Text = Messages.ControlsForm_lblUp;
            lblDown.Text = Messages.ControlsForm_lblDown;
            lblLeft.Text = Messages.ControlsForm_lblLeft;
            lblRight.Text = Messages.ControlsForm_lblRight;
            lblDrop.Text = Messages.ControlsForm_lblDrop;
            lblPause.Text = Messages.ControlsForm_lblPause;
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sets default key settings on buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDefault_Click(object sender, EventArgs e)
        {
            IKeysSettings settings = new UserKeysSettings();
            setButtons(settings);
            btUp.BackColor = Color.Gainsboro;
            btDown.BackColor = Color.Gainsboro;
            btLeft.BackColor = Color.Gainsboro;
            btRight.BackColor = Color.Gainsboro;
            btPause.BackColor = Color.Gainsboro;
            btDrop.BackColor = Color.Gainsboro;
            listening = 0;
        }

        /// <summary>
        /// Sets buttons texts according to given settings.
        /// </summary>
        /// <param name="settings"></param>
        private void setButtons(IKeysSettings settings)
        {
            btUp.Text = settings.UpKey.ToString();
            btDown.Text = settings.DownKey.ToString();
            btLeft.Text = settings.LeftKey.ToString();
            btRight.Text = settings.RightKey.ToString();
            btPause.Text = settings.PauseKey.ToString();
            btDrop.Text = settings.DropKey.ToString();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            if (btUp.BackColor == Color.GreenYellow)
            {
                btUp.BackColor = Color.Gainsboro;
            }
            else
            {
                listening = 1;
                btUp.BackColor = Color.GreenYellow;
                btDown.BackColor = Color.Gainsboro;
                btLeft.BackColor = Color.Gainsboro;
                btRight.BackColor = Color.Gainsboro;
                btPause.BackColor = Color.Gainsboro;
                btDrop.BackColor = Color.Gainsboro;
            }
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (btDown.BackColor == Color.GreenYellow)
                btDown.BackColor = Color.Gainsboro;
            else
            {
                listening = 2;
                btUp.BackColor = Color.Gainsboro;
                btDown.BackColor = Color.GreenYellow;
                btLeft.BackColor = Color.Gainsboro;
                btRight.BackColor = Color.Gainsboro;
                btPause.BackColor = Color.Gainsboro;
                btDrop.BackColor = Color.Gainsboro;
            }
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            if (btLeft.BackColor == Color.GreenYellow)
                btLeft.BackColor = Color.Gainsboro;
            else
            {
                listening = 3;
                btUp.BackColor = Color.Gainsboro;
                btDown.BackColor = Color.Gainsboro;
                btLeft.BackColor = Color.GreenYellow;
                btRight.BackColor = Color.Gainsboro;
                btPause.BackColor = Color.Gainsboro;
                btDrop.BackColor = Color.Gainsboro;
            }
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            if (btRight.BackColor == Color.GreenYellow)
                btRight.BackColor = Color.Gainsboro;
            else
            {
                listening = 4;
                btUp.BackColor = Color.Gainsboro;
                btDown.BackColor = Color.Gainsboro;
                btLeft.BackColor = Color.Gainsboro;
                btRight.BackColor = Color.GreenYellow;
                btPause.BackColor = Color.Gainsboro;
                btDrop.BackColor = Color.Gainsboro;
            }
        }

        private void btDrop_Click(object sender, EventArgs e)
        {
            if (btDrop.BackColor == Color.GreenYellow)
                btDrop.BackColor = Color.Gainsboro;
            else
            {
                listening = 5;
                btUp.BackColor = Color.Gainsboro;
                btDown.BackColor = Color.Gainsboro;
                btLeft.BackColor = Color.Gainsboro;
                btRight.BackColor = Color.Gainsboro;
                btPause.BackColor = Color.Gainsboro;
                btDrop.BackColor = Color.GreenYellow;
            }
        }

        private void btPause_Click(object sender, EventArgs e)
        {
            if (btPause.BackColor == Color.GreenYellow)
                btPause.BackColor = Color.Gainsboro;
            else
            {
                listening = 6;
                btUp.BackColor = Color.Gainsboro;
                btDown.BackColor = Color.Gainsboro;
                btLeft.BackColor = Color.Gainsboro;
                btRight.BackColor = Color.Gainsboro;
                btPause.BackColor = Color.GreenYellow;
                btDrop.BackColor = Color.Gainsboro;
            }
        }
    }
}
