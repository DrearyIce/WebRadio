using RadioBrowserWrapper.Models;
using System.Security.Cryptography;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp1.Dialogs;
using WpfApp1.Flyouts;
using WpfApp1.Models;
using WpfApp1.Windows;

namespace WpfApp1.Controls
{
    /// <summary>
    /// ControlBar.xaml 的交互逻辑
    /// </summary>
    public partial class ControlBar : UserControl
    {
        public ControlBar()
        {
            InitializeComponent();

            Loaded += ControlBar_Loaded;
        }

        private void ControlBar_Loaded(object sender, RoutedEventArgs e)
        {
            App.Player?.onMediaEnded += Player_onMediaEnded;
            App.Player?.onMediaOpened += ControlBar_onMediaOpened;
            App.Player?.onMediaError += ControlBar_onMediaError;
            App.Player?.OnCurrentSongChanged += ControlBar_OnCurrentSongChanged;
            App.Player?.onPlay += ControlBar_onPlay;
        }

        private void ControlBar_onPlay(object sender, RoutedEventArgs e)
        {
            Checkpin();
        }

        public void Checkpin()
        {
            if (App.Player?.PlayingStation != null)
            {
                int? idx = App.PinStations?.FindIndex(s => s.StationUuid == App.Player?.PlayingStation.StationUuid);
                if (idx != null && idx != -1)
                    pin.Content = "􀎨";
                else if (idx != null)
                    pin.Content = "􀎦";
            }
        }

        private void ControlBar_OnCurrentSongChanged(object? sender, OnlineRadio.Core.CurrentSongEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                maintitle.Text = e.NewSong.Title;
                subtitle.Text = e.NewSong.Artist;
                Checkpin();
            });
        }

        private void ControlBar_onMediaError(object? sender, ExceptionRoutedEventArgs e)
        {
            App.GetMainWindow?.mainDialogPlacement.Show(e.ErrorException, "MEDIA PLAYER ERROR");
        }

        private void ControlBar_onMediaOpened(object sender, RoutedEventArgs e)
        {
            Checkpin();
            try
            {
                favicon.Source = BitmapFrame.Create(new Uri(App.Player?.PlayingStation.Favicon));
                nofavicon.Opacity = 0;
            }
            catch
            {
                nofavicon.Opacity = 0.2;
            }
            SetCCVisibility(2);
        }

        private void Player_onMediaEnded(object sender, RoutedEventArgs e)
        {
            SetCCVisibility(0);
        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            SetCCVisibility(1);
            App.Player?.Play();
            maintitle.Text = App.Player?.PlayingStation?.Name;
            maintitle.Text = App.Player?.PlayingStation?.Url;
            SetCCVisibility(2);
        }

        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            App.Player?.Stop();
            SetCCVisibility(0);
        }

        private void SetCCVisibility(int i)
        {
            btn_Pause.Visibility = Visibility.Collapsed;
            btn_Loading.Visibility = Visibility.Collapsed;
            btn_Play.Visibility = Visibility.Collapsed;
            switch (i)
            {
                case 0:
                    btn_Play.Visibility = Visibility.Visible;
                    break;
                case 1:
                    btn_Loading.Visibility = Visibility.Visible;
                    break;
                case 2:
                    btn_Pause.Visibility = Visibility.Visible;
                    break;
            }
            Checkpin();
        }

        private void shareQR_Click(object sender, RoutedEventArgs e)
        {
            if (App.Player?.PlayingStation != null)
                App.GetMainWindow?.mainDialogPlacement.Show(new ShareStationQRCode(App.Player.PlayingStation));
        }

        private void copyurl_Click(object sender, RoutedEventArgs e)
        {
            if (App.Player?.PlayingStation != null)
            { 
                Clipboard.SetText(App.Player?.PlayingStation.Url);
                App.GetMainWindow?.mainNotificationPlacement.Show($"Copied : {App.Player?.PlayingStation.Url}");
            }
        }

        private void pin_Click(object sender, RoutedEventArgs e)
        {
            if (App.Player?.PlayingStation != null)
            {
                int? idx = App.PinStations?.FindIndex(s => s.StationUuid == App.Player?.PlayingStation.StationUuid);
                if (idx != null && idx != -1)
                {
                    App.PinStations?.RemoveAt((int)idx);
                    pin.Content = "􀎦";
                }
                else if (idx != null)
                { 
                    App.PinStations?.Add(App.Player?.PlayingStation);
                    pin.Content = "􀎨";
                }
                App.SaveSettings();
                App.GetMainWindow?.UpdatePage();
            }
        }

        private async void more_Click(object sender, RoutedEventArgs e)
        {
            if (App.Player?.PlayingStation != null)
                await App.GetMainWindow?.mainDialogPlacement.Show(new MoreInfoDialog(App.Player?.PlayingStation));
        }

        private async void addtolist_Click(object sender, RoutedEventArgs e)
        {
            if (App.Player?.PlayingStation != null)
            {
                var res = await App.GetMainWindow?.mainDialogPlacement.Show(new SelectGroup());
                if (res != null)
                {
                    int? idx = (res as GroupCard).groupIndex;
                    if (idx != null)
                        App.Groups[(int)idx].Stations?.Add(App.Player?.PlayingStation);
                    App.SaveSettings();
                    App.GetMainWindow?.UpdatePage();
                }
            }    
        }

        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set
            {
                SetValue(VolumeProperty, value);
                App.GetMainWindow?.mainPlayer.me.Volume = value;
                if (value == 0)
                    volume.Content = "􀊣";
                else if (value > 0 && value <= 0.3)
                    volume.Content = "􀊥";
                else if (value > 0.3 && value <= 0.6)
                    volume.Content = "􀊧";
                else
                    volume.Content = "􀊩";
            }
        }

        // Using a DependencyProperty as the backing store for Volume.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VolumeProperty =
            DependencyProperty.Register("Volume", typeof(double), typeof(ControlBar), new PropertyMetadata(double.Parse("1")));

        private void volume_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new ChangeVolume(), 80, 290);
            @base.Show();
        }
    }
}
