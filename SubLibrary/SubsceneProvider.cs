using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SubLibrary
{
    public class SubsceneProvider : SubtitleProviderBase
    {
        public override string SubtitleApi { get; } = "https://subscene.com{0}";
        public override string SubtitleSearchApi { get; } = "https://subscene.com/subtitles/searchbytitle?query={0}";

        public override string FileExt { get; } = ".zip";
        public string ShowReleaseYear { get; } = "<strong>\\s*Year:\\s*.*\\s*(.+)\\s*";
        public string ShowTitle { get; } = "<div class=\"header\">\\s*.*\\s*(.+)\\s*";
        public string ShowPoster { get; } = "class=\"poster\">\\s+.+src=\"(.+)\" alt=\"Poster\"";
        public string ShowsInfo { get; } = "class=\"title\".+\\s+.+href=\"(.+)\">(.+)</a>\\s+.+\\s+.+count\">\\s*(.+) subtitles";
        public string SubtitleInfo { get; } = "<td class=\"a1\">[\\s]+<a href=\"(.+)\">[\\s.]+<span class=\"(.+)\">[\\s.]+(.+)[\\s.]+.+\\s+.+\\s+(.+)\\n";
        public string SubtitleAuthor { get; } = "<td class=\"a5\">[\\s.]+.+\\s+(.+)";
        public string SubtitleDownloadLink { get; } = "class=\"download\">[\\s.]+.+ href=\"(.+)\" rel";
        public string PopularShow { get; } = "div class=\"title\">\\s*<a href=\"(.+)\">(.+)</a>\\s*";
        public string PopularShowSubCount { get; } = "[\\d]+ retrievals of (.+) subtitles";


        public override async Task<string> GetDownloadLinkAsync(SubtitleModel subtitle)
        {
            try
            {
                var page = await WebHelper.DownloadStringAsync(string.Format(SubtitleApi, subtitle.Link));
                var pageMatch = new RegexMatch(page, SubtitleDownloadLink);
                if (pageMatch.Results[0].NoMatch == false)
                {
                    return String.Format(SubtitleApi, pageMatch.Results.First().Matches.First()[1]);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override IEnumerable<SubtitleModel> GetSubtitleList(string page)
        {
            try
            {
                List<SubtitleModel> subList = new List<SubtitleModel>();

                var subtitlesMatch = new RegexMatch(page, SubtitleInfo, SubtitleAuthor);
                if (subtitlesMatch.Results[0].Matches.Count != subtitlesMatch.Results[1].Matches.Count)
                    throw new Exception("Failed To Match Some Regex Pattern!");

                var count = subtitlesMatch.Results[0].Matches.Count;
                for (int i = 0; i < count; i++)
                {
                    var authorMatch = subtitlesMatch.Results[1].Matches[i];
                    var author = authorMatch[1];
                    if (subtitlesMatch.Results[1].Matches[i][1].Contains("Anonymous"))
                    {
                        author = "[Anonymous]";
                    }
                    var subInfo = subtitlesMatch.Results[0].Matches[i];
                    subList.Add(new SubtitleModel
                    {
                        Owner = author,
                        Link = subInfo[1],
                        Language = subInfo[3],
                        Title = subInfo[4],
                        Rating = subInfo[2],
                    });
                }
                return subList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public override IEnumerable<ShowModel> GetShowList(string page)
        {
            List<ShowModel> showModels = new List<ShowModel>();
            var pageMatch = new RegexMatch(page, RegexPattern.ShowsInfo);
            foreach (var item in pageMatch.Results.First().Matches)
            {
                showModels.Add(new ShowModel
                {
                    Link = item[1],
                    Name = item[2],
                    SubCount = $"{item[3]} Subtitles"
                });
            }

            return showModels;
        }

        public override ShowInfoModel GetShowInfo(string page)
        {
            ShowInfoModel show = new ShowInfoModel();

            var pageMatch = new RegexMatch(page, RegexPattern.ShowReleaseYear, RegexPattern.ShowTitle, RegexPattern.ShowPoster);
            var year = pageMatch.Results[0].Matches.First()[1];
            var title = pageMatch.Results[1].Matches.First()[1];

            var img = string.Empty;
            if (pageMatch.Results[2].NoMatch == false)
                img = pageMatch.Results[2].Matches.First()[1];

            show.ReleaseYear = year;
            show.Title = title;
            show.PosterUrl = img;

            return show;
        }
    }
}
