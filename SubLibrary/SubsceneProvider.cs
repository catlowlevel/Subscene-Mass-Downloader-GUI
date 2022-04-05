using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace SubLibrary
{
    public class SubsceneProvider : SubtitleProviderBase
    {
        public override string SubtitleApi { get; } = "https://subscene.com{0}";
        public override string SubtitleSearchApi { get; } = "https://subscene.com/subtitles/searchbytitle?query={0}";

        public override string FileExt { get; } = ".zip";
      


        public override async Task<string> GetDownloadLinkAsync(SubtitleModel subtitle)
        {
            try
            {
                var page = await WebHelper.DownloadStringAsync(string.Format(SubtitleApi, subtitle.Link));
                var html = new HtmlDocument();
                html.LoadHtml(page);
                var dlNodes = html.DocumentNode.SelectSingleNode("//*[@id=\"downloadButton\"]");
                return dlNodes.GetAttributeValue("href", "");
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

                var html = new HtmlDocument();
                html.LoadHtml(page);
                var subtitles = html.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div[4]/table/tbody/tr");

                foreach (var subtitle in subtitles)
                {
                    var firstChildHasATag = subtitle.SelectSingleNode("./td[1]/a");
                    if (firstChildHasATag is null) continue;
                    var subClasses = subtitle.FirstChild.NextSibling.GetClasses();
                    var language = subtitle.SelectSingleNode("./td[1]/a/span[1]").InnerText.Trim();
                    var title = subtitle.SelectSingleNode("./td[1]/a/span[2]").InnerText.Trim();
                    var link = subtitle.SelectSingleNode("./td[1]/a").GetAttributeValue("href", "").Trim();
                    var author = subtitle.SelectSingleNode("./td[4]/a")?.InnerText.Trim() ?? "[Anonymous]";
                    var comment = subtitle.SelectSingleNode("./td[5]").InnerText.Trim();
                    comment = System.Web.HttpUtility.HtmlDecode(comment);
                    subList.Add(new SubtitleModel
                    {
                        Language = language,
                        Link = link,
                        Title = title,
                        Owner = author,
                        Rating = "neutral"
                    });
                }


                //var subtitlesMatch = new RegexMatch(page, SubtitleInfo, SubtitleAuthor);
                //if (subtitlesMatch.Results[0].Matches.Count != subtitlesMatch.Results[1].Matches.Count)
                //    throw new Exception("Failed To Match Some Regex Pattern!");

                //var count = subtitlesMatch.Results[0].Matches.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    var authorMatch = subtitlesMatch.Results[1].Matches[i];
                //    var author = authorMatch[1];
                //    if (subtitlesMatch.Results[1].Matches[i][1].Contains("Anonymous"))
                //    {
                //        author = "[Anonymous]";
                //    }
                //    var subInfo = subtitlesMatch.Results[0].Matches[i];
                //    subList.Add(new SubtitleModel
                //    {
                //        Owner = author,
                //        Link = subInfo[1],
                //        Language = subInfo[3],
                //        Title = subInfo[4],
                //        Rating = subInfo[2],
                //    });
                //}
                return subList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public override IEnumerable<ShowModel> SearchShowList(string page)
        {
            List<ShowModel> showModels = new List<ShowModel>();
            var html = new HtmlDocument();
            html.LoadHtml(page);
            var showNodes = html.DocumentNode.SelectNodes("//*[@id=\"left\"]/div/div/ul/li");
            foreach (var show in showNodes)
            {
                //*[@id="left"]/div/div/ul[1]/li/div[1]/a
                var title = show.SelectSingleNode("./div[1]/a").InnerText.Trim();
                var link = show.SelectSingleNode("./div[1]/a").GetAttributeValue("href", "").Trim();
                //var subtitleCount = show.LastChild.PreviousSibling.InnerText.Trim();
                var subtitleCount = show.SelectSingleNode("./*[contains(@class, 'count')]").InnerText.Trim();
                showModels.Add(new ShowModel
                {
                    Link = link,
                    Name = title,
                    SubCount = subtitleCount
                });
            }
            //var pageMatch = new RegexMatch(page, ShowsInfo);
            //foreach (var item in pageMatch.Results.First().Matches)
            //{
            //    showModels.Add(new ShowModel
            //    {
            //        Link = item[1],
            //        Name = item[2],
            //        SubCount = $"{item[3]} Subtitles"
            //    });
            //}

            return showModels;
        }
        public override IEnumerable<ShowModel> MainShowList(string page)
        {
            List<ShowModel> showModels = new List<ShowModel>();
            var html = new HtmlDocument();
            html.LoadHtml(page);
            var showNodes = html.DocumentNode.SelectNodes("//div[@class='title']");
            foreach (var show in showNodes)
            {
                var title = show.SelectSingleNode("./a").InnerText.Trim() ?? "";
                var link = show.SelectSingleNode("./a").GetAttributeValue("href", "").Trim();
                showModels.Add(new ShowModel
                {
                    Link = link,
                    Name = title,
                    SubCount = "0"
                });
            }
            return showModels;
        }

        public override ShowInfoModel GetShowInfo(string page)
        {
            ShowInfoModel show = new ShowInfoModel();

            var html = new HtmlDocument();
            html.LoadHtml(page);
            var boxNode = html.DocumentNode.SelectSingleNode("//*[@id=\"content\"]/div[2]/div[1]");
            var title = boxNode.SelectSingleNode("//h2").FirstChild.InnerText.Trim();
            var year = boxNode.SelectSingleNode("//strong").NextSibling.InnerText.Trim();
            var img = boxNode.SelectSingleNode("//*[@class=\"poster\"]//img").GetAttributeValue("src", "") ?? "";


            show.ReleaseYear = year;
            show.Title = title;
            show.PosterUrl = img;

            return show;
        }
    }
}
