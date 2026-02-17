using OnlineRadio.Core;
using RadioBrowserWrapper.Models;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Controls
{
    /// <summary>
    /// PlayerElement.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerElement : UserControl
    {
        public event RoutedEventHandler? onMediaOpened;
        public event RoutedEventHandler? onMediaEnded;
        public event EventHandler<ExceptionRoutedEventArgs>? onMediaError;
        public event RoutedEventHandler? onPlay;
        public event EventHandler<CurrentSongEventArgs>? OnCurrentSongChanged;
        public event EventHandler<StreamStartEventArgs>? OnStreamStart;
        public event EventHandler<StreamUpdateEventArgs>? OnStreamUpdate;
        public event EventHandler<StreamOverEventArgs>? OnStreamOver;
        public Radio? radiocore;
        public bool isPlaying = false;
        public Station? PlayingStation { get; set; }

        public PlayerElement()
        {
            InitializeComponent();

            me.MediaFailed += (s, e) => onMediaError?.Invoke(s, e);
            me.MediaEnded += (s, e) => onMediaEnded?.Invoke(s, e);
            me.MediaOpened += (s, e) => onMediaOpened?.Invoke(s, e);
        }

        public void Play(Station station)
        {
            try
            {
                if (isPlaying)
                    Stop();
                PlayingStation = station;
                onPlay?.Invoke(this,new RoutedEventArgs());
                StartPlayRadio();
            }
            catch (Exception ex)
            {
                App.GetMainWindow?.mainDialogPlacement.Show(ex.Message, "ERROR");
            }
        }

        public void Play()
        {
            try
            {
                if (isPlaying)
                    Stop();
                onPlay?.Invoke(this, new RoutedEventArgs());
                StartPlayRadio();
            }
            catch (Exception ex)
            {
                App.GetMainWindow?.mainDialogPlacement.Show(ex.Message, "ERROR");
            }
        }

        public void StartPlayRadio()
        {
            if (PlayingStation != null)
            {
                radiocore = new Radio(PlayingStation.Url, true);
                radiocore.Start();
                radiocore.OnCurrentSongChanged += OnCurrentSongChanged;
                radiocore.OnStreamStart += OnStreamStart;
                radiocore.OnStreamUpdate += OnStreamUpdate;
                radiocore.OnStreamOver += OnStreamOver;
                me.Source = new Uri(PlayingStation.Url);
                me.Play();
                isPlaying = true;
            }
        }

        public void Stop()
        {
            me.Stop();
            radiocore?.Dispose();
            isPlaying = false;
        }
    }
}
