using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SubLibrary
{
    public abstract class SubtitleProviderBase : ISubtitleProvider
    {
        virtual public string SubtitleApi => throw new NotImplementedException();
        virtual public string SubtitleSearchApi => throw new NotImplementedException();
        virtual public string FileExt => throw new NotImplementedException();

        public async Task<string> DownloadSubtitleAsync(SubtitleModel subtitle, string path)
        {
            try
            {
                var link = await GetDownloadLinkAsync(subtitle);
                var output = Path.Combine(path, Utility.FilterString(subtitle.Title));
                return await WebHelper.DownloadFileAsync(link, output, FileExt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        virtual public Task<string> GetDownloadLinkAsync(SubtitleModel subtitle)
        {
            throw new NotImplementedException();
        }

        virtual public ShowInfoModel GetShowInfo(string page)
        {
            throw new NotImplementedException();
        }

        virtual public IEnumerable<ShowModel> SearchShowList(string page)
        {
            throw new NotImplementedException();
        }

        virtual public IEnumerable<SubtitleModel> GetSubtitleList(string page)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoadPageAsync(string url)
        {
            try
            {
                string page = await WebHelper.DownloadStringAsync(url);
                page = HttpUtility.HtmlDecode(page);
                return page;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task MassDownloadSubtitleAsync(IEnumerable<SubtitleModel> subtitles, string path, Action<List<Task<string>>> reportProgressAction)
        {
            try
            {
                List<Task<string>> tasks = new List<Task<string>>();
                foreach (var subtitle in subtitles)
                {
                    tasks.Add(DownloadSubtitleAsync(subtitle, path));
                }
                await Utility.WhenAllEx(tasks, _ => reportProgressAction?.Invoke(_));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        virtual public IEnumerable<ShowModel> MainShowList(string page)
        {
            throw new NotImplementedException();
        }
    }
}
