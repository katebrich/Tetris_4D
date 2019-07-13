using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris4D.Forms
{
    /// <summary>
    /// Form used for displaying messages to client.
    /// </summary>
    public partial class TetrisMessageBox : Form
    {
        int squareSide = 30;
        int heightSquaresCount = 8;
        int widthSquaresCount = 11;
        Color color;

        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="heading">Short message displayed on the top of the form.</param>
        /// <param name="style">Style decides message box icon and color of components.</param>
        /// <param name="buttons">Buttons decides visibility and texts of buttons.</param>
        public TetrisMessageBox(string message, string heading, TetrisMesBoxStyleEnum style, TetrisMesBoxButtonsEnum buttons)
        {
            InitializeComponent();
            setButtons(buttons);
            this.CenterToParent();
            lblHeading.Text = heading;
            lblMessage.Text = message;
            setStyle(style);
            createView();
        }

        /// <summary>
        /// Sets buttons texts and visibility
        /// </summary>
        private void setButtons(TetrisMesBoxButtonsEnum buttons)
        {
            switch (buttons)
            {
                case TetrisMesBoxButtonsEnum.OK:
                    btOK.Text = Messages.Common_btOK;
                    btCancel.Enabled = false;
                    btCancel.Visible = false;
                    break;
                case TetrisMesBoxButtonsEnum.CancelOk:
                    btOK.Text = Messages.Common_btOK;
                    btCancel.Text = Messages.Common_btCancel;
                    break;
                case TetrisMesBoxButtonsEnum.NoYes:
                    btOK.Text = Messages.Common_btYes;
                    btCancel.Text = Messages.Common_btNo;
                    break;
                default:
                    break;
            }
            
        }

        /// <summary>
        /// Sets the icon and color.
        /// </summary>
        /// <param name="style"></param>
        private void setStyle(TetrisMesBoxStyleEnum style)
        {
            switch (style)
            {
                case TetrisMesBoxStyleEnum.Warning:
                    pbIcon.Image = SystemIcons.Warning.ToBitmap();
                    color = Color.Yellow;
                    break;
                case TetrisMesBoxStyleEnum.Error:
                    pbIcon.Image = SystemIcons.Error.ToBitmap();
                    color = Color.Red;
                    break;
                case TetrisMesBoxStyleEnum.Info:
                    pbIcon.Image = SystemIcons.Information.ToBitmap();
                    color = Color.Blue;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets proportions of components. Creates view.
        /// </summary>
        private void createView()
        {
            this.ClientSize = new Size(widthSquaresCount * squareSide, heightSquaresCount * squareSide);
            pbBackground.Size = this.ClientSize;
            pbBackground.Location = new Point(0, 0);
            pbBackground.Image = createBackground();
            pbBackground.SendToBack();

            lblHeading.Parent = pbBackground;
            lblHeading.BackColor = Color.Transparent;
            lblMessage.Parent = pbBackground;
            lblMessage.BackColor = Color.Transparent;
            pbIcon.Parent = pbBackground;
            pbIcon.BackColor = Color.Transparent;

            int offset = squareSide / 2;
            btOK.Location = new Point(widthSquaresCount * squareSide - offset - btOK.Width, heightSquaresCount * squareSide - squareSide * 2 + (squareSide * 2 - btOK.Height) / 2);
            btCancel.Location = new Point(widthSquaresCount * squareSide - 2 * offset - btOK.Width - btCancel.Width, heightSquaresCount * squareSide - squareSide * 2 + (squareSide * 2 - btOK.Height) / 2);

            lblHeading.Location = new Point(0, 0);
            using (Graphics g = CreateGraphics())
            {
                SizeF size = g.MeasureString(lblHeading.Text, lblHeading.Font);
                lblHeading.Width = (int)Math.Ceiling(size.Width) + squareSide;
                lblHeading.Height = 2 * squareSide;
            }
            lblHeading.TextAlign = ContentAlignment.MiddleCenter;

            pbIcon.Height = pbIcon.Width;
            pbIcon.Location = new Point(offset, 2 * squareSide + ((heightSquaresCount - 4) * squareSide - pbIcon.Height) / 2);

            lblMessage.Height = (heightSquaresCount - 4) * squareSide;
            lblMessage.Width = widthSquaresCount * squareSide - 2*offset - pbIcon.Width;
            lblMessage.Location = new Point(offset + pbIcon.Width + offset, 2 * squareSide);
        }

        /// <summary>
        /// Creates background for the whole form.
        /// </summary>
        /// <returns></returns>
        private Bitmap createBackground()
        {
            Bitmap bmp = new Bitmap(widthSquaresCount * squareSide, heightSquaresCount * squareSide);
            Bitmap squareImage = TetrisSquareImage.GetTetrisSquareImage(color, squareSide, squareSide);

            using (Graphics grfx = Graphics.FromImage(bmp))
            {
                grfx.FillRectangle(Brushes.Gainsboro, 0, 0, bmp.Width, bmp.Height);
                grfx.FillRectangle(Brushes.Black, 0, 0, bmp.Width, squareSide * 2);
                grfx.FillRectangle(Brushes.Black, 0, bmp.Height - squareSide * 2, bmp.Width, squareSide * 2);

                List<Point> toDraw = new List<Point>
                {
                    new Point(heightSquaresCount - 2, 0),
                    new Point(heightSquaresCount - 1, 0),
                    new Point(heightSquaresCount - 1, 1),
                    new Point(heightSquaresCount - 1, 2),
                    new Point(0, widthSquaresCount - 3),
                    new Point(0, widthSquaresCount - 2),
                    new Point(0, widthSquaresCount - 1),
                    new Point(1, widthSquaresCount - 1),
                };
                foreach (var item in toDraw)
                {
                    grfx.DrawImage(squareImage, item.Y * squareSide, item.X * squareSide, squareSide, squareSide);
                }
            }

            return bmp;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
