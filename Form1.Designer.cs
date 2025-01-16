namespace PlayerMP3
{
    partial class Form1
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
            buttonPlayPause = new Button();
            TitleLab = new Label();
            progressBar1 = new ProgressBar();
            listBoxSongs = new ListBox();
            SuspendLayout();
            // 
            // buttonPlayPause
            // 
            buttonPlayPause.BackColor = SystemColors.ActiveCaptionText;
            buttonPlayPause.ForeColor = Color.FloralWhite;
            buttonPlayPause.Location = new Point(262, 334);
            buttonPlayPause.Name = "buttonPlayPause";
            buttonPlayPause.Size = new Size(250, 104);
            buttonPlayPause.TabIndex = 0;
            buttonPlayPause.Text = "Play";
            buttonPlayPause.UseVisualStyleBackColor = false;
            buttonPlayPause.Click += button1_Click;
            // 
            // TitleLab
            // 
            TitleLab.AutoSize = true;
            TitleLab.Location = new Point(362, 241);
            TitleLab.Name = "TitleLab";
            TitleLab.Size = new Size(38, 20);
            TitleLab.TabIndex = 2;
            TitleLab.Text = "Title";
            TitleLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(39, 299);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(692, 29);
            progressBar1.TabIndex = 3;
            // 
            // listBoxSongs
            // 
            listBoxSongs.FormattingEnabled = true;
            listBoxSongs.ItemHeight = 20;
            listBoxSongs.Location = new Point(39, 12);
            listBoxSongs.Name = "listBoxSongs";
            listBoxSongs.Size = new Size(610, 104);
            listBoxSongs.TabIndex = 4;
            listBoxSongs.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBoxSongs);
            Controls.Add(progressBar1);
            Controls.Add(TitleLab);
            Controls.Add(buttonPlayPause);
            Name = "Form1";
            Text = "MP3PlayerCustom";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonPlayPause;
        private Label TitleLab;
        private ProgressBar progressBar1;
        private ListBox listBoxSongs;
    }
}
