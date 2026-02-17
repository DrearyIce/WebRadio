using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.Controls
{
    /// <summary>
    /// NotificationPlacement.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationPlacement : UserControl
    {
        public NotificationPlacement()
        {
            InitializeComponent();
        }

        public void Show(object? messagecontent)
        {
            var noti = new Notification(messagecontent);
            noti.Close += Noti_Close;
            notisp.Children.Add(noti);
        }

        private void Noti_Close(object sender, RoutedEventArgs e)
        {
            notisp.Children.Remove(sender as Notification);
        }
    }
}
