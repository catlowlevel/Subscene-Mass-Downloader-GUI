using System.Windows.Forms;
using System.Threading.Tasks;

namespace SubLibrary
{
    public class AnimateText
    {
        public bool IsRunning { get; set; } = false;
        public Control ControlObj { get; set; }
        public string[] Texts { get; set; }
        public string DefaultText { get; set; }
        public int Delay { get; set; }
        public AnimateText(Control control, string defaultText, int msDelay, params string[] texts)
        {
            ControlObj = control;
            DefaultText = defaultText;
            Delay = msDelay;
            Texts = texts;
        }

        public async Task Start()
        {
            IsRunning = true;
            while (IsRunning)
            {
                foreach (var text in Texts)
                {
                    ControlObj.Text = text;
                    await Task.Delay(Delay);
                    if (IsRunning == false) break;
                }
            }
        }
        public void Stop()
        {
            IsRunning = false;
        }
    }
}
