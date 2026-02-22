using RadioBrowserWrapper.Models;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp1.Models;

namespace WpfApp1.Controls
{
    /// <summary>
    /// RadioCard.xaml 的交互逻辑
    /// </summary>
    public partial class RadioCard : AutoWrapUserControl
    {
        public event RoutedEventHandler? OnDelete;

        public RadioCard()
        {
            InitializeComponent();
            aminWidth = 320;
        }

        public Station station
        {
            get { return (Station)GetValue(stationProperty); }
            set
            {
                SetValue(stationProperty, value);
                sname.Text = value.Name;
                surl.Text = value.Url;
                stags.Text = value.Tags;
                try
                {
                    favicon.Source = BitmapFrame.Create(new Uri(value.Favicon));
                    nofavicon.Opacity = 0;
                }
                catch
                {
                    nofavicon.Opacity = 0.2;
                }
            }
        }

        // Using a DependencyProperty as the backing store for station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty stationProperty =
            DependencyProperty.Register(nameof(station), typeof(Station), typeof(RadioCard), new PropertyMetadata(new Station()));
        public RadioCard(Station _station)
        {
            InitializeComponent();
            aminWidth = 320;
            station = _station;
        }

        public RadioCard(Station _station, bool isDelenab)
        {
            InitializeComponent();
            aminWidth = 320;
            station = _station;
            Loaded += (s, e) =>
            {
                DelBtn.Visibility = Visibility.Visible;
            };
        }

        private void RippleHost_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (App.Player != null && App.Player.isPlaying)
                App.Player.Stop();
            App.Player?.Play(station);
            App.RecentlyStations?.Insert(0, station);
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            OnDelete?.Invoke(this, e);
        }
    }
}
