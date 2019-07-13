namespace Tetris4D.Forms
{
    partial class ControlsForm
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btPause = new System.Windows.Forms.Button();
            this.btDefault = new System.Windows.Forms.Button();
            this.btDrop = new System.Windows.Forms.Button();
            this.btRight = new System.Windows.Forms.Button();
            this.btLeft = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblUp = new System.Windows.Forms.Label();
            this.lblPause = new System.Windows.Forms.Label();
            this.lblDrop = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblDown = new System.Windows.Forms.Label();
            this.btConfirm = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.Black;
            this.lblHeading.Font = new System.Drawing.Font("Comic Sans MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblHeading.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblHeading.Location = new System.Drawing.Point(47, 27);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(270, 47);
            this.lblHeading.TabIndex = 1;
            this.lblHeading.Text = "Keyboard settings";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Controls.Add(this.btPause);
            this.groupBox1.Controls.Add(this.btDefault);
            this.groupBox1.Controls.Add(this.btDrop);
            this.groupBox1.Controls.Add(this.btRight);
            this.groupBox1.Controls.Add(this.btLeft);
            this.groupBox1.Controls.Add(this.btDown);
            this.groupBox1.Controls.Add(this.btUp);
            this.groupBox1.Controls.Add(this.lblRight);
            this.groupBox1.Controls.Add(this.lblUp);
            this.groupBox1.Controls.Add(this.lblPause);
            this.groupBox1.Controls.Add(this.lblDrop);
            this.groupBox1.Controls.Add(this.lblLeft);
            this.groupBox1.Controls.Add(this.lblDown);
            this.groupBox1.Location = new System.Drawing.Point(36, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 248);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btPause
            // 
            this.btPause.BackColor = System.Drawing.Color.Gainsboro;
            this.btPause.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btPause.Location = new System.Drawing.Point(162, 177);
            this.btPause.Name = "btPause";
            this.btPause.Size = new System.Drawing.Size(119, 23);
            this.btPause.TabIndex = 19;
            this.btPause.Text = "button6";
            this.btPause.UseVisualStyleBackColor = false;
            this.btPause.Click += new System.EventHandler(this.btPause_Click);
            // 
            // btDefault
            // 
            this.btDefault.BackColor = System.Drawing.Color.Gainsboro;
            this.btDefault.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btDefault.Location = new System.Drawing.Point(77, 219);
            this.btDefault.Name = "btDefault";
            this.btDefault.Size = new System.Drawing.Size(146, 23);
            this.btDefault.TabIndex = 20;
            this.btDefault.Text = "Reset Defaults";
            this.btDefault.UseVisualStyleBackColor = false;
            this.btDefault.Click += new System.EventHandler(this.btDefault_Click);
            // 
            // btDrop
            // 
            this.btDrop.BackColor = System.Drawing.Color.Gainsboro;
            this.btDrop.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btDrop.Location = new System.Drawing.Point(162, 147);
            this.btDrop.Name = "btDrop";
            this.btDrop.Size = new System.Drawing.Size(119, 23);
            this.btDrop.TabIndex = 18;
            this.btDrop.Text = "button5";
            this.btDrop.UseVisualStyleBackColor = false;
            this.btDrop.Click += new System.EventHandler(this.btDrop_Click);
            // 
            // btRight
            // 
            this.btRight.BackColor = System.Drawing.Color.Gainsboro;
            this.btRight.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btRight.Location = new System.Drawing.Point(162, 114);
            this.btRight.Name = "btRight";
            this.btRight.Size = new System.Drawing.Size(119, 23);
            this.btRight.TabIndex = 17;
            this.btRight.Text = "button4";
            this.btRight.UseVisualStyleBackColor = false;
            this.btRight.Click += new System.EventHandler(this.btRight_Click);
            // 
            // btLeft
            // 
            this.btLeft.BackColor = System.Drawing.Color.Gainsboro;
            this.btLeft.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btLeft.Location = new System.Drawing.Point(162, 81);
            this.btLeft.Name = "btLeft";
            this.btLeft.Size = new System.Drawing.Size(119, 23);
            this.btLeft.TabIndex = 16;
            this.btLeft.Text = "button3";
            this.btLeft.UseVisualStyleBackColor = false;
            this.btLeft.Click += new System.EventHandler(this.btLeft_Click);
            // 
            // btDown
            // 
            this.btDown.BackColor = System.Drawing.Color.Gainsboro;
            this.btDown.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btDown.Location = new System.Drawing.Point(162, 48);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(119, 23);
            this.btDown.TabIndex = 15;
            this.btDown.Text = "button2";
            this.btDown.UseVisualStyleBackColor = false;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btUp
            // 
            this.btUp.BackColor = System.Drawing.Color.Gainsboro;
            this.btUp.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btUp.Location = new System.Drawing.Point(162, 16);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(119, 23);
            this.btUp.TabIndex = 14;
            this.btUp.Text = "button1";
            this.btUp.UseVisualStyleBackColor = false;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblRight.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblRight.Location = new System.Drawing.Point(54, 112);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(96, 23);
            this.lblRight.TabIndex = 10;
            this.lblRight.Text = "Move Right";
            // 
            // lblUp
            // 
            this.lblUp.AutoSize = true;
            this.lblUp.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUp.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblUp.Location = new System.Drawing.Point(73, 16);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(77, 23);
            this.lblUp.TabIndex = 8;
            this.lblUp.Text = "Move Up";
            // 
            // lblPause
            // 
            this.lblPause.AutoSize = true;
            this.lblPause.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPause.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPause.Location = new System.Drawing.Point(97, 177);
            this.lblPause.Name = "lblPause";
            this.lblPause.Size = new System.Drawing.Size(53, 23);
            this.lblPause.TabIndex = 13;
            this.lblPause.Text = "Pause";
            // 
            // lblDrop
            // 
            this.lblDrop.AutoSize = true;
            this.lblDrop.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDrop.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDrop.Location = new System.Drawing.Point(103, 145);
            this.lblDrop.Name = "lblDrop";
            this.lblDrop.Size = new System.Drawing.Size(47, 23);
            this.lblDrop.TabIndex = 9;
            this.lblDrop.Text = "Drop";
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLeft.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblLeft.Location = new System.Drawing.Point(60, 79);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(90, 23);
            this.lblLeft.TabIndex = 11;
            this.lblLeft.Text = "Move Left";
            // 
            // lblDown
            // 
            this.lblDown.AutoSize = true;
            this.lblDown.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDown.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDown.Location = new System.Drawing.Point(55, 46);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(95, 23);
            this.lblDown.TabIndex = 12;
            this.lblDown.Text = "Move Down";
            // 
            // btConfirm
            // 
            this.btConfirm.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btConfirm.Location = new System.Drawing.Point(143, 345);
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.Size = new System.Drawing.Size(92, 28);
            this.btConfirm.TabIndex = 21;
            this.btConfirm.Text = "Confirm";
            this.btConfirm.UseVisualStyleBackColor = true;
            this.btConfirm.Click += new System.EventHandler(this.btOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(12, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 374);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(338, 15);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(24, 24);
            this.btClose.TabIndex = 23;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // ControlsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(374, 401);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btConfirm);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ControlsForm";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btPause;
        private System.Windows.Forms.Button btDrop;
        private System.Windows.Forms.Button btRight;
        private System.Windows.Forms.Button btLeft;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblUp;
        private System.Windows.Forms.Label lblPause;
        private System.Windows.Forms.Label lblDrop;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblDown;
        private System.Windows.Forms.Button btDefault;
        private System.Windows.Forms.Button btConfirm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btClose;
    }
}