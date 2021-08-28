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
        public AnimateText(Control controlToAnimate, string textAfterAnimate, int msDelay, params string[] texts)
        {
            ControlObj = controlToAnimate;
            DefaultText = textAfterAnimate;
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
            if (string.IsNullOrEmpty(DefaultText) == false)
            {
                ControlObj.Text = DefaultText;
            }
        }
        public void Stop()
        {
            IsRunning = false;
        }
    }
}
