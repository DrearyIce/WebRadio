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
using WpfApp1.Controls;
using WpfApp1.Models;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// EditGroups.xaml 的交互逻辑
    /// </summary>
    public partial class EditGroups : CustomDialog
    {
        public EditGroups()
        {
            InitializeComponent();

            Loaded += EditGroups_Loaded;
        }

        private void EditGroups_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGroup();
        }

        public void LoadGroup()
        {
            groups.Children.Clear();
            for (int i = 0; i < App.Groups?.Count; i++)
            {
                var control = new GroupCard(i, true);
                control.onDel += async (s, e) => {
                    var result = await dialogPlace.Show(new MessageDialogWithButton($"Delete {App.Groups?[(s as GroupCard).groupIndex].Name} ?", "", ["Cancel", "Delete"]));
                    if ((int)result == 1)
                        App.Groups?.RemoveAt((s as GroupCard).groupIndex);
                    LoadGroup();
                };
                control.onEdit += async (s, e) => {
                    string? name = (string)await dialogPlace.Show(new NameGroup());
                    if (name != null && name != "")
                        App.Groups[(s as GroupCard).groupIndex].Name = name;
                    else
                        App.GetMainWindow.mainNotificationPlacement.Show("Invalid name");
                    LoadGroup();
                };
                groups.Children.Add(control);
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            CloseWithoutResult();
        }

        private async void addbtn_Click(object sender, RoutedEventArgs e)
        {
            string? name = (string)await dialogPlace.Show(new NameGroup());
            if (name != null && name != "")
                App.Groups?.Add(new Group() { Id = Guid.NewGuid().ToString(), Name = name ,Stations = new List<Station>()});
            else
                App.GetMainWindow.mainNotificationPlacement.Show("Invalid name");
            LoadGroup();
        }
    }
}
