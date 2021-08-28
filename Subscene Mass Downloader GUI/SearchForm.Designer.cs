
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
            this.btnPopularShow = new System.Windows.Forms.Button();
            this.lblShowCount = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(10, 23);
            this.tbTitle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(423, 20);
            this.tbTitle.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(366, 46);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(66, 24);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelShowList
            // 
            this.panelShowList.AutoScroll = true;
            this.panelShowList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShowList.Location = new System.Drawing.Point(0, 78);
            this.panelShowList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelShowList.Name = "panelShowList";
            this.panelShowList.Size = new System.Drawing.Size(444, 261);
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
            this.panelAction.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(444, 78);
            this.panelAction.TabIndex = 3;
            // 
            // btnPopularShow
            // 
            this.btnPopularShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPopularShow.Location = new System.Drawing.Point(246, 46);
            this.btnPopularShow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPopularShow.Name = "btnPopularShow";
            this.btnPopularShow.Size = new System.Drawing.Size(115, 24);
            this.btnPopularShow.TabIndex = 4;
            this.btnPopularShow.Text = "Popular Show";
            this.btnPopularShow.UseVisualStyleBackColor = true;
            this.btnPopularShow.Click += new System.EventHandler(this.btnPopularShow_Click);
            // 
            // lblShowCount
            // 
            this.lblShowCount.AutoSize = true;
            this.lblShowCount.Location = new System.Drawing.Point(8, 56);
            this.lblShowCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblShowCount.Name = "lblShowCount";
            this.lblShowCount.Size = new System.Drawing.Size(0, 13);
            this.lblShowCount.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(7, 7);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(65, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Show Name";
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(444, 339);
            this.Controls.Add(this.panelShowList);
            this.Controls.Add(this.panelAction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(460, 223);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Show";
            this.Load += new System.EventHandler(this.SearchForm_Load_1);
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