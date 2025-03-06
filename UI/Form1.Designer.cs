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
            buttonPlayPause = new System.Windows.Forms.Button();
            TitleLab = new System.Windows.Forms.Label();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            listBoxSongs = new System.Windows.Forms.ListBox();
            buttonPreviousSong = new System.Windows.Forms.Button();
            buttonNextSong = new System.Windows.Forms.Button();
            listBoxPlaylists = new System.Windows.Forms.ListBox();
            MusicPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)MusicPictureBox).BeginInit();
            SuspendLayout();
            // 
            // buttonPlayPause
            // 
            buttonPlayPause.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            buttonPlayPause.ForeColor = System.Drawing.Color.FloralWhite;
            buttonPlayPause.Location = new System.Drawing.Point(1146, 642);
            buttonPlayPause.Name = "buttonPlayPause";
            buttonPlayPause.Size = new System.Drawing.Size(250, 104);
            buttonPlayPause.TabIndex = 0;
            buttonPlayPause.Text = "Play";
            buttonPlayPause.UseVisualStyleBackColor = false;
            buttonPlayPause.Click += button1_Click;
            // 
            // TitleLab
            // 
            TitleLab.AutoSize = true;
            TitleLab.Location = new System.Drawing.Point(1242, 770);
            TitleLab.Name = "TitleLab";
            TitleLab.Size = new System.Drawing.Size(60, 32);
            TitleLab.TabIndex = 2;
            TitleLab.Text = "Title";
            TitleLab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            TitleLab.Click += TitleLab_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(979, 822);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(594, 40);
            progressBar1.TabIndex = 3;
            progressBar1.Click += progressBar1_Click;
            // 
            // listBoxSongs
            // 
            listBoxSongs.FormattingEnabled = true;
            listBoxSongs.ItemHeight = 32;
            listBoxSongs.Location = new System.Drawing.Point(584, 57);
            listBoxSongs.Name = "listBoxSongs";
            listBoxSongs.Size = new System.Drawing.Size(357, 804);
            listBoxSongs.TabIndex = 4;
            // 
            // buttonPreviousSong
            // 
            buttonPreviousSong.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            buttonPreviousSong.ForeColor = System.Drawing.Color.FloralWhite;
            buttonPreviousSong.Location = new System.Drawing.Point(974, 663);
            buttonPreviousSong.Name = "buttonPreviousSong";
            buttonPreviousSong.Size = new System.Drawing.Size(166, 63);
            buttonPreviousSong.TabIndex = 5;
            buttonPreviousSong.Text = "⏮️";
            buttonPreviousSong.UseVisualStyleBackColor = false;
            buttonPreviousSong.Click += button1_Click_1;
            // 
            // buttonNextSong
            // 
            buttonNextSong.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            buttonNextSong.ForeColor = System.Drawing.Color.FloralWhite;
            buttonNextSong.Location = new System.Drawing.Point(1402, 663);
            buttonNextSong.Name = "buttonNextSong";
            buttonNextSong.Size = new System.Drawing.Size(166, 63);
            buttonNextSong.TabIndex = 6;
            buttonNextSong.Text = "⏭️";
            buttonNextSong.UseVisualStyleBackColor = false;
            buttonNextSong.Click += buttonNextSong_Click;
            // 
            // listBoxPlaylists
            // 
            listBoxPlaylists.FormattingEnabled = true;
            listBoxPlaylists.ItemHeight = 32;
            listBoxPlaylists.Location = new System.Drawing.Point(249, 57);
            listBoxPlaylists.Name = "listBoxPlaylists";
            listBoxPlaylists.Size = new System.Drawing.Size(238, 804);
            listBoxPlaylists.TabIndex = 7;
            // 
            // MusicPictureBox
            // 
            MusicPictureBox.Location = new System.Drawing.Point(1054, 206);
            MusicPictureBox.Name = "MusicPictureBox";
            MusicPictureBox.Size = new System.Drawing.Size(428, 420);
            MusicPictureBox.TabIndex = 8;
            MusicPictureBox.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1595, 903);
            Controls.Add(MusicPictureBox);
            Controls.Add(listBoxPlaylists);
            Controls.Add(buttonNextSong);
            Controls.Add(buttonPreviousSong);
            Controls.Add(listBoxSongs);
            Controls.Add(progressBar1);
            Controls.Add(TitleLab);
            Controls.Add(buttonPlayPause);
            Margin = new System.Windows.Forms.Padding(5);
            Text = "MP3PlayerCustom";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)MusicPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ListBox listBoxPlaylists;
        private System.Windows.Forms.PictureBox MusicPictureBox;

        private System.Windows.Forms.Button buttonPreviousSong;
        private System.Windows.Forms.Button buttonNextSong;

        #endregion

        private System.Windows.Forms.Button buttonPlayPause;
        private System.Windows.Forms.Label TitleLab;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox listBoxSongs;
    }
}
