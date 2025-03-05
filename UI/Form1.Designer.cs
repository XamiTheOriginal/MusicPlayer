namespace MusicPlayer
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonPlayPause = new Button();
            TitleLab = new Label();
            progressBar1 = new ProgressBar();
            listBoxSongs = new ListBox();
            buttonPreviousSong = new Button();
            buttonNextSong = new Button();
            SuspendLayout();
            // 
            // buttonPlayPause
            // 
            buttonPlayPause.BackColor = SystemColors.ActiveCaptionText;
            buttonPlayPause.ForeColor = Color.FloralWhite;
            buttonPlayPause.Location = new Point(569, 299);
            buttonPlayPause.Margin = new Padding(2, 2, 2, 2);
            buttonPlayPause.Name = "buttonPlayPause";
            buttonPlayPause.Size = new Size(154, 65);
            buttonPlayPause.TabIndex = 0;
            buttonPlayPause.Text = "Play";
            buttonPlayPause.UseVisualStyleBackColor = false;
            buttonPlayPause.Click += button1_Click;
            // 
            // TitleLab
            // 
            TitleLab.AutoSize = true;
            TitleLab.Location = new Point(22, 299);
            TitleLab.Margin = new Padding(2, 0, 2, 0);
            TitleLab.Name = "TitleLab";
            TitleLab.Size = new Size(38, 20);
            TitleLab.TabIndex = 2;
            TitleLab.Text = "Title";
            TitleLab.TextAlign = ContentAlignment.MiddleCenter;
            TitleLab.Click += TitleLab_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(22, 334);
            progressBar1.Margin = new Padding(2, 2, 2, 2);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(426, 18);
            progressBar1.TabIndex = 3;
            progressBar1.Click += progressBar1_Click;
            // 
            // listBoxSongs
            // 
            listBoxSongs.FormattingEnabled = true;
            listBoxSongs.ItemHeight = 20;
            listBoxSongs.Location = new Point(228, 27);
            listBoxSongs.Margin = new Padding(2, 2, 2, 2);
            listBoxSongs.Name = "listBoxSongs";
            listBoxSongs.Size = new Size(377, 64);
            listBoxSongs.TabIndex = 4;
            // 
            // buttonPreviousSong
            // 
            buttonPreviousSong.BackColor = SystemColors.ActiveCaptionText;
            buttonPreviousSong.ForeColor = Color.FloralWhite;
            buttonPreviousSong.Location = new Point(463, 312);
            buttonPreviousSong.Margin = new Padding(2, 2, 2, 2);
            buttonPreviousSong.Name = "buttonPreviousSong";
            buttonPreviousSong.Size = new Size(102, 39);
            buttonPreviousSong.TabIndex = 5;
            buttonPreviousSong.Text = "⏮️";
            buttonPreviousSong.UseVisualStyleBackColor = false;
            buttonPreviousSong.Click += button1_Click_1;
            // 
            // buttonNextSong
            // 
            buttonNextSong.BackColor = SystemColors.ActiveCaptionText;
            buttonNextSong.ForeColor = Color.FloralWhite;
            buttonNextSong.Location = new Point(727, 312);
            buttonNextSong.Margin = new Padding(2, 2, 2, 2);
            buttonNextSong.Name = "buttonNextSong";
            buttonNextSong.Size = new Size(102, 39);
            buttonNextSong.TabIndex = 6;
            buttonNextSong.Text = "⏭️";
            buttonNextSong.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(828, 372);
            Controls.Add(buttonNextSong);
            Controls.Add(buttonPreviousSong);
            Controls.Add(listBoxSongs);
            Controls.Add(progressBar1);
            Controls.Add(TitleLab);
            Controls.Add(buttonPlayPause);
            Name = "Form1";
            Text = "MP3PlayerCustom";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button buttonPreviousSong;
        private System.Windows.Forms.Button buttonNextSong;

        #endregion

        private System.Windows.Forms.Button buttonPlayPause;
        private System.Windows.Forms.Label TitleLab;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox listBoxSongs;
    }
}
