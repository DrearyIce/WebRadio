using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1.Controls
{
    /// <summary>
    /// Notification.xaml 的交互逻辑
    /// </summary>
    public partial class Notification : UserControl
    {
        object? content;
        public event RoutedEventHandler? Close;
        public Notification(object? _content)
        {
            InitializeComponent();

            content = _content;
            Loaded += Notification_Loaded;
        }

        private void Notification_Loaded(object sender, RoutedEventArgs e)
        {
            noticontent.Content = content;
            BeginStoryboard((Storyboard)TryFindResource("Show"));
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        int i = 0;
        private void Timer_Tick(object? sender, EventArgs e)
        {
            i++;
            if (i == 12)
                BeginStoryboard((Storyboard)TryFindResource("Close"));
            if (i == 13)
            {
                Close?.Invoke(this, new RoutedEventArgs());
                DispatcherTimer? timer = sender as DispatcherTimer;
                if (timer != null)
                    timer.Stop();
            }
        }
    }
}
