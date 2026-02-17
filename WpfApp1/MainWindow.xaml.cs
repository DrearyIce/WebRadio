using AdonisUI;
using AdonisUI.Controls;
using Microsoft.Win32;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfApp1.Controls;
using WpfApp1.Pages;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {
        private void GetThemeColor()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            if (key != null)
            {
                int theme = (int)key.GetValue("AppsUseLightTheme", -1);
                if (theme == 0)
                    IsDark = true;
                else if (theme == 1)
                    IsDark = false;
                key.Close();
            }
        }

        public bool IsDark
        {
            get => (bool)GetValue(IsDarkProperty);
            set => SetValue(IsDarkProperty, value);
        }

        public static readonly DependencyProperty IsDarkProperty = DependencyProperty.Register("IsDark", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, OnIsDarkChanged));

        private static void OnIsDarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MainWindow)d).ChangeTheme((bool)e.OldValue);
        }

        private void ChangeTheme(bool oldValue)
        {
            ResourceLocator.SetColorScheme(Application.Current.Resources, oldValue ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);
        }

        DispatcherTimer Themetimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            GetThemeColor();

            Themetimer.Interval = TimeSpan.FromSeconds(5);
            Themetimer.Tick += Timer_Tick;
            Themetimer.Start();
            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTheme();
        }

        public void UpdatePage()
        {
            if (MainMenu.SelectedIndex == 1)
                (MainFrame.Content as PinsPage)?.LoadStations();
            else if (MainMenu.SelectedIndex == 3)
                (MainFrame.Content as LibraryPage)?.Reload();
        }

        public void UpdateTheme()
        {
            Themetimer.Stop();
            if (WSettings.Default.darkmode == 1)
                Themetimer.Start();
            else if (WSettings.Default.darkmode == -1)
                IsDark = false;
            else
                IsDark = true;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            WSettings.Default.Save();
            Application.Current.Shutdown();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            GetThemeColor();
        }

        private void MainMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox)?.SelectedIndex != -1 && IsLoaded)
            {
                ASetMenu.SelectedIndex = -1;
                SetMenuTitle((sender as ListBox)?.SelectedItem);

                switch ((sender as ListBox)?.SelectedIndex)
                {
                    case 1:
                        MainFrame.Navigate(new PinsPage());
                        break;
                    case 2:
                        MainFrame.Navigate(new NewPage());
                        break;
                    case 3:
                        MainFrame.Navigate(new LibraryPage());
                        break;
                }
            }
        }

        private void ASetMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox)?.SelectedIndex != -1)
            {
                MainMenu.SelectedIndex = -1;
                SetMenuTitle((sender as ListBox)?.SelectedItem);

                switch ((sender as ListBox)?.SelectedIndex) 
                {
                    case 0:
                        MainFrame.Navigate(new SettingsPage());
                        break;
                    case 1:
                        MainFrame.Navigate(new AboutPage());
                        break;
                }
            }
        }

        private void SetMenuTitle(object? navitemobj)
        {
            try
            {
                ListBoxItem? item = navitemobj as ListBoxItem;
                NavItem? navitem = item?.Content as NavItem;
                if (navitem != null)
                    MenuTitle.Text = navitem.Title;
                else
                    MenuTitle.Text = "[SetMenuTitle] NULL";
            }
            catch (Exception ex)
            {
                MenuTitle.Text = "[SetMenuTitle] ERROR" + ex.Message;
            }
        }
    }
}