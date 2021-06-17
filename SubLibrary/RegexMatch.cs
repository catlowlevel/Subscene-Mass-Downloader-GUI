using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SubLibrary
{
    public class RegExModel
    {
        public List<List<string>> Matches { get; set; } = new List<List<string>>();
        public bool NoMatch { get; set; }
    }
    public class RegexMatch
    {
        public List<RegExModel> Results;
        public RegexMatch()
        {
            Results = new List<RegExModel>();
        }
        public RegexMatch(string input, params string[] patterns)
        {
            Results = new List<RegExModel>();

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(input, pattern);
                RegExModel regEx = new RegExModel
                {
                    NoMatch = !match.Success
                };
                while (match.Success)
                {
                    List<string> res = new List<string>();

                    foreach (Group item in match.Groups)
                    {

                        res.Add(item.Value);
                    }
                    regEx.Matches.Add(res);

                    match = match.NextMatch();
                }
                Results.Add(regEx);

            }

        }

    }
}
