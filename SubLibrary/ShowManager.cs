using System.Collections.Generic;
using System.Linq;

namespace SubLibrary
{
    public class ShowManager
    {
        public static string subSearchApi = "https://subscene.com/subtitles/searchbytitle?query={0}";
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
