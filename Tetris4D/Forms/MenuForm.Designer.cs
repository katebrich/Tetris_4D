namespace Tetris4D.Forms
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btNewGame = new System.Windows.Forms.Button();
            this.btLoadGame = new System.Windows.Forms.Button();
            this.btContinue = new System.Windows.Forms.Button();
            this.btSaveGame = new System.Windows.Forms.Button();
            this.btShowScore = new System.Windows.Forms.Button();
            this.btQuit = new System.Windows.Forms.Button();
            this.pbView = new System.Windows.Forms.PictureBox();
            this.btControls = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbView)).BeginInit();
            this.SuspendLayout();
            // 
            // btNewGame
            // 
            this.btNewGame.Location = new System.Drawing.Point(108, 77);
            this.btNewGame.Name = "btNewGame";
            this.btNewGame.Size = new System.Drawing.Size(75, 23);
            this.btNewGame.TabIndex = 0;
            this.btNewGame.Text = "New Game";
            this.btNewGame.UseVisualStyleBackColor = true;
            this.btNewGame.Click += new System.EventHandler(this.btNewGame_Click);
            // 
            // btLoadGame
            // 
            this.btLoadGame.Location = new System.Drawing.Point(108, 106);
            this.btLoadGame.Name = "btLoadGame";
            this.btLoadGame.Size = new System.Drawing.Size(75, 23);
            this.btLoadGame.TabIndex = 1;
            this.btLoadGame.Text = "Load Game";
            this.btLoadGame.UseVisualStyleBackColor = true;
            this.btLoadGame.Click += new System.EventHandler(this.btLoadGame_Click);
            // 
            // btContinue
            // 
            this.btContinue.Enabled = false;
            this.btContinue.Location = new System.Drawing.Point(108, 48);
            this.btContinue.Name = "btContinue";
            this.btContinue.Size = new System.Drawing.Size(75, 23);
            this.btContinue.TabIndex = 2;
            this.btContinue.Text = "Continue";
            this.btContinue.UseVisualStyleBackColor = true;
            this.btContinue.Click += new System.EventHandler(this.btContinue_Click);
            // 
            // btSaveGame
            // 
            this.btSaveGame.Location = new System.Drawing.Point(108, 136);
            this.btSaveGame.Name = "btSaveGame";
            this.btSaveGame.Size = new System.Drawing.Size(75, 23);
            this.btSaveGame.TabIndex = 3;
            this.btSaveGame.Text = "Save Game";
            this.btSaveGame.UseVisualStyleBackColor = true;
            this.btSaveGame.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btShowScore
            // 
            this.btShowScore.Location = new System.Drawing.Point(108, 166);
            this.btShowScore.Name = "btShowScore";
            this.btShowScore.Size = new System.Drawing.Size(75, 23);
            this.btShowScore.TabIndex = 4;
            this.btShowScore.Text = "High score";
            this.btShowScore.UseVisualStyleBackColor = true;
            this.btShowScore.Click += new System.EventHandler(this.btShowScore_Click);
            // 
            // btQuit
            // 
            this.btQuit.BackColor = System.Drawing.SystemColors.Control;
            this.btQuit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btQuit.Location = new System.Drawing.Point(108, 226);
            this.btQuit.Name = "btQuit";
            this.btQuit.Size = new System.Drawing.Size(75, 23);
            this.btQuit.TabIndex = 5;
            this.btQuit.Text = "End Game";
            this.btQuit.UseVisualStyleBackColor = false;
            this.btQuit.Click += new System.EventHandler(this.btEnd_Click);
            // 
            // pbView
            // 
            this.pbView.Location = new System.Drawing.Point(13, 13);
            this.pbView.Name = "pbView";
            this.pbView.Size = new System.Drawing.Size(100, 50);
            this.pbView.TabIndex = 6;
            this.pbView.TabStop = false;
            // 
            // btControls
            // 
            this.btControls.Location = new System.Drawing.Point(108, 195);
            this.btControls.Name = "btControls";
            this.btControls.Size = new System.Drawing.Size(75, 23);
            this.btControls.TabIndex = 7;
            this.btControls.Text = "Controls";
            this.btControls.UseVisualStyleBackColor = true;
            this.btControls.Click += new System.EventHandler(this.btControls_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btNewGame);
            this.Controls.Add(this.btContinue);
            this.Controls.Add(this.btControls);
            this.Controls.Add(this.btQuit);
            this.Controls.Add(this.btShowScore);
            this.Controls.Add(this.btSaveGame);
            this.Controls.Add(this.btLoadGame);
            this.Controls.Add(this.pbView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MenuForm";
            this.Text = "MenuWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btNewGame;
        private System.Windows.Forms.Button btLoadGame;
        private System.Windows.Forms.Button btContinue;
        private System.Windows.Forms.Button btSaveGame;
        private System.Windows.Forms.Button btShowScore;
        private System.Windows.Forms.Button btQuit;
        private System.Windows.Forms.PictureBox pbView;
        private System.Windows.Forms.Button btControls;
    }
}