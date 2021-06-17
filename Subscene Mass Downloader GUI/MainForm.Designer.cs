
using System;

namespace Subscene_Mass_Downloader_GUI
{
    partial class mainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.btnGetSubsList = new System.Windows.Forms.Button();
            this.panelAction = new System.Windows.Forms.Panel();
            this.lblPosterStatus = new System.Windows.Forms.Label();
            this.lblSubsCount = new System.Windows.Forms.Label();
            this.pictureBoxPoster = new System.Windows.Forms.PictureBox();
            this.lblLink = new System.Windows.Forms.Label();
            this.btnSearchTitle = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.comboBoxLang = new System.Windows.Forms.ComboBox();
            this.panelSave = new System.Windows.Forms.Panel();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblDownloadStatus = new System.Windows.Forms.Label();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.listViewSubs = new System.Windows.Forms.ListView();
            this.colLang = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOwner = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerElapsedCounter = new System.Windows.Forms.Timer(this.components);
            this.panelAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).BeginInit();
            this.panelSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(25, 294);
            this.tbUrl.Margin = new System.Windows.Forms.Padding(4);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(299, 22);
            this.tbUrl.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbUrl, "Url to subscene\'s subtitle link");
            this.tbUrl.TextChanged += new System.EventHandler(this.tbUrl_TextChanged);
            // 
            // btnGetSubsList
            // 
            this.btnGetSubsList.Location = new System.Drawing.Point(25, 326);
            this.btnGetSubsList.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetSubsList.Name = "btnGetSubsList";
            this.btnGetSubsList.Size = new System.Drawing.Size(145, 28);
            this.btnGetSubsList.TabIndex = 1;
            this.btnGetSubsList.Text = "Get Subtitle List";
            this.toolTip1.SetToolTip(this.btnGetSubsList, "Retrieve all available subtitles from specified url");
            this.btnGetSubsList.UseVisualStyleBackColor = true;
            this.btnGetSubsList.Click += new System.EventHandler(this.btnGetSubsList_Click);
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.lblPosterStatus);
            this.panelAction.Controls.Add(this.lblSubsCount);
            this.panelAction.Controls.Add(this.pictureBoxPoster);
            this.panelAction.Controls.Add(this.lblLink);
            this.panelAction.Controls.Add(this.tbUrl);
            this.panelAction.Controls.Add(this.btnSearchTitle);
            this.panelAction.Controls.Add(this.btnDownload);
            this.panelAction.Controls.Add(this.comboBoxLang);
            this.panelAction.Controls.Add(this.btnGetSubsList);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAction.Location = new System.Drawing.Point(0, 0);
            this.panelAction.Margin = new System.Windows.Forms.Padding(4);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(338, 415);
            this.panelAction.TabIndex = 2;
            // 
            // lblPosterStatus
            // 
            this.lblPosterStatus.AutoSize = true;
            this.lblPosterStatus.Location = new System.Drawing.Point(22, 9);
            this.lblPosterStatus.Name = "lblPosterStatus";
            this.lblPosterStatus.Size = new System.Drawing.Size(0, 17);
            this.lblPosterStatus.TabIndex = 8;
            // 
            // lblSubsCount
            // 
            this.lblSubsCount.AutoSize = true;
            this.lblSubsCount.Location = new System.Drawing.Point(152, 274);
            this.lblSubsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubsCount.Name = "lblSubsCount";
            this.lblSubsCount.Size = new System.Drawing.Size(0, 17);
            this.lblSubsCount.TabIndex = 6;
            // 
            // pictureBoxPoster
            // 
            this.pictureBoxPoster.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPoster.Image")));
            this.pictureBoxPoster.Location = new System.Drawing.Point(24, 49);
            this.pictureBoxPoster.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxPoster.Name = "pictureBoxPoster";
            this.pictureBoxPoster.Size = new System.Drawing.Size(293, 221);
            this.pictureBoxPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPoster.TabIndex = 5;
            this.pictureBoxPoster.TabStop = false;
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(21, 274);
            this.lblLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(85, 17);
            this.lblLink.TabIndex = 7;
            this.lblLink.Text = "Subtitle Link";
            // 
            // btnSearchTitle
            // 
            this.btnSearchTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearchTitle.Location = new System.Drawing.Point(24, 380);
            this.btnSearchTitle.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearchTitle.Name = "btnSearchTitle";
            this.btnSearchTitle.Size = new System.Drawing.Size(148, 28);
            this.btnSearchTitle.TabIndex = 5;
            this.btnSearchTitle.Text = "Search Title";
            this.btnSearchTitle.UseVisualStyleBackColor = true;
            this.btnSearchTitle.Click += new System.EventHandler(this.btnSearchTitle_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(178, 380);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(148, 28);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Download Selected";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // comboBoxLang
            // 
            this.comboBoxLang.FormattingEnabled = true;
            this.comboBoxLang.Items.AddRange(new object[] {
            "Albanian",
            "Arabic",
            "Armenian",
            "Azerbaijani",
            "Basque",
            "Belarusian",
            "Bengali",
            "Big 5 code",
            "Bosnian",
            "Brazillian Portuguese",
            "Bulgarian",
            "Bulgarian/ English",
            "Burmese",
            "Cambodian/Khmer",
            "Catalan",
            "Chinese BG code",
            "Croatian",
            "Czech",
            "Danish",
            "Dutch",
            "Dutch/ English",
            "English",
            "English/ German",
            "Esperanto",
            "Estonian",
            "Farsi/Persian",
            "Finnish",
            "French",
            "Georgian",
            "German",
            "Greek",
            "Greenlandic",
            "Hebrew",
            "Hindi",
            "Hungarian",
            "Hungarian/ English",
            "Icelandic",
            "Indonesian",
            "Italian",
            "Japanese",
            "Kannada",
            "Korean",
            "Kurdish",
            "Latvian",
            "Lithuanian",
            "Macedonian",
            "Malay",
            "Malayalam",
            "Manipuri",
            "Mongolian",
            "Nepali",
            "Norwegian",
            "Pashto",
            "Polish",
            "Portuguese",
            "Punjabi",
            "Romanian",
            "Russian",
            "Serbian",
            "Sinhala",
            "Slovak",
            "Slovenian",
            "Somali",
            "Spanish",
            "Sundanese",
            "Swahili",
            "Swedish",
            "Tagalog",
            "Tamil",
            "Telugu",
            "Thai",
            "Turkish",
            "Ukrainian",
            "Urdu",
            "Vietnamese",
            "Yoruba"});
            this.comboBoxLang.Location = new System.Drawing.Point(173, 326);
            this.comboBoxLang.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxLang.Name = "comboBoxLang";
            this.comboBoxLang.Size = new System.Drawing.Size(150, 24);
            this.comboBoxLang.TabIndex = 2;
            this.comboBoxLang.Text = "Available Language";
            this.comboBoxLang.SelectedIndexChanged += new System.EventHandler(this.comboBoxLang_SelectedIndexChanged);
            // 
            // panelSave
            // 
            this.panelSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSave.Controls.Add(this.lblElapsed);
            this.panelSave.Controls.Add(this.lblPath);
            this.panelSave.Controls.Add(this.lblDownloadStatus);
            this.panelSave.Controls.Add(this.pbDownload);
            this.panelSave.Controls.Add(this.tbPath);
            this.panelSave.Controls.Add(this.btnSelectPath);
            this.panelSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSave.Location = new System.Drawing.Point(0, 415);
            this.panelSave.Margin = new System.Windows.Forms.Padding(4);
            this.panelSave.Name = "panelSave";
            this.panelSave.Size = new System.Drawing.Size(882, 88);
            this.panelSave.TabIndex = 3;
            // 
            // lblElapsed
            // 
            this.lblElapsed.AutoSize = true;
            this.lblElapsed.Location = new System.Drawing.Point(732, 62);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(0, 17);
            this.lblElapsed.TabIndex = 9;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(15, 11);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(91, 17);
            this.lblPath.TabIndex = 7;
            this.lblPath.Text = "Download To";
            this.toolTip1.SetToolTip(this.lblPath, "Open The Following Path");
            this.lblPath.Click += new System.EventHandler(this.lblPath_Click);
            // 
            // lblDownloadStatus
            // 
            this.lblDownloadStatus.AutoSize = true;
            this.lblDownloadStatus.Location = new System.Drawing.Point(14, 62);
            this.lblDownloadStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            this.lblDownloadStatus.Size = new System.Drawing.Size(0, 17);
            this.lblDownloadStatus.TabIndex = 6;
            // 
            // pbDownload
            // 
            this.pbDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDownload.Location = new System.Drawing.Point(18, 37);
            this.pbDownload.Margin = new System.Windows.Forms.Padding(2);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(858, 22);
            this.pbDownload.TabIndex = 5;
            this.toolTip1.SetToolTip(this.pbDownload, "Download Progress Bar");
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.Enabled = false;
            this.tbPath.Location = new System.Drawing.Point(113, 7);
            this.tbPath.Margin = new System.Windows.Forms.Padding(4, 4, 28, 4);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(730, 22);
            this.tbPath.TabIndex = 0;
            this.toolTip1.SetToolTip(this.tbPath, "Downloaded Subtitle(s) saved to this path");
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPath.Location = new System.Drawing.Point(844, 6);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(32, 25);
            this.btnSelectPath.TabIndex = 1;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // listViewSubs
            // 
            this.listViewSubs.CheckBoxes = true;
            this.listViewSubs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLang,
            this.colTitle,
            this.colOwner,
            this.colRating});
            this.listViewSubs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSubs.FullRowSelect = true;
            this.listViewSubs.HideSelection = false;
            this.listViewSubs.Location = new System.Drawing.Point(338, 0);
            this.listViewSubs.Margin = new System.Windows.Forms.Padding(4);
            this.listViewSubs.Name = "listViewSubs";
            this.listViewSubs.Size = new System.Drawing.Size(544, 415);
            this.listViewSubs.TabIndex = 4;
            this.listViewSubs.UseCompatibleStateImageBehavior = false;
            this.listViewSubs.View = System.Windows.Forms.View.Details;
            // 
            // colLang
            // 
            this.colLang.Name = "colLang";
            this.colLang.Text = "Language";
            this.colLang.Width = 78;
            // 
            // colTitle
            // 
            this.colTitle.Name = "colTitle";
            this.colTitle.Text = "Title";
            this.colTitle.Width = 257;
            // 
            // colOwner
            // 
            this.colOwner.Name = "colOwner";
            this.colOwner.Text = "Owner";
            this.colOwner.Width = 82;
            // 
            // colRating
            // 
            this.colRating.Name = "colRating";
            this.colRating.Text = "Rating";
            this.colRating.Width = 100;
            // 
            // timerElapsedCounter
            // 
            this.timerElapsedCounter.Tick += new System.EventHandler(this.timerElapsedCounter_Tick);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(882, 503);
            this.Controls.Add(this.listViewSubs);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subscene Mass Downloader GUI";
            this.ResizeEnd += new System.EventHandler(this.mainWindow_ResizeEnd);
            this.Resize += new System.EventHandler(this.mainWindow_Resize);
            this.panelAction.ResumeLayout(false);
            this.panelAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPoster)).EndInit();
            this.panelSave.ResumeLayout(false);
            this.panelSave.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.ComboBox comboBoxLang;
        private System.Windows.Forms.Panel panelSave;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.ListView listViewSubs;
        private System.Windows.Forms.ColumnHeader colLang;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colOwner;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblSubsCount;
        private System.Windows.Forms.Label lblDownloadStatus;
        private System.Windows.Forms.ColumnHeader colRating;
        public System.Windows.Forms.Button btnGetSubsList;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button btnSearchTitle;
        public System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.PictureBox pictureBoxPoster;
        private System.Windows.Forms.Label lblPosterStatus;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.Timer timerElapsedCounter;
    }
}

