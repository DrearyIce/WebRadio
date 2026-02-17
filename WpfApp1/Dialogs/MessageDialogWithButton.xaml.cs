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

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// MessageDialogWithButton.xaml 的交互逻辑
    /// </summary>
    public partial class MessageDialogWithButton : CustomDialog
    {
        public MessageDialogWithButton(string Title,string Message,string[] buttons)
        {
            InitializeComponent();

            this.Title.Text = Title;
            this.Message.Text = Message;
            foreach (var item in buttons)
            {
                ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto, MinWidth = 100 });
                ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8)});
                Button button = new Button() { Content = item, Cursor = Cursors.Hand, Padding = new Thickness(8, 6, 8, 6), Margin = new Thickness(0, 10, 0, 0) };
                Grid.SetColumn(button, buttons.IndexOf(item) * 2);
                button.Click += (s, e) => {
                    CloseWithResult(buttons.IndexOf((s as Button)?.Content));
                };
                ButtonGrid.Children.Add(button);
            }
            ButtonGrid.ColumnDefinitions.RemoveAt(ButtonGrid.ColumnDefinitions.Count - 1);
        }
    }
}
