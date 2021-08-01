using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SubLibrary
{
    public static class SubtitleManager
    {
        public static string baseUrl = "https://subscene.com{0}";


        public static async Task<string> GetDownloadLinkAsync(SubtitleModel subtitle)
        {
            try
            {
                var page = await WebHelper.DownloadStringAsync(string.Format(baseUrl, subtitle.Link));
                var pageMatch = new RegexMatch(page, RegexPattern.SubtitleDownloadLink);
                if (pageMatch.Results[0].NoMatch == false)
                {
                    return String.Format(baseUrl, pageMatch.Results.First().Matches.First()[1]);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static async Task DownloadSubtitlesAsync(List<SubtitleModel> subtitles, string path, Action<List<Task<string>>> reportProgressAction)
        {
            try
            {
                List<Task<string>> tasks = new List<Task<string>>();
                subtitles.ForEach(subtitle => tasks.Add(DownloadSubtitleAsync(subtitle, path)));
                await Utility.WhenAllEx(tasks, _ => reportProgressAction(_));

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static async Task<string> DownloadSubtitleAsync(SubtitleModel subtitle, string path)
        {
            try
            {
                var link = await GetDownloadLinkAsync(subtitle);
                var output = Path.Combine(path, Utility.FilterString(subtitle.Title));
                return await WebHelper.DownloadFileAsync(link, output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<SubtitleModel> ParseSubtitlesPage(string page)
        {
            try
            {
                List<SubtitleModel> subList = new List<SubtitleModel>();

                var subtitlesMatch = new RegexMatch(page, RegexPattern.SubtitleInfo, RegexPattern.SubtitleAuthor);
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

    }
}
