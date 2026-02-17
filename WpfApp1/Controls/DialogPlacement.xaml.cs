using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using WpfApp1.Models;

namespace WpfApp1.Controls
{
    /// <summary>
    /// DialogPlacement.xaml 的交互逻辑
    /// </summary>
    public partial class DialogPlacement : UserControl
    {
        public DialogPlacement()
        {
            InitializeComponent();
        }

        public void Show(object message, string title)
        {
            try
            {
                mc_message.Content = message;
                mc_title.Text = title;
                MessageContent.Visibility = Visibility.Visible;
                CustomContent.Visibility = Visibility.Collapsed;
                BeginStoryboard((Storyboard)TryFindResource("ShowDialog"));
            }
            catch (Exception ex)
            {
                mc_message.Content = ex.Message;
            }
        }

        public Task<object>? Show(object obj)
        {
            try
            {
                MessageContent.Visibility = Visibility.Collapsed;
                CustomContent.Visibility = Visibility.Visible;
                (obj as CustomDialog)?.Tag = this;
                cc_content.Content = obj;
                BeginStoryboard((Storyboard)TryFindResource("ShowDialog"));
                CustomDialog? dialog = obj as CustomDialog;
                if (dialog != null)
                    return dialog.ShowAsync();
                return null;
            }
            catch (Exception ex)
            {
                mc_message.Content = ex.Message;
                return null;
            }
        }


        public void CloseDialog()
        {
            BeginStoryboard((Storyboard)TryFindResource("CloseDialog"));
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog();
        }
    }
}
