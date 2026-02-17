using RadioBrowserWrapper.Models;
using System.Reflection;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// MoreInfoDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MoreInfoDialog : CustomDialog
    {
        Station station;
        public MoreInfoDialog(Station sta)
        {
            InitializeComponent();

            station = sta;
            Loaded += MoreInfoDialog_Loaded;
        }

        private void MoreInfoDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Info.Text = "";
            PropertyInfo[] ps = typeof(Station).GetProperties();
            foreach (var item in ps)
                Info.Text += $"{item.Name} : {item.GetValue(station)}\r\n";
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            CloseWithoutResult();
        }
    }
}
