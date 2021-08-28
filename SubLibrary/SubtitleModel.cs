using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubLibrary
{
    public class SubtitleModel
    {
        private string _rating = "neutral";

        public string Title { get ; set ; }
        public string Link { get ; set ; }
        public string Language { get ; set ; }
        public string Owner { get; set; }
        public string Rating
        {
            get
            {
                if (_rating.Contains("positive"))
                    return "Good";
                else if (_rating.Contains("neutral"))
                    return "Not Rated";
                else if (_rating.Contains("bad"))
                    return "Bad";

                return _rating;
            }
            set { _rating = value; }
        }
    }
}
