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
using WpfApp1.Dialogs;

namespace WpfApp1.Controls
{
    /// <summary>
    /// GroupDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GroupDetail : UserControl
    {
        public event RoutedEventHandler? onClose;
        int groupindex = -1;
        public GroupDetail(int index)
        {
            InitializeComponent();

            groupindex = index;
            Loaded += GroupDetail_Loaded;
        }

        private void GroupDetail_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCards();
            BeginStoryboard((Storyboard)TryFindResource("Open"));
        }

        public void LoadCards()
        {
            List<object> cards = new List<object>();
            grouptitle.Text = App.Groups?[groupindex].Name;
            for (int i = 0; i < App.Groups?[groupindex]?.Stations?.Count; i++)
            {
                var card = new RadioCard(App.Groups?[groupindex].Stations[i],true);
                card.OnDelete += async (s, e) => {
                    var sta = (s as RadioCard).station;
                    var result = await App.GetMainWindow?.mainDialogPlacement.Show(new MessageDialogWithButton($"Delete {sta.Name} ?", "", ["Cancel", "Delete"]));
                    if ((int)result == 1)
                        App.Groups?[groupindex].Stations.Remove((s as RadioCard).station);
                    LoadCards();
                };
                cards.Add(card);
            }
            carditem.Children = cards;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            BeginStoryboard((Storyboard)TryFindResource("Close"));
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            onClose?.Invoke(this, new RoutedEventArgs());
            DispatcherTimer? timer = sender as DispatcherTimer;
            if (timer != null)
                timer.Stop();
        }
    }
}
