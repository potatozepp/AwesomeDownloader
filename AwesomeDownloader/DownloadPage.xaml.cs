using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AwesomeDownloader.BusinessLayer;
namespace AwesomeDownloader.View {
    /// <summary>
    /// Interaction logic for DownloadPage.xaml
    /// </summary>
    public partial class DownloadPage : Page {
        public DownloadPage() {
            InitializeComponent();
        }
        string DownloadFolder { get; set; } = @"C:\AwesomeDownloader\";

        private void Button_Download_Click(object sender, RoutedEventArgs e) {
            DownloadFromYT.DownloadMP3(DownloadFolder, TextBox_URL.Text);
        }
    }
}
