using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubLibrary
{
    public interface ISubtitleProvider
    {
        string SubtitleApi { get; }
        string SubtitleSearchApi { get; }
        string FileExt { get; }
        Task<string> LoadPageAsync(string url);
        Task<string> GetDownloadLinkAsync(SubtitleModel subtitle);
        IEnumerable<SubtitleModel> GetSubtitleList(string page);
        Task<string> DownloadSubtitleAsync(SubtitleModel subtitle, string path);
        Task MassDownloadSubtitleAsync(IEnumerable<SubtitleModel> subtitles, string path, Action<List<Task<string>>> reportProgressAction);
        IEnumerable<ShowModel> SearchShowList(string page);
        IEnumerable<ShowModel> MainShowList(string page);

        ShowInfoModel GetShowInfo(string page);
    }
}
