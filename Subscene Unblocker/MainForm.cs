using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SubLibrary;
namespace Subscene_Unblocker
{
    public partial class mainWindow : Form
    {
        private static AnimateText _animateStatus;
        private static string subsceneUrl = "https://subscene.com/";
        private static string hostPath = @"C:\Windows\System32\drivers\etc\hosts";
        private int _attempCount = 0;
        public mainWindow()
        {
            InitializeComponent();

            _animateStatus = new AnimateText(lblStatus, "", 500,
                "Checking if https://subscene.com/ is accessible.",
                "Checking if https://subscene.com/ is accessible..",
                "Checking if https://subscene.com/ is accessible...",
                "Checking if https://subscene.com/ is accessible.."
                );


        }

        private async Task<bool> CheckResponse()
        {
            try
            {
                _ = _animateStatus.Start();
                var html = await WebHelper.DownloadStringAsync(subsceneUrl);
                _animateStatus.Stop();
                return true;
            }
            catch (Exception)
            {
                _animateStatus.Stop();
                return false;
            }
        }

        private async void mainWindow_Load(object sender, EventArgs e)
        {
            lblStatus.ForeColor = Color.Black;
            var result = await CheckResponse();
            if (result)
            {
                IsUnblocked();
            }
            else
            {
                IsBlocked();
            }
        }

        private void IsBlocked()
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "subscene.com is inaccessible\ntry to unblock it?";
            Button btnOk = new Button
            {
                Name = "btnOk",
                Text = "Yes",
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(lblStatus.Location.X, lblStatus.Location.Y + lblStatus.Height + 10)
            };
            Button btnNo = new Button
            {
                Name = "btnNo",
                Text = "No",
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(lblStatus.Location.X + btnOk.Width + 10, lblStatus.Location.Y + lblStatus.Height + 10)
            };
            btnOk.Click += btnOk_Click;
            btnNo.Click += btnNo_Click;
            Controls.Add(btnOk);
            Controls.Add(btnNo);
        }
        private void cleanUp()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        private void btnNo_Click(object sender, EventArgs e)
        {
            ClearButton();
            cleanUp();
            mainWindow_Load(this, null);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                PerformUnblock();
                btnNo_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Subscene Unblocker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PerformUnblock()
        {
            if (_attempCount != 0) throw new Exception("Something Went Wrong!");
            _attempCount++;
            var fileWrite = new StreamWriter(new FileStream(hostPath, FileMode.Open));
            fileWrite.BaseStream.Seek(0, SeekOrigin.End);
            string[] subscene = {
                "104.27.207.92 subscene.com www.subscene.com forum.subscene.com c.subscene.com papi.subscene.com",
                "104.27.206.92 v2.subscene.com u.subscene.com" };
            fileWrite.WriteLine();
            fileWrite.WriteLine();
            fileWrite.WriteLine("# Subscene");

            foreach (var sub in subscene)
            {
                fileWrite.WriteLine(sub);
            }
            fileWrite.Flush();
            fileWrite.Close();
        }

        private void ClearButton()
        {
            var btnList = new List<Button>();
            foreach (var obj in Controls)
            {
                if (obj is Button btn)
                {
                    btnList.Add(btn);
                }
            }
            foreach (var btn in btnList)
            {
                Controls.Remove(btn);
            }
        }

        private void IsUnblocked()
        {
            lblStatus.ForeColor = Color.Green;
            lblStatus.Text = "Succesfully getting response from subscene.com";
            Button btnClose = new Button
            {
                Name = "btnClose",
                Text = "Exit",
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(lblStatus.Location.X, lblStatus.Location.Y + lblStatus.Height + 10)
            };
            btnClose.Click += (sender, e) => Close();
            Controls.Add(btnClose);
        }

    }
}
