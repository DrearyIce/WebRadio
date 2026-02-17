using RadioBrowserWrapper.Models;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// AdvancedStationSearchDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AdvancedStationSearchDialog : CustomDialog
    {
        public AdvancedStationSearchDialog()
        {
            InitializeComponent();

            Options = new AdvancedStationSearchOptions() { Limit = 10 };
            Loaded += AdvancedStationSearchDialog_Loaded;
        }

        public AdvancedStationSearchDialog(AdvancedStationSearchOptions options)
        {
            InitializeComponent();

            Options = options;
            Loaded += AdvancedStationSearchDialog_Loaded;
        }

        private void AdvancedStationSearchDialog_Loaded(object sender, RoutedEventArgs e)
        {
            SetDisplayOption();
        }

        public AdvancedStationSearchOptions Options
        {
            get { return (AdvancedStationSearchOptions)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register(nameof(Options), typeof(AdvancedStationSearchOptions), typeof(AdvancedStationSearchDialog), new PropertyMetadata(new AdvancedStationSearchOptions() { Limit = 10 }));

        public void SetDisplayOption()
        {
            _name.Text = Options.Name;
            _ename.IsChecked = Options.IsExactName;
            _country.Text = Options.Country;
            _ecountry.IsChecked = Options.IsExactCountry;
            _countrycode.Text = Options.CountryCode;
            _state.Text = Options.State;
            _estate.IsChecked = Options.IsExactState;
            _lang.Text = Options.Language;
            _elang.IsChecked = Options.IsExactLanguage;
            _tag.Text = Options.Tag;
            _etag.IsChecked = Options.IsExactTag;
            _tags.Text = Options.TagList;
            _minbit.Text = Options.MinBitrate.ToString();
            _maxbit.Text = Options.MaxBitrate.ToString();
            _reverse.IsChecked = Options.Reverse;
            _offset.Text = Options.Offset.ToString();
            _limit.Text = Options.Limit.ToString();
            _hidebroken.IsChecked = Options.HideBroken;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_ename.IsChecked == null)
                    _ename.IsChecked = false;
                if (_ecountry.IsChecked == null)
                    _ecountry.IsChecked = false;
                if (_estate.IsChecked == null)
                    _estate.IsChecked = false;
                if (_elang.IsChecked == null)
                    _elang.IsChecked = false;
                if (_etag.IsChecked == null)
                    _etag.IsChecked = false;
                if (_reverse.IsChecked == null)
                    _reverse.IsChecked = false;
                if (_hidebroken.IsChecked == null)
                    _hidebroken.IsChecked = false;
                Options.Name = _name.Text;
                Options.IsExactName = (bool)_ename.IsChecked;
                Options.Country = _country.Text;
                Options.IsExactCountry = (bool)_ecountry.IsChecked;
                Options.CountryCode = _countrycode.Text;
                Options.State = _state.Text;
                Options.IsExactState = (bool)_estate.IsChecked;
                Options.Language = _lang.Text;
                Options.IsExactLanguage = (bool)_elang.IsChecked;
                Options.Tag = _tag.Text;
                Options.IsExactTag = (bool)_etag.IsChecked;
                Options.TagList = _tags.Text;
                Options.MinBitrate = int.Parse(_minbit.Text);
                Options.MaxBitrate = int.Parse(_maxbit.Text);
                Options.Reverse = (bool)_reverse.IsChecked;
                Options.Offset = int.Parse(_offset.Text);
                Options.Limit = int.Parse(_limit.Text);
                Options.HideBroken = (bool)_hidebroken.IsChecked;

                CloseWithResult(Options);
            }
            catch (Exception ex)
            {
                App.GetMainWindow?.mainNotificationPlacement.Show(ex.Message);
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            CloseWithResult(new AdvancedStationSearchOptions() { Limit = 10 });
        }
    }
}
