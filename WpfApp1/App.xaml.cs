using RadioBrowserWrapper;
using RadioBrowserWrapper.Models;
using System.Text.Json;
using System.Windows;
using WpfApp1.Controls;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RadioBrowser Browser = new RadioBrowser();
        public static List<Station>? PinStations;
        public static List<Group>? Groups;
        public static List<Station>? RecentlyStations;

        public static PlayerElement? Player
        {
            get => (Current.MainWindow as MainWindow)?.mainPlayer;
            set { }
        }

        public static MainWindow? GetMainWindow
        {
            get => Current.MainWindow as MainWindow;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                PinStations = JsonSerializer.Deserialize<List<Station>>(WSettings.Default.pins);
                if (PinStations == null)
                    PinStations = new List<Station>();
            }
            catch (Exception ex)
            {
                PinStations = new List<Station>();
            }
            try
            {
                Groups = JsonSerializer.Deserialize<List<Group>>(WSettings.Default.group);
                if (Groups == null)
                    Groups = new List<Group>();
            }
            catch (Exception ex)
            {
                Groups = new List<Group>();
            }
            try
            {
                RecentlyStations = JsonSerializer.Deserialize<List<Station>>(WSettings.Default.recently);
                if (RecentlyStations == null)
                    RecentlyStations = new List<Station>();
            }
            catch (Exception ex)
            {
                RecentlyStations = new List<Station>();
            }
        }

        public static void SaveSettings()
        {
            WSettings.Default.pins = JsonSerializer.Serialize(PinStations);
            WSettings.Default.group = JsonSerializer.Serialize(Groups);
            WSettings.Default.recently = JsonSerializer.Serialize(RecentlyStations);
            WSettings.Default.Save();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SaveSettings();
        }
    }

}
