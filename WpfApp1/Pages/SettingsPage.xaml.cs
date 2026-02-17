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
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            Loaded += SettingsPage_Loaded;
        }

        private void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            int theme = WSettings.Default.darkmode;
            (rbsp.Children[theme + 1] as RadioButton)?.IsChecked = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string? checkedbtn = (sender as RadioButton)?.Content.ToString();
            if (checkedbtn == "Light")
                WSettings.Default.darkmode = -1;
            else if (checkedbtn == "Dark")
                WSettings.Default.darkmode = 0;
            else if (checkedbtn == "System")
                WSettings.Default.darkmode = 1;
            WSettings.Default.Save();
            App.GetMainWindow?.UpdateTheme();
        }
    }
}
