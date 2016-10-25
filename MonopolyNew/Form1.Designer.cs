namespace MonopolyNew
{
    partial class Game
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
            Game.Container = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Container
            // 
            Game.Container.AutoSize = true;
            Game.Container.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Game.Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            Game.Container.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Game.Container.Cursor = System.Windows.Forms.Cursors.Arrow;
            Game.Container.Location = new System.Drawing.Point(0, 0);
            Game.Container.Margin = new System.Windows.Forms.Padding(0);
            Game.Container.Name = "Container";
            Game.Container.Size = new System.Drawing.Size(2, 2);
            Game.Container.TabIndex = 0;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(726, 709);
            this.Controls.Add(Game.Container);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monopoly";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
    }
}

