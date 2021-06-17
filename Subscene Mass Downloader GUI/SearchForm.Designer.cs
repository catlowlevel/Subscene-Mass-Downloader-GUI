
namespace Subscene_Mass_Downloader_GUI
{
    partial class SearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelShowList = new System.Windows.Forms.Panel();
            this.panelAction = new System.Windows.Forms.Panel();
            this.lblShowCount = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnPopularShow = new System.Windows.Forms.Button();
            this.panelAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(12, 29);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(527, 22);
            this.tbTitle.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(456, 57);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(83, 30);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelShowList
            // 
            this.panelShowList.AutoScroll = true;
            this.panelShowList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShowList.Location = new System.Drawing.Point(0, 97);
            this.panelShowList.Name = "panelShowList";
            this.panelShowList.Size = new System.Drawing.Size(553, 327);
            this.panelShowList.TabIndex = 2;
            // 
            // panelAction
            // 
            this.panelAction.AutoScroll = true;
            this.panelAction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAction.Controls.Add(this.btnPopularShow);
            this.panelAction.Controls.Add(this.lblShowCount);
            this.panelAction.Controls.Add(this.lblTitle);
            this.panelAction.Controls.Add(this.btnSearch);
            this.panelAction.Controls.Add(this.tbTitle);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAction.Location = new System.Drawing.Point(0, 0);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(553, 97);
            this.panelAction.TabIndex = 3;
            // 
            // lblShowCount
            // 
            this.lblShowCount.AutoSize = true;
            this.lblShowCount.Location = new System.Drawing.Point(10, 70);
            this.lblShowCount.Name = "lblShowCount";
            this.lblShowCount.Size = new System.Drawing.Size(0, 17);
            this.lblShowCount.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(9, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(83, 17);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Show Name";
            // 
            // btnPopularShow
            // 
            this.btnPopularShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPopularShow.Location = new System.Drawing.Point(306, 57);
            this.btnPopularShow.Name = "btnPopularShow";
            this.btnPopularShow.Size = new System.Drawing.Size(144, 30);
            this.btnPopularShow.TabIndex = 4;
            this.btnPopularShow.Text = "Popular Show";
            this.btnPopularShow.UseVisualStyleBackColor = true;
            this.btnPopularShow.Click += new System.EventHandler(this.btnPopularShow_Click);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(553, 424);
            this.Controls.Add(this.panelShowList);
            this.Controls.Add(this.panelAction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(571, 269);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Show";
            this.panelAction.ResumeLayout(false);
            this.panelAction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelShowList;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblShowCount;
        private System.Windows.Forms.Button btnPopularShow;
    }
}