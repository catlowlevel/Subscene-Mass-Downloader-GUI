using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubLibrary
{
    public static class ShowManager
    {
        public static string subSearchApi = "https://subscene.com/subtitles/searchbytitle?query={0}";

        public static ShowInfoModel GetShowInfo(string page)
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


        public static List<ShowModel> ParseShowPage(string page)
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
    }
}
