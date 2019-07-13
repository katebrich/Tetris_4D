using System;
using System.Windows.Forms;

namespace Tetris4D.Forms
{
    /// <summary>
    /// Form that asks client for his name to save the score.
    /// </summary>
    public partial class AskNameForm : Form
    {
        /// <summary>
        /// Clients name to return from this form.
        /// </summary>
        public string _Name {get; private set;}
        public AskNameForm()
        {
            InitializeComponent();
            initMessages();
            this.CenterToParent();
        }

        private void initMessages()
        {
            label1.Text = Messages.AskNameForm_lblQuestion;
            label2.Text = Messages.AskNameForm_lblEnterName;
            btOK.Text = Messages.Common_btOK;
            btCancel.Text = Messages.Common_btCancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Name = tbName.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
