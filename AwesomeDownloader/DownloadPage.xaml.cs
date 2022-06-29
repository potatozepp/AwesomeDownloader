using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AwesomeDownloader.BusinessLayer;
using AwesomeDownloader.Models;

namespace AwesomeDownloader.View {
    /// <summary>
    /// Interaction logic for DownloadPage.xaml
    /// </summary>
    public partial class DownloadPage : Page {
        public DownloadPage() {
            InitializeComponent();
        }

        string DownloadFolder { get; set; } = @"C:\AwesomeDownloader\";
        List<MP3Data> Songs { get; set; } = new List<MP3Data>();

        private void Button_Download_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Download started");
            ProgressBar progressBar = new ProgressBar();
            progressBar.Value = 10;
            switch(ComboBox_FileType.SelectedIndex) {
                case 0:
                    DownloadFromYT.DownloadMP3Async(GetSettings(), TextBox_URL.Text);
                    break;
                case 1:
                    DownloadFromYT.DownloadMP4Async(GetSettings(), TextBox_URL.Text);
                    break;
                case 2:
                    DownloadFromYT.DownloadBothAsync(GetSettings(), TextBox_URL.Text);
                    break;
            }

        }
        public DownloadSettings GetSettings() {
            return new DownloadSettings() {
                DownloadFolder = UserSettings.Default.DownloadFolder,
                Bitrate = UserSettings.Default.Bitrate,
                VideoQuality = UserSettings.Default.VideoQuality
            };
        }
        private void Window_DownloadPage_Loaded(object sender, RoutedEventArgs e) {
        }

        private void Button_OpenDownloadFolder_Click(object sender, RoutedEventArgs e) {
            ProcessStartInfo startInfo = new ProcessStartInfo() {
                Arguments = UserSettings.Default.DownloadFolder,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
    }
}
