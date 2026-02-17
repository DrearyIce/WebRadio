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
using WpfApp1.Models;

namespace WpfApp1.Controls
{
    /// <summary>
    /// GroupCard.xaml 的交互逻辑
    /// </summary>
    public partial class GroupCard : UserControl
    {
        public event RoutedEventHandler? onDel;
        public event RoutedEventHandler? onEdit;
        public event RoutedEventHandler? onClick;

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register(nameof(GroupName), typeof(string), typeof(GroupCard), new PropertyMetadata(""));

        public int groupIndex = -1;
        public GroupCard(int groupindex, bool enableEdit)
        {
            InitializeComponent();

            groupIndex = groupindex;
            if (enableEdit)
                editPanel.Visibility = Visibility.Visible;
            GroupName = App.Groups?[groupindex].Name;
        }

        public GroupCard()
        {
            InitializeComponent();
        }

        private void delbtn_Click(object sender, RoutedEventArgs e)
        {
            onDel?.Invoke(this, e);
        }

        private void editbtn_Click(object sender, RoutedEventArgs e)
        {
            onEdit?.Invoke(this, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            onClick?.Invoke(this, e);
        }
    }
}
