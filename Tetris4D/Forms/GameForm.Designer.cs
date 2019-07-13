namespace Tetris4D.Forms
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.keyListener = new System.Windows.Forms.Timer(this.components);
            this.pieceShiftTimer = new System.Windows.Forms.Timer(this.components);
            this.pbNextPiece = new System.Windows.Forms.PictureBox();
            this.pbTetrisGrid = new System.Windows.Forms.PictureBox();
            this.btMenu = new System.Windows.Forms.Button();
            this.pbScore = new System.Windows.Forms.PictureBox();
            this.lblNewScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbNextPiece)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTetrisGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbScore)).BeginInit();
            this.SuspendLayout();
            // 
            // keyListener
            // 
            this.keyListener.Interval = 10;
            this.keyListener.Tick += new System.EventHandler(this.keyListener_Tick);
            // 
            // pieceShiftTimer
            // 
            this.pieceShiftTimer.Interval = 1000;
            this.pieceShiftTimer.Tick += new System.EventHandler(this.pieceShiftTimer_Tick);
            // 
            // pbNextPiece
            // 
            this.pbNextPiece.Location = new System.Drawing.Point(1045, 96);
            this.pbNextPiece.Name = "pbNextPiece";
            this.pbNextPiece.Size = new System.Drawing.Size(129, 116);
            this.pbNextPiece.TabIndex = 1;
            this.pbNextPiece.TabStop = false;
            // 
            // pbTetrisGrid
            // 
            this.pbTetrisGrid.Location = new System.Drawing.Point(169, 12);
            this.pbTetrisGrid.Name = "pbTetrisGrid";
            this.pbTetrisGrid.Size = new System.Drawing.Size(559, 544);
            this.pbTetrisGrid.TabIndex = 0;
            this.pbTetrisGrid.TabStop = false;
            // 
            // btMenu
            // 
            this.btMenu.Location = new System.Drawing.Point(1191, 301);
            this.btMenu.Name = "btMenu";
            this.btMenu.Size = new System.Drawing.Size(129, 36);
            this.btMenu.TabIndex = 3;
            this.btMenu.Text = "Back to menu";
            this.btMenu.UseVisualStyleBackColor = true;
            this.btMenu.Click += new System.EventHandler(this.btMenu_Click);
            // 
            // pbScore
            // 
            this.pbScore.Location = new System.Drawing.Point(1045, 36);
            this.pbScore.Name = "pbScore";
            this.pbScore.Size = new System.Drawing.Size(129, 89);
            this.pbScore.TabIndex = 4;
            this.pbScore.TabStop = false;
            // 
            // lblNewScore
            // 
            this.lblNewScore.AutoSize = true;
            this.lblNewScore.Location = new System.Drawing.Point(267, 301);
            this.lblNewScore.Name = "lblNewScore";
            this.lblNewScore.Size = new System.Drawing.Size(0, 13);
            this.lblNewScore.TabIndex = 5;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(953, 573);
            this.Controls.Add(this.lblNewScore);
            this.Controls.Add(this.pbScore);
            this.Controls.Add(this.btMenu);
            this.Controls.Add(this.pbNextPiece);
            this.Controls.Add(this.pbTetrisGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "GameForm";
            this.Text = "GameWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ClientSizeChanged += new System.EventHandler(this.GameWindow_ClientSizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyUp);
            this.Resize += new System.EventHandler(this.GameWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbNextPiece)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTetrisGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbScore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer keyListener;
        private System.Windows.Forms.Timer pieceShiftTimer;
        private System.Windows.Forms.PictureBox pbTetrisGrid;
        private System.Windows.Forms.PictureBox pbNextPiece;
        private System.Windows.Forms.Button btMenu;
        private System.Windows.Forms.PictureBox pbScore;
        private System.Windows.Forms.Label lblNewScore;
    }
}