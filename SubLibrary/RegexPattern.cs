namespace SubLibrary
{
    public class RegexPattern
    {
        public static string ShowReleaseYear { get; } = "<strong>\\s*Year:\\s*.*\\s*(.+)\\s*";
        public static string ShowTitle { get; } = "<div class=\"header\">\\s*.*\\s*(.+)\\s*";
        public static string ShowPoster { get; } = "class=\"poster\">\\s+.+src=\"(.+)\" alt=\"Poster\"";
        public static string ShowsInfo { get; } = "class=\"title\".+\\s+.+href=\"(.+)\">(.+)</a>\\s+.+\\s+.+count\">\\s*(.+) subtitles";
        public static string SubtitleInfo { get; } = "<td class=\"a1\">[\\s]+<a href=\"(.+)\">[\\s.]+<span class=\"(.+)\">[\\s.]+(.+)[\\s.]+.+\\s+.+\\s+(.+)\\n";
        public static string SubtitleAuthor { get; } = "<td class=\"a5\">[\\s.]+.+\\s+(.+)";
        public static string SubtitleDownloadLink { get; } = "class=\"download\">[\\s.]+.+ href=\"(.+)\" rel";
        public static string PopularShow { get; } = "div class=\"title\">\\s*<a href=\"(.+)\">(.+)</a>\\s*";
        public static string PopularShowSubCount { get; } = "[\\d]+ retrievals of (.+) subtitles";

    }
}
