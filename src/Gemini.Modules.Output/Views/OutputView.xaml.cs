using Caliburn.Micro;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace Gemini.Modules.Output.Views
{
	/// <summary>
	/// Interaction logic for OutputView.xaml
	/// </summary>
	public partial class OutputView : UserControl, IOutputView
	{
        private DispatcherTimer _timer;
        //private static readonly uint _maxStartsSinceLastScroll = 100;
        //private uint _startsSinceLastScroll;

		public OutputView()
		{
			InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            _timer.Tick += _timer_Tick;
		}

        private void ScrollToEnd()
        {
            //_startsSinceLastScroll = 0;

            var paragraph = outputText.Document.Blocks.FirstBlock as Paragraph;
            var lastInline = paragraph.Inlines.Where(i => i.IsLoaded).LastOrDefault();
            if (lastInline == null)
                return;

            lastInline.BringIntoView();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            ScrollToEnd();
            _timer.Stop();
        }

        public void StartScrollTimer()
        {
            _timer.Start();
            //if (++_startsSinceLastScroll >= _maxStartsSinceLastScroll)
                //Execute.OnUIThread(() => ScrollToEnd());
        }
    }
}
