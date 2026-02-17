using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Controls
{
    /// <summary>
    /// WrapGrid.xaml 的交互逻辑
    /// </summary>
    public partial class WrapGrid : UserControl
    {
        public WrapGrid()
        {
            InitializeComponent();

            Children = new List<object>();
            SizeChanged += WrapGrid_SizeChanged;
        }

        private void WrapGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReplaceControls(false);
        }

        int RowCount = 0, ColCount = 0;

        public void ReplaceControls(bool isChildRebuild)
        {
            try
            {
                AutoWrapUserControl? control = Children[0] as AutoWrapUserControl;
                if (control != null && control.aminWidth > 0)
                {
                    if (isChildRebuild)
                    {
                        ContentGrid.Children.Clear();
                        foreach (var child in Children)
                            ContentGrid.Children.Add((UIElement)child);
                        isChildRebuild = true;
                    }
                    int nColCount = (int)(this.ActualWidth / control.aminWidth);
                    if (nColCount != ColCount && ContentGrid.Children.Count > 0)
                        SetGridWrap(nColCount);
                    else if (isChildRebuild)
                        SetGridWrap(nColCount);
                    isChildRebuild = false;
                }
            }
            catch { }
        }

        public void SetGridWrap(int nColCount)
        {
            ColCount = nColCount;
            RowCount = (int)(ContentGrid.Children.Count / nColCount) + 1;
            ContentGrid.ColumnDefinitions.Clear();
            ContentGrid.RowDefinitions.Clear();
            for (int i = 0; i < ColCount; i++)
                ContentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < RowCount; i++)
                ContentGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    int conindex = ColCount * i + j;
                    if (conindex < Children.Count)
                    {
                        Grid.SetRow(ContentGrid.Children[conindex], i);
                        Grid.SetColumn(ContentGrid.Children[conindex], j);
                    }
                }
            }
        }

        public List<object> Children
        {
            get { return (List<object>)GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); ReplaceControls(true); }
        }

        // Using a DependencyProperty as the backing store for Children.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildrenProperty =
            DependencyProperty.Register(nameof(Children), typeof(List<object>), typeof(WrapGrid), new PropertyMetadata(new List<object>()));



    }
}
