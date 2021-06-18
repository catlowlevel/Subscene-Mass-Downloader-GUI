using SubLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
namespace Subscene_Mass_Downloader_GUI
{
    public partial class mainWindow : Form
    {
        private static Stopwatch stopwatch;
        private static Image _subsceneImage;
        private static SearchForm _searchForm;
        private static AnimateText _animateLblPosterStatus;
        private static ListViewColumnSorter lvwColumnSorter;
        public mainWindow()
        {
            InitializeComponent();
            var path = Properties.Settings.Default.lastPath;
            tbPath.Text = string.IsNullOrEmpty(path) ? Application.StartupPath : path;
            tbUrl.Text = "https://subscene.com/subtitles/chicago-typewriter-sikago-tajagi";

            updateListViewColumnWitdh();
            updateLabelElapsedTimePosition();

            _subsceneImage = (Image)pictureBoxPoster.Image.Clone();
            _searchForm = new SearchForm();

            lvwColumnSorter = new ListViewColumnSorter();
            listViewSubs.ColumnClick += listViewSubs_ColumnClick;

            _animateLblPosterStatus = new AnimateText(lblPosterStatus, "", 300,
                "Loading.", "Loading..", "Loading...", "Loading..");
        }

        private void refreshComboBoxLang(List<SubtitleModel> subtitles)
        {
            comboBoxLang.Items.Clear();
            comboBoxLang.Tag = subtitles;
            comboBoxLang.Items.Add("All");
            foreach (var subtitle in subtitles)
            {
                if (comboBoxLang.Items.Contains(subtitle.Language) == false)
                {
                    comboBoxLang.Items.Add(subtitle.Language);
                }
            }
            comboBoxLang.SelectedIndex = 0;
        }
        private string comboBoxLangValue => comboBoxLang.SelectedItem.ToString();
        private void listSubsToListView(List<SubtitleModel> subtitles, string lang)
        {
            listViewSubs.Items.Clear();
            listViewSubs.ListViewItemSorter = null;
            listViewSubs.BeginUpdate();
            int subCount = 0;
            foreach (var subtitle in subtitles)
            {
                if (lang != "All")
                {
                    if (subtitle.Language != lang)
                        continue;
                }
                ListViewItem subItem = new ListViewItem(subtitle.Language);
                subItem.Tag = subtitle;
                subItem.SubItems.Add(subtitle.Title);
                subItem.SubItems.Add(subtitle.Owner);
                subItem.SubItems.Add(subtitle.Rating);
                //subItem.SubItems.Add(subtitle.Link);
                listViewSubs.Items.Add(subItem);
                subCount++;
            }
            listViewSubs.EndUpdate();
            listViewSubs.ListViewItemSorter = lvwColumnSorter;
            lblSubsCount.Text = $"Available Subtitle(s) : {subCount}";
        }


        private async Task loadShowInformationAsync(string page)
        {
            var pageMatch = new RegexMatch(page, RegexPattern.ShowReleaseYear, RegexPattern.ShowTitle, RegexPattern.ShowPoster);
            var year = pageMatch.Results[0].Matches.First()[1];
            var title = pageMatch.Results[1].Matches.First()[1];

            toolTip1.SetToolTip(pictureBoxPoster, $"Title\t: {title}\nYear\t: {year}");

            if (pageMatch.Results[2].NoMatch)
            {
                _animateLblPosterStatus.Stop();
                lblPosterStatus.Text = $"Title\t: {title}\nRelease Year\t: {year}";
                pictureBoxPoster.Image = _subsceneImage;
                return;
            }
            var img = pageMatch.Results[2].Matches.First()[1]; ;
            pictureBoxPoster.Image = await WebHelper.GetImageAsync(img);
            _animateLblPosterStatus.Stop();
            lblPosterStatus.Text = $"{title}\nRelease Year\t: {year}";
            while (lblPosterStatus.Size.Width + lblPosterStatus.Location.X >= listViewSubs.Location.X)
            {
                title = title.Remove(title.Length - 5, 5) + "...";
                lblPosterStatus.Text = $"{title}\nRelease Year\t: {year}";
            }

        }

        private void cleanUp()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void updateListViewColumnWitdh()
        {
            int w = listViewSubs.Size.Width;

            var langW = valueOfPercentage(10, w);
            var titleW = valueOfPercentage(60, w);
            var ownerW = valueOfPercentage(15, w);
            var ratingW = valueOfPercentage(13, w);
            //var total = langW + titleW + ownerW + ratingW;

            listViewSubs.Columns["colLang"].Width = (int)langW;
            listViewSubs.Columns["colTitle"].Width = (int)titleW;
            listViewSubs.Columns["colOwner"].Width = (int)ownerW;
            listViewSubs.Columns["colRating"].Width = (int)ratingW;
        }

        private void updateLabelElapsedTimePosition()
        {
            lblElapsed.Location = new Point(panelSave.Width - (int)(lblElapsed.Width * 1.1m), panelSave.Height - (int)(lblElapsed.Height * 1.5m));
        }
        private decimal valueOfPercentage(decimal percentage, decimal num)
        {
            return (percentage * 0.01m) * num;
        }
        #region UI Event Handler
        #region Button
        private void onGetSubList()
        {
            btnGetSubsList.Enabled = false;
            btnDownload.Enabled = false;
        }
        private void onStopGetList()
        {
            btnGetSubsList.Enabled = true;
            btnDownload.Enabled = true;
            _animateLblPosterStatus.Stop();
            lblPosterStatus.Text = "";

        }

        private async void btnGetSubsList_Click(object sender, EventArgs e)
        {
            cleanUp();
            onGetSubList();
            _ = _animateLblPosterStatus.Start();

            List<SubtitleModel> subList;
            string page;
            try
            {
                page = await WebHelper.DownloadStringAsync(tbUrl.Text);
                page = HttpUtility.HtmlDecode(page);
                subList = SubtitleManager.ParseSubtitlesPage(page);
            }
            catch (Exception ex)
            {
                onStopGetList();
                MessageBox.Show(ex.Message);
                return;
            }

            refreshComboBoxLang(subList);
            listSubsToListView(subList, comboBoxLangValue);

            onStopGetList();

            _ = loadShowInformationAsync(page);

        }


        private void btnSearchTitle_Click(object sender, EventArgs e)
        {
            _searchForm.ShowDialog(this);
        }

        private void onStartDownload()
        {
            btnDownload.Enabled = false;
            pbDownload.Value = 0;
            stopwatch = Stopwatch.StartNew();
            timerElapsedCounter.Start();
        }
        private void onStopDownload()
        {
            btnDownload.Enabled = true;
            stopwatch.Stop();
            timerElapsedCounter.Stop();
        }
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            List<SubtitleModel> sub2Download = new List<SubtitleModel>();
            foreach (ListViewItem item in listViewSubs.Items)
            {
                if (item.Checked)
                {
                    SubtitleModel subtitle = (SubtitleModel)item.Tag;
                    sub2Download.Add(subtitle);
                }
            }
            if (sub2Download.Count == 0)
            {
                MessageBox.Show("Choose atleast one subtitle to download", "SMD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            onStartDownload();
            var downloaded = 0;
            await SubtitleManager.DownloadSubtitlesAsync(sub2Download, tbPath.Text, _ =>
            {
                var count = _.Count(task => task.IsCompleted);
                var total = _.Count;
                pbDownload.Value = (count * 100) / total;
                downloaded = count;
                lblDownloadStatus.Text = $"{pbDownload.Value}% | {count}/{total} Downloaded";
            });

            lblDownloadStatus.Text = $"{downloaded} Subtitle(s) Downloaded";
            onStopDownload();
            MessageBox.Show(null, "Done", "SMD", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            try
            {
                using (var vfbd = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog())
                {
                    if (vfbd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(vfbd.SelectedPath))
                    {
                        tbPath.Text = vfbd.SelectedPath;
                        Properties.Settings.Default.lastPath = tbPath.Text;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        #endregion
        private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SubtitleModel> subtitles = (List<SubtitleModel>)(sender as ComboBox).Tag;
            if (subtitles != null && subtitles.Count > 0)
            {
                listSubsToListView(subtitles, comboBoxLangValue);
            }
        }

        private void listViewSubs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            listViewSubs.Sort();
        }

        private void mainWindow_Resize(object sender, EventArgs e)
        {
            updateLabelElapsedTimePosition();
            if (listViewSubs.Items.Count == 0)
                updateListViewColumnWitdh();
        }

        private void mainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (listViewSubs.Items.Count > 0)
                updateListViewColumnWitdh();
        }
        private void lblPath_Click(object sender, EventArgs e)
        {
            Process.Start(tbPath.Text);
        }

        private void timerElapsedCounter_Tick(object sender, EventArgs e)
        {
            lblElapsed.Text = $"Elapsed {String.Format("{0:0.00}", stopwatch.Elapsed.TotalSeconds)} s";
            updateLabelElapsedTimePosition();
        }
        private void tbUrl_TextChanged(object sender, EventArgs e)
        {
            string staticText = "https://subscene.com/subtitles/";
            if (!tbUrl.Text.StartsWith(staticText))
            {
                if (tbUrl.Text.Length < staticText.Length)
                {
                    tbUrl.Text = staticText;
                    tbUrl.Select(tbUrl.Text.Length, 0);
                }
                for (int i = 0; i < tbUrl.Text.Length; i++)
                {
                    if (i > staticText.Length) break;
                    if (tbUrl.Text[i] != staticText[i])
                    {
                        tbUrl.Text = tbUrl.Text.Remove(i, 1);
                        tbUrl.Select(tbUrl.Text.Length, 0);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
