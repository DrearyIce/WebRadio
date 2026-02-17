using System.Windows.Controls;
using WpfApp1.Controls;

namespace WpfApp1.Models
{
    public class CustomDialog : UserControl
    {
        private TaskCompletionSource<object>? Source;

        public Task<object> ShowAsync()
        {
            Source = new TaskCompletionSource<object>();
            return Source.Task;
        }

        protected void CloseWithResult(object result)
        {
            (this.Tag as DialogPlacement)?.CloseDialog();
            Source?.TrySetResult(result);
        }

        protected void CloseWithoutResult()
        {
            (this.Tag as DialogPlacement)?.CloseDialog();
            Source?.TrySetResult(null);
        }
    }
}
