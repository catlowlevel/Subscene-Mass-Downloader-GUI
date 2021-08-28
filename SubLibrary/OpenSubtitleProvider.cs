using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubLibrary
{
    public class OpenSubtitleProvider : SubtitleProviderBase
    {
        public override string SubtitleApi { get; } = "https://opensubtitle.info{0}";
        public override string SubtitleSearchApi { get; } = "https://opensubtitle.info/search?q={0}";
        public override string FileExt { get; } = "";

        public string RegexLanguage { get; } = "<a href=.+\\s+(.+).+<\\/a>";
        public string RegexLink { get; } = "href=\"(.+)\" class=\"list-group-item\">";
        public string RegexName { get; } = "<span class=\"name\">.+<strong>(.+)<\\/strong>";
        public string RegexDownloadLink { get; } = "class=\"btn btn-danger\" href=\"(.+)\">";

        public override async Task<string> GetDownloadLinkAsync(SubtitleModel subtitle)
        {
            try
            {
                var page = await LoadPageAsync(subtitle.Link);
                var pageMatch = new RegexMatch(page, RegexDownloadLink);
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

                string[] h3s = page.Split(new string[] { "<h3>" }, StringSplitOptions.None);

                foreach (var h3 in h3s)
                {

                    var subtitlesMatch = new RegexMatch(h3, RegexLanguage, RegexLink, RegexName);

                    var count = subtitlesMatch.Results[0].Matches.Count;

                    var language = subtitlesMatch.Results[0].Matches[0][1];
                    language = language.Trim();
                    var subtitleCount = subtitlesMatch.Results[1].Matches.Count;

                    for (int j = 0; j < subtitleCount; j++)
                    {
                        var link = subtitlesMatch.Results[1].Matches[j][1];
                        var name = subtitlesMatch.Results[2].Matches[j][1];
                        subList.Add(new SubtitleModel
                        {
                            Language = language,
                            Link = link,
                            Title = name
                        });

                    }



                }

                return subList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
