using QRCoder;
using QRCoder.Xaml;
using RadioBrowserWrapper.Models;
using System.Windows;
using System.Windows.Media;
using WpfApp1.Models;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// ShareStationQRCode.xaml 的交互逻辑
    /// </summary>
    public partial class ShareStationQRCode : CustomDialog
    {
        Station SStation;
        public ShareStationQRCode(Station station)
        {
            InitializeComponent();

            SStation = station;
            Loaded += ShareStationQRCode_Loaded;
        }

        private void ShareStationQRCode_Loaded(object sender, RoutedEventArgs e)
        {
            title.Text = SStation.Name;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(SStation.Url, QRCodeGenerator.ECCLevel.Q);
            XamlQRCode qrCode = new XamlQRCode(qrCodeData);
            DrawingImage qrCodeAsXaml = qrCode.GetGraphic(20);
            QR.Source = qrCodeAsXaml;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            CloseWithoutResult();
        }
    }
}
