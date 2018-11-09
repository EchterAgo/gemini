using System.Windows;

using Microsoft.Win32;

namespace Gemini.Modules.Inspector.Inspectors
{
    public class FileBrowserEditorViewModel<T> : EditorBase<string>, ILabelledInspector where T : FileDialog, new()
    {
        public T Dialog { get; }

        public FileBrowserEditorViewModel()
        {
            Dialog = new T();
        }

        public void Select()
        {
            Dialog.FileName = Value;

            var resNullable = Dialog.ShowDialog(Application.Current.MainWindow);
            if (!resNullable.HasValue)
                return;

            var res = resNullable.Value;
            if (!res)
                return;

            Value = Dialog.FileName;
        }
    }
}
