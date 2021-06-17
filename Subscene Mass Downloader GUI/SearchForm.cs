using SubLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Subscene_Mass_Downloader_GUI
{
    public partial class SearchForm : Form
    {
        private static Button _exitBtn;
        public SearchForm()
        {
            InitializeComponent();

            tbTitle.Text = "typewriter";
            AcceptButton = btnSearch;

            _exitBtn = new Button();
            _exitBtn.Click += (sender, e) => Close();
            CancelButton = _exitBtn;
        }

        private void listShows(List<ShowModel> shows)
        {
            panelShowList.Controls.Clear();
            for (int y = 0; y < shows.Count; y++)
            {
                var show = shows[y];
                var text = $"{show.Name} : Approx. {show.SubCount}";
                LinkLabel lLabel = new LinkLabel
                {
                    //TextAlign = ContentAlignment.MiddleLeft,
                    AutoSize = true,
                    Text = text,
                    Tag = show,
                    Location = new Point(tbTitle.Location.X, (25 * y)),
                    //Size = new Size(text.Length, 25),
                };
                lLabel.Click += Show_Click;
                panelShowList.Controls.Add(lLabel);
            }

            lblShowCount.Text = $"Found {shows.Count} Shows:";
        }

        private async Task<string> GetShowPageAsync(string title)
        {
            try
            {

                var page = await WebHelper.DownloadStringAsync(string.Format(ShowManager.subSearchApi, title));
                return HttpUtility.HtmlDecode(page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task PauseSearchButton()
        {
            btnSearch.Enabled = false;
            await Task.Delay(5000);
            btnSearch.Enabled = true;
        }
        private void Show_Click(object sender, EventArgs e)
        {
            ShowModel show = (ShowModel)((LinkLabel)sender).Tag;
            Owner.Controls["panelAction"].Controls["tbUrl"].Text = string.Format(SubtitleManager.baseUrl, show.Link);
            Button btn = Owner.Controls["panelAction"].Controls["btnGetSubsList"] as Button;
            btn.PerformClick();
            this.Close();
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _ = PauseSearchButton();
            string page = String.Empty;
            List<ShowModel> showList = null;

            try
            {
                page = await GetShowPageAsync(tbTitle.Text);
                showList = ShowManager.ParseShowPage(page);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            List<ShowModel> filteredShows = new List<ShowModel>();
            foreach (var show in showList)
            {
                var itemLink = show.Link;
                var sameItem = false;
                foreach (var newItem in filteredShows)
                {
                    if (itemLink == newItem.Link)
                    {
                        sameItem = true;
                        break;
                    }
                }
                if (sameItem == false)
                {
                    filteredShows.Add(show);
                }
            }

            listShows(filteredShows);

        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            tbTitle.Focus();
            tbTitle.SelectAll();
        }

        private async void btnPopularShow_Click(object sender, EventArgs e)
        {
            try
            {
                var page = await WebHelper.DownloadStringAsync(string.Format(SubtitleManager.baseUrl, ""));
                page = HttpUtility.HtmlDecode(page);
                var popularMatch = new RegexMatch(page, RegexPattern.PopularShow, RegexPattern.PopularShowSubCount);
                if (popularMatch.Results[0].Matches.Count != popularMatch.Results[1].Matches.Count)
                {
                    MessageBox.Show("Fail To Match Popular Show Regex");
                    return;
                }
                List<ShowModel> showList = new List<ShowModel>();
                for (int i = 0; i < popularMatch.Results[0].Matches.Count; i++)
                {
                    var show = popularMatch.Results[0].Matches[i];
                    var showSubCount = popularMatch.Results[1].Matches[i][1];
                    ShowModel showModel = new ShowModel
                    {
                        Link = show[1],
                        Name = show[2],
                        SubCount = $"{showSubCount} Subtitles",
                    };
                    showList.Add(showModel);
                }

                listShows(showList);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
