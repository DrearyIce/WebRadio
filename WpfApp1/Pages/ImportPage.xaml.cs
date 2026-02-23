using RadioBrowserWrapper.Models;
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

namespace WpfApp1.Pages
{
    /// <summary>
    /// ImportPage.xaml 的交互逻辑
    /// </summary>
    public partial class ImportPage : Page
    {
        public ImportPage()
        {
            InitializeComponent();
        }

        private void prebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Station station = new Station() { Name = name.Text,Url = url.Text,Tags = tags.Text,Favicon = favicon.Text,StationUuid = Guid.NewGuid().ToString() };
                precard.station = station;
            }
            catch (Exception ex)
            {
                App.GetMainWindow?.mainNotificationPlacement?.Show(ex.Message);
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Station station = new Station() { Name = name.Text, Url = url.Text, Tags = tags.Text, Favicon = favicon.Text, StationUuid = Guid.NewGuid().ToString() };
                int? idx = App.PinStations?.FindIndex(s => s.StationUuid == station.StationUuid);
                if (idx != null && idx != -1)
                    App.GetMainWindow?.mainNotificationPlacement?.Show("Already existed");
                else if (idx != null)
                {
                    App.PinStations?.Add(App.Player?.PlayingStation);
                    App.GetMainWindow?.mainNotificationPlacement?.Show("Added to pins!");
                    App.SaveSettings();
                    App.GetMainWindow?.UpdatePage();
                }
            }
            catch (Exception ex)
            {
                App.GetMainWindow?.mainNotificationPlacement?.Show(ex.Message);
            }
        }
    }
}
