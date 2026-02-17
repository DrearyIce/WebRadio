using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Controls
{
    /// <summary>
    /// NavItem.xaml 的交互逻辑
    /// </summary>
    public partial class NavItem : UserControl
    {
        public NavItem()
        {
            InitializeComponent();
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(string), typeof(NavItem), new PropertyMetadata(""));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(NavItem), new PropertyMetadata(""));




    }
}
