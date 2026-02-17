using System.Windows.Controls;
using WpfApp1.Controls;
using WpfApp1.Dialogs;

namespace WpfApp1.Pages
{
    /// <summary>
    /// LibraryPage.xaml 的交互逻辑
    /// </summary>
    public partial class LibraryPage : Page
    {
        public LibraryPage()
        {
            InitializeComponent();

            Loaded += LibraryPage_Loaded;
        }

        private void LibraryPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadGroups();
        }

        private async void editgroups_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await App.GetMainWindow?.mainDialogPlacement.Show(new EditGroups());
            LoadGroups();
            LoadRecently();
        }

        public void LoadGroups()
        {
            groups.Children.Clear();
            for (int i = 0; i < App.Groups?.Count; i++)
            {
                var control = new GroupCard(i, false);
                control.onClick += (s, e) => {
                    var idx = (s as GroupCard)?.groupIndex;
                    if (idx != null && idx != -1)
                    {
                        var con = new GroupDetail((int)idx);
                        con.onClose += (s, e) => {
                            App.GetMainWindow?.mainEmbControlPlacement.Children.Remove(s as GroupDetail);
                        };
                        App.GetMainWindow?.mainEmbControlPlacement.Children.Add(con);
                    }
                };
                groups.Children.Add(control);
            }
        }

        int LoadedRecentContent = 0;

        public void LoadRecently()
        {
            List<object> controls = new List<object>();
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    var control = new RadioCard(App.RecentlyStations?[LoadedRecentContent + i]);
                    controls.Add(control);
                }
                catch { break; }
            }
            recent.Children = controls;
            LoadedRecentContent += 10;
        }

        private void Loadmore_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadRecently();
        }

        public void Reload()
        {
            LoadRecently();
            LoadGroups();
        }
    }
}
