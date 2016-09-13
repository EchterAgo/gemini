using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Documents;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Output.Properties;
using Gemini.Modules.Output.Views;
using System.Windows.Media;

namespace Gemini.Modules.Output.ViewModels
{
    [Export(typeof(IOutput))]
    public class OutputViewModel : Tool, IOutput
    {
        private readonly OutputWriter _writer;
        private IOutputView _view;

        public FlowDocument Document { get; private set; }
        private Paragraph _paragraph;

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Bottom; }
        }

        public TextWriter Writer
        {
            get { return _writer; }
        }

        public OutputViewModel()
        {
            DisplayName = Resources.OutputDisplayName;
            _writer = new OutputWriter(this);
            Clear();
        }

        public void Clear()
        {
            Execute.OnUIThread(() => {
                Document = new FlowDocument();
                Document.FontFamily = new FontFamily("Consolas");
                Document.FontSize = 12;

                _paragraph = new Paragraph();
                Document.Blocks.Add(_paragraph);
            });
        }

        public void AppendLine(string text)
        {
            Append(text + Environment.NewLine);
        }

        private static readonly string[] _newLine = new[] { Environment.NewLine };

        public void Append(string text)
        {
            Execute.OnUIThread(() => {
                var lines = text.Split(_newLine, StringSplitOptions.None);

                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    if (!string.IsNullOrEmpty(line))
                    {
                        var run = new Run(line);
                        _paragraph.Inlines.Add(run);
                    }

                    if (i < lines.Length - 1)
                    {
                        _paragraph.Inlines.Add(new LineBreak());
                    }
                }

                if (_view != null)
                    _view.StartScrollTimer();
            });
        }

        private void ScrollRun_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var run = sender as Run;
            run.Loaded -= ScrollRun_Loaded;
            run.BringIntoView();
        }

        protected override void OnViewLoaded(object view)
        {
            _view = (IOutputView) view;
            _view.StartScrollTimer();
        }
    }
}
