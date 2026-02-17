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
    /// SelectGroup.xaml 的交互逻辑
    /// </summary>
    public partial class SelectGroup : CustomDialog
    {
        public SelectGroup()
        {
            InitializeComponent();

            Loaded += SelectGroup_Loaded;
        }

        private void SelectGroup_Loaded(object sender, RoutedEventArgs e)
        {
            groups.Children.Clear();
            for (int i = 0; i < App.Groups?.Count; i++)
            {
                var control = new GroupCard(i, false);
                control.onClick += (s, e) => { CloseWithResult(s); };
                groups.Children.Add(control);
            }
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            CloseWithoutResult();
        }
    }
}
