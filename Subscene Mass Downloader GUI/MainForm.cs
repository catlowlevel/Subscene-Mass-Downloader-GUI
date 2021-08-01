using SubLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
namespace Subscene_Mass_Downloader_GUI
{
    public partial class mainWindow : Form
    {
        private Stopwatch stopwatch;
        private Image _subsceneImage;
        private SearchForm _searchForm;
        private AnimateText _animateLblPosterStatus;
        private AnimateText _animateLblDlStatus;
        private ListViewColumnSorter lvwColumnSorter;
        public mainWindow()
        {
            InitializeComponent();
            var path = Properties.Settings.Default.lastPath;
            tbPath.Text = string.IsNullOrEmpty(path) ? Application.StartupPath : path;
            tbUrl.Text = "https://subscene.com/subtitles/breaking-bad-first-season";

            listViewSubs.GetType()
            .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .SetValue(listViewSubs, true, null); //https://stackoverflow.com/a/42389596


            updateListViewColumnWitdh();
            updateLabelElapsedTimePosition();

            _subsceneImage = (Image)pictureBoxPoster.Image.Clone();
            _searchForm = new SearchForm();

            lvwColumnSorter = new ListViewColumnSorter();
            listViewSubs.ColumnClick += listViewSubs_ColumnClick;

            _animateLblPosterStatus = new AnimateText(lblPosterStatus, "", 300,
                "Loading.", "Loading..", "Loading...", "Loading..");

            _animateLblDlStatus = new AnimateText(lblDownloadStatus, "", 400,
                "Fetching Data.",
                "Fetching Data..",
                "Fetching Data...",
                "Fetching Data..",
                "Fetching Data."
                );
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
        private string comboBoxLangValue => comboBoxLang.SelectedItem?.ToString();

        private int addSubsToListView(List<SubtitleModel> subtitles, string lang)
        {
            var lvItems = subtitles.Select(subtitle =>
            {
                ListViewItem subItem = new ListViewItem(subtitle.Language);
                subItem.Tag = subtitle;
                subItem.SubItems.Add(subtitle.Title);
                subItem.SubItems.Add(subtitle.Owner);
                subItem.SubItems.Add(subtitle.Rating);
                return subItem;
            }).Where(lvItem =>
            {
                var sub = lvItem.Tag as SubtitleModel;
                if (lang == "All")
                {
                    return true;
                }
                else if (lang == sub.Language)
                {
                    return true;
                }

                return false;
            }).ToArray();

            listViewSubs.Items.AddRange(lvItems);
            Application.DoEvents();
            return lvItems.Count();
        }

        private bool lvProcessing = false;
        private void listSubsToListView(List<SubtitleModel> subtitles, string lang)
        {
            if (lvProcessing) return;
            lvProcessing = true;
            listViewSubs.Items.Clear();
            listViewSubs.ListViewItemSorter = null;

            var subsChunks = Utility.SplitList(subtitles, 100);

            int subCount = 0;
            foreach (var subs in subsChunks)
            {
                subCount += addSubsToListView(subs, lang);
            }

       
            listViewSubs.ListViewItemSorter = lvwColumnSorter;
            lblSubsCount.Text = $"Available Subtitle(s) : {subCount}";
            lvProcessing = false;
        }


        private async Task loadPoster(string page)
        {
            var show = ShowManager.GetShowInfo(page);

            toolTip1.SetToolTip(pictureBoxPoster, $"Title\t: {show.Title}\nYear\t: {show.ReleaseYear}");

            pictureBoxPoster.Image = string.IsNullOrEmpty(show.PosterUrl) ? _subsceneImage : await WebHelper.GetImageAsync(show.PosterUrl);

            _animateLblPosterStatus.Stop();

            var title = show.Title;
            lblPosterStatus.Text = $"{title}\nRelease Year\t: {show.ReleaseYear}";
            while (lblPosterStatus.Size.Width + lblPosterStatus.Location.X >= listViewSubs.Location.X)
            {
                title = title.Remove(title.Length - 5, 5) + "...";
                lblPosterStatus.Text = $"{title}\nRelease Year\t: {show.ReleaseYear}";
            }

        }

        private List<SubtitleModel> filterSubtitles(List<SubtitleModel> subtitles, string filter)
        {
            List<SubtitleModel> filteredSubs = new List<SubtitleModel>();

            try
            {

                if (cbRegex.Checked)
                {
                    var reg = new Regex(filter.ToLower());
                    filteredSubs.AddRange(subtitles.Where(i => string.IsNullOrEmpty(filter) ||
                    reg.Match(i.Title.ToLower()).Success ||
                    reg.Match(i.Owner.ToLower()).Success ||
                    reg.Match(i.Language.ToLower()).Success

                    ).ToArray());
                }
                else
                {
                    filteredSubs.AddRange(subtitles.Where(i => string.IsNullOrEmpty(filter) ||
                    i.Title.ToLower().Contains(filter.ToLower()) ||
                    i.Owner.ToLower().Contains(filter.ToLower()) ||
                    i.Language.ToLower().Contains(filter.ToLower())


                    ).ToArray());
                }
            }
            catch (Exception)
            {

            }

            return filteredSubs;
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

            //var langW = valueOfPercentage(10, w);
            var langW = 100;
            var titleW = valueOfPercentage(60, w - langW);
            var ownerW = valueOfPercentage(17, w - langW);
            var ratingW = valueOfPercentage(17, w - langW);
            //var total = langW + titleW + ownerW + ratingW;

            listViewSubs.Columns["colLang"].Width = langW;
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

            onStopGetList();
            _ = loadPoster(page);

            ctbFilter.Tag = subList;
            refreshComboBoxLang(subList);
            listSubsToListView(subList, comboBoxLangValue);
            SystemSounds.Exclamation.Play();
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
            _animateLblDlStatus.Stop();
        }
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            List<SubtitleModel> sub2Download = new List<SubtitleModel>();
            var selectedTag = listViewSubs.CheckedItems.Cast<ListViewItem>().Select(x => x.Tag);
            sub2Download.AddRange(selectedTag.Cast<SubtitleModel>());

            if (sub2Download.Count == 0)
            {
                MessageBox.Show("Check atleast one subtitle to download", "SMD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            onStartDownload();
            var downloaded = 0;
            bool once = true;
            var __ = _animateLblDlStatus.Start();
            List<Task<string>> tasks = null;
            await SubtitleManager.DownloadSubtitlesAsync(sub2Download, tbPath.Text, _ =>
            {
                var count = _.Count(task => task.IsCompleted);
                var total = _.Count;
                pbDownload.Value = (count * 100) / total;
                downloaded = count;
                if (downloaded > 0)
                {
                    lblDownloadStatus.Text = $"{pbDownload.Value}% | {count}/{total} Downloading";
                    if (once)
                    {
                        _animateLblDlStatus.Stop();
                    }
                    once = false;
                }
                if (tasks == null) tasks = _;
            });

            //TODO: Log the reason for this fails
            var fail = tasks.Count(t => t.IsFaulted);

            onStopDownload();
            lblDownloadStatus.Text = $"{downloaded - fail}/{sub2Download.Count} Subtitle(s) Downloaded";
            MessageBox.Show(null, "Done", "SMD", MessageBoxButtons.OK, fail == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Exclamation);
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
                comboBoxLang.Enabled = false;
                listSubsToListView(subtitles, comboBoxLangValue);
                comboBoxLang.Enabled = true;
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


        private void mainWindow_ResizeBegin(object sender, EventArgs e)
        {

        }
        private void mainWindow_Resize(object sender, EventArgs e)
        {
            return;
            //updateLabelElapsedTimePosition();
            //if (listViewSubs.Items.Count == 0 || WindowState == FormWindowState.Maximized)
            //    updateListViewColumnWitdh();
        }

        private void mainWindow_ResizeEnd(object sender, EventArgs e)
        {
            updateListViewColumnWitdh();
        }
        private void lblPath_Click(object sender, EventArgs e)
        {
            Process.Start(tbPath.Text);
        }

        private void timerElapsedCounter_Tick(object sender, EventArgs e)
        {
            lblElapsed.Text = $"Elapsed {string.Format("{0:0.00}", stopwatch.Elapsed.TotalSeconds)} s";
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

        private void ctbFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBoxLangValue)) return;
                var subList = ((CTextBox)sender).Tag as List<SubtitleModel>;
                subList = filterSubtitles(subList, ctbFilter.Text);
                listSubsToListView(subList, comboBoxLangValue);
            }
            catch (Exception)
            {

            }
        }


        private void cbRegex_CheckedChanged(object sender, EventArgs e)
        {
            ctbFilter_TextChanged(ctbFilter, null);
            ctbFilter.Focus();
        }

        #endregion


    }
}
