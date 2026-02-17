using RadioBrowserWrapper.Models;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Controls;

namespace WpfApp1.Pages
{
    /// <summary>
    /// PinsPage.xaml 的交互逻辑
    /// </summary>
    public partial class PinsPage : Page
    {
        public PinsPage()
        {
            InitializeComponent();

            Loaded += PinsPage_Loaded;
        }

        private void PinsPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStations();
        }

        public void LoadStations()
        {
            try
            {
                var stations = App.PinStations;
                wrapgrid.Visibility = Visibility.Visible;
                List<object> radioCards = new List<object>();
                if (stations?.Count == 0)
                    nopins.Visibility = Visibility.Visible;
                else
                {
                    for (int i = 0; i < stations?.Count; i++)
                        radioCards.Add(new RadioCard(stations[i]));
                    wrapgrid.Children = radioCards;
                    nopins.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                App.GetMainWindow?.mainDialogPlacement.Show(ex.Message, "ERROR");
                wrapgrid.Visibility = Visibility.Collapsed;
                nopins.Visibility = Visibility.Collapsed;
            }
        }
    }
}
