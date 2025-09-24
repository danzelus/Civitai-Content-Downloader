namespace CivitaiVideoDownloader
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            usernameBox = new TextBox();
            downloadButton = new Button();
            stopButton = new Button();
            statusLabel = new Label();
            logBox = new TextBox();
            overallProgress = new ProgressBar();
            fileProgress = new ProgressBar();
            apiKeyBox = new TextBox();
            apiKeyLabel = new Label();
            outputFolderBox = new TextBox();
            outputFolderLabel = new Label();
            toolTip1 = new ToolTip(components);
            kofiPictureBox = new PictureBox();
            groupBoxFilters = new GroupBox();
            groupBoxRating = new GroupBox();
            sfwCheck = new CheckBox();
            nsfwCheck = new CheckBox();
            groupBoxContentType = new GroupBox();
            downloadVideosCheck = new CheckBox();
            downloadImagesCheck = new CheckBox();
            sortLabel = new Label();
            sortComboBox = new ComboBox();
            periodLabel = new Label();
            periodComboBox = new ComboBox();
            limitNumericUpDown = new NumericUpDown();
            limitLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)kofiPictureBox).BeginInit();
            groupBoxFilters.SuspendLayout();
            groupBoxRating.SuspendLayout();
            groupBoxContentType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)limitNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // usernameBox
            // 
            usernameBox.AcceptsReturn = true;
            usernameBox.Location = new Point(12, 12);
            usernameBox.Multiline = true;
            usernameBox.Name = "usernameBox";
            usernameBox.PlaceholderText = "One or more usernames, one per line";
            usernameBox.ScrollBars = ScrollBars.Vertical;
            usernameBox.Size = new Size(400, 82);
            usernameBox.TabIndex = 0;
            // 
            // downloadButton
            // 
            downloadButton.Location = new Point(12, 306);
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new Size(151, 45);
            downloadButton.TabIndex = 1;
            downloadButton.Text = "Download";
            downloadButton.UseVisualStyleBackColor = true;
            downloadButton.Click += downloadButton_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(169, 306);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(136, 45);
            stopButton.TabIndex = 8;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(12, 354);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 15);
            statusLabel.TabIndex = 8;
            statusLabel.Text = "Ready";
            // 
            // logBox
            // 
            logBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logBox.Location = new Point(12, 372);
            logBox.Multiline = true;
            logBox.Name = "logBox";
            logBox.ReadOnly = true;
            logBox.ScrollBars = ScrollBars.Vertical;
            logBox.Size = new Size(400, 139);
            logBox.TabIndex = 9;
            // 
            // overallProgress
            // 
            overallProgress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            overallProgress.Location = new Point(12, 517);
            overallProgress.Name = "overallProgress";
            overallProgress.Size = new Size(400, 20);
            overallProgress.TabIndex = 10;
            // 
            // fileProgress
            // 
            fileProgress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            fileProgress.Location = new Point(12, 547);
            fileProgress.Name = "fileProgress";
            fileProgress.Size = new Size(400, 20);
            fileProgress.TabIndex = 11;
            // 
            // apiKeyBox
            // 
            apiKeyBox.Location = new Point(12, 115);
            apiKeyBox.Name = "apiKeyBox";
            apiKeyBox.Size = new Size(400, 23);
            apiKeyBox.TabIndex = 2;
            // 
            // apiKeyLabel
            // 
            apiKeyLabel.AutoSize = true;
            apiKeyLabel.Location = new Point(12, 97);
            apiKeyLabel.Name = "apiKeyLabel";
            apiKeyLabel.Size = new Size(50, 15);
            apiKeyLabel.TabIndex = 7;
            apiKeyLabel.Text = "API Key:";
            // 
            // outputFolderBox
            // 
            outputFolderBox.Location = new Point(12, 162);
            outputFolderBox.Name = "outputFolderBox";
            outputFolderBox.Size = new Size(400, 23);
            outputFolderBox.TabIndex = 3;
            toolTip1.SetToolTip(outputFolderBox, "If specified, all user folders will be created inside this main folder.");
            // 
            // outputFolderLabel
            // 
            outputFolderLabel.AutoSize = true;
            outputFolderLabel.Location = new Point(12, 144);
            outputFolderLabel.Name = "outputFolderLabel";
            outputFolderLabel.Size = new Size(73, 15);
            outputFolderLabel.TabIndex = 12;
            outputFolderLabel.Text = "Main Folder:";
            // 
            // kofiPictureBox
            // 
            kofiPictureBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            kofiPictureBox.Cursor = Cursors.Hand;
            kofiPictureBox.Image = Properties.Resources.ko_fi;
            kofiPictureBox.Location = new Point(301, 573);
            kofiPictureBox.Name = "kofiPictureBox";
            kofiPictureBox.Size = new Size(111, 35);
            kofiPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            kofiPictureBox.TabIndex = 27;
            kofiPictureBox.TabStop = false;
            toolTip1.SetToolTip(kofiPictureBox, "Support the developer!");
            kofiPictureBox.Click += kofiPictureBox_Click;
            // 
            // groupBoxFilters
            // 
            groupBoxFilters.Controls.Add(groupBoxRating);
            groupBoxFilters.Controls.Add(groupBoxContentType);
            groupBoxFilters.Controls.Add(sortLabel);
            groupBoxFilters.Controls.Add(sortComboBox);
            groupBoxFilters.Controls.Add(periodLabel);
            groupBoxFilters.Controls.Add(periodComboBox);
            groupBoxFilters.Controls.Add(limitNumericUpDown);
            groupBoxFilters.Controls.Add(limitLabel);
            groupBoxFilters.Location = new Point(12, 193);
            groupBoxFilters.Name = "groupBoxFilters";
            groupBoxFilters.Size = new Size(400, 107);
            groupBoxFilters.TabIndex = 26;
            groupBoxFilters.TabStop = false;
            groupBoxFilters.Text = "Filters and Options";
            // 
            // groupBoxRating
            // 
            groupBoxRating.Controls.Add(sfwCheck);
            groupBoxRating.Controls.Add(nsfwCheck);
            groupBoxRating.Location = new Point(269, 51);
            groupBoxRating.Name = "groupBoxRating";
            groupBoxRating.Size = new Size(125, 50);
            groupBoxRating.TabIndex = 19;
            groupBoxRating.TabStop = false;
            groupBoxRating.Text = "Rating";
            // 
            // sfwCheck
            // 
            sfwCheck.AutoSize = true;
            sfwCheck.Location = new Point(10, 22);
            sfwCheck.Name = "sfwCheck";
            sfwCheck.Size = new Size(49, 19);
            sfwCheck.TabIndex = 0;
            sfwCheck.Text = "SFW";
            sfwCheck.UseVisualStyleBackColor = true;
            // 
            // nsfwCheck
            // 
            nsfwCheck.AutoSize = true;
            nsfwCheck.Location = new Point(65, 22);
            nsfwCheck.Name = "nsfwCheck";
            nsfwCheck.Size = new Size(58, 19);
            nsfwCheck.TabIndex = 1;
            nsfwCheck.Text = "NSFW";
            nsfwCheck.UseVisualStyleBackColor = true;
            // 
            // groupBoxContentType
            // 
            groupBoxContentType.Controls.Add(downloadVideosCheck);
            groupBoxContentType.Controls.Add(downloadImagesCheck);
            groupBoxContentType.Location = new Point(123, 51);
            groupBoxContentType.Name = "groupBoxContentType";
            groupBoxContentType.Size = new Size(140, 50);
            groupBoxContentType.TabIndex = 18;
            groupBoxContentType.TabStop = false;
            groupBoxContentType.Text = "Content Type";
            // 
            // downloadVideosCheck
            // 
            downloadVideosCheck.AutoSize = true;
            downloadVideosCheck.Location = new Point(9, 22);
            downloadVideosCheck.Name = "downloadVideosCheck";
            downloadVideosCheck.Size = new Size(61, 19);
            downloadVideosCheck.TabIndex = 16;
            downloadVideosCheck.Text = "Videos";
            downloadVideosCheck.UseVisualStyleBackColor = true;
            downloadVideosCheck.CheckedChanged += downloadVideosCheck_CheckedChanged;
            // 
            // downloadImagesCheck
            // 
            downloadImagesCheck.AutoSize = true;
            downloadImagesCheck.Location = new Point(75, 22);
            downloadImagesCheck.Name = "downloadImagesCheck";
            downloadImagesCheck.Size = new Size(64, 19);
            downloadImagesCheck.TabIndex = 17;
            downloadImagesCheck.Text = "Images";
            downloadImagesCheck.UseVisualStyleBackColor = true;
            downloadImagesCheck.CheckedChanged += downloadImagesCheck_CheckedChanged;
            // 
            // sortLabel
            // 
            sortLabel.AutoSize = true;
            sortLabel.Location = new Point(12, 25);
            sortLabel.Name = "sortLabel";
            sortLabel.Size = new Size(47, 15);
            sortLabel.TabIndex = 17;
            sortLabel.Text = "Sort by:";
            // 
            // sortComboBox
            // 
            sortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sortComboBox.FormattingEnabled = true;
            sortComboBox.Location = new Point(64, 22);
            sortComboBox.Name = "sortComboBox";
            sortComboBox.Size = new Size(121, 23);
            sortComboBox.TabIndex = 4;
            // 
            // periodLabel
            // 
            periodLabel.AutoSize = true;
            periodLabel.Location = new Point(195, 25);
            periodLabel.Name = "periodLabel";
            periodLabel.Size = new Size(44, 15);
            periodLabel.TabIndex = 19;
            periodLabel.Text = "Period:";
            // 
            // periodComboBox
            // 
            periodComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            periodComboBox.FormattingEnabled = true;
            periodComboBox.Location = new Point(245, 22);
            periodComboBox.Name = "periodComboBox";
            periodComboBox.Size = new Size(121, 23);
            periodComboBox.TabIndex = 5;
            // 
            // limitNumericUpDown
            // 
            limitNumericUpDown.Location = new Point(52, 53);
            limitNumericUpDown.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            limitNumericUpDown.Name = "limitNumericUpDown";
            limitNumericUpDown.Size = new Size(65, 23);
            limitNumericUpDown.TabIndex = 20;
            // 
            // limitLabel
            // 
            limitLabel.AutoSize = true;
            limitLabel.Location = new Point(12, 55);
            limitLabel.Name = "limitLabel";
            limitLabel.Size = new Size(37, 15);
            limitLabel.TabIndex = 21;
            limitLabel.Text = "Limit:";
            // 
            // Form1
            // 
            ClientSize = new Size(424, 605);
            Controls.Add(kofiPictureBox);
            Controls.Add(groupBoxFilters);
            Controls.Add(outputFolderLabel);
            Controls.Add(outputFolderBox);
            Controls.Add(apiKeyLabel);
            Controls.Add(apiKeyBox);
            Controls.Add(fileProgress);
            Controls.Add(overallProgress);
            Controls.Add(logBox);
            Controls.Add(statusLabel);
            Controls.Add(stopButton);
            Controls.Add(downloadButton);
            Controls.Add(usernameBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(426, 644);
            Name = "Form1";
            Text = "Civitai Content Downloader";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)kofiPictureBox).EndInit();
            groupBoxFilters.ResumeLayout(false);
            groupBoxFilters.PerformLayout();
            groupBoxRating.ResumeLayout(false);
            groupBoxRating.PerformLayout();
            groupBoxContentType.ResumeLayout(false);
            groupBoxContentType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)limitNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.ProgressBar overallProgress;
        private System.Windows.Forms.ProgressBar fileProgress;
        private System.Windows.Forms.TextBox apiKeyBox;
        private System.Windows.Forms.Label apiKeyLabel;
        private System.Windows.Forms.CheckBox sfwCheck;
        private System.Windows.Forms.CheckBox nsfwCheck;
        private System.Windows.Forms.TextBox outputFolderBox;
        private System.Windows.Forms.Label outputFolderLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox sortComboBox;
        private System.Windows.Forms.Label sortLabel;
        private System.Windows.Forms.ComboBox periodComboBox;
        private System.Windows.Forms.Label periodLabel;
        private System.Windows.Forms.NumericUpDown limitNumericUpDown;
        private System.Windows.Forms.Label limitLabel;
        private System.Windows.Forms.CheckBox downloadVideosCheck;
        private System.Windows.Forms.CheckBox downloadImagesCheck;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.GroupBox groupBoxRating;
        private System.Windows.Forms.GroupBox groupBoxContentType;
        private System.Windows.Forms.PictureBox kofiPictureBox;
    }
}