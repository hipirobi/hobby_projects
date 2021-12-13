namespace Black_hole.View
{
    partial class BlackHole
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnGameSave = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(26, 37);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(95, 40);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "Új Játék";
            this.btnNewGame.UseVisualStyleBackColor = true;
            // 
            // btnGameSave
            // 
            this.btnGameSave.Location = new System.Drawing.Point(152, 37);
            this.btnGameSave.Name = "btnGameSave";
            this.btnGameSave.Size = new System.Drawing.Size(134, 40);
            this.btnGameSave.TabIndex = 0;
            this.btnGameSave.Text = "Játék mentése";
            this.btnGameSave.UseVisualStyleBackColor = true;
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.Location = new System.Drawing.Point(324, 37);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(132, 40);
            this.btnLoadGame.TabIndex = 0;
            this.btnLoadGame.Text = "Játék betöltése";
            this.btnLoadGame.UseVisualStyleBackColor = true;
            this.btnLoadGame.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Jelenlegi játékos: piros";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Fekete Lyuk Tábla (*.sav)|*.sav";
            this.openFileDialog.Title = "Fekete Lyuk játék betöltése";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Fekete Lyuk Tábla (*.sav)|*.sav";
            this.saveFileDialog.Title = "Fekete Lyuk játék mentése";
            // 
            // BlackHole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoadGame);
            this.Controls.Add(this.btnGameSave);
            this.Controls.Add(this.btnNewGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BlackHole";
            this.Text = "Fekete Lyuk";
            this.Load += new System.EventHandler(this.BlackHole_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnGameSave;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

