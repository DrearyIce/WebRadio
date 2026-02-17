using RadioBrowserWrapper.Enums;
using RadioBrowserWrapper.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WpfApp1.Controls;
using WpfApp1.Dialogs;

namespace WpfApp1.Pages
{
    /// <summary>
    /// NewPage.xaml 的交互逻辑
    /// </summary>
    public partial class NewPage : Page
    {
        public NewPage()
        {
            InitializeComponent();

            Loaded += NewPage_Loaded;
        }

        private async void NewPage_Loaded(object sender, RoutedEventArgs e)
        {
            GetSelectedItems(topselectedItem.clicks);
            foreach (var item in Enum.GetValues(typeof(StationOrder)))
                _orderby.Items.Add(new ComboBoxItem() { Content = $"Order by {Enum.GetName(typeof(StationOrder), item)}" });
        }

        private void GetSelectedItems(topselectedItem item)
        {
            discoverLoading.Visibility = Visibility.Visible;
            wrapgrid.Opacity = 0.7;
            Thread t = new Thread(async () =>
            {
                IEnumerable<Station>? stations = null;
                switch (item)
                {
                    case topselectedItem.clicks:
                        stations = await App.Browser.GetTopStationsByClicksAsync(10);
                        break;
                    case topselectedItem.votes:
                        stations = await App.Browser.GetTopStationsByVotesAsync(10);
                        break;
                    case topselectedItem.recclick:
                        stations = await App.Browser.GetTopStationsByRecentClicksAsync(10);
                        break;
                    case topselectedItem.recchange:
                        stations = await App.Browser.GetRecentlyChangedStationsAsync(10);
                        break;
                    case topselectedItem.broken:
                        stations = await App.Browser.GetBrokenStationsAsync(10);
                        break;
                    default:
                        break;
                }

                if (stations != null)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        List<object> stationCards = new List<object>();
                        foreach (var station in stations)
                            stationCards.Add(new RadioCard(station));
                        wrapgrid.Children = stationCards;
                        discoverLoading.Visibility = Visibility.Collapsed;
                        wrapgrid.Opacity = 1;
                    }));
                }
            });
            t.Start();
        }

        AdvancedStationSearchOptions searchoptions = new AdvancedStationSearchOptions() { Limit = 10 };

        public void TrySearch()
        {
            searchLoading.Visibility = Visibility.Visible;
            searchwrapgrid.Opacity = 0.7;
            _searchpaneltitle.Text = $"Result of \"{_searchbox.Text}\"";
            searchoptions.Name = _searchbox.Text;
            Thread t = new Thread(async () =>
            {
                var stations = await App.Browser.SearchStationsAsync(searchoptions);
                if (stations != null)
                {
                    Dispatcher.Invoke(() =>
                    {
                        List<object> stationCards = new List<object>();
                        foreach (var station in stations)
                            stationCards.Add(new RadioCard(station));
                        searchwrapgrid.Children = stationCards;
                        searchLoading.Visibility = Visibility.Collapsed;
                        searchwrapgrid.Opacity = 1;
                    });
                }
            });
            t.Start();
        }

        private void topsortby_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
                GetSelectedItems((topselectedItem)topsortby.SelectedIndex);
        }

        bool isSearchpanelopen = false;
        private void _opensearchpanel_Click(object sender, RoutedEventArgs e)
        {
            if (!isSearchpanelopen)
                BeginStoryboard((Storyboard)TryFindResource("Search_PanelOpen"));
            else
                TrySearch();
            isSearchpanelopen = true;
        }

        private void _backbtn_Click(object sender, RoutedEventArgs e)
        {
            BeginStoryboard((Storyboard)TryFindResource("Search_PanelClose"));
            isSearchpanelopen = false;
        }

        private void _orderby_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                searchoptions.Order = (StationOrder)_orderby.SelectedIndex;
                TrySearch();
            }
        }

        private async void _setfilter_Click(object sender, RoutedEventArgs e)
        {
            object? dialogobj = null;
            if (searchoptions == null)
                dialogobj = new AdvancedStationSearchDialog();
            else
            {
                searchoptions.Name = _searchbox.Text;
                dialogobj = new AdvancedStationSearchDialog(searchoptions);
            }
            AdvancedStationSearchOptions? options = (AdvancedStationSearchOptions)await App.GetMainWindow?.mainDialogPlacement.Show(dialogobj);
            if (options != null)
            {
                searchoptions = options;
                _searchbox.Text = options.Name;
                TrySearch();
            }

        }

        private void _mclearfilter_Click(object sender, RoutedEventArgs e)
        {
            searchoptions = new AdvancedStationSearchOptions() { Limit = 10 };
            _searchbox.Text = "";
            TrySearch();
        }

        private void _searchbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TrySearch();
        }
    }

    public enum topselectedItem
    {
        clicks = 0,
        votes = 1,
        recclick = 2,
        recchange = 3,
        broken = 4,
    }
}
